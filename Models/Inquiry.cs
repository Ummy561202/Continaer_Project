namespace Continaer_Project.Models;

public class Inquiry
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string PropertyTitle { get; set; } = "";
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string? Phone { get; set; }
    public string Message { get; set; } = "";
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
