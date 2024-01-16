using Point = NetTopologySuite.Geometries.Point;
namespace RbsNetTopology.Utils
{
    public class MappingUtils
    {
        public Point CalculateLocation(double? latitude, double? longitude)
        {
            Point location = null;
            if (latitude.HasValue && longitude.HasValue)
            {
                var geometryFactory = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                location = geometryFactory.CreatePoint(new NetTopologySuite.Geometries.Coordinate(longitude.Value, latitude.Value));
            }
            return location;
        }
    }
}
