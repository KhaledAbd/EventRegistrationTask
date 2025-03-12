using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using RAG.EventRegistrationTask.Base;
using RAG.EventRegistrationTask.Events.Entities;
using RAG.EventRegistrationTask.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace RAG.EventRegistrationTask.Events
{

    public class EventAppService : BaseApplicationService, IEventAppService
    {
        private readonly IRepository<Event, Guid> _eventRepository;
        private readonly IValidator<CreateUpdateEventDto> _validator;

        public EventAppService(IRepository<Event, Guid> eventRepository, IValidator<CreateUpdateEventDto> validator)
        {
            _eventRepository = eventRepository;
            _validator = validator;
        }
        [Authorize(EventRegistrationTaskPermissions.GetEventPermission)]

        public async Task<EventDto> GetAsync(Guid id)
        {
            var eventEntity = await _eventRepository.GetAsync(id);
            return ObjectMapper.Map<Event, EventDto>(eventEntity);
        }
        [Authorize(EventRegistrationTaskPermissions.ListEventPermission)]

        public async Task<List<EventDto>> GetListAsync()
        {
            var events = await _eventRepository.GetListAsync();
            return ObjectMapper.Map<List<Event>, List<EventDto>>(events);
        }
        [Authorize(EventRegistrationTaskPermissions.CreateEditEventPermission)]

        public async Task<EventDto> CreateAsync(CreateUpdateEventDto input)
        {
            var validationResult = await _validator.ValidateAsync(input);

            if (!validationResult.IsValid)
            {
                var exception = GetValidationException(validationResult);
                throw exception;
            }
            var eventEntity = ObjectMapper.Map<CreateUpdateEventDto, Event>(input);
            eventEntity = await _eventRepository.InsertAsync(eventEntity);
            return ObjectMapper.Map<Event, EventDto>(eventEntity);
        }
        [Authorize(EventRegistrationTaskPermissions.CreateEditEventPermission)]
        public async Task<EventDto> UpdateAsync(Guid id, CreateUpdateEventDto input)
        {
            var eventEntity = await _eventRepository.GetAsync(id);

            var validationResult = await _validator.ValidateAsync(input);
            if (!validationResult.IsValid)
            {
                var exception = GetValidationException(validationResult);
                throw exception;
            }

            ObjectMapper.Map(input, eventEntity);
            eventEntity = await _eventRepository.UpdateAsync(eventEntity);
            return ObjectMapper.Map<Event, EventDto>(eventEntity);
        }

        [Authorize(EventRegistrationTaskPermissions.DeleteEventPermission)]
        public async Task DeleteAsync(Guid id)
        {
            await _eventRepository.DeleteAsync(id);
        }
    }
}