namespace Docs.Domain.Repositories;

using Entities;


public class DocumentRepository : IDocumentRepository
{
  public Task<Document> AddAsync(Document document)
  {
    document.DocumentId = Guid.NewGuid();

    Task.Delay(TimeSpan.FromSeconds(10));
    
    return Task.FromResult(document);
  }
}