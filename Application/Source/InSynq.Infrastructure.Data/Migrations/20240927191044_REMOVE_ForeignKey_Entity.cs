using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InSynq.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class REMOVE_ForeignKey_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_User_CreatedBy",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_LastChangedBy",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_aud_User_CreatedBy",
                schema: "dbo",
                table: "User_aud");

            migrationBuilder.DropForeignKey(
                name: "FK_User_aud_User_DeletedBy",
                schema: "dbo",
                table: "User_aud");

            migrationBuilder.DropForeignKey(
                name: "FK_User_aud_User_LastChangedBy",
                schema: "dbo",
                table: "User_aud");

            migrationBuilder.DropIndex(
                name: "IX_User_aud_CreatedBy",
                schema: "dbo",
                table: "User_aud");

            migrationBuilder.DropIndex(
                name: "IX_User_aud_DeletedBy",
                schema: "dbo",
                table: "User_aud");

            migrationBuilder.DropIndex(
                name: "IX_User_aud_LastChangedBy",
                schema: "dbo",
                table: "User_aud");

            migrationBuilder.DropIndex(
                name: "IX_User_CreatedBy",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_LastChangedBy",
                schema: "dbo",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_aud_CreatedBy",
                schema: "dbo",
                table: "User_aud",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_aud_DeletedBy",
                schema: "dbo",
                table: "User_aud",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_aud_LastChangedBy",
                schema: "dbo",
                table: "User_aud",
                column: "LastChangedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedBy",
                schema: "dbo",
                table: "User",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_LastChangedBy",
                schema: "dbo",
                table: "User",
                column: "LastChangedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_CreatedBy",
                schema: "dbo",
                table: "User",
                column: "CreatedBy",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_LastChangedBy",
                schema: "dbo",
                table: "User",
                column: "LastChangedBy",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_aud_User_CreatedBy",
                schema: "dbo",
                table: "User_aud",
                column: "CreatedBy",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_aud_User_DeletedBy",
                schema: "dbo",
                table: "User_aud",
                column: "DeletedBy",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_aud_User_LastChangedBy",
                schema: "dbo",
                table: "User_aud",
                column: "LastChangedBy",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}