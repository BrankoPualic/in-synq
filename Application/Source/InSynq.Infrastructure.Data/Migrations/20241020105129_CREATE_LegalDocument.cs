using InSynq.Core.Model.Models.Application;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InSynq.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CREATE_LegalDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LegalDocument",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    LastChangedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalDocument_aud",
                schema: "dbo",
                columns: table => new
                {
                    AuditId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastChangedBy = table.Column<long>(type: "bigint", nullable: false),
                    LastChangedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalDocument_aud", x => x.AuditId);
                });

            migrationBuilder.Audit<LegalDocument>();
            migrationBuilder.Up(LegalDocument.IX_LegalDocument_TypeId);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegalDocument",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LegalDocument_aud",
                schema: "dbo");

            migrationBuilder.Down(LegalDocument.IX_LegalDocument_TypeId);
        }
    }
}