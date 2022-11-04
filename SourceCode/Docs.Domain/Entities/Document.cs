namespace Docs.Domain.Entities;

public class Document
{
  public Guid DocumentId { get; set; }
  public string Name { get; set; }
  public string Title { get; set; }
  public string Text { get; set; }
  public string Description { get; set; }
  public string Author { get; set; }
  public string Content { get; set; }
}
