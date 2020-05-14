using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccessors.EntityFramework
{
    internal class Claims
    {
        public Claims()
        {
            UserClaims = new HashSet<UserClaims>();
        }

        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }

        [StringLength(256)]
        public string ClaimType { get; set; }

        [StringLength(256)]
        public string ClaimValue { get; set; }

        public virtual Applications Application { get; set; }
        public virtual ICollection<UserClaims> UserClaims { get; set; }
    }
}