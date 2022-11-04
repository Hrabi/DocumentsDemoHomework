namespace Docs.Application.Documents.Commands;

using Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;


internal sealed class CreateDocumentCommandHandler : ICommandHandler<CreateDocumentCommand>
{
  private IDocumentRepository DocumentRepository { get; } 
  //private IUnitOfWork UnitOfWork { get; }

  public CreateDocumentCommandHandler(IDocumentRepository documentRepository)
  {
    DocumentRepository = documentRepository;
  }

  public async Task<Result> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
  {
    var document = new Document { Title = request.Title, Text = request.Text };
    var docResult = await DocumentRepository.AddAsync(document);
    
    // TODO: Implement UnityOfWork
    // await UnityOfWork.SaveChangesAsync(cancellationToken);

    return new Result(true, string.Empty);
  }
}