using System;

namespace Application.Exceptions
{
    public class DogNotFoundException : Exception
    {
        public DogNotFoundException(Guid dogId)
            : base($"Dog not found with ID: {dogId}")
        {
        }
    }
}

