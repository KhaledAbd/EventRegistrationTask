using AutoMapper;
using RAG.EventRegistrationTask.Events;
using RAG.EventRegistrationTask.Events.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAG.EventRegistrationTask.Mappings
{
    public class EventMappingProfile: Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, EventDto>();
            CreateMap<CreateUpdateEventDto, Event>();

            CreateMap<EventRegistration,EventRegistrationDto>()
                .ForMember(c => c.EventNameAr, dest => dest.MapFrom(c => c.Event.NameAr))
                .ForMember(c => c.EventNameEn, dest => dest.MapFrom(c => c.Event.NameEn))
                .ForMember(c => c.EventLink, dest => dest.MapFrom(c => c.Event.Link))
                .ForMember(c => c.EventLocation, dest => dest.MapFrom(c => c.Event.Location));

            CreateMap<CreateUpdateEventDto, EventRegistration>();
        }
    }
}
