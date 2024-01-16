using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Point = NetTopologySuite.Geometries.Point;

namespace RbsNetTopology.Models.rbs_net_topology
{
    public partial class Issue
    {
#if !RADZEN
        public Point Location { get; set; }
#endif
    }
}