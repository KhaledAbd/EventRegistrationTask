using RAG.EventRegistrationTask.Base;
using RAG.EventRegistrationTask.Events.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace RAG.EventRegistrationTask.Events
{
    public class EventAppService : BaseApplicationService, IEventAppService
    {
        private readonly IRepository<Event, Guid> _eventRepository;

        public EventAppService(IRepository<Event, Guid> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<EventDto> GetAsync(Guid id)
        {
            var eventEntity = await _eventRepository.GetAsync(id);
            return ObjectMapper.Map<Event, EventDto>(eventEntity);
        }

        public async Task<List<EventDto>> GetListAsync()
        {
            var events = await _eventRepository.GetListAsync();
            return ObjectMapper.Map<List<Event>, List<EventDto>>(events);
        }

        public async Task<EventDto> CreateAsync(CreateUpdateEventDto input)
        {
            var eventEntity = ObjectMapper.Map<CreateUpdateEventDto, Event>(input);
            eventEntity = await _eventRepository.InsertAsync(eventEntity);
            return ObjectMapper.Map<Event, EventDto>(eventEntity);
        }

        public async Task<EventDto> UpdateAsync(Guid id, CreateUpdateEventDto input)
        {
            var eventEntity = await _eventRepository.GetAsync(id);
            ObjectMapper.Map(input, eventEntity);
            eventEntity = await _eventRepository.UpdateAsync(eventEntity);
            return ObjectMapper.Map<Event, EventDto>(eventEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _eventRepository.DeleteAsync(id);
        }
    }
}