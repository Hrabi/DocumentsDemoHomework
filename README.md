# RWS Homework
## Instructions
Attached, please find a console application which main purpose is to convert the formats.

Prepare:

1. Please find at least 5 potential code issues an be able to explain the reason behind it.
2. Refactor the app to allow:
* Work with documents of various storages eg. filesystem, cloud storage or HTTP (HTML read-only) etc. Implement just one of them but
be sure that implementation is versatile for adding other storages.
* Be capable of reading/writing different formats. Implement XML and JSON format, but be sure that implementation is versatile for adding
more formats (YAML, BSON, etc.).
* Build the app in the way to be able to test classes in isolation
* Be able to add new formats and storages in the future so it will have none or minimal impact on the existing code
* Be able to use any combination of input/output storages and formats (eg. read JSON from filesystem, convert to XML and upload to
cloud storage)

We’re going to appraise the design of given code that should match the quality of production application. Thus imagine this application as a system
ready for feature development (adding new storages or formats).

Tests should be written as demonstration of your skills, there is no need to cover everything.

Please version progress as usual, upload your homework to GitHub or other preferred git storage and send us link. Alternatively, use git archive and send
us a ZIP archive.

## Source file

   ```csharp
   using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
namespace Moravia.Homework
{
public class Document
{
public string Title { get; set; }
public string Text { get; set; }
}
class Program
{
static void Main(string[] args)
{
var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");
try
{
FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
var reader = new StreamReader(sourceStream);
string input = reader.ReadToEnd();
}
catch (Exception ex)
{
throw new Exception(ex.Message);
}
var xdoc = XDocument.Parse(input);
var doc = new Document
{
Title = xdoc.Root.Element("title").Value,
Text = xdoc.Root.Element("text").Value
};
var serializedDoc = JsonConvert.SerializeObject(doc);
var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
var sw = new StreamWriter(targetStream);
sw.Write(serializedDoc);
}
}
}
   ```
