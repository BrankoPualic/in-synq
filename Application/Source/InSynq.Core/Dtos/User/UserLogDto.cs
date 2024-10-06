namespace InSynq.Core.Dtos.User;

public class UserLogDto
{
    public int UsernameCount { get; set; }

    public List<string> Usernames { get; set; } = [];
}