using System.Collections.Generic;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp;
using RAG.EventRegistrationTask.Events.ValueObjects;
using Volo.Abp.Domain.Entities.Auditing;

namespace RAG.EventRegistrationTask.Events.Entities
{
    public class Event: FullAuditedAggregateRoot<Guid>
    {
        public string NameEn { get; protected set; }
        public string NameAr { get; protected set; }
        public Capacity Capacity { get; protected set; }
        public bool IsOnline { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public Guid OrganizerId { get; protected set; }
        public string Link { get; protected set; }
        public Location Location { get; protected set; }  // Use Location as a ValueObject
        public bool IsActive { get; protected set; }

        private readonly List<EventRegistration> _registrations = new();
        public virtual IReadOnlyCollection<EventRegistration> Registrations => _registrations.AsReadOnly();

        private Event() { } // For EF Core

        public Event(
            string nameEn, string nameAr, Capacity capacity, bool isOnline,
            DateTime startDate, DateTime endDate, Guid organizerId,
            string link, Location location, bool isActive)
        {
            NameEn = nameEn;
            NameAr = nameAr;
            Capacity = capacity;
            IsOnline = isOnline;
            StartDate = startDate;
            EndDate = endDate;
            OrganizerId = organizerId;
            Link = isOnline ? link : null;
            Location = isOnline ? null : location;  // Use Location value object
            IsActive = isActive;
        }

        // Register Participant
        public void RegisterParticipant(Guid userId)
        {
            if (!IsActive) throw new BusinessException("Event is not active");
            if (StartDate < DateTime.UtcNow) throw new BusinessException("Event has already started");
            if (Capacity.IsFull(_registrations.Count)) throw new BusinessException("Event capacity is full");
            if (_registrations.FindIndex(r => r.UserId == userId) < 0) throw new BusinessException("User is already registered");

            var registration = new EventRegistration(userId, Id);
            _registrations.Add(registration);

        }

        // Cancel Participant Registration
        public void CancelRegistration(Guid userId)
        {
            if (StartDate < DateTime.UtcNow.AddHours(1))
                throw new BusinessException("Cannot cancel within 1 hour of event start time");

            var registration = _registrations.Find(r => r.UserId == userId);
            if (registration is null) throw new BusinessException("User is not registered");

            registration.SetRegisterCancel();

        }
    }
}
