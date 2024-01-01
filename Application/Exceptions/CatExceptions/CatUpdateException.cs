using System;
public class CatUpdateException : Exception
{
    public CatUpdateException(string message, Exception ex) : base(message)
    {
        Console.Write("Cat failed to update");
    }
}