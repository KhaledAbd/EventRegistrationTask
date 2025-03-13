using RAG.EventRegistrationTask;
using System;
using Volo.Abp;

namespace ABPCourse.Demo1.Products
{
    public class EventNotFoundException: BusinessException
    {
        public EventNotFoundException(Guid id) : base(EventRegistrationTaskDomainErrorCodes.Event_NOT_FOUND)
        {
            WithData("id", id);        
        }
    }
}