## Solution
### Clean architecture
https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures
![image](https://user-images.githubusercontent.com/37542708/200276861-91a3cb4a-3461-4658-b102-c8242856f963.png)

### CQRS pattern
https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/apply-simplified-microservice-cqrs-ddd-patterns
![image](https://user-images.githubusercontent.com/37542708/200277488-3ee525b5-3cde-49b5-ab79-c5e0843fdf36.png)

### MediatR in ASP.NET or ASP.NET Core
https://medium.com/dotnet-hub/use-mediatr-in-asp-net-or-asp-net-core-cqrs-and-mediator-in-dotnet-how-to-use-mediatr-cqrs-aspnetcore-5076e2f2880c
![image](https://user-images.githubusercontent.com/37542708/200278489-096e325d-3aca-41a3-ad0e-4a05c0c8ef29.png)

### Scrutor 
https://github.com/khellang/Scrutor

## Data Processing

### Cinchoo ETL
Simple, intutive Extract, transform and load (ETL) library for .NET. Extremely fast, flexible, and easy to use.
https://github.com/Cinchoo/ChoETL

## Testing

### Moq
https://github.com/moq/moq4

### ArchUnit - enforce architecture rules
https://www.ben-morris.com/writing-archunit-style-tests-for-net-and-c-for-self-testing-architectures/

# ChatGPT

```csharp
var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source
Files\\Document1.xml");
var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target
Files\\Document1.json");
try
{
FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
var reader = new StreamReader(sourceStream);
string input = reader.ReadToEnd();
}
catch (Exception ex)
{
throw new Exception(ex.Message);
}
var xdoc = XDocument.Parse(input);
var doc = new Document
{
Title = xdoc.Root.Element("title").Value,
Text = xdoc.Root.Element("text").Value
};
var serializedDoc = JsonConvert.SerializeObject(doc);
var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
var sw = new StreamWriter(targetStream);
sw.Write(serializedDoc);
```
ChatBot:
"This code appears to be converting an XML file into a JSON file. It first combines the source and target file paths and opens the source file as a FileStream. It then reads the entire file into a string called input, which is used to create an XDocument object called xdoc. The code then extracts the title and text elements from the xdoc object and uses them to create a new Document object. Finally, it uses the JsonConvert class to serialize the Document object into a JSON string, which it writes to the target file."

### OpenAI Update
There are a few ways to optimize this code. One way is to use a using statement for the sourceStream and targetStream objects to ensure that the streams are properly disposed of after they are no longer needed. This can help free up resources more efficiently.

Another way to optimize the code is to use a JSON serializer that supports writing directly to a file stream, rather than serializing the object to a string and then writing the string to a file. This can reduce memory usage and improve performance, especially for large files.

Additionally, you could consider using asynchronous programming techniques to read and write the files in a non-blocking manner. This can improve the overall performance of the code, especially if it is running on a multi-threaded system.

Here is an example of how the code could be optimized:
```csharp
var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");

// Use a using statement for the source and target streams to ensure that they are disposed of properly
using (FileStream sourceStream = File.Open(sourceFileName, FileMode.Open))
using (var reader = new StreamReader(sourceStream))
{
    string input = await reader.ReadToEndAsync();

    var xdoc = XDocument.Parse(input);
    var doc = new Document
    {
        Title = xdoc.Root.Element("title").Value,
        Text = xdoc.Root.Element("text").Value
    };

    // Use a JSON serializer that supports writing directly to a file stream
    var serializer = new JsonSerializer();
    using (var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write))
    using (var sw = new StreamWriter(targetStream))
    using (var writer = new JsonTextWriter(sw))
    {
        serializer.Serialize(writer, doc);
    }
}
```

### Please find at least 5 potential code issues an be able to explain the reason behind it.

The code does not include a check to verify that the source XML file exists before attempting to read it. If the file does not exist, an exception will be thrown when trying to open it.

The code does not handle the case where the title or text elements are not present in the XML file. If either of these elements are missing, a NullReferenceException will be thrown when trying to access their values.

The code uses a StreamReader to read the contents of the XML file, but it does not specify the encoding of the file. If the XML file uses a non-ASCII character set, the characters may not be decoded properly, resulting in corrupted or incorrect data.

The code uses a FileStream to open the target JSON file, but it does not specify the file sharing mode. If the file is already open by another process, the code will fail to open it and will throw an exception.

The code does not include any error handling for the JSON serialization process. If the Document object cannot be serialized for any reason, an exception will be thrown and the code will terminate without writing the JSON data to the target file.

### Refactor source code, Work with documents of various storages eg. filesystem, cloud storage or HTTP (HTML read-only) etc. Implement just one of them but
be sure that implementation is versatile for adding other storages.
Here is an example of how the code could be refactored to support 


To refactor the code to support multiple storage types, we can use an abstract class or interface to define a common set of methods that each storage type must implement. This allows us to use a single method to read and write files from any storage type, without having to write separate code for each type.

To refactor the code to work with documents from various storages, one approach could be to create an abstract DocumentSource class that defines a common interface for reading and writing documents. The DocumentSource class could have methods such as ReadDocumentAsync and WriteDocumentAsync that take a document identifier and return a Task representing the asynchronous operation.

Concrete implementations of DocumentSource could then be created for each type of storage, such as a FileSystemDocumentSource for reading and writing files on the local file system, or a CloudStorageDocumentSource for reading and writing documents from a cloud storage provider.

Here is an example of how the refactored code might look:
```csharp
public abstract class DocumentSource
{
    public abstract Task<Document> ReadDocumentAsync(string documentId);
    public abstract Task WriteDocumentAsync(string documentId, Document document);
}

public class FileSystemDocumentSource : DocumentSource
{
    public override async Task<Document> ReadDocumentAsync(string documentId)
    {
        // Read the document from the file system using the specified documentId
        // ...

        return document;
    }

    public override async Task WriteDocumentAsync(string documentId, Document document)
    {
        // Write the document to the file system using the specified documentId
        // ...
    }
}

public class CloudStorageDocumentSource : DocumentSource
{
    public override async Task<Document> ReadDocumentAsync(string documentId)
    {
        // Read the document from cloud storage using the specified documentId
        // ...

        return document;
    }

    public override async Task WriteDocumentAsync(string documentId, Document document)
    {
        // Write the document to cloud storage using the specified documentId
        // ...
    }
}

public static async Task ConvertXmlToJsonAsync(DocumentSource source, string sourceDocumentId, string targetDocumentId
```

### Be capable of reading/writing different formats. Implement XML and JSON format, but be sure that implementation is versatile for adding
more formats (YAML, BSON, etc.).

To make the code more versatile for reading and writing different file formats, you could create separate classes for each format that you want to support. For example, you could create an XMLReader class and a JSONReader class, each with methods for reading from and writing to a file in their respective formats. These classes could implement a common interface that defines the methods that they must implement, such as a Read() method for reading data from a file and a Write() method for writing data to a file.

You could then use these classes in your code to read and write different file formats in a flexible and extensible way. For example, you could use a Factory design pattern to create instances of the appropriate reader or writer class based on the desired file format, and then use the common interface to read or write the data. This would allow you to easily add support for new file formats in the future by simply creating new classes that implement the interface and adding them to the Factory.

Here is an example of how you could implement the classes mentioned above in .NET:
```csharp
public interface IDocumentReader
{
    Document Read(string filePath);
    void Write(string filePath, Document doc);
}

public class XMLReader : IDocumentReader
{
    public Document Read(string filePath)
    {
        // Read XML file and create Document object
    }

    public void Write(string filePath, Document doc)
    {
        // Write Document object to XML file
    }
}

public class JSONReader : IDocumentReader
{
    public Document Read(string filePath)
    {
        // Read JSON file and create Document object
    }

    public void Write(string filePath, Document doc)
    {
        // Write Document object to JSON file
    }
}

public class DocumentReaderFactory
{
    public static IDocumentReader Create(string format)
    {
        if (format == "xml")
        {
            return new XMLReader();
        }
        else if (format == "json")
        {
            return new JSONReader();
        }
        else
        {
            throw new Exception("Unsupported file format");
        }
    }
}

public class Document
{
    public string Title { get; set; }
    public string Text { get; set; }
}
```

You can then use the DocumentReaderFactory and the IDocumentReader interface to read and write different file formats in a flexible and extensible way:

```csharp
// Read XML file
IDocumentReader reader = DocumentReaderFactory.Create("xml");
Document doc = reader.Read("C:\SourceFiles\Document1.xml");

// Write JSON file
IDocumentReader writer = DocumentReaderFactory.Create("json");
writer.Write("C:\TargetFiles\Document1.json", doc);
```

You can add support for additional file formats by creating new classes that implement the IDocumentReader interface and adding them to the DocumentReaderFactory.


### Build the app in the way to be able to test classes in isolation
To build an application in a way that allows classes to be tested in isolation, you should follow a few key principles:

Write modular, reusable code: Divide your code into small, self-contained classes or functions that can be easily tested and reused in other contexts.

Use dependency injection: Avoid tightly coupling your classes to other classes or external resources. Instead, use dependency injection to pass in the objects and resources that your classes need to function. This will make it easier to test your classes in isolation by allowing you to easily mock or stub dependencies.

Write tests for each class: Create separate unit tests for each class in your application. Each test should verify the behavior of the class under different scenarios and ensure that it is functioning as expected.

Use test-driven development (TDD): Adopt a TDD approach to development, where you write tests before you write the code. This will help you to identify the requirements and design of your classes before you start implementing them, and ensure that your code is well-tested and easy to maintain.

Use a mocking framework: To make it easier to test your classes in isolation, you can use a mocking framework to create mock or stub objects that simulate the behavior of dependencies. This will allow you to test your classes without actually having to use the real dependencies, which can be difficult or impossible to set up in a test environment.

By following these principles, you can build an application that is easy to test and maintain, and that will be more robust and reliable as a result.


