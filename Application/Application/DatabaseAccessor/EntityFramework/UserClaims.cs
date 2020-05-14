using System;

namespace DatabaseAccessors.EntityFramework
{
    internal class UserClaims
    {
        public Guid Id { get; set; }
        public Guid ClaimId { get; set; }
        public Guid UserId { get; set; }

        public virtual Claims Claim { get; set; }
    }
}