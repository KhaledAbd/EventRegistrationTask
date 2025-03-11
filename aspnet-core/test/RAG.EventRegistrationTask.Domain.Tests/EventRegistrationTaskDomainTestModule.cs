using Volo.Abp.Modularity;

namespace RAG.EventRegistrationTask;

[DependsOn(
    typeof(EventRegistrationTaskDomainModule),
    typeof(EventRegistrationTaskTestBaseModule)
)]
public class EventRegistrationTaskDomainTestModule : AbpModule
{

}
