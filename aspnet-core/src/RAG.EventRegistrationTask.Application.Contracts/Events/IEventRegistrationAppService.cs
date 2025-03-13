using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
namespace RAG.EventRegistrationTask.Events;
public interface IEventRegistrationAppService
{
    Task<bool> RegisterAsync(Guid eventId);
    Task<bool> CancelAsync(Guid id);
    Task<PagedResultDto<EventRegistrationDto>> GetRegistrationsEventAsync(Guid eventId,int skipCount = 0, int maxResultCount = 10);

}
