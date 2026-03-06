
namespace Contoso.StorageMachine;

/// <summary>Generic Result type for representing either success or failure.</summary>
public sealed class Result<T, TError>
{
    private readonly T? _value;
    private readonly TError? _error;

    private Result(T value) => (_value, IsOk) = (value, true);
    private Result(TError error) => (_error, IsOk) = (error, false);

    public bool IsOk { get; }
    public bool IsError => !IsOk;

    public static Result<T, TError> Ok(T value) => new(value);
    public static Result<T, TError> Error(TError error) => new(error);

    public TResult Match<TResult>(Func<T, TResult> onOk, Func<TError, TResult> onError)
        => IsOk ? onOk(_value!) : onError(_error!);

    public Result<TNew, TError> Bind<TNew>(Func<T, Result<TNew, TError>> f)
        => IsOk ? f(_value!) : Result<TNew, TError>.Error(_error!);

    public Result<TNew, TError> Map<TNew>(Func<T, TNew> f)
        => IsOk ? Result<TNew, TError>.Ok(f(_value!)) : Result<TNew, TError>.Error(_error!);
}

/// <summary>Represents the successful completion of an operation with no meaningful return value.</summary>
public readonly struct Unit
{
    public static readonly Unit Value = default;
}
