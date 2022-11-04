namespace Docs.Domain.Shared;

public class Result 
{
  public bool IsSuccess { get; }
  public string ErrorMessage { get; }

  public Result(bool isSuccess, string errorMessage)
  {
    IsSuccess = isSuccess;
    ErrorMessage = errorMessage;
  }
}

public class Result<TData> : Result
{
  public TData Data { get; }

  public Result(bool isSuccess, string errorMessage, TData data) : base(isSuccess, errorMessage)
  {
    Data = data;
  }
}
