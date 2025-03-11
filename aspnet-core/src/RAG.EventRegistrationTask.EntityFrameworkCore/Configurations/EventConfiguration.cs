using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RAG.EventRegistrationTask.Events.Entities;
using System.Reflection.Emit;

namespace RAG.EventRegistrationTask.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ConfigureByConvention();
            // Table name
            builder.ToTable("Events");

            // Properties
            builder.Property(e => e.NameEn)
                .IsRequired()
                .HasMaxLength(EventRegistrationTaskConsts.TextFieldLength);

            builder.Property(e => e.NameAr)
                .IsRequired()
                .HasMaxLength(EventRegistrationTaskConsts.TextFieldLength);


            builder.Property(e => e.IsOnline)
                .IsRequired();

            builder.Property(e => e.StartDate)
                .IsRequired();

            builder.Property(e => e.EndDate)
                .IsRequired();

            builder.Property(e => e.Link)
                .HasMaxLength(EventRegistrationTaskConsts.HyperLinkFieldLength);

            builder.Property(e => e.IsActive)
            .IsRequired();

            builder.HasMany(c => c.EventRegistrations)
           .WithOne(er => er.Event)
           .HasForeignKey(er => er.EventId)
           .IsRequired();

            builder.OwnsOne(e => e.Capacity, capacity =>
                 {
                     capacity.Property(c => c.Value).HasColumnName("Capacity");
                 });

            builder.OwnsOne(e => e.Location);
            builder.OwnsOne(e => e.Location, location =>
                   {
                       location.Property(l => l.Government).HasColumnName("Government");
                       location.Property(l => l.City).HasColumnName("City");
                       location.Property(l => l.Street).HasColumnName("Street");
                   });

            // Indexes
            builder.HasIndex(e => e.IsActive);
            builder.HasIndex(e => new { e.StartDate, e.EndDate });
        }
    }
}
