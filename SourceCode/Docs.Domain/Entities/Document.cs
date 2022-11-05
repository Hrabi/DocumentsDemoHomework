namespace Docs.Domain.Entities;

public class Document : IDocumentBase
{
  public Guid DocumentId { get; set; }
  public string Name { get; set; }
  public string Title { get; set; }
  public string Text { get; set; }
  public string Description { get; set; }
  public string Author { get; set; }
  public dynamic Content { get; set; }
  public ContentFormat ContentFormat { get; set; }

  public object Clone()
  {
    var other = (Document)MemberwiseClone();
    other.DocumentId = Guid.Empty;
    return other;
  }
}