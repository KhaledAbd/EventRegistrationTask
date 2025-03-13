using RAG.EventRegistrationTask;
using System;
using Volo.Abp;

namespace RAG.EventRegistrationTask.Events.Exceptions
{
    public class EventRegistrationNotFoundException: BusinessException
    {
        public EventRegistrationNotFoundException(Guid id) : base(EventRegistrationTaskDomainErrorCodes.EVENT_REGISTRATION_NOT_FOUND, EventRegistrationTaskDomainErrorCodes.EVENT_REGISTRATION_NOT_FOUND)
        {
            WithData("id", id);        
        }
    }
}
