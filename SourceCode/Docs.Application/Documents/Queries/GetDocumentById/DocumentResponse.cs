namespace Docs.Application.Documents.Queries.GetDocumentById;

using Domain.Entities;


public sealed record DocumentResponse(Guid documentId, Document document)
{

}