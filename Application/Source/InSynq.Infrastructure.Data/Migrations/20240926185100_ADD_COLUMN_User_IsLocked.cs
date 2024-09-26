using InSynq.Core.Model.Models.Application.User;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InSynq.Infrastructure.Data.Migrations
{
	/// <inheritdoc />
	public partial class ADD_COLUMN_User_IsLocked : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>(
				name: "IsLocked",
				schema: "dbo",
				table: "User_aud",
				type: "bit",
				nullable: true);

			migrationBuilder.AddColumn<bool>(
				name: "IsLocked",
				schema: "dbo",
				table: "User",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.Audit<User>();
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "IsLocked",
				schema: "dbo",
				table: "User_aud");

			migrationBuilder.DropColumn(
				name: "IsLocked",
				schema: "dbo",
				table: "User");
		}
	}
}