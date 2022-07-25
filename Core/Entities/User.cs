using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsActive { get; set; } = true;
    public Picture? ProfilePicture { get; set; }
    public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
    public IEnumerable<HistoryRecord> HistoryRecords { get; set; }
}