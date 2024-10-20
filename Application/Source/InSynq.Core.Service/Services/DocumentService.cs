using InSynq.Core.Dtos.Document;

namespace InSynq.Core.Service.Services;

public class DocumentService(IDatabaseContext db, IMapper mapper) : BaseService(db), IDocumentService
{
    public async Task<ResponseWrapper<DocumentDto>> GetByTypeAsync(eLegalDocumentType type)
    {
        var result = await db.LegalDocuments.GetSingleAsync(_ => _.TypeId == type);
        return result == null ? new(ERROR_NOT_FOUND) : new(mapper.To<DocumentDto>(result));
    }
}