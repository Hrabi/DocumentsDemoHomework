namespace Docs.Domain.Converters;

using Entities;


public interface IDocumentConverter
{
  TDocument JsonConverter<TDocument>(TDocument document, ContentFormat newFormat) where TDocument : IDocumentBase;
}