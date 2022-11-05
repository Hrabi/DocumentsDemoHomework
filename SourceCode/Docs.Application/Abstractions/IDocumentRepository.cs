namespace Docs.Application.Abstractions;

using Domain.Entities;


public interface IDocumentRepository
{
  Task<Document?> AddAsync(Document document, CancellationToken cancellationToken);
  Task<Document?> GetByIdAsync(Guid requestDocumentId, CancellationToken cancellationToken);
  Task<bool> IsDocumentUniqueAsync(Document document, CancellationToken cancellationToken);
}