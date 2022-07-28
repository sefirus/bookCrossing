using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid ProfilePictureId { get; set; }
    public Picture? ProfilePicture { get; set; }
    public IEnumerable<Comment> Comments { get; set; } 
        = new List<Comment>();    
    public IEnumerable<BookCopy> CurrentBooks { get; set; } 
        = new List<BookCopy>();
    public IEnumerable<HistoryRecord> HistoryRecords { get; set; }
        = new List<HistoryRecord>();
}