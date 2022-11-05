namespace Docs.DomainTests.Converters;

using Common.Data;


[TestClass()]
public class DocumentConverterTests
{
  // TODO: Implement JsonConverterTest 

  [TestMethod()]
  public void JsonConverterTest()
  {
    // Arrange
    var documentConverter = new DocumentConverter();
    var document = new Document { Text = DocumentGenerator.Json };

    // Act
    var result = documentConverter.JsonConverter(document, ContentFormat.Yaml);

    // Asert

  }
}
