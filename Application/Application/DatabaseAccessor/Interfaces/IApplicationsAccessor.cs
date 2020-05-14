using DatabaseAccessors.DataTransferObjects;
using System;

namespace DatabaseAccessors.Interfaces
{
    public interface IApplicationsAccessor
    {
        Application Find(Guid id);

        Application SaveApplication(Application application);

        void DeleteApplication(Guid id);

        Application[] FindAllApplications();

        Membership[] FindAllUsersForApplication(string applicationName);
    }
}