namespace Docs.Domain.Repositories;

using Entities;


public class DocumentRepository : IDocumentRepository
{
  public async Task<Document?> AddAsync(Document document)
  {
    document.DocumentId = Guid.NewGuid();
    
    await Task.Delay(TimeSpan.FromSeconds(10));
    
    return document;
  }

  public async Task<Document?> GetByIdAsync(Guid requestDocumentId, CancellationToken cancellationToken)
  {
    await Task.Delay(TimeSpan.FromSeconds(10));

    return null;
  }
}