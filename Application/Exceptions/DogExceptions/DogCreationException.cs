using System;
public class DogCreationException : Exception
{
    public DogCreationException(string message, Exception ex) : base(message)
    {
        Console.Write("Dog failed to create");
    }
}