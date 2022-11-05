namespace Docs.Application.Documents.Queries.GetDocumentById;

using Domain.Entities;


public sealed record DocumentResponse(Guid DocumentId, Document Document)
{

}