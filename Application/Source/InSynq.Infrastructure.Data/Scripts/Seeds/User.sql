SET IDENTITY_INSERT [User] ON;

-- Disable both foreign key constraints
ALTER TABLE [User] NOCHECK CONSTRAINT FK_User_User_CreatedBy;
ALTER TABLE [User] NOCHECK CONSTRAINT FK_User_User_LastChangedBy;

INSERT INTO [User]
(
    Id,
    FirstName,
    MiddleName,
    LastName,
    Username,
    Email,
    Password,
    ProfileImageUrl,
    PublicId,
    Biography,
    DateOfBirth,
    Details,
    IsActive,
    CreatedOn,
    CreatedBy,
    LastChangedOn,
    LastChangedBy
)
VALUES
(
    1,
    'System',
    NULL,
    'Admin',
    'admin',
    'sysadmin@insinq.com',
    '$2a$12$ASR.eawVK67Rp6cXiul9zuYYYgmeosye4mLSEbqTj.lsunl8S63wi', --Pa$$w0rd
    NULL,
    NULL,
    NULL,
    '2002-10-10 00:00:00.000',
    NULL,
    1,
    CURRENT_TIMESTAMP,
    1,
    CURRENT_TIMESTAMP,
    1
),
(
    2,
    'System',
    NULL,
    'Member',
    'member',
    'sysmember@insinq.com',
    '$2a$12$ASR.eawVK67Rp6cXiul9zuYYYgmeosye4mLSEbqTj.lsunl8S63wi', --Pa$$w0rd
    NULL,
    NULL,
    NULL,
    '2002-10-10 00:00:00.000',
    NULL,
    1,
    CURRENT_TIMESTAMP,
    1,
    CURRENT_TIMESTAMP,
    1
),
(
    3,
    'System',
    NULL,
    'UserAdmin',
    'useradmin',
    'sysuseradmin@insinq.com',
    '$2a$12$ASR.eawVK67Rp6cXiul9zuYYYgmeosye4mLSEbqTj.lsunl8S63wi', --Pa$$w0rd
    NULL,
    NULL,
    NULL,
    '2002-10-10 00:00:00.000',
    NULL,
    1,
    CURRENT_TIMESTAMP,
    1,
    CURRENT_TIMESTAMP,
    1
),
(
    4,
    'System',
    NULL,
    'Moderator',
    'moderator',
    'sysmoderator@insinq.com',
    '$2a$12$ASR.eawVK67Rp6cXiul9zuYYYgmeosye4mLSEbqTj.lsunl8S63wi', --Pa$$w0rd
    NULL,
    NULL,
    NULL,
    '2002-10-10 00:00:00.000',
    NULL,
    1,
    CURRENT_TIMESTAMP,
    1,
    CURRENT_TIMESTAMP,
    1
);

SET IDENTITY_INSERT [User] OFF;

-- Re-enable the foreign key constraints
ALTER TABLE [User] CHECK CONSTRAINT FK_User_User_CreatedBy;
ALTER TABLE [User] CHECK CONSTRAINT FK_User_User_LastChangedBy;

-- User Roles

INSERT INTO UserRole
(UserId,RoleId)
VALUES
(1,1), (2,2), (3,3), (4,4);
