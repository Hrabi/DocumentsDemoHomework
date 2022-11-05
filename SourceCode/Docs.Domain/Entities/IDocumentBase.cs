namespace Docs.Domain.Entities;

public interface IDocumentBase : ICloneable
{
  string Text { get; set; }
  ContentFormat ContentFormat { get; set; }
}