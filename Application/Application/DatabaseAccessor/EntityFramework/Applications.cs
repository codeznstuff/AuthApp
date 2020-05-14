using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccessors.EntityFramework
{
    internal class Applications
    {
        public Applications()
        {
            Memberships = new HashSet<Memberships>();
        }

        public Guid Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public virtual ICollection<Memberships> Memberships { get; set; }
    }
}