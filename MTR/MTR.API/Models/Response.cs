namespace MTR.API.Models;

public record Response<TModel> where TModel : class, IDto, new()
{
    public string? Message { get; set; }
    public bool Success { get; set; }
    public TModel? Model { get; set; }
}
