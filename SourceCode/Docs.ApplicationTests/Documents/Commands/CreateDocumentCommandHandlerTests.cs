namespace Docs.ApplicationTests.Documents.Commands;

using Application.Abstractions;
using Application.Documents.Commands;
using Domain.Entities;


[TestClass()]
public class CreateDocumentCommandHandlerTests
{
  private Mock<IDocumentRepository> DocumentRepositoryMock { get; }

  public CreateDocumentCommandHandlerTests()
  {
    DocumentRepositoryMock = new();
  }

  [TestMethod()]
  public async Task Handle_Should_ReturnFailureResult_WhenDocumentIsNotUnique()
  {
    // Arrange
    var command = new CreateDocumentCommand("TitleTest", "TextTest");
    var handler = new CreateDocumentCommandHandler(DocumentRepositoryMock.Object);
    DocumentRepositoryMock.Setup(repo => repo.IsDocumentUniqueAsync(It.IsAny<Document>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(false);

    // Act
    var result = await handler.Handle(command, default);

    // Assert
    Assert.IsFalse(result.IsSuccess);
    Assert.IsTrue(!string.IsNullOrEmpty(result.ErrorMessage));
    Assert.AreEqual(result.Data, Guid.Empty);
  }

  [TestMethod()]
  public async Task Handle_Should_ReturnFailureResult_WhenDocumentIsUnique()
  {
    // Arrange
    var command = new CreateDocumentCommand("TitleTest", "TextTest");
    var handler = new CreateDocumentCommandHandler(DocumentRepositoryMock.Object);
    DocumentRepositoryMock.Setup(repo => repo.IsDocumentUniqueAsync(It.IsAny<Document>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(true);

    // Act
    var result = await handler.Handle(command, default);

    // Assert
    Assert.IsFalse(result.IsSuccess);
    Assert.IsTrue(string.IsNullOrEmpty(result.ErrorMessage));
    Assert.AreEqual(result.Data, Guid.Empty);
  }

  [TestMethod()]
  public async Task Handle_Should_CallAddOnRepository_WhenDocumentIsUnique()
  {
    // Arrange
    var command = new CreateDocumentCommand("TitleTest", "TextTest");
    var handler = new CreateDocumentCommandHandler(DocumentRepositoryMock.Object);
    DocumentRepositoryMock.Setup(repo => repo.IsDocumentUniqueAsync(It.IsAny<Document>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(true);

    // Act
    var result = await handler.Handle(command, default);

    // Assert
    DocumentRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Document>(doc => doc.DocumentId == result.Data), It.IsAny<CancellationToken>()),
      Times.Once);
  }

  [TestMethod()]
  public async Task Handle_Should_NotCallAddOnRepository_WhenDocumentIsNotUnique()
  {
    // Arrange
    var command = new CreateDocumentCommand("TitleTest", "TextTest");
    var handler = new CreateDocumentCommandHandler(DocumentRepositoryMock.Object);
    DocumentRepositoryMock.Setup(repo => repo.IsDocumentUniqueAsync(It.IsAny<Document>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(false);

    // Act
    var result = await handler.Handle(command, default);

    // Assert
    DocumentRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Document>(), It.IsAny<CancellationToken>()), Times.Never);
  }

}
