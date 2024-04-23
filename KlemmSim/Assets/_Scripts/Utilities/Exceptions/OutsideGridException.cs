using System;

// Source: Gewarren. (2022, 12. August). How to Create User-Defined Exceptions - .NET. Microsoft Learn. https://learn.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions 23.04.2024
public class OutsideGridException : Exception
{
    public OutsideGridException()
    {
    }

    public OutsideGridException(string message)
        : base(message)
    {
    }

    public OutsideGridException(string message, Exception inner)
        : base(message, inner)
    {
    } 
}
