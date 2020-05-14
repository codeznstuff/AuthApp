namespace API.Messages
{
    public class AddApplication
    {
        public AddApplication(string applicationName)
        {
            ApplicationName = applicationName;
        }

        public string ApplicationName { get; }

        public class ApplicationNotFound
        {
            private ApplicationNotFound()
            {
            }

            public static ApplicationNotFound Instance { get; } = new ApplicationNotFound();
        }
    }
}