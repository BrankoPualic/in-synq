using InSynq.Core.Model.Models.Application.User;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InSynq.Infrastructure.Data.Migrations;

[DbContext(typeof(DatabaseContext))]
[Migration("20240929152333_CREATE_INDEXES_User")]
public partial class CreateUserIndexes : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.Up([User.IX_User_Username, User.IX_User_Email, User.IX_User_IsActive_IsLocked]);

    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.Down([User.IX_User_Username, User.IX_User_Email, User.IX_User_IsActive_IsLocked]);
}