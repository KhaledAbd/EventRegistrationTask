using System.Collections.Generic;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp;
using RAG.EventRegistrationTask.Events.ValueObjects;
using Volo.Abp.Domain.Entities.Auditing;

namespace RAG.EventRegistrationTask.Events.Entities
{
    public class Event: FullAuditedEntity<Guid>
    {
        public string NameEn { get;  set; }
        public string NameAr { get;  set; }
        public Capacity Capacity { get;  set; }
        public bool IsOnline { get;  set; }
        public DateTime StartDate { get;  set; }
        public DateTime EndDate { get;  set; }
        public Guid OrganizerId { get;  set; }
        public string Link { get;  set; }
        public Location Location { get;  set; }  
        public bool IsActive { get;  set; }

        public virtual ICollection<EventRegistration> EventRegistrations { get; set; }

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
