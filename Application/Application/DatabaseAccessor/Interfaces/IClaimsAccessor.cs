using DatabaseAccessors.DataTransferObjects;
using System;

namespace DatabaseAccessors.Interfaces
{
    public interface IClaimsAccessor
    {
        Claim Find(Guid id);

        Claim SaveClaim(Claim claim);

        void DeleteClaim(Guid id);
    }
}