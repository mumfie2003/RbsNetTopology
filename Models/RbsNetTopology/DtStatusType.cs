using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RbsNetTopology.Models.rbs_net_topology
{
    [Table("DtStatusTypes", Schema = "dbo")]
    public partial class DtStatusType
    {
        [Key]
        [Required]
        public string Code { get; set; }

        public string Title { get; set; }

        public ICollection<Issue> Issues { get; set; }

    }
}