namespace InSynq.Core.Dtos.Document;

public class DocumentDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public eLegalDocumentType TypeId { get; set; }

    public LookupValueDto Type { get; set; }

    public string Version { get; set; }

    public string Language { get; set; }

    public DateTime CreatedOn { get; set; }
}