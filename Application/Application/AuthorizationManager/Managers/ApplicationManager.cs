using ActiveDirectoryAccessors.Interfaces;
using AuthorizationManager.Interfaces;
using Contracts;
using DatabaseAccessors.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationManager.Managers
{
    internal class ApplicationManager : IApplicationManager
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("AuthorizationManager");

        public User GetUser(Guid userId)
        {
            try
            {
                var user = new User();
                user.UserId = userId;

                var activeDirectoryAccessorFactory = new ActiveDirectoryAccessors.AccessorFactory();
                IGraphAccessor graphAccessor = activeDirectoryAccessorFactory.CreateAccessor<IGraphAccessor>();

                var userInformation = graphAccessor.GetUserById(userId.ToString());

                user.DisplayName = userInformation.DisplayName;
                user.EmailAddress = userInformation.EmailAddress;
                user.FirstName = userInformation.FirstName;
                user.LastName = userInformation.LastName;

                //Enigne that takes a user and spits out claims
                var databaseAccessorFactory = new DatabaseAccessors.AccessorFactory();
                IApplicationsAccessor applicationAccessor = databaseAccessorFactory.CreateAccessor<IApplicationsAccessor>();
                IClaimsAccessor claimsAccessor = databaseAccessorFactory.CreateAccessor<IClaimsAccessor>();
                IUserClaimsAccessor userClaimsAccessor = databaseAccessorFactory.CreateAccessor<IUserClaimsAccessor>();
                IMembershipsAccessor membershipsAccessor = databaseAccessorFactory.CreateAccessor<IMembershipsAccessor>();

                var userClaims = userClaimsAccessor.FindAllUserClaims(user.UserId);
                var userApplications = membershipsAccessor.FindAllUserApplications(user.UserId);

                var applicationClaims = new List<ApplicationClaims>();
                foreach (var userApplication in userApplications)
                {
                    var application = applicationAccessor.Find(userApplication.ApplicationId);
                    var applicationClaim = new ApplicationClaims
                    {
                        ApplicationName = application.Name,
                        Claims = new ClaimData[0]
                    };
                    applicationClaims.Add(applicationClaim);
                }

                foreach (var userClaim in userClaims)
                {
                    var userClaimData = claimsAccessor.Find(userClaim.ClaimId);
                    var application = applicationAccessor.Find(userClaimData.ApplicationId);
                    var applicationClaim = applicationClaims.Where(app => app.ApplicationName == application.Name).FirstOrDefault();
                    var claimData = new ClaimData
                    {
                        ClaimType = userClaimData.ClaimType,
                        ClaimValue = userClaimData.ClaimValue
                    };
                    var claimDataList = applicationClaim.Claims.ToList();
                    claimDataList.Add(claimData);
                    applicationClaim.Claims = claimDataList.ToArray();
                }

                user.Applications = applicationClaims.ToArray();

                return user;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetUser", ex);
                return null;
            }
        }

        public ApplicationUser GetApplicationUser(string applicationName, Guid userId)
        {
            try
            {
                var databaseAccessorFactory = new DatabaseAccessors.AccessorFactory();
                IApplicationsAccessor applicationAccessor = databaseAccessorFactory.CreateAccessor<IApplicationsAccessor>();

                var applicationUser = applicationAccessor.FindAllUsersForApplication(applicationName).Where(appUser => appUser.UserId == userId);
                var user = GetUser(userId);
                var mappedUser = new ApplicationUser
                {
                    DisplayName = user.DisplayName,
                    EmailAddress = user.EmailAddress,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserId = user.UserId,
                    Claims = user.Applications.Where(app => app.ApplicationName == applicationName).Select(app => app.Claims).FirstOrDefault()
                };

                return mappedUser;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetApplicationUsers", ex);
                return null;
            }
        }

        public ApplicationUser[] GetApplicationUsers(string applicationName)
        {
            try
            {
                var users = new List<ApplicationUser>();

                var databaseAccessorFactory = new DatabaseAccessors.AccessorFactory();
                IApplicationsAccessor applicationAccessor = databaseAccessorFactory.CreateAccessor<IApplicationsAccessor>();

                var applicationUsers = applicationAccessor.FindAllUsersForApplication(applicationName);

                foreach (var applicationUser in applicationUsers)
                {
                    var user = GetUser(applicationUser.UserId);
                    var mappedUser = new ApplicationUser
                    {
                        DisplayName = user.DisplayName,
                        EmailAddress = user.EmailAddress,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserId = user.UserId,
                        Claims = user.Applications.Where(app => app.ApplicationName == applicationName).Select(app => app.Claims).FirstOrDefault()
                    };
                    users.Add(mappedUser);
                }

                return users.ToArray();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetApplicationUsers", ex);
                return null;
            }
        }

        public void RemoveApplication(Guid applicationId)
        {
            try
            {
                var databaseAccessorFactory = new DatabaseAccessors.AccessorFactory();
                IApplicationsAccessor applicationAccessor = databaseAccessorFactory.CreateAccessor<IApplicationsAccessor>();

                applicationAccessor.DeleteApplication(applicationId);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in DeleteApplication", ex);
            }
        }

        public Application AddApplication(string applicationName)
        {
            try
            {
                var application = new DatabaseAccessors.DataTransferObjects.Application
                {
                    Id = Guid.NewGuid(),
                    Name = applicationName
                };

                var databaseAccessorFactory = new DatabaseAccessors.AccessorFactory();
                IApplicationsAccessor applicationAccessor = databaseAccessorFactory.CreateAccessor<IApplicationsAccessor>();

                var result = applicationAccessor.SaveApplication(application);

                var mappedApplication = new Application
                {
                    ApplicationName = result.Name
                };

                return mappedApplication;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in SaveApplication", ex);
                return null;
            }
        }

        public void AddApplicationClaims(Guid applicationId, ClaimData claim)
        {
            throw new NotImplementedException();
        }

        public void RemoveApplicationClaims(Guid applicationId, Guid claimId)
        {
            throw new NotImplementedException();
        }

        public void AddUserToApplication(Guid applicationId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserFromApplication(Guid applicationId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void AddApplicationClaimForUser(Guid applicationId, Guid userId, ClaimData claim)
        {
            throw new NotImplementedException();
        }

        public void RemoveApplicationClaimForUser(Guid applicationId, Guid userId, Guid claimId)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}