namespace Docs.Domain.Converters;

using System.Text;
using ChoETL;
using Entities;

// https://www.codeproject.com/search.aspx?q=Cinchoo+ETL

public class DocumentConverter : IDocumentConverter
{
  private List<ContentFormat> AllowedOutputFormat { get; } = new List<ContentFormat> { ContentFormat.Xml, ContentFormat.Csv, ContentFormat.Yaml };

  public TDocument JsonConverter<TDocument>(TDocument document, ContentFormat newFormat) where TDocument : IDocumentBase
  {
    if (!AllowedOutputFormat.Contains(newFormat)) throw new NotSupportedException($"Format {newFormat} is not supported.");
    if (document.ContentFormat != ContentFormat.Json) throw new NotSupportedException($"Document format {document.ContentFormat} is not supported for conversion.");

    var result = (TDocument)document.Clone();
    result.Text = newFormat switch
    {
      ContentFormat.Xml => JsonToXml(document.Text),
      ContentFormat.Csv => JsonToCsv(document.Text),
      ContentFormat.Yaml => JsonToYaml(document.Text)
    };
    result.ContentFormat = newFormat;

    return result;
  }

  private string JsonToXml(string json)
  {
    var result = new StringBuilder();

    using var jsonReader = ChoJSONReader.LoadText(json);
    using var xmlWriter = new ChoXmlWriter(result).WithRootName("RootName").WithNodeName("NodeName");
    xmlWriter.Write(jsonReader);

    return result.ToString();
  }

  private string JsonToCsv(string json)
  {
    var result = new StringBuilder();

    using var jsonReader = ChoJSONReader.LoadText(json);
    using var xmlWriter = new ChoCSVWriter(result);
    xmlWriter.Write(jsonReader);

    return result.ToString();
  }

  private string JsonToYaml(string json)
  {
    var result = new StringBuilder();

    using var jsonReader = ChoJSONReader.LoadText(json);
    using var xmlWriter = new ChoYamlWriter(result);
    xmlWriter.Write(jsonReader);

    return result.ToString();
  }

}