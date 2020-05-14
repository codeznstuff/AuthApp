using System;

namespace ActiveDirectoryAccessors.DataTransferObjects
{
    public class UserInformation
    {
        public string[] BusinessPhones { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }
        public string DisplayName { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public Guid Id { get; set; }
        public string JobTitle { get; set; }
        public string EmailAddress { get; set; }
        public string OfficeLocation { get; set; }
        public string LastName { get; set; }
    }
}