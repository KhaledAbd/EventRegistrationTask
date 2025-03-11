using RAG.EventRegistrationTask.Events.Entities;
using RAG.EventRegistrationTask.Events.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace RAG.EventRegistrationTask.Data.Events
{
    public class EventDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Event> _repository;

        public EventDataSeeder(IRepository<Event> repository)
        {
            _repository = repository;
        }
        public Task SeedAsync(DataSeedContext context)
        {
            return _repository.InsertManyAsync(GetSampleEvents());
        }


        private List<Event> GetSampleEvents()
        {
            return new List<Event>
            {
                new Event(
                    "Music Festival",
                    "مهرجان موسيقي",
                    new Capacity(200),
                    false,
                    DateTime.UtcNow.AddMonths(1),
                    DateTime.UtcNow.AddMonths(1).AddDays(2),
                    Guid.NewGuid(), // Organizer ID
                    null, // Link
                    new Location("Cairo", "6th October City", "123 Example Street"),
                    true
                ),
                new Event(
                    "Tech Conference",
                    "مؤتمر تقني",
                    new Capacity(300),
                    true,
                    DateTime.UtcNow.AddMonths(2),
                    DateTime.UtcNow.AddMonths(2).AddDays(3),
                    Guid.NewGuid(), // Organizer ID
                    "https://tech-conference.com",
                    null, // Location (null for online events)
                    true
                ),
                new Event(
                    "Art Exhibition",
                    "معرض فنون",
                    new Capacity(100),
                    false,
                    DateTime.UtcNow.AddMonths(3),
                    DateTime.UtcNow.AddMonths(3).AddDays(1),
                    Guid.NewGuid(), // Organizer ID
                    null, // Link
                    new Location("Alexandria", "Downtown", "456 Art Avenue"),
                    true
                )
            };
        }
    }

}
