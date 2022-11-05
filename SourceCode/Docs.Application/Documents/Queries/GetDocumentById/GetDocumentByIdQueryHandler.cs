namespace Docs.Application.Documents.Queries.GetDocumentById;

using Abstractions;
using Abstractions.Messaging;

using Domain.Shared;


public class GetDocumentByIdQueryHandler : IQueryHandler<GetDocumentByIdQuery, DocumentResponse>
{
  private IDocumentRepository DocumentRepository { get; }

  public GetDocumentByIdQueryHandler(IDocumentRepository documentRepository)
  {
    DocumentRepository = documentRepository;
  }

  public async Task<Result<DocumentResponse>> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
  {
    var document = await DocumentRepository.GetByIdAsync(request.DocumentId, cancellationToken);

    if (document is null)
    {
      return new Result<DocumentResponse>(false, $"Document with {request.DocumentId} not found.", null);
    }

    var response = new Result<DocumentResponse>(true, null, new DocumentResponse(document.DocumentId, document));

    return response;
  }
}
