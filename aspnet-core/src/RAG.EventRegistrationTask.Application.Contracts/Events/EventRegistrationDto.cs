using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace RAG.EventRegistrationTask.Events
{
    public class EventRegistrationDto: EntityDto<Guid>
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public bool? IsCanceled { get; private set; }
        public string EventNameEn { get; set; }
        public string EventNameAr { get; set; }
        public string EventLink { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }

    public class ActionEventRegistration
    {
        public EventRegistrationDto EventRegistration { get; set; }

        public bool CanAddAction { get; set; }
    }
}
