namespace Docs.Domain.Repositories;

using Entities;


public class DocumentRepository : IDocumentRepository
{
  public async Task<Document?> AddAsync(Document document, CancellationToken cancellationToken)
  {
    document.DocumentId = Guid.NewGuid();
    
    // TODO: Store document 
    await Task.Delay(TimeSpan.FromSeconds(3));
    
    return document;
  }

  public async Task<bool> IsDocumentUniqueAsync(Document document, CancellationToken cancellationToken)
  {
    // var doc = await GetByIdAsync(document.DocumentId, cancellationToken);
    var result = new Random().NextDouble() >= 0.5;

    await Task.Delay(TimeSpan.FromSeconds(3));

    return result;
  }

  public async Task<Document?> GetByIdAsync(Guid requestDocumentId, CancellationToken cancellationToken)
  {
    await Task.Delay(TimeSpan.FromSeconds(3));

    return null;
  }
}