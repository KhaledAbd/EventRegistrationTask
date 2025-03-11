using System.Threading.Tasks;

namespace RAG.EventRegistrationTask.Data;

public interface IEventRegistrationTaskDbSchemaMigrator
{
    Task MigrateAsync();
}
