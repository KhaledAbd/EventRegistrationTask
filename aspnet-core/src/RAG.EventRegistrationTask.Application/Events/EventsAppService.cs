using ABPCourse.Demo1.Products;
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

            if (eventEntity is null)
            {
                // TODO:: I will add custom Exception InHirate from Bisness and Impelment NOTFOUND
                throw new EventNotFoundException(id);
            }

            return ObjectMapper.Map<Event, EventDto>(eventEntity);
        }

        #region
        [Authorize(EventRegistrationTaskPermissions.ListEventPermission)]
        public async Task<PagedResultDto<EventActionDto>> GetListAsync(string? organizerName = null, bool ReteiveOwnUser = false, int skipCount = 0, int maxResultCount = 10)
        {
            var query = (await _eventRepository
            .WithDetailsAsync(c => c.Organizer, c => c.EventRegistrations))
                .AsQueryable();

            // Filter by Organizer Name if provided
            if (!string.IsNullOrWhiteSpace(organizerName))
            {
                query = query.Where(e => e.Organizer.Name.Contains(organizerName));
            }

            if (ReteiveOwnUser)
            {
                query = query.Where(e => e.CreatorId == CurrentUser.Id);
            }

            // Get the total count before applying paging
            var totalCount = await AsyncExecuter.CountAsync(query);

            // Apply Paging
            var events = await AsyncExecuter.ToListAsync(
                        query
                            .OrderBy(e => e.CreationTime) // Optional sorting
                            .Skip(skipCount)
                            .Take(maxResultCount)
                            .Select(c => new EventActionDto
                            {
                                Event = ObjectMapper.Map<Event, EventDto>(c),
                                CanEdit = CurrentUser.Id == c.CreatorId,
                                HasActiveAction = CurrentUser.Id == c.CreatorId && DateTime.Now.Date > c.StartDate.Date
                            })
                    );

            return new PagedResultDto<EventActionDto>(
                totalCount,
                events
            );
        }

        public async Task<PagedResultDto<EventActiveDto>> GetActiveEventAsync(string? name = null, string? organizerName = null, int skipCount = 0, int maxResultCount = 10)
        {
            var query = (await _eventRepository
            .WithDetailsAsync(c => c.Organizer, c => c.EventRegistrations))
            .Where(c => c.IsActive == true || c.EventRegistrations.Any(d => d.UserId == CurrentUser.Id && !d.IsCanceled!.Value))
            .AsQueryable();


            // Filter by Organizer Name if provided
            if (!string.IsNullOrWhiteSpace(organizerName))
            {
                query = query.Where(e => e.Organizer.Name.Contains(organizerName));
            }
            // Filter by Organizer Name if provided
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(e => e.NameAr.Contains(name) || e.NameEn.Contains(name));
            }

            // Get the total count before applying paging
            var totalCount = await AsyncExecuter.CountAsync(query);

            // Apply Paging
            var events = await AsyncExecuter.ToListAsync(
                query
                    .OrderBy(e => e.CreationTime) // Optional sorting
                    .Skip(skipCount)
                    .Take(maxResultCount)
                    .Select(c => new EventActiveDto
                    {
                        Event = ObjectMapper.Map<Event, EventDto>(c),
                        IsRegistered = c.EventRegistrations.Any(c => c.UserId == CurrentUser.Id && !c.IsCanceled!.Value),
                        EventRegistrationId = c.EventRegistrations.Where(c => c.UserId == CurrentUser.Id && !c.IsCanceled!.Value).Select(c => c.Id).SingleOrDefault()
                    })
            );
            return new PagedResultDto<EventActiveDto>(
              totalCount,
            events
          );

            // TODO:: Context Map NOt work
            //  return new PagedResultDto<EventActiveDto>(
            //    totalCount,
            //    ObjectMapper.Map<List<Event>, List<EventActiveDto>>(events, opts =>
            //        opts.Items["UserId"] = CurrentUser.Id)
            //);

        }

        #endregion
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

        [Authorize(EventRegistrationTaskPermissions.ActiveEventPermission)]
        public async Task<bool> ActiveAsync(Guid id)
        {
            var eventEntity = await _eventRepository.GetAsync(id);
            eventEntity.IsActive = !eventEntity.IsActive;
            eventEntity = await _eventRepository.UpdateAsync(eventEntity);
            return eventEntity != null && eventEntity.Id != Guid.Empty;
        }

    }
}