using Volo.Abp.Modularity;

namespace RAG.EventRegistrationTask;

public abstract class EventRegistrationTaskApplicationTestBase<TStartupModule> : EventRegistrationTaskTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
