using Microsoft.AspNetCore.Authorization;
using RAG.EventRegistrationTask.Events.Entities;
using RAG.EventRegistrationTask.Events.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

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
            if (eventEntity is null)
            {
                throw new EventNotFoundException(eventId);
            }

            if (eventEntity.Capacity.Value < eventEntity.RegistrationCount)
            {
                throw new EventCapacityException(eventEntity.Capacity.Value ?? 0);
            }


            EventRegistration registration = await _eventRegistrationRepository.GetAsync(eventId);
            if (registration is null || registration.IsCanceled!.Value)
            {

                registration = new EventRegistration(CurrentUser.Id!.Value, eventId);

                // Insert the registration and wait for the result
                registration = await _eventRegistrationRepository.InsertAsync(registration);

                // Return true if registration was successful, else false
                return registration != null && registration.Id != Guid.Empty;
            }
            else
            {
                registration.IsCanceled = false;
            }

            return registration != null && registration.Id != Guid.Empty;
        }


        public async Task<bool> CancelAsync(Guid id)
        {
            // Fetch the registration with event details
            var registration = (await _eventRegistrationRepository.WithDetailsAsync(c => c.Event))
                .AsQueryable()
                .FirstOrDefault(c => c.Id == id);

            if (registration == null)
            {
                throw new EventRegistrationNotFoundException(id);
            }

            // Check if the current user is the owner of the registration
            if (registration.UserId != CurrentUser.Id)
            {
                throw new UnauthorizedAccessException();
            }

            // Check if the event start date is within 1 hour from now
            if (registration.Event.StartDate <= DateTime.UtcNow.AddHours(1))
            {
                throw new EventExeceedPeriodCancaltionException(registration.Event.StartDate);
            }

            // Mark the registration as canceled
            registration.IsCanceled = true;

            // Update the registration in the repository
            registration = await _eventRegistrationRepository.UpdateAsync(registration);

            // Return true if the update was successful
            return registration != null && registration.Id != Guid.Empty;
        }

        public async Task<PagedResultDto<ActionEventRegistration>> GetRegistrationsEventAsync(Guid eventId,  int skipCount = 0, int maxResultCount = 10)
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
                    .Select(co => new ActionEventRegistration()
                    {
                        EventRegistration = ObjectMapper.Map<EventRegistration, EventRegistrationDto>(co),
                        CanAddAction = co.Event.StartDate <= DateTime.UtcNow.AddHours(1)
                    })
            );

            return new PagedResultDto<ActionEventRegistration>(
                totalCount,
                registrations
            );
        }
    }
}