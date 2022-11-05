namespace Docs.Application.Documents.Commands;

using Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;


internal sealed class CreateDocumentCommandHandler : ICommandHandler<CreateDocumentCommand, Guid>
{
  private IDocumentRepository DocumentRepository { get; } 
  //private IUnitOfWork UnitOfWork { get; }

  public CreateDocumentCommandHandler(IDocumentRepository documentRepository)
  {
    DocumentRepository = documentRepository;
  }

  public async Task<Result<Guid>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
  {
    var document = new Document { Title = request.Title, Text = request.Text };

    if (!await DocumentRepository.IsDocumentUniqueAsync(document, cancellationToken))
    {
      return new Result<Guid>(false, $"Document {document.DocumentId} already exists.", Guid.Empty);
    }

    var docResult = await DocumentRepository.AddAsync(document, cancellationToken);
    
    // TODO: Implement UnityOfWork
    // await UnityOfWork.SaveChangesAsync(cancellationToken);

    return new Result<Guid>(docResult != null, string.Empty, docResult?.DocumentId ?? Guid.Empty);
  }
}