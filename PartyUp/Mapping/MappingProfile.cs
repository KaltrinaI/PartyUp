using AutoMapper;
using PartyUp.DTOs;
using PartyUp.Models;

namespace PartyUp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserResponseDTO, User>();

            CreateMap<Location, LocationDTO>();
            CreateMap<LocationDTO, Location>();

            CreateMap<EventRequestDTO, Event>()
                 .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                 .ForMember(dest => dest.Organizer, opt => opt.MapFrom(src => src.Organizer));

            CreateMap<Event, EventRequestDTO>();

            CreateMap<Event, EventResponseDTO>();

            _ = CreateMap<Event, EventResponseDTO>()
                 .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.EventName))
                 .ForMember(dest => dest.DateTimeOfEvent, opt => opt.MapFrom(src => src.DateTimeOfEvent))
                 .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                 .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags == null ? new List<Tag>() : src.Tags.ToList()));

            CreateMap<BusinessEntityRequestDTO, BusinessEntity>()
                    .ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.Events))
                    .ForMember(dest => dest.Owner, opt => opt.Ignore());

            CreateMap<BusinessEntityDTO, BusinessEntity>();
            CreateMap<BusinessEntityRequestDTO, BusinessEntity>();

            CreateMap<ReservationRequest, ReservationRequestDTO>()
                 .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                 .ForMember(dest => dest.NrOfPeople, opt => opt.MapFrom(src => src.NrOfPeople));

            CreateMap<ReservationRequestDTO, ReservationRequest>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.NrOfPeople, opt => opt.MapFrom(src => src.NrOfPeople))
                .ForMember(dest => dest.Event, opt => opt.Ignore());

            CreateMap<ReservationRequest, ReservationResponseDTO>()
                .ForMember(dest => dest.ReservationRequestId, opt => opt.MapFrom(src => src.ReservationRequestId))
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.NrOfPeople, opt => opt.MapFrom(src => src.NrOfPeople));

            CreateMap<CommentDTO, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TimeOfComment, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore()) 
                .ForMember(dest => dest.Event, opt => opt.Ignore());

            CreateMap<Comment, CommentResponseDTO>()
                .ForMember(dest => dest.CommentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));


            CreateMap<CommentResponseDTO, Comment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CommentId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.User, opt => opt.Ignore()) 
                .ForMember(dest => dest.Event, opt => opt.Ignore())
                .ForMember(dest => dest.TimeOfComment, opt => opt.Ignore());
        }
    }
}
