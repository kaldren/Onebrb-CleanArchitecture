using Onebrb.Infrastructure.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Onebrb.Core.Entities;

namespace Onebrb.Infrastructure.Data {
    public class ApplicationDbContext : KeyApiAuthorizationDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity => { entity.ToTable(name: "Users"); });
            modelBuilder.Entity<ApplicationRole>(entity => { entity.ToTable(name: "Roles"); });

            modelBuilder.Entity<ApplicationUserMessage>()
                .HasKey(cs => new { cs.MessageId, cs.ApplicationUserId });
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<ApplicationUserMessage> ApplicationUserMessages { get; set; }
    }
}
