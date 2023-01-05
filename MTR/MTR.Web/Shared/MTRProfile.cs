using AutoMapper;

using MTR.Web.Shared.Commands;
using MTR.Domain;
using MTR.DTO;

namespace MTR.Web.Shared;

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
            .ForMember(d => d.Guid, o => o.MapFrom(s => s.MTRUser.Guid));

        CreateMap<SignUpUserCommand, MTRUser>()
            .ForMember(d => d.Details, o => o.MapFrom(s => new List<UserDetail>
            {
                new UserDetail
                {
                    Guid = s.Guid,
                    Email = s.Email,
                    Name = s.Username
                    //Image = new() { Id = s.Image }
                }
            }))
            .AfterMap((src, dest) => dest.Details.ForEach(d => d.MTRUser = dest));

        CreateMap<UserDetail, UserDto>()
            .ForMember(d => d.Username, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Guid, o => o.MapFrom(s => s.MTRUser.Guid))
            .ForMember(d => d.Image, o => o.MapFrom(s => s.Image != null ? s.Image.Path : default));

        CreateMap<(Game, MTRUser), Player>()
            .ForMember(d => d.Game, o => o.MapFrom(s => s.Item1))
            .ForMember(d => d.GameId, o => o.MapFrom(s => s.Item1.Id))
            .ForMember(d => d.MTRUserId, o => o.MapFrom(s => s.Item2.Id));
    }
}
