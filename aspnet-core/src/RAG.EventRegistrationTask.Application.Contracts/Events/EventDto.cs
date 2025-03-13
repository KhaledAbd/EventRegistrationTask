using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace RAG.EventRegistrationTask.Events
{
    public class EventDto : FullAuditedEntityDto<Guid>
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public int? Capacity { get; set; }
        public bool IsOnline { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid OrganizerId { get; set; }
        public string Link { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public int RegistrationCount { get; set; }
        public string OrganizerName { get; set; }
        public string Government { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }

    public class EventActiveDto
    {
        public EventDto Event { get; set; }
        public bool IsRegistered { get; set; }

        public Guid EventRegistrationId {get; set;}

    }

    public class EventActionDto
    {
        public EventDto Event { get; set; }
        public bool CanEdit { get; set; }
        public bool HasActiveAction { get; set; }

    }
}
