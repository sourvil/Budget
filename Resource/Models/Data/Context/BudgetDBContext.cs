using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using Resource.Models.Data.Models;

namespace Resource.Models.Data.Context
{
    public class BudgetDBContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //base.OnModelCreating(modelBuilder);

        }
        public BudgetDBContext()
        {
            Database.Connection.ConnectionString = "server=.;database=Budget;Trusted_Connection=TRUE;";
            this.Configuration.LazyLoadingEnabled = true;
        }

        public virtual ObjectResult<string> Decrypt(byte[] encryptedData)
        {
            var paramEncryptedData = encryptedData != null ?
                            new SqlParameter("encryptedData", encryptedData) :
                            new SqlParameter("encryptedData", typeof(byte[]));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<string>("decrypt @encryptedData", paramEncryptedData);
        }

        public virtual ObjectResult<byte[]> Encrypt(int encryptedData)
        {

            var paramEncryptedData = new SqlParameter("encryptedData", encryptedData);

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<byte[]>("encrypt @encryptedData", paramEncryptedData);
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Item> Item { get; set; }
    }
}