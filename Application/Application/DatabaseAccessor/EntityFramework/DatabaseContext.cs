using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace DatabaseAccessors.EntityFramework
{
    internal class DatabaseContext : DbContext
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("DatabaseContext");

        // Everyone that uses the eCommerceDbContext will use this
        // constructor method
        internal static DatabaseContext Create(bool allowDispose = true)
        {
            try
            {
                return new DatabaseContext()
                {
                    AllowDispose = allowDispose
                };
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in Create", ex);
                return null;
            }
        }

        public DatabaseContext() : base()
        {
        }

        public override void Dispose()
        {
            try
            {
                // this is the secret of the wrapper, without this do nothing we won't handle rolling back transactions
                // only dispose if we are allowing it to dispose
                if (AllowDispose)
                {
                    base.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in Dispose", ex);
            }
        }

        protected IConfigurationRoot Configuration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                base.OnConfiguring(optionsBuilder);

                string connectionString = Shared.Config.GetConfigValue("Sql:ConnectionString");

                if (!string.IsNullOrEmpty(connectionString))
                {
                    if (connectionString.Equals("XUnitTests"))
                    {
                        optionsBuilder.UseInMemoryDatabase("XUnitTests");
                    }
                    else
                    {
                        optionsBuilder.UseSqlServer(connectionString);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Connection string environment variable missing.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in OnConfiguring", ex);
                throw ex;
            }
        }

        public virtual DbSet<Applications> Applications { get; set; }
        public virtual DbSet<Claims> Claims { get; set; }
        public virtual DbSet<Memberships> Memberships { get; set; }
        public virtual DbSet<UserClaims> UserClaims { get; set; }
        public bool AllowDispose { get; set; } = true;
    }
}