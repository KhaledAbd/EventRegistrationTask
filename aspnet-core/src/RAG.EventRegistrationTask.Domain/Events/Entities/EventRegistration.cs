using Microsoft.Extensions.Logging;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace RAG.EventRegistrationTask.Events.Entities
{
    public class EventRegistration : Entity<Guid>
    {
        public Guid EventId { get; set; }
        public Guid UserId { get;  set; }
        public DateTime RegisteredAt { get;  set; }
        public bool? IsCanceled { get;  set; }
        public virtual Event Event { get;  set; }

        public virtual IdentityUser User { get; set; }

        public EventRegistration() { } // For EF Core

        public EventRegistration(Guid userId, Guid eventId)
        {
            UserId = userId;
            EventId = eventId;
            RegisteredAt = DateTime.UtcNow;
        }

        public void SetRegisterCancel()
        {
            IsCanceled = true;
        }

    }

}
