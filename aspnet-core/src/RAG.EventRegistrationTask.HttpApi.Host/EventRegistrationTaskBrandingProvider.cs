using Microsoft.Extensions.Localization;
using RAG.EventRegistrationTask.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace RAG.EventRegistrationTask;

[Dependency(ReplaceServices = true)]
public class EventRegistrationTaskBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<EventRegistrationTaskResource> _localizer;

    public EventRegistrationTaskBrandingProvider(IStringLocalizer<EventRegistrationTaskResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
