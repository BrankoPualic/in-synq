using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InSynq.Infrastructure.Data.Migrations;

[DbContext(typeof(DatabaseContext))]
[Migration("20241020115129_SEED_LegalDocument")]
public partial class SeedLegalDocument : ViewsMigration
{
    protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.Seed("LegalDocument.sql");
}