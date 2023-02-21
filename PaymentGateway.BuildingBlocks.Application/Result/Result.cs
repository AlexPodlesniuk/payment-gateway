namespace PaymentGateway.BuildingBlocks.Application.Result;

public class Result
    {
        protected Result(bool isSuccess, Error? error)
        {
            if (isSuccess && error is not null)
            {
                throw new InvalidOperationException("A success result can not contain an error message.");
            }

            if (!isSuccess && error is null)
            {
                throw new InvalidOperationException("A failure result must contain an error message.");
            }

            IsSuccess = isSuccess;
            Error = error;
        }
        
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error? Error { get; }
        
        public static Result Ok() => new (true, default);
        public static Result<TValue> Ok<TValue>(TValue? value)
            => new (value, true, default);
        
        public static Result Fail(Error error) => new(false, error);
        public static Result<TValue> Fail<TValue>(Error error)
            => new(default, false, error);
    }

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error? error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue? Value()
    {
        if (IsFailure)
        {
            throw new InvalidOperationException("Can not access the value of a failure result.");
        }

        return _value;
    }
}