using ABPCourse.Demo1.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RAG.EventRegistrationTask.Events.Entities;
using RAG.EventRegistrationTask.Events.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace RAG.EventRegistrationTask.Events
{
    [Authorize]
    public class EventRegistrationAppService : ApplicationService, IEventRegistrationAppService
    {
        private readonly IRepository<EventRegistration, Guid> _eventRegistrationRepository;
        private readonly IRepository<Event, Guid> _eventRepository;

        public EventRegistrationAppService(IRepository<EventRegistration, Guid> eventRegistrationRepository, IRepository<Event, Guid> eventRepository)
        {
            _eventRegistrationRepository = eventRegistrationRepository;
            _eventRepository = eventRepository;
        }

        public async Task<bool> RegisterAsync(Guid eventId)
        {
            var eventEntity = await _eventRepository.FindAsync(c => c.Id == eventId);
            if(eventEntity is null)
            {
                throw new EventNotFoundException(eventId);
            }

            if(eventEntity.Capacity.Value < eventEntity.RegistrationCount)
            {
                throw new EventCapacityException(eventEntity.Capacity.Value ?? 0);
            }

            var registration = new EventRegistration(CurrentUser.Id!.Value, eventId);

            // Insert the registration and wait for the result
            registration = await _eventRegistrationRepository.InsertAsync(registration);

            // Return true if registration was successful, else false
            return registration != null && registration.Id != Guid.Empty;
        }


        public async Task<bool> CancelAsync(Guid id)
        {
            var registration = await _eventRegistrationRepository.GetAsync(id);
          
            if (registration.UserId != CurrentUser.Id)
            {
                throw new UnauthorizedAccessException("You can only cancel your own registrations.");
            }

            registration.IsCanceled = true;
            registration = await _eventRegistrationRepository.UpdateAsync(registration);
            return registration != null && registration.Id != Guid.Empty;

        }

        public async Task<PagedResultDto<EventRegistrationDto>> GetRegistrationsEventAsync(Guid eventId,  int skipCount = 0, int maxResultCount = 10)
        {
            var query = (await _eventRegistrationRepository
                .WithDetailsAsync(r => r.User, cc => cc.Event))
                .AsQueryable()
                .Where(r => r.EventId == eventId);

            // Get total count before applying paging
            var totalCount = await AsyncExecuter.CountAsync(query);

            // Apply paging and mapping
            var registrations = await AsyncExecuter.ToListAsync(
                query
                    .Skip(skipCount)
                    .Take(maxResultCount)
            );

            return new PagedResultDto<EventRegistrationDto>(
                totalCount,
                ObjectMapper.Map<List<EventRegistration>, List<EventRegistrationDto>>(registrations)
            );
        }
    }
}