using System;
public class CatCreationException : Exception
{
    public CatCreationException(string message, Exception ex) : base(message)
    {
        Console.Write("Cat failed to create");
    }
}