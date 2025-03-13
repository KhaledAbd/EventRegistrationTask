using RAG.EventRegistrationTask;
using System;
using Volo.Abp;

namespace ABPCourse.Demo1.Products
{
    public class EventExeceedPeriodCancaltionException: BusinessException
    {
        public EventExeceedPeriodCancaltionException(DateTime dateTime) : base(EventRegistrationTaskDomainErrorCodes.EVENT_EXECEED_PERIOD_CANCALATION)
        {
            WithData("nameEn", dateTime);        
        }
    }
}
