namespace Docs.Application.Documents.Queries.GetDocumentById;

using Abstractions.Messaging;


public sealed record GetDocumentByIdQuery(Guid DocumentId) : IQuery<DocumentResponse> { }