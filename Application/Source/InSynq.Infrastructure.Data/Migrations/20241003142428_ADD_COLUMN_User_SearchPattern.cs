using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InSynq.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_COLUMN_User_SearchPattern : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchPattern",
                schema: "dbo",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                computedColumnSql: "[Username] + ' ' + [FirstName] + ' ' + [LastName] + CASE WHEN [MiddleName] IS NOT NULL AND [MiddleName] <> '' THEN ' ' + [MiddleName] ELSE '' END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchPattern",
                schema: "dbo",
                table: "User");
        }
    }
}