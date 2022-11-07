namespace Docs.Presentation.QueryEntities;

public class ConvertDocumentRequest
{
  public Guid DocumentId { get; set; } = Guid.NewGuid();
  public string Title { get; set; } = "TitleTestJsonToXml";
  public string Text { get; set; } = "TextTestJsonToXml";
}
