using System;

namespace API.Messages
{
    public class GetUser
    {
        public GetUser(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }


        public class UserNotFound
        {
            private UserNotFound() { }
            public static UserNotFound Instance { get; } = new UserNotFound();
        }
    }
}