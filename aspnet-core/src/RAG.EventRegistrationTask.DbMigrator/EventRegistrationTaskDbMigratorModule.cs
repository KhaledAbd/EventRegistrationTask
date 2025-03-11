using RAG.EventRegistrationTask.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace RAG.EventRegistrationTask.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EventRegistrationTaskEntityFrameworkCoreModule),
    typeof(EventRegistrationTaskApplicationContractsModule)
    )]
public class EventRegistrationTaskDbMigratorModule : AbpModule
{
}
