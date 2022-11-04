using Microsoft.AspNetCore.Mvc;

namespace Docs.Presentation.Controllers;

using Abstractions;
using Application.Documents.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;




[Route("[controller]")]
public sealed class DocumentController : ApiController
{
  private ILogger<DocumentController> Logger { get; }

  public DocumentController(ISender sender, IPublisher publisher, ILogger<DocumentController> logger) : base(sender, publisher)
  {
    Logger = logger;
  }

  //[HttpGet(Name = "GetDocuments")]
  //public IEnumerable<Document> Get()
  //{
  //}

  [HttpPost]
  public async Task<IActionResult> RegisterDocument(CancellationToken cancellationToken)
  {
    var docCommand = new CreateDocumentCommand("Title", "Text");
    var result = await Sender.Send(docCommand, cancellationToken);

    return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
  }


}
