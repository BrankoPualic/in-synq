using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InSynq.Infrastructure.Data.Migrations;

[DbContext(typeof(DatabaseContext))]
[Migration("20240921200850_CREATE_usp_CreateAuditTrigger")]
public partial class CreateAuditTrigger : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        var sqlFilePath = Path.Combine("Scripts", "Procedures", "usp_CreateAuditTriggerStoredProcedure.sql");
        var sql = File.ReadAllText(sqlFilePath);
        migrationBuilder.Sql(sql);
    }
}