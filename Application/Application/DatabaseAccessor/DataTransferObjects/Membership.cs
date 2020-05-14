using System;

namespace DatabaseAccessors.DataTransferObjects
{
    public class Membership
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
    }
}