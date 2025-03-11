using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RAG.EventRegistrationTask.Data;
using Volo.Abp.DependencyInjection;

namespace RAG.EventRegistrationTask.EntityFrameworkCore;

public class EntityFrameworkCoreEventRegistrationTaskDbSchemaMigrator
    : IEventRegistrationTaskDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreEventRegistrationTaskDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the EventRegistrationTaskDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<EventRegistrationTaskDbContext>()
            .Database
            .MigrateAsync();
    }
}
