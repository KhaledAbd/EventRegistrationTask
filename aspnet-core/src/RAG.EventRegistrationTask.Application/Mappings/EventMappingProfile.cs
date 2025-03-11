using AutoMapper;
using RAG.EventRegistrationTask.Events;
using RAG.EventRegistrationTask.Events.Entities;
using RAG.EventRegistrationTask.Events.ValueObjects;
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

            
            CreateMap<Event, EventDto>()
            // impilicit convert to string is not work must be by customResolver in Automapper.
                .ForMember(c => c.Location, opt => opt.MapFrom(dest => $"{dest.Location.Government}, {dest.Location.City}, {dest.Location.Street}"))
                   .ForMember(c => c.Capacity, opt => opt.MapFrom(dest => dest.Capacity.Value));
            CreateMap<CreateUpdateEventDto, Event>()
                .ForMember(c => c.Location, opt => opt.MapFrom(dest => new Location(dest.Government, dest.City, dest.Street)));

            CreateMap<EventRegistration,EventRegistrationDto>()
                .ForMember(c => c.EventNameAr, dest => dest.MapFrom(c => c.Event.NameAr))
                .ForMember(c => c.EventNameEn, dest => dest.MapFrom(c => c.Event.NameEn))
                .ForMember(c => c.EventLink, dest => dest.MapFrom(c => c.Event.Link))
                .ForMember(c => c.EventLocation, dest => dest.MapFrom(c => c.Event.Location));

            CreateMap<CreateUpdateEventRegistrationDto, EventRegistration>();
        }
    }
}
