using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InSynq.Infrastructure.Data.Migrations;

[DbContext(typeof(DatabaseContext))]
[Migration("20241020200824_CREATE_Lookup_Views")]
public partial class Views : ViewsMigration
{ }