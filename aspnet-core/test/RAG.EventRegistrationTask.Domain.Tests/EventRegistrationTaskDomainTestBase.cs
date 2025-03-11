using Volo.Abp.Modularity;

namespace RAG.EventRegistrationTask;

/* Inherit from this class for your domain layer tests. */
public abstract class EventRegistrationTaskDomainTestBase<TStartupModule> : EventRegistrationTaskTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
