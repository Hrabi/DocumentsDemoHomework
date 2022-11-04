
var sourceFileName = Path.Combine(Environment.CurrentDirectory, "Documents\\SourceFile\\Document1.xml");
//var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
var targetFileName = Path.Combine(Environment.CurrentDirectory, "Documents\\SourceFile\\Document1.json");
//var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");

// Issue: lexical scope of the variable due to try-catch-block
var input = string.Empty;

try
{
  // Issue: FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
  if (!string.IsNullOrEmpty(sourceFileName) && File.Exists(sourceFileName))
  {
    await using var sourceStream = File.Open(sourceFileName, FileMode.Open);
    using var reader = new StreamReader(sourceStream);
    input = await reader.ReadToEndAsync();
  }
}
// catch(System.IO.Exceptions)
catch (Exception ex)
{
  // Issue: 
  // throw: rethrows the original exception and preserves its original stack trace
  // throw ex: throws the original exception but resets the stack trace, destroying all stack trace information until catch block
  // Generic Exception handler -> fine if logged and rethrow or used on Hightest level in UI  
  throw new Exception(ex.Message);
}
// Hint: Possible XML Validation via XSD
//var schemaSet = new XmlSchemaSet();
//schemaSet.Add("", inputXsd);
//var xdoc = XDocument.Load(inputXml);
//xdoc.Validate(schemaSet, (o, args) =>
//{
//  XmlSeverityType type;
//  if (Enum.TryParse("Error", out type))
//  {
//    if (type == XmlSeverityType.Error)
//    {
//      Log.Error(o + Environment.NewLine + args.Message);
//    }
//  }
//});

// Issue: Encoding and Escaping special symbols in XML text file

// Issue: Possible null exception
// Issue: System.ArgumentNullException: 'Value cannot be null'
// Issue: System.Xml.XmlException: 'Unexpected end of file has occurred.
// Issue: Lexical scope of the variable - The name 'input' does not exist in the current context
var xdoc = XDocument.Parse(input) ?? throw new ArgumentNullException("XDocument.Parse(input)");

// Issue: xdoc.Root != null, "xdoc.Root != null");
if (xdoc.Root != null)
{
  // Mapping + see more in class Document.cs
  var doc = new Document
  {
    Title = xdoc.Root.Element("Title")?.Value,
    Text = xdoc.Root.Element("Text")?.Value
  };

  // Hint: Serializes and deserializes objects into and from XML documents
  //var serializer = new XmlSerializer(typeof(Document));
  //// Declare an object variable of the type to be deserialized.
  //await using (var reader = new FileStream(sourceFileName, FileMode.Open))
  //{
  //  // Call the Deserialize method to restore the object's state.
  //  var document = (Document)serializer.Deserialize(reader)!;
  //}
  
  var serializedDoc = JsonConvert.SerializeObject(doc);
  // Issue: Empty outcome json file because stream must be flushed and the object is disposed
  //var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
  //var sw = new StreamWriter(targetStream);
  //sw.Write(serializedDoc); 
  //sw.Flush();
  // or using statement -> flush and the object is disposed
  await using var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
  await using var sw = new StreamWriter(targetStream);
  await sw.WriteAsync(serializedDoc);
}