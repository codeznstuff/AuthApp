using System;

namespace API.Messages
{
    public class GetApplicationUser
    {
        public GetApplicationUser(string applicationName, Guid userId)
        {
            ApplicationName = applicationName;
            UserId = userId;
        }

        public string ApplicationName { get; }
        public Guid UserId { get; }


        public class ApplicationUserNotFound
        {
            private ApplicationUserNotFound() { }
            public static ApplicationUserNotFound Instance { get; } = new ApplicationUserNotFound();
        }
    }
}