using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RAG.EventRegistrationTask.Events
{
    public interface IEventAppService
    {
        Task<EventDto> GetAsync(Guid id);
        Task<List<EventDto>> GetListAsync();
        Task<EventDto> CreateAsync(CreateUpdateEventDto input);
        Task<EventDto> UpdateAsync(Guid id, CreateUpdateEventDto input);
        Task DeleteAsync(Guid id);
    }

}
