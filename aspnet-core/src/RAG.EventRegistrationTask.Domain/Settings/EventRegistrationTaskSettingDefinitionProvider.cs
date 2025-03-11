using Volo.Abp.Settings;

namespace RAG.EventRegistrationTask.Settings;

public class EventRegistrationTaskSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(EventRegistrationTaskSettings.MySetting1));
    }
}
