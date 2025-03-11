using Volo.Abp.Modularity;

namespace RAG.EventRegistrationTask;

[DependsOn(
    typeof(EventRegistrationTaskApplicationModule),
    typeof(EventRegistrationTaskDomainTestModule)
)]
public class EventRegistrationTaskApplicationTestModule : AbpModule
{

}
