namespace Docs.Domain.Repositories;

using Entities;


public interface IDocumentRepository
{
  Task<Document?> AddAsync(Document document);
  Task<Document?> GetByIdAsync(Guid requestDocumentId, CancellationToken cancellationToken);
}