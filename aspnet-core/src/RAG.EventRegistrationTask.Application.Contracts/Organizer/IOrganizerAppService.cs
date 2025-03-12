using RAG.EventRegistrationTask.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAG.EventRegistrationTask.Organizer
{
    public interface IOrganizerAppService
    {
        Task<List<OrganizerDto>> GetListAsync();
    }
}
