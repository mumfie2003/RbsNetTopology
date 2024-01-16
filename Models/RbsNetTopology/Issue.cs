using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RbsNetTopology.Models.rbs_net_topology
{
    [Table("Issues", Schema = "dbo")]
    public partial class Issue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Subject { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

    }
}