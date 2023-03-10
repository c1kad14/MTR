namespace MTR.DTO;

public record UserDto : IDto
{
    public Guid Guid { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Image { get; set; }
}
