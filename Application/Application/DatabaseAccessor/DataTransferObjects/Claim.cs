using System;

namespace DatabaseAccessors.DataTransferObjects
{
    public class Claim
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}