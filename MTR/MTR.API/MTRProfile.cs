using AutoMapper;

using MTR.API.Commands;
using MTR.Domain;
using MTR.DTO;

namespace MTR.API;

public class MTRProfile : Profile
{
    public MTRProfile()
    {
        CreateMap<JoinGameCommand, Game>();

        CreateMap<(Game, List<UserDetail>), GameDto>()
            .ForMember(d => d.Guid, o => o.MapFrom(s => s.Item1.Guid))
            .ForMember(d => d.Players, o => o.MapFrom(s => s.Item2));

        CreateMap<UserDetail, PlayerDto>()
            .ForMember(d => d.Username, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Guid, o => o.MapFrom(s => s.User.Guid));

        CreateMap<CreateUserCommand, User>()
            .ForMember(d => d.Details, o => o.MapFrom(s => new List<UserDetail>
            {
                new UserDetail
                {
                    Guid = s.Guid,
                    Email = s.Email,
                    Name = s.Username,
                    Password = s.Password,
                    //Image = new() { Id = s.Image }
                }
            }))
            .AfterMap((src, dest) => dest.Details.ForEach(d => d.User = dest));

        CreateMap<UserDetail, UserDto>()
            .ForMember(d => d.Username, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Guid, o => o.MapFrom(s => s.User.Guid))
            .ForMember(d => d.Image, o => o.MapFrom(s => s.Image != null ? s.Image.Path : default));

        CreateMap<(Game, User), Player>()
            .ForMember(d => d.Game, o => o.MapFrom(s => s.Item1))
            .ForMember(d => d.GameId, o => o.MapFrom(s => s.Item1.Id))
            .ForMember(d => d.UserId, o => o.MapFrom(s => s.Item2.Id));
    }
}
