﻿using InSynq.Core.Model.Attributes;
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

    [Description("Legal Department")]
    LegalDepartment = 5,
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
    [Description("")]
    [BgColor("#D7D7D7")]
    NotSet = 0,

    [BgColor("#00FFFF")]
    Male = 1,

    [BgColor("#FF0000")]
    Female = 2,

    [BgColor("#00FFFF")]
    Other = 3
}

public enum eLegalDocumentType
{
    [Description("Privacy Policy")]
    PrivacyPolicy = 10,

    [Description("Terms of Use")]
    TermsOfUse = 11,
}