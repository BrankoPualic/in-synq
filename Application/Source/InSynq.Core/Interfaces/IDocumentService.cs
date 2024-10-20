using InSynq.Core.Dtos.Document;

namespace InSynq.Core.Interfaces;

public interface IDocumentService
{
    Task<ResponseWrapper<DocumentDto>> GetByTypeAsync(eLegalDocumentType type);
}