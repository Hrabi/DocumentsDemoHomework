namespace Docs.Application.Documents.Queries.ConvertDocumentById;

using Abstractions;
using Domain.Shared;
using Abstractions.Messaging;
using Domain.Converters;
using Domain.Entities;


internal class ConvertDocumentByIdQueryHandler : IQueryHandler<ConvertDocumentByIdQuery, DocumentResponse>
{
  private IDocumentRepository DocumentRepository { get; }
  private IDocumentConverter DocumentConverter { get; }

  public ConvertDocumentByIdQueryHandler(IDocumentRepository documentRepository, IDocumentConverter documentConverter)
  {
    DocumentRepository = documentRepository;
    DocumentConverter = documentConverter;
  }


  public async Task<Result<DocumentResponse>> Handle(ConvertDocumentByIdQuery request, CancellationToken cancellationToken)
  {
    var documentId = request.DocumentId;
    var document = await DocumentRepository.GetByIdAsync(documentId, cancellationToken);

    try
    {

      if (document is null)
      {
        return new Result<DocumentResponse>(false, $"Document with {request.DocumentId} not found.", null);
      }

      // convert json to xml document text
      document.Text = request.Text;
      var result = DocumentConverter.JsonConverter(document, ContentFormat.Xml);

      return new Result<DocumentResponse>(true, null, new DocumentResponse(result.DocumentId, result));
    }
    catch (NotSupportedException ex)
    {
      return new Result<DocumentResponse>(false, ex.Message, null);
    }
    catch (Exception ex)
    {
      // TODO: global exception
      throw new NotImplementedException("Not Implemented!", ex);
    }
  }
}
