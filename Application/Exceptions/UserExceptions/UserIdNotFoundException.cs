using System;

namespace Application.Exceptions
{
    public class UserIdNotFoundException : Exception
    {
        public UserIdNotFoundException(Guid userId)
            : base($"User not found with username: {userId}")
        {
        }
    }
}