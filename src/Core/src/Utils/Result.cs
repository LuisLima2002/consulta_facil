
using System.Diagnostics.Contracts;
using VozAmiga.Api.Utils.Enums;

namespace VozAmiga.Api.Utils;

public class Paginated<TValue>
{
    public int Page { get; set; }
    public int Total { get; set; }
    public int ItemsPerPage { get; set; }
    public IEnumerable<TValue> Values { get; set; }

    public Paginated(IEnumerable<TValue> values)
    {
        Values = values;
    }
}

public class Result<TValue> : Result
{
    public readonly TValue? Value;

    public Result(TValue value) : base()
    {
        Value = value;
    }
    public Result(Exception exception) : base(exception)
    {
        Value = default;
    }
    public Result(Error error) : base(error)
    {
        Value = default;
    }

    [Pure]
    public static implicit operator Result<TValue>(TValue success) => new(success);
    [Pure]
    public static implicit operator Result<TValue>(Error error) => new(error);
    [Pure]
    public static implicit operator Result<TValue>(Exception exception) => new(exception);

    public T Match<T>(
        Func<TValue, T> success,
        Func<Error, T> err,
        Func<Exception, T>? except
    )
    {
        return _status switch
        {
            EResultState.Success => success(Value!),
            EResultState.Error => err(Error!.Value),
            EResultState.Exception => except != null ? except(Exception!) : default!,
            _ => throw new NotImplementedException()
        };
    }
}
public class Result
{
    public readonly Error? Error;
    public readonly Exception? Exception;

    public Result()
    {
        Error = default;
        _status = EResultState.Success;
    }
    public Result(Exception exception)
    {
        Exception = exception;
        _status = EResultState.Exception;
    }
    public Result(Error error)
    {
        Error = error;
        _status = EResultState.Error;
    }

    protected readonly EResultState _status;

    [Pure]
    public bool IsError => _status == EResultState.Error;
    [Pure]
    public bool IsSuccess => _status == EResultState.Success;
    [Pure]
    public bool IsException => _status == EResultState.Exception;
    [Pure]
    public static Result Success => new();
    [Pure]
    public static implicit operator Result(Error error) => new(error);
    [Pure]
    public static implicit operator Result(Exception exception) => new(exception);

    public T Match<T>(
        Func<T> success,
        Func<Error, T> err,
        Func<Exception, T>? except
    )
    {
        return _status switch
        {
            EResultState.Success => success(),
            EResultState.Error => err(Error!.Value),
            EResultState.Exception => except != null ? except(Exception!) : default!,
            _ => throw new NotImplementedException("Result status unknon!")
        };
    }
}
