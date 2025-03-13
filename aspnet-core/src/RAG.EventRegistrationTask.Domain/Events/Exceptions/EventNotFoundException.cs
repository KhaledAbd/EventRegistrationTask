using RAG.EventRegistrationTask;
using System;
using Volo.Abp;

namespace RAG.EventRegistrationTask.Events.Exceptions
{
    public class EventNotFoundException: BusinessException
    {
        public EventNotFoundException(Guid id) : base(EventRegistrationTaskDomainErrorCodes.Event_NOT_FOUND, EventRegistrationTaskDomainErrorCodes.Event_NOT_FOUND)
        {
            WithData("id", id);        
        }
    }
}
