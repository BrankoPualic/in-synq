using InSynq.Core.Dtos.Document;
using InSynq.Core.Interfaces;
using InSynq.Core.Model;
using InSynq.Web.Api.Controllers._Base;
using InSynq.Web.Api.ReinforcedTypings.Generator;
using Microsoft.AspNetCore.Mvc;

namespace InSynq.Web.Api.Controllers;

public class DocumentController(IDocumentService documentService) : BaseController
{
    [HttpGet]
    [AngularMethod(typeof(DocumentDto))]
    public async Task<IActionResult> GetByType([FromQuery] eLegalDocumentType type) => Result(await documentService.GetByTypeAsync(type));
}