using System;
using System.Collections.Generic;
using System.Text;
using RAG.EventRegistrationTask.Localization;
using Volo.Abp.Application.Services;

namespace RAG.EventRegistrationTask;

/* Inherit your application services from this class.
 */
public abstract class EventRegistrationTaskAppService : ApplicationService
{
    protected EventRegistrationTaskAppService()
    {
        LocalizationResource = typeof(EventRegistrationTaskResource);
    }
}
