namespace RwsConsoleApp;

public class Document
{
  // Issue CS8618: Non-nullable variable must contain a non-null value when exiting constructor
  //public string Title { get; set; }
  public string? Title { get; set; }
  
  //public string Text { get; set; }
  public string? Text { get; set; }
}