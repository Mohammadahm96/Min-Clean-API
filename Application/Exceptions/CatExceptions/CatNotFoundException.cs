using System;

namespace Application.Exceptions
{
    public class CatNotFoundException : Exception
    {
        public CatNotFoundException(Guid catId)
            : base($"Cat not found with ID: {catId}")
        {
        }
    }
}
