using MTR.Domain;

namespace MTR.Core.Abstractions;

public interface IRoundManager
{
    Task<Round> BeginRoundAsync(Game game, Guid roundGuid);

}
