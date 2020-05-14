using DatabaseAccessors.DataTransferObjects;
using System;

namespace DatabaseAccessors.Interfaces
{
    public interface IMembershipsAccessor
    {
        Membership[] FindAllUserApplications(Guid userId);

        Membership SaveMembership(Membership membership);

        void DeleteMembership(Guid id);
    }
}