using System.Diagnostics.CodeAnalysis;

namespace HousExpenses.Domain.Enums;

[ExcludeFromCodeCoverage]
public static class MessageType
{
    public const string Info = "Info";
    public const string Error = "Error";
    public const string Warning = "Warning";
}
