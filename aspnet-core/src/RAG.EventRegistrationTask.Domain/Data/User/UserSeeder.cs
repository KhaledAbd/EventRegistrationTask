using RAG.EventRegistrationTask;
using RAG.EventRegistrationTask.Events.Entities;
using RAG.EventRegistrationTask.Events.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Uow;
using Microsoft.Extensions.Logging;

public class UserSeeder : IDataSeedContributor, ITransientDependency
{
    private readonly IdentityUserManager _userManager;
    private readonly IdentityRoleManager _roleManager;
    private readonly IIdentityUserRepository _identityUserRepository;
    private readonly IRepository<Event> _eventRepository;
    private readonly ILogger<UserSeeder> _logger;

    public UserSeeder(IdentityUserManager userManager, IdentityRoleManager roleManager,
        IIdentityUserRepository identityUserRepository, IRepository<Event> eventRepository, ILogger<UserSeeder> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _identityUserRepository = identityUserRepository;
        _eventRepository = eventRepository;
        _logger = logger;
    }

    [UnitOfWork]
    public async Task SeedAsync(DataSeedContext context)
    {
        // Step 1: Create or find the 'Organizer' user
        var user = await _userManager.FindByEmailAsync("organizer@example.com");
        if (user == null)
        {
            user = new IdentityUser(
                Guid.NewGuid(),
                EventRegistrationTaskConsts.OrganizerUserName,
                "organizer@example.com"
            );

            var result = await _userManager.CreateAsync(user, "123qweQWE!");
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
            }

            _logger.LogInformation("User 'Organizer' created successfully.");
        }

        // Step 2: Ensure 'Organizer' role exists
        var organizerRole = await _roleManager.FindByNameAsync("Organizer");
        if (organizerRole == null)
        {
            organizerRole = new IdentityRole(Guid.NewGuid(), "Organizer")
            {
                IsStatic = true,
                IsPublic = true
            };

            await _roleManager.CreateAsync(organizerRole);
            _logger.LogInformation("Role 'Organizer' created successfully.");
        }

        // Step 3: Assign the 'Organizer' role to the user if not already assigned
        if (!await _userManager.IsInRoleAsync(user, "Organizer"))
        {
            await _userManager.AddToRoleAsync(user, "Organizer");
            _logger.LogInformation($"User '{user.UserName}' assigned to 'Organizer' role.");
        }

        // Step 4: Seed events if none exist
        if (await _eventRepository.CountAsync() == 0)
        {
            var events = GetSampleEvents(user.Id);
            await _eventRepository.InsertManyAsync(events);
            _logger.LogInformation("Sample events seeded successfully.");
        }
        else
        {
            _logger.LogInformation("Events already seeded. Skipping seeding.");
        }
    }

    private List<Event> GetSampleEvents(Guid organizerId)
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
                organizerId, // Organizer ID
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
                organizerId, // Organizer ID
                "https://tech-conference.com", // Link
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
                organizerId, // Organizer ID
                null, // Link
                new Location("Alexandria", "Downtown", "456 Art Avenue"),
                true
            )
        };
    }
}
