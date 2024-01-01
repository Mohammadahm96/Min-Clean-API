using System;
public class BirdCreationException : Exception
{
    public BirdCreationException(string message, Exception ex) : base(message)
    {
        Console.Write("Bird failed to create");
    }
}