using System;
using Application.Common.Interfaces.DbContexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class TenantDbContext : DbContext, ITenantDBContext
    {

        public TenantDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }

        //Used in muli tenant situations
        //Use middleware to intercept and set
        public void SetConnectionString(string connectionString)
        {
            this.Database.GetDbConnection().ConnectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Move to configurations- seperate from context
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account", "Account");
                entity.HasKey(x => x.AccountId);
                entity.Property(x => x.AccountId).ValueGeneratedNever();
                entity.Property(x => x.Name).HasMaxLength(200);
                entity.Property(x => x.AccountNumber).IsRequired(true);
            });
        }
    }
}
