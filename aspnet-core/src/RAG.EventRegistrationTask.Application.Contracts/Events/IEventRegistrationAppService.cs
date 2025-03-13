using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace RAG.EventRegistrationTask.Events;
public interface IEventRegistrationAppService
{
    Task<bool> RegisterAsync(Guid eventId);
    Task<bool> CancelAsync(Guid id);
    Task<List<EventRegistrationDto>> GetRegistrationsForEventAsync(Guid eventId);
}
