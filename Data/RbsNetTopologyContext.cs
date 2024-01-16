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
              .HasOne(i => i.DtReportRecipient)
              .WithMany(i => i.Issues)
              .HasForeignKey(i => i.AssignedReportToRecipientCode)
              .HasPrincipalKey(i => i.Code);

            builder.Entity<RbsNetTopology.Models.rbs_net_topology.Issue>()
              .HasOne(i => i.DtStatusType)
              .WithMany(i => i.Issues)
              .HasForeignKey(i => i.AssignedStatusCode)
              .HasPrincipalKey(i => i.Code);

            builder.Entity<RbsNetTopology.Models.rbs_net_topology.Issue>()
              .Property(p => p.Subject)
              .HasDefaultValueSql(@"(N'')");

            builder.Entity<RbsNetTopology.Models.rbs_net_topology.Issue>()
              .Property(p => p.CreateDateTime)
              .HasDefaultValueSql(@"('0001-01-01T00:00:00.0000000')");

            builder.Entity<RbsNetTopology.Models.rbs_net_topology.Issue>()
              .Property(p => p.CreateDateTime)
              .HasColumnType("datetime2");

            builder.Entity<RbsNetTopology.Models.rbs_net_topology.Issue>()
              .Property(p => p.CompletedDateTime)
              .HasColumnType("datetime2");

            builder.Entity<RbsNetTopology.Models.rbs_net_topology.Issue>()
              .Property(p => p.ReportToCreateDateTime)
              .HasColumnType("datetime2");
            this.OnModelBuilding(builder);
        }

        public DbSet<RbsNetTopology.Models.rbs_net_topology.DtReportRecipient> DtReportRecipients { get; set; }

        public DbSet<RbsNetTopology.Models.rbs_net_topology.DtStatusType> DtStatusTypes { get; set; }

        public DbSet<RbsNetTopology.Models.rbs_net_topology.Issue> Issues { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    
    }
}