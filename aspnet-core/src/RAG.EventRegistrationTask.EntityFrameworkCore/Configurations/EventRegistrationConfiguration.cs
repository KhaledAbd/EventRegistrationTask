using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RAG.EventRegistrationTask.Events.Entities;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RAG.EventRegistrationTask.Configurations
{
    class EventRegistrationConfiguration : IEntityTypeConfiguration<EventRegistration>
    {
        public void Configure(EntityTypeBuilder<EventRegistration> builder)
        {

            builder.ConfigureByConvention();

            builder.ToTable("EventRegistrations");

            builder.Property(er => er.RegisteredAt).IsRequired();
            builder.Property(er => er.IsCanceled).IsRequired(false);
        }
    }
}
