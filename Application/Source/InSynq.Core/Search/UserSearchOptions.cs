namespace InSynq.Core.Search;

public class UserSearchOptions : SearchOptions
{
    public string Name { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsLocked { get; set; }
}