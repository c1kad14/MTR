using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTR.Domain;

public class MTRUser : IdentityUser<Guid>
{
    public Guid Guid { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public virtual List<Player> Players { get; set; }
    public virtual List<UserDetail> Details { get; set; }
}
