namespace Docs.Application.Documents;

using Domain.Entities;


public sealed record DocumentResponse(Guid DocumentId, Document Document) { }