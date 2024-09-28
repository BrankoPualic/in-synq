using System.ComponentModel;

namespace InSynq.Core.Model;

public enum eSystemRole
{
    [Description("Administrator")]
    Admin = 1,

    [Description("Member")]
    Member = 2,

    [Description("User Admin")]
    UserAdmin = 3,

    [Description("Moderator")]
    Moderator = 4,
}

public enum eAuditChangeType
{
    Create = 1,
    Update = 2,
    Delete = 3,
    CreateRemoved = 4
}

public enum eGender
{
    Male = 1,
    Female = 2,
    Other = 3
}