using Microsoft.Extensions.Logging;
using System;
using Volo.Abp.Domain.Entities;

namespace RAG.EventRegistrationTask.Events.Entities
{
    public class EventRegistration : Entity<Guid>
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public bool? IsCanceled { get; private set; } = false;
        public virtual Event Event { get; private set; }

        private EventRegistration() { } // For EF Core

        internal EventRegistration(Guid userId, Guid EventId)
        {
            UserId = userId;
            RegisteredAt = DateTime.UtcNow;
        }

        public void SetRegisterCancel()
        {
            IsCanceled = true;
        }

    }


}
