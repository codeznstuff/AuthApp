using System;

namespace API.Messages
{
    public class GetApplicationUsers
    {
        public GetApplicationUsers(string applicationName)
        {
            ApplicationName = applicationName;
        }

        public string ApplicationName { get; }


        public class ApplicationUsersNotFound
        {
            private ApplicationUsersNotFound() { }
            public static ApplicationUsersNotFound Instance { get; } = new ApplicationUsersNotFound();
        }
    }
}