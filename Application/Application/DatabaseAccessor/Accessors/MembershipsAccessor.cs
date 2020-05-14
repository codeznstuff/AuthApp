using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseAccessors.Accessors
{
    internal class MembershipsAccessor : Interfaces.IMembershipsAccessor
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("MembershipsAccessor");

        public DataTransferObjects.Membership[] FindAllUserApplications(Guid userId)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var memberships = db.Memberships.ToList();
                    var result = new List<DataTransferObjects.Membership>();
                    foreach (var member in memberships)
                    {
                        if (member.UserId == userId)
                        {
                            var membership = new DataTransferObjects.Membership
                            {
                                Id = member.Id,
                                ApplicationId = member.ApplicationId,
                                UserId = member.UserId
                            };
                            result.Add(membership);
                        }
                    }
                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in FindAllUserApplications", ex);
                return null;
            }
        }

        public void DeleteMembership(Guid id)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var memberships = new EntityFramework.Memberships
                    {
                        Id = id,
                    };
                    db.Memberships.Remove(memberships);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in DeleteMembership", ex);
            }
        }

        public DataTransferObjects.Membership SaveMembership(DataTransferObjects.Membership membership)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var memberships = new EntityFramework.Memberships
                    {
                        Id = membership.Id,
                        ApplicationId = membership.ApplicationId,
                        UserId = membership.UserId
                    };
                    db.Memberships.Add(memberships);
                    db.SaveChanges();
                    var result = new DataTransferObjects.Membership
                    {
                        Id = memberships.Id,
                        ApplicationId = memberships.ApplicationId,
                        UserId = memberships.UserId
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in SaveMembership", ex);
                return null;
            }
        }
    }
}