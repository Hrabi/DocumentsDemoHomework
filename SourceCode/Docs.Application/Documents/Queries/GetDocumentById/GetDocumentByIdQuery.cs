namespace Docs.Application.Documents.Queries.GetDocumentById;

using Abstractions.Messaging;


public sealed record GetDocumentByIdQuery(Guid documentId) : IQuery<DocumentResponse>
{
}