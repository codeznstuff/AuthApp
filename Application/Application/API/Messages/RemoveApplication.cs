using System;

namespace API.Messages
{
    public class RemoveApplication
    {
        public RemoveApplication(Guid applicationId)
        {
            ApplicationId = applicationId;
        }

        public Guid ApplicationId { get; }

        public class ApplicationNotFound
        {
            private ApplicationNotFound()
            {
            }

            public static ApplicationNotFound Instance { get; } = new ApplicationNotFound();
        }
    }
}