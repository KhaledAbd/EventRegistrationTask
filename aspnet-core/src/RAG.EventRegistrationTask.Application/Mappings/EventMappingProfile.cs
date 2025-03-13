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
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {


            CreateMap<Event, EventDto>()
                .ForMember(c => c.Government, opt => opt.MapFrom(dest => dest.Location.Government))
                .ForMember(c => c.City, opt => opt.MapFrom(dest => dest.Location.City))
                .ForMember(c => c.Street, opt => opt.MapFrom(dest => dest.Location.Street)) // Fixed here
                .ForMember(c => c.Location, opt => opt.MapFrom(dest => $"{dest.Location.Government}, {dest.Location.City}, {dest.Location.Street}"))
                .ForMember(c => c.Capacity, opt => opt.MapFrom(dest => dest.Capacity.Value))
                .ForMember(c => c.OrganizerName, opt => opt.MapFrom(dest => dest.Organizer != null ? dest.Organizer.UserName : ""));


            /// can use if ABP Impelemtent Mapper Context
            //CreateMap<Event, EventActiveDto>()
            //    .ForMember(c => c.IsRegistered, opt => opt.MapFrom((dest, _, _, context) =>
            //        dest.EventRegistrations.Any(c => c.UserId.Equals(context.Items["UserId"]))));


            CreateMap<CreateUpdateEventDto, Event>()
                .ForMember(c => c.Location, opt => opt.MapFrom(dest => new Location(dest.Government, dest.City, dest.Street)))
                .ForMember(c => c.StartDate, opt => opt.MapFrom(dest => DateTime.SpecifyKind(dest.StartDate, DateTimeKind.Utc)))
                .ForMember(c => c.EndDate, opt => opt.MapFrom(dest => DateTime.SpecifyKind(dest.EndDate, DateTimeKind.Utc)));


            CreateMap<EventRegistration, EventRegistrationDto>()
                .ForMember(c => c.EventNameAr, dest => dest.MapFrom(c => c.Event.NameAr))
                .ForMember(c => c.EventNameEn, dest => dest.MapFrom(c => c.Event.NameEn))
                .ForMember(c => c.EventLink, dest => dest.MapFrom(c => c.Event.Link))
                .ForMember(c => c.UserName, dest => dest.MapFrom(c => c.User != null ? c.User.Name : ""))
                .ForMember(c => c.Email, opt => opt.MapFrom(c=> c.User != null ? c.User.Email : ""));

            CreateMap<CreateUpdateEventRegistrationDto, EventRegistration>();
            CreateMap<EventRegistration, EventRegistrationDto>();
        }
    }
}
