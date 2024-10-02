SET IDENTITY_INSERT [User] ON;

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
    GenderId,
    CountryId,
    Phone,
    Privacy,
    IsActive,
    IsLocked,
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
    1,
    155,
    '+381 640230105',
    1,
    1,
    0,
    CURRENT_TIMESTAMP,
    0,
    CURRENT_TIMESTAMP,
    0
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
    1,
    155,
    '+381 640230105',
    1,
    1,
    0,
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
    1,
    155,
    '+381 640230105',
    1,
    1,
    0,
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
    1,
    155,
    '+381 640230105',
    1,
    1,
    0,
    CURRENT_TIMESTAMP,
    1,
    CURRENT_TIMESTAMP,
    1
);

SET IDENTITY_INSERT [User] OFF;

-- User Roles

INSERT INTO UserRole
(UserId,RoleId)
VALUES
(1,1), (2,2), (3,3), (4,4);
