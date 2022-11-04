namespace Docs.Domain.Generators;

using Entities;


public abstract class GeneratorBase<TDoc, TDocResult>
{
  protected Task<TDocResult> ConvertDocumen(TDoc document)
  {
    // TODO: Implement document convertion
    throw new NotImplementedException();
  }
}
