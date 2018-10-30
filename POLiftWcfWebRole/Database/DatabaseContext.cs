using POLiftWcfWebRole.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace POLiftWcfWebRole.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(string nameOrConnectionString)
           : base(nameOrConnectionString)
        {
        }

        public DatabaseContext(DbConnection connection, bool contextOwnsConnection)
           : base(connection, contextOwnsConnection)
        {
        }

        public DbSet<DeviceRegistration> DeviceRegistrations { get; set; }
        public DbSet<RegistrationLookup> RegistrationLookups { get; set; }
    }
}