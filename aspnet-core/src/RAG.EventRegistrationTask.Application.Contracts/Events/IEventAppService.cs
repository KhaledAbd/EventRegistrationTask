using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace RAG.EventRegistrationTask.Events
{
    public interface IEventAppService
    {
        Task<EventDto> GetAsync(Guid id);
        public Task<PagedResultDto<EventDto>> GetListAsync(string organizerName = null, int skipCount = 0, int maxResultCount = 10);
        Task<EventDto> CreateAsync(CreateUpdateEventDto input);
        Task<EventDto> UpdateAsync(Guid id, CreateUpdateEventDto input);
        Task DeleteAsync(Guid id);
    }

}
