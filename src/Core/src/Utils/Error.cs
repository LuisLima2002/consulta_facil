

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace VozAmiga.Api.Utils;

/// <summary>
///  Aplication erro class
/// </summary>

public readonly struct Error
{
    public readonly string[] Messages { get; init; }
    public Error(string message)
    {
        Messages = [message];
    }
    public Error(string[] message)
    {
        Messages = message;
    }

    public static implicit operator Error(string error) => new([error]);
    public static implicit operator Error(string[] errors) => new(errors);
    public static Error operator +(Error e, Error b)
    {
        string[] list = [..e.Messages, ..b.Messages];
        return new Error(list);
    }

}
