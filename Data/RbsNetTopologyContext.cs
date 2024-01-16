using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RbsNetTopology.Models.rbs_net_topology;

namespace RbsNetTopology.Data
{
    public partial class rbs_net_topologyContext : DbContext
    {
        public rbs_net_topologyContext()
        {
        }

        public rbs_net_topologyContext(DbContextOptions<rbs_net_topologyContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RbsNetTopology.Models.rbs_net_topology.Issue>()
              .Property(p => p.Subject)
              .HasDefaultValueSql(@"(N'')");
            this.OnModelBuilding(builder);
        }

        public DbSet<RbsNetTopology.Models.rbs_net_topology.Issue> Issues { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    
    }
}