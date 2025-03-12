using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using RAG.EventRegistrationTask.Base;
using RAG.EventRegistrationTask.Events.Entities;
using RAG.EventRegistrationTask.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
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
            var eventEntity = (await _eventRepository
                         .WithDetailsAsync(c => c.Organizer))
                         .FirstOrDefault(c => c.Id == id);
            
            if(eventEntity is null)
            {
                // TODO:: I will add custom Exception InHirate from Bisness and Impelment NOTFOUND
                throw new BusinessException("", "Not Found");
            }

            return ObjectMapper.Map<Event, EventDto>(eventEntity);
        }

        [Authorize(EventRegistrationTaskPermissions.ListEventPermission)]
        public async Task<PagedResultDto<EventDto>> GetListAsync(string? organizerName = null, int skipCount = 0, int maxResultCount = 10)
        {
            var query = (await _eventRepository
                .WithDetailsAsync(c => c.Organizer))
                .AsQueryable();

            // Filter by Organizer Name if provided
            if (!string.IsNullOrWhiteSpace(organizerName))
            {
                query = query.Where(e => e.Organizer.Name.Contains(organizerName));
            }

            // Get the total count before applying paging
            var totalCount = await AsyncExecuter.CountAsync(query);

            // Apply Paging
            var events = await AsyncExecuter.ToListAsync(
                query
                    .OrderBy(e => e.CreationTime) // Optional sorting
                    .Skip(skipCount)
                    .Take(maxResultCount)
            );

            return new PagedResultDto<EventDto>(
                totalCount,
                ObjectMapper.Map<List<Event>, List<EventDto>>(events)
            );
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