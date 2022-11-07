using Docs.Application.Abstractions.Messaging;

namespace Docs.Application.Documents.Queries.ConvertDocumentById;

public sealed record ConvertDocumentByIdQuery(Guid DocumentId, string Text) : IQuery<DocumentResponse> { }
