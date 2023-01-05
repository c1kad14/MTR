using MTR.DTO;

namespace MTR.Web.Shared.Models;

public record ResponseMultiple<TModel> where TModel : class, IDto, new()
{
    public string? Message { get; set; }
    public bool Success { get; set; }
    public List<TModel> Model { get; set; } = new();
}
