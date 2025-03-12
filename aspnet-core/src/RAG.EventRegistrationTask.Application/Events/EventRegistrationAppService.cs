using Microsoft.AspNetCore.Authorization;
using RAG.EventRegistrationTask.Events.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace RAG.EventRegistrationTask.Events
{
    [Authorize]
    public class EventRegistrationAppService : ApplicationService, IEventRegistrationAppService
    {
        private readonly IRepository<EventRegistration, Guid> _eventRegistrationRepository;

        public EventRegistrationAppService(IRepository<EventRegistration, Guid> eventRegistrationRepository)
        {
            _eventRegistrationRepository = eventRegistrationRepository;
        }

        public async Task<EventRegistrationDto> RegisterAsync(Guid eventId)
        {
            var registration = new EventRegistration(eventId, CurrentUser.Id!.Value);
            registration = await _eventRegistrationRepository.InsertAsync(registration);

            return ObjectMapper.Map<EventRegistration, EventRegistrationDto>(registration);
        }

        public async Task CancelAsync(Guid id)
        {
            var registration = await _eventRegistrationRepository.GetAsync(id);
            if (registration.UserId != CurrentUser.Id)
            {
                throw new UnauthorizedAccessException("You can only cancel your own registrations.");
            }
            await _eventRegistrationRepository.DeleteAsync(id);
        }

        public async Task<List<EventRegistrationDto>> GetRegistrationsForEventAsync(Guid eventId)
        {
            var registrations = await _eventRegistrationRepository.GetListAsync(r => r.EventId == eventId);
            return ObjectMapper.Map<List<EventRegistration>, List<EventRegistrationDto>>(registrations);
        }
    }
}