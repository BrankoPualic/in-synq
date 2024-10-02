using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InSynq.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_COLUMN_NEW_User_FullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "dbo",
                table: "User",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true,
                computedColumnSql: "[FirstName] + CASE WHEN [MiddleName] IS NOT NULL AND [MiddleName] <> '' THEN ' ' + [MiddleName] ELSE '' END + ' ' + [LastName]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "dbo",
                table: "User");
        }
    }
}
