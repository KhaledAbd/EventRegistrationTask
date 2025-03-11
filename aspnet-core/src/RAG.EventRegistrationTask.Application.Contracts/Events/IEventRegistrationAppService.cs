using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace RAG.EventRegistrationTask.Events;
public interface IEventRegistrationAppService
{
    Task<EventRegistrationDto> RegisterAsync(Guid eventId);
    Task CancelAsync(Guid id);
    Task<List<EventRegistrationDto>> GetRegistrationsForEventAsync(Guid eventId);
}
