namespace Docs.Domain.Repositories;

using Entities;


public interface IDocumentRepository
{
  Task<Document> AddAsync(Document document);
}