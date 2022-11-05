namespace Docs.Presentation.QueryEntities;
public class RegisterDocumentRequest
{
  public string? Title { get; set; }
  public string? Text { get; set; }
}


public class GetDocumentRequest
{
  public Guid Id { get; set; }
}