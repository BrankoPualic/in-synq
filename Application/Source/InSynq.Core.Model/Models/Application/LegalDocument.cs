namespace InSynq.Core.Model.Models.Application;

public class LegalDocument : BaseIndexAuditedDomain<LegalDocument, int>, IConfigurableEntity
{
    public string Title { get; set; }

    public string Content { get; set; }

    public eLegalDocumentType TypeId { get; set; }

    public string Version { get; set; }

    public string Language { get; set; }

    //
    // Indexes
    //

    public static IDatabaseIndex IX_LegalDocument_TypeId => new DatabaseIndex(nameof(IX_LegalDocument_TypeId))
    {
        Columns = { nameof(TypeId) },
        Include = { nameof(Version), nameof(Language) }
    };

    public void Configure(ModelBuilder builder)
    {
        builder.Entity<LegalDocument>(_ =>
        {
            _.Property(_ => _.Title).IsRequired().HasMaxLength(255);
            _.Property(_ => _.Content).IsRequired();
            _.Property(_ => _.Version).IsRequired().HasMaxLength(10);
            _.Property(_ => _.Language).IsRequired().HasMaxLength(10);
        });
    }
}