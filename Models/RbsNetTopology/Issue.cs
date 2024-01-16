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

        public string ReportToCaseRef { get; set; }

        public string What3Words { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [Required]
        public int LocationType { get; set; }

        public string AdditionalInfo { get; set; }

        public string ActionTaken { get; set; }

        [Column(TypeName="datetime2")]
        public DateTime CreateDateTime { get; set; }

        [Column(TypeName="datetime2")]
        public DateTime? CompletedDateTime { get; set; }

        [Required]
        public string AssignedStatusCode { get; set; }

        [Required]
        public int Category { get; set; }

        public bool IsActive { get; set; }

        public string AssignedReportToRecipientCode { get; set; }

        [Column(TypeName="datetime2")]
        public DateTime? ReportToCreateDateTime { get; set; }

        public DtReportRecipient DtReportRecipient { get; set; }

        public DtStatusType DtStatusType { get; set; }

    }
}