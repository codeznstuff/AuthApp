using DatabaseAccessors.DataTransferObjects;
using System;

namespace DatabaseAccessors.Interfaces
{
    public interface IUserClaimsAccessor
    {
        UserClaim[] FindAllUserClaims(Guid userId);

        UserClaim SaveUserClaim(UserClaim userClaim);

        void DeleteUserClaim(Guid id);
    }
}