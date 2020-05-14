using System;

namespace DatabaseAccessors.DataTransferObjects
{
    public class UserClaim
    {
        public Guid Id { get; set; }
        public Guid ClaimId { get; set; }
        public Guid UserId { get; set; }
    }
}