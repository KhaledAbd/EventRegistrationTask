using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace RAG.EventRegistrationTask.Data;

/* This is used if database provider does't define
 * IEventRegistrationTaskDbSchemaMigrator implementation.
 */
public class NullEventRegistrationTaskDbSchemaMigrator : IEventRegistrationTaskDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
