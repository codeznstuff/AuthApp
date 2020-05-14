using System;

namespace DatabaseAccessors.EntityFramework
{
    internal class Memberships
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }

        public virtual Applications Application { get; set; }
    }
}