using RAG.EventRegistrationTask;
using System;
using Volo.Abp;

namespace RAG.EventRegistrationTask.Events.Exceptions
{
    public class EventExeceedPeriodCancaltionException: BusinessException
    {
        public EventExeceedPeriodCancaltionException(DateTime dateTime) : base(EventRegistrationTaskDomainErrorCodes.EVENT_EXECEED_PERIOD_CANCALATION)
        {
            WithData("lastDate", dateTime.ToUniversalTime());        
        }
    }
}
