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


        CreateMap<Player, PlayerDto>()
            .ForMember(d => d.Username, o => o.MapFrom(s => s.MTRUser.UserName))
            .ForMember(d => d.IsReady, o => o.MapFrom(s => s.RoundReady.Any() ? s.RoundReady.OrderByDescending(rr => rr.Modified).First().Ready : false))
            .ForMember(d => d.Guid, o => o.MapFrom(s => s.Guid));

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

        CreateMap<TableType, string>().ConvertUsing(src => src.ToString());
        CreateMap<StatusType, string>().ConvertUsing(src => src.ToString());

        CreateMap<Game, GameDto>()
            .ForMember(d => d.MaxPlayers, o => o.MapFrom(s => (int)s.TableType))
            .ReverseMap();

        CreateMap<Game, GameListItemDto>()
            .ForMember(d => d.GameGuid, o => o.MapFrom(s => s.Guid))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.TableType))
            .ForMember(d => d.PlayersInfo, o => o.MapFrom(s => $"{s.Players.Count(p => !p.Removed.Any())} / {(int)s.TableType}"))
            .ForMember(d => d.Owner, o => o.MapFrom(s => s.Players.Single(p => p.Guid == s.Guid).MTRUser.UserName))
            .ReverseMap();

        CreateMap<(JoinGameCommand, Game, MTRUser), Player>()
            .ForMember(d => d.Guid, o => o.MapFrom(s => s.Item1.PlayerGuid))
            .ForMember(d => d.Game, o => o.MapFrom(s => s.Item2))
            .ForMember(d => d.GameId, o => o.MapFrom(s => s.Item2.Id))
            .ForMember(d => d.MTRUserId, o => o.MapFrom(s => s.Item3.Id));

        CreateMap<(Player, SitPlayerCommand), PlayerPosition>()
            .ForMember(d => d.PlayerId, o => o.MapFrom(s => s.Item1.Id))
            .ForMember(d => d.Guid, o => o.MapFrom(s => s.Item2.Guid))
            .ForMember(d => d.Position, o => o.MapFrom(s => s.Item2.Position));

        CreateMap<PlayerPosition, SitPlayerDto>();
    }
}
