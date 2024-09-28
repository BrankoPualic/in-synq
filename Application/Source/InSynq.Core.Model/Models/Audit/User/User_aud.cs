namespace InSynq.Core.Model.Models.Audit;

public class User_aud : AuditDomain<long>
{
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string ProfileImageUrl { get; set; }

    public string PublicId { get; set; }

    public string Biography { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string Details { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsLocked { get; set; }
}