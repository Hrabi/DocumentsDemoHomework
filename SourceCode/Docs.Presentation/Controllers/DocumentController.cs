using Microsoft.AspNetCore.Mvc;

namespace Docs.Presentation.Controllers;

using Abstractions;
using Application.Documents.Commands;
using Application.Documents.Queries.GetDocumentById;
using MediatR;
using Microsoft.Extensions.Logging;
using QueryEntities;


public sealed class DocumentController : ApiController
{
  private ILogger<DocumentController> Logger { get; }

  public DocumentController(ISender sender, IPublisher publisher, ILogger<DocumentController> logger) : base(sender, publisher)
  {
    Logger = logger;
  }

  [HttpGet]
  public async Task<IActionResult> GetDocumentById([FromQuery] GetDocumentRequest documentRequest, CancellationToken cancellationToken)
  {
    if (documentRequest.Id == default(Guid))
    {
      return BadRequest("Document request parameter ID cannot be empty.");
    }

    var query = new GetDocumentByIdQuery(documentRequest.Id);
    var response = await Sender.Send(query, cancellationToken);

    return response.IsSuccess ? Ok(response) : BadRequest(response.ErrorMessage);
  }

  [HttpPost]
  public async Task<IActionResult> RegisterDocument([FromBody] RegisterDocumentRequest registerDocumentRequest, CancellationToken cancellationToken)
  {
    if (string.IsNullOrWhiteSpace(registerDocumentRequest.Title) || string.IsNullOrWhiteSpace(registerDocumentRequest.Text))
    {
      return BadRequest("Document request parameters cannot be empty.");
    }

    var docCommand = new CreateDocumentCommand(registerDocumentRequest.Title, registerDocumentRequest.Text);
    var result = await Sender.Send(docCommand, cancellationToken);

    return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
  }
}
