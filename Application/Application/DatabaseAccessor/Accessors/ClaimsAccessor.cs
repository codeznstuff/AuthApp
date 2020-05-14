using Microsoft.Extensions.Logging;
using System;

namespace DatabaseAccessors.Accessors
{
    internal class ClaimsAccessor : Interfaces.IClaimsAccessor
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("ClaimsAccessor");

        public DataTransferObjects.Claim Find(Guid id)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var result = db.Claims.Find(id);
                    var claim = new DataTransferObjects.Claim
                    {
                        Id = result.Id,
                        ApplicationId = result.ApplicationId,
                        ClaimType = result.ClaimType,
                        ClaimValue = result.ClaimValue
                    };
                    return claim;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in Find", ex);
                return null;
            }
        }

        public void DeleteClaim(Guid id)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var claims = new EntityFramework.Claims
                    {
                        Id = id,
                    };
                    db.Claims.Remove(claims);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in DeleteClaim", ex);
            }
        }

        public DataTransferObjects.Claim SaveClaim(DataTransferObjects.Claim claim)
        {
            try
            {
                using (var db = EntityFramework.DatabaseContext.Create())
                {
                    var claims = new EntityFramework.Claims
                    {
                        Id = claim.Id,
                        ApplicationId = claim.ApplicationId,
                        ClaimType = claim.ClaimType,
                        ClaimValue = claim.ClaimValue
                    };
                    db.Claims.Add(claims);
                    db.SaveChanges();
                    var result = new DataTransferObjects.Claim
                    {
                        Id = claims.Id,
                        ApplicationId = claims.ApplicationId,
                        ClaimType = claims.ClaimType,
                        ClaimValue = claims.ClaimValue
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in SaveClaim", ex);
                return null;
            }
        }
    }
}