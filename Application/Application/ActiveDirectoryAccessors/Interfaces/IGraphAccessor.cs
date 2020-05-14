using ActiveDirectoryAccessors.DataTransferObjects;

namespace ActiveDirectoryAccessors.Interfaces
{
    public interface IGraphAccessor
    {
        UserInformation GetUserByEmail(string email);

        UserInformation GetUserById(string id);

        UserInformation GetUserByDisplayName(string displayName);
    }
}