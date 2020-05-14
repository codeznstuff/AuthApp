using System;

namespace Contracts
{
    public class User
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get; set; }
        public ApplicationClaims[] Applications { get; set; }
    }
}