namespace Docs.Presentation.Abstractions;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public abstract class ApiController : ControllerBase
{
  protected readonly ISender Sender;
  protected readonly IPublisher Publisher;

  protected ApiController(ISender sender, IPublisher publisher)
  {
    Sender = sender;
    Publisher = publisher;
  }
}
