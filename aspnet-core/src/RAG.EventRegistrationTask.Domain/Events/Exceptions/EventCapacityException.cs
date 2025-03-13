using RAG.EventRegistrationTask;
using Volo.Abp;

namespace RAG.EventRegistrationTask.Events.Exceptions
{
    public class EventCapacityException: BusinessException
    {
        public EventCapacityException(int capacity) : base(EventRegistrationTaskDomainErrorCodes.Event_NOT_AVAILABLE_CAPACITY, EventRegistrationTaskDomainErrorCodes.Event_NOT_AVAILABLE_CAPACITY)
        {
            WithData("capacity", capacity);        
        }
    }
}
