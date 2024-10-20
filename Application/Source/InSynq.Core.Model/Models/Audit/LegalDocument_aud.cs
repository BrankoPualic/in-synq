namespace InSynq.Core.Model.Models.Audit;

public class LegalDocument_aud : AuditDomain<int>
{
    public string Title { get; set; }

    public string Content { get; set; }

    public eLegalDocumentType? TypeId { get; set; }

    public string Version { get; set; }

    public string Language { get; set; }
}