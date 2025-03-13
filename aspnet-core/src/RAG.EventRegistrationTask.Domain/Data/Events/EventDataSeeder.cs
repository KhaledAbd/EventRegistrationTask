//using RAG.EventRegistrationTask.Events.Entities;
//using RAG.EventRegistrationTask.Events.ValueObjects;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Volo.Abp.Data;
//using Volo.Abp.DependencyInjection;
//using Volo.Abp.Domain.Repositories;
//using Volo.Abp.Identity;

//namespace RAG.EventRegistrationTask.Data.Events
//{
//    public class EventDataSeeder : IDataSeedContributor, ITransientDependency
//    {
//        private readonly IRepository<Event> _repository;
//        private readonly IIdentityUserRepository _identityUserRepository;

//        public EventDataSeeder(
//            IRepository<Event> repository,
//            IIdentityUserRepository identityUserRepository)
//        {
//            _repository = repository;
//            _identityUserRepository = identityUserRepository;
//        }

//        public async Task SeedAsync(DataSeedContext context)
//        {
//            // Use the actual normalized username instead of role name
//            var user = await _identityUserRepository.FindByNormalizedUserNameAsync(EventRegistrationTaskConsts.OrganizerUserName);
//            if (user == null)
//            {
//                throw new Exception("User with username 'ORGANIZER' not found");
//            }

//            // Ensure events are not already seeded
//            if (await _repository.CountAsync() == 0)
//            {
//                await _repository.InsertManyAsync(GetSampleEvents(user.Id));
//            }
//        }

//        private List<Event> GetSampleEvents(Guid organizerId)
//        {
//            return new List<Event>
//            {
//                new Event(
//                    "Music Festival",
//                    "مهرجان موسيقي",
//                    new Capacity(200),
//                    false,
//                    DateTime.UtcNow.AddMonths(1),
//                    DateTime.UtcNow.AddMonths(1).AddDays(2),
//                    organizerId, // Organizer ID
//                    null, // Link
//                    new Location("Cairo", "6th October City", "123 Example Street"),
//                    true
//                ),
//                new Event(
//                    "Tech Conference",
//                    "مؤتمر تقني",
//                    new Capacity(300),
//                    true,
//                    DateTime.UtcNow.AddMonths(2),
//                    DateTime.UtcNow.AddMonths(2).AddDays(3),
//                    organizerId, // Organizer ID
//                    "https://tech-conference.com",
//                    null, // Location (null for online events)
//                    true
//                ),
//                new Event(
//                    "Art Exhibition",
//                    "معرض فنون",
//                    new Capacity(100),
//                    false,
//                    DateTime.UtcNow.AddMonths(3),
//                    DateTime.UtcNow.AddMonths(3).AddDays(1),
//                    organizerId, // Organizer ID
//                    null, // Link
//                    new Location("Alexandria", "Downtown", "456 Art Avenue"),
//                    true
//                )
//            };
//        }
//    }
//}
