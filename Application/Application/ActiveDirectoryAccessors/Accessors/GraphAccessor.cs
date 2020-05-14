using ActiveDirectoryAccessors.DataTransferObjects;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActiveDirectoryAccessors.Accessors
{
    internal class GraphAccessor : Interfaces.IGraphAccessor
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("GraphAccessor");
        //private GraphApi.GraphApiClient graphApiClient = new GraphApi.GraphApiClient();


        // JUST FOR EXAMPLE
        private UserInformation dummyResponse = new UserInformation
        {
            BusinessPhones = null,
            CompanyName = "Fake",
            Department = "Fake",
            DisplayName = "Fake.User",
            EmailAddress = "Fake.User@dummy.com",
            EmployeeId = "0",
            FirstName = "Fake",
            Id = new Guid("be64b27b-59b6-43bb-a824-51c16d328ffb"),
            JobTitle = "Nuisance",
            LastName = "User",
            OfficeLocation = "Home"
        };

        public UserInformation GetUserByEmail(string email)
        {
            try
            {
                //GraphServiceClient graphClient = graphApiClient.GetGraphClient();
                //List<QueryOption> options = new List<QueryOption>
                //{
                //    new QueryOption("$filter", $"mail eq '{email}'"),
                //    new QueryOption("$format", "json")
                //};

                //var graphResult = graphClient.Users.Request(options).Select(e => new
                //{
                //    e.BusinessPhones,
                //    e.CompanyName,
                //    e.Department,
                //    e.DisplayName,
                //    e.EmployeeId,
                //    e.GivenName,
                //    e.Id,
                //    e.JobTitle,
                //    e.Mail,
                //    e.OfficeLocation,
                //    e.Surname
                //}).GetAsync().Result;

                //var result = new UserInformation
                //{
                //    BusinessPhones = graphResult.CurrentPage.FirstOrDefault().BusinessPhones.ToArray(),
                //    CompanyName = graphResult.CurrentPage.FirstOrDefault().CompanyName,
                //    Department = graphResult.CurrentPage.FirstOrDefault().Department,
                //    DisplayName = graphResult.CurrentPage.FirstOrDefault().DisplayName,
                //    EmailAddress = graphResult.CurrentPage.FirstOrDefault().Mail,
                //    EmployeeId = graphResult.CurrentPage.FirstOrDefault().EmployeeId,
                //    FirstName = graphResult.CurrentPage.FirstOrDefault().GivenName,
                //    Id = new Guid(graphResult.CurrentPage.FirstOrDefault().Id),
                //    JobTitle = graphResult.CurrentPage.FirstOrDefault().JobTitle,
                //    LastName = graphResult.CurrentPage.FirstOrDefault().Surname,
                //    OfficeLocation = graphResult.CurrentPage.FirstOrDefault().OfficeLocation
                //};

                //return result;
                return dummyResponse;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetUserByEmail", ex);
                return null;
            }
        }

        public UserInformation GetUserById(string id)
        {
            try
            {
                //GraphServiceClient graphClient = graphApiClient.GetGraphClient();
                //List<QueryOption> options = new List<QueryOption>
                //{
                //    new QueryOption("$filter", $"id eq '{id}'"),
                //    new QueryOption("$format", "json")
                //};

                //var graphResult = graphClient.Users.Request(options).Select(e => new
                //{
                //    e.BusinessPhones,
                //    e.CompanyName,
                //    e.Department,
                //    e.DisplayName,
                //    e.EmployeeId,
                //    e.GivenName,
                //    e.Id,
                //    e.JobTitle,
                //    e.Mail,
                //    e.OfficeLocation,
                //    e.Surname
                //}).GetAsync().Result;

                //var result = new UserInformation
                //{
                //    BusinessPhones = graphResult.CurrentPage.FirstOrDefault().BusinessPhones.ToArray(),
                //    CompanyName = graphResult.CurrentPage.FirstOrDefault().CompanyName,
                //    Department = graphResult.CurrentPage.FirstOrDefault().Department,
                //    DisplayName = graphResult.CurrentPage.FirstOrDefault().DisplayName,
                //    EmailAddress = graphResult.CurrentPage.FirstOrDefault().Mail,
                //    EmployeeId = graphResult.CurrentPage.FirstOrDefault().EmployeeId,
                //    FirstName = graphResult.CurrentPage.FirstOrDefault().GivenName,
                //    Id = new Guid(graphResult.CurrentPage.FirstOrDefault().Id),
                //    JobTitle = graphResult.CurrentPage.FirstOrDefault().JobTitle,
                //    LastName = graphResult.CurrentPage.FirstOrDefault().Surname,
                //    OfficeLocation = graphResult.CurrentPage.FirstOrDefault().OfficeLocation
                //};

                //return result;
                return dummyResponse;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetUserById", ex);
                return null;
            }
        }

        public UserInformation GetUserByDisplayName(string displayName)
        {
            try
            {
                //GraphServiceClient graphClient = graphApiClient.GetGraphClient();
                //List<QueryOption> options = new List<QueryOption>
                //{
                //    new QueryOption("$filter", $"displayName eq '{displayName}'"),
                //    new QueryOption("$format", "json")
                //};

                //var graphResult = graphClient.Users.Request(options).Select(e => new
                //{
                //    e.BusinessPhones,
                //    e.CompanyName,
                //    e.Department,
                //    e.DisplayName,
                //    e.EmployeeId,
                //    e.GivenName,
                //    e.Id,
                //    e.JobTitle,
                //    e.Mail,
                //    e.OfficeLocation,
                //    e.Surname
                //}).GetAsync().Result;

                //var result = new UserInformation
                //{
                //    BusinessPhones = graphResult.CurrentPage.FirstOrDefault().BusinessPhones.ToArray(),
                //    CompanyName = graphResult.CurrentPage.FirstOrDefault().CompanyName,
                //    Department = graphResult.CurrentPage.FirstOrDefault().Department,
                //    DisplayName = graphResult.CurrentPage.FirstOrDefault().DisplayName,
                //    EmailAddress = graphResult.CurrentPage.FirstOrDefault().Mail,
                //    EmployeeId = graphResult.CurrentPage.FirstOrDefault().EmployeeId,
                //    FirstName = graphResult.CurrentPage.FirstOrDefault().GivenName,
                //    Id = new Guid(graphResult.CurrentPage.FirstOrDefault().Id),
                //    JobTitle = graphResult.CurrentPage.FirstOrDefault().JobTitle,
                //    LastName = graphResult.CurrentPage.FirstOrDefault().Surname,
                //    OfficeLocation = graphResult.CurrentPage.FirstOrDefault().OfficeLocation
                //};

                //return result;
                return dummyResponse;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetUserByDisplayName", ex);
                return null;
            }
        }
    }
}