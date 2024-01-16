using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RbsNetTopology.Models.rbs_net_topology
{
    [Table("DtReportRecipients", Schema = "dbo")]
    public partial class DtReportRecipient
    {
        [Key]
        [Required]
        public string Code { get; set; }

        public string Name { get; set; }

        public ICollection<Issue> Issues { get; set; }

    }
}