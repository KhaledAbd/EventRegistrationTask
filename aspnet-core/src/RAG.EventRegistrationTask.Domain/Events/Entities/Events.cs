using System.Collections.Generic;
using System;
using RAG.EventRegistrationTask.Events.ValueObjects;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using System.Linq;

namespace RAG.EventRegistrationTask.Events.Entities
{
    public class Event : FullAuditedEntity<Guid>
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public Capacity Capacity { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid OrganizerId { get; set; }
        public string Link { get; set; }
        public Location Location { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EventRegistration> EventRegistrations { get; set; }

        public virtual IdentityUser Organizer { get; set; }

        public int RegistrationCount => EventRegistrations is not null ? EventRegistrations.AsQueryable().Where(c => !c.IsCanceled.Value).Count(): 0;

        private Event() { }
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
            Location = isOnline ? null : location; 
            IsActive = isActive;
        }

    }
}
