using RAG.EventRegistrationTask.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace RAG.EventRegistrationTask.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class EventRegistrationTaskController : AbpControllerBase
{
    protected EventRegistrationTaskController()
    {
        LocalizationResource = typeof(EventRegistrationTaskResource);
    }
}
