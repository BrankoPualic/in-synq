using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InSynq.Infrastructure.Data.Migrations
{
	/// <inheritdoc />
	public partial class CREATE_Country : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Country",
				schema: "dbo",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Iso2Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
					Iso3Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
					DialCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
					FlagUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Country", x => x.Id);
				});

			migrationBuilder.Seed("Countries.sql");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Country",
				schema: "dbo");
		}
	}
}