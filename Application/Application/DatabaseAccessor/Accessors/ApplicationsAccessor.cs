using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseAccessors.Accessors
{
    internal class ApplicationsAccessor : Interfaces.IApplicationsAccessor
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("ApplicationsAccessor");

        public void DeleteApplication(Guid id)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var applications = new EntityFramework.Applications
                    {
                        Id = id,
                    };
                    db.Applications.Remove(applications);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in DeleteApplication", ex);
            }
        }

        public DataTransferObjects.Application Find(Guid id)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var result = db.Applications.Find(id);
                    var application = new DataTransferObjects.Application
                    {
                        Id = result.Id,
                        Name = result.Name
                    };
                    return application;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in Find", ex);
                return null;
            }
        }

        public DataTransferObjects.Application[] FindAllApplications()
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var applications = db.Applications.ToList();
                    var result = new List<DataTransferObjects.Application>();
                    foreach (var app in applications)
                    {
                        var application = new DataTransferObjects.Application
                        {
                            Id = app.Id,
                            Name = app.Name
                        };
                        result.Add(application);
                    }
                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in FindAllApplications", ex);
                return null;
            }
        }

        public DataTransferObjects.Membership[] FindAllUsersForApplication(string applicationName)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var applications = db.Applications.Where(app => app.Name == applicationName).Include(app => app.Memberships).FirstOrDefault();
                    var memberships = applications.Memberships;
                    var result = new List<DataTransferObjects.Membership>();
                    foreach (var member in memberships)
                    {
                        var membership = new DataTransferObjects.Membership
                        {
                            ApplicationId = member.ApplicationId,
                            Id = member.Id,
                            UserId = member.UserId
                        };
                        result.Add(membership);
                    }
                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in FindAllUsersForApplication", ex);
                return null;
            }
        }

        public DataTransferObjects.Application SaveApplication(DataTransferObjects.Application application)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var applications = new EntityFramework.Applications
                    {
                        Id = application.Id,
                        Name = application.Name
                    };
                    db.Applications.Add(applications);
                    db.SaveChanges();
                    var result = new DataTransferObjects.Application
                    {
                        Id = applications.Id,
                        Name = applications.Name
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in SaveApplication", ex);
                return null;
            }
        }
    }
}