using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RAG.EventRegistrationTask.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class EventRegistrationTaskDbContextFactory : IDesignTimeDbContextFactory<EventRegistrationTaskDbContext>
{
    public EventRegistrationTaskDbContext CreateDbContext(string[] args)
    {
        EventRegistrationTaskEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<EventRegistrationTaskDbContext>()
            .UseSqlite(configuration.GetConnectionString("Default"));

        return new EventRegistrationTaskDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../RAG.EventRegistrationTask.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
