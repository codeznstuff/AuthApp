using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseAccessors.Accessors
{
    internal class UserClaimsAccessor : Interfaces.IUserClaimsAccessor
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("UserClaimsAccessor");

        public void DeleteUserClaim(Guid id)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var userClaims = new EntityFramework.UserClaims
                    {
                        Id = id,
                    };
                    db.UserClaims.Remove(userClaims);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in DeleteUserClaim", ex);
            }
        }

        public DataTransferObjects.UserClaim[] FindAllUserClaims(Guid userId)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var userClaims = db.UserClaims.ToList();
                    var result = new List<DataTransferObjects.UserClaim>();
                    foreach (var claim in userClaims)
                    {
                        if (claim.UserId == userId)
                        {
                            var userClaim = new DataTransferObjects.UserClaim
                            {
                                Id = claim.Id,
                                ClaimId = claim.ClaimId,
                                UserId = claim.UserId
                            };
                            result.Add(userClaim);
                        }
                    }
                    return result.ToArray();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in FindAllUserClaims", ex);
                return null;
            }
        }

        public DataTransferObjects.UserClaim SaveUserClaim(DataTransferObjects.UserClaim userClaim)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var userClaims = new EntityFramework.UserClaims
                    {
                        Id = userClaim.Id,
                        ClaimId = userClaim.ClaimId,
                        UserId = userClaim.UserId
                    };
                    db.UserClaims.Add(userClaims);
                    db.SaveChanges();
                    var result = new DataTransferObjects.UserClaim
                    {
                        Id = userClaims.Id,
                        ClaimId = userClaims.ClaimId,
                        UserId = userClaims.UserId
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in SaveUserClaim", ex);
                return null;
            }
        }
    }
}