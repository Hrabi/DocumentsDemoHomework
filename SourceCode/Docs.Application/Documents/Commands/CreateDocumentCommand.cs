namespace Docs.Application.Documents.Commands;

using Abstractions.Messaging;


public sealed record CreateDocumentCommand(string Title, string Text) : ICommand;