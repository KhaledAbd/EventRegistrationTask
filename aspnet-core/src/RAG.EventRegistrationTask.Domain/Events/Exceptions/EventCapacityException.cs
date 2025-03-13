using RAG.EventRegistrationTask;
using Volo.Abp;

namespace ABPCourse.Demo1.Products
{
    public class EventCapacityException: BusinessException
    {
        public EventCapacityException(int capacity) : base(EventRegistrationTaskDomainErrorCodes.Event_NOT_AVAILABLE_CAPACITY)
        {
            WithData("nameEn", capacity);        
        }
    }
}
