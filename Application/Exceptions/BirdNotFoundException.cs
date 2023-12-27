using System;

namespace Application.Exceptions
{
    public class BirdNotFoundException : Exception
    {
        public BirdNotFoundException(Guid birdId)
            : base($"Bird not found with ID: {birdId}")
        {
        }
    }
}