namespace NGAT.Services.IO.Osm
{
    public class OsmRoadLinksFilterCollection : LinkFiltrerCollection
    {
        public OsmRoadLinksFilterCollection()
        {
            this.Add(attrs =>
            {
                return attrs.ContainsKey("highway")
               && (attrs["highway"].ToLowerInvariant() != "pedestrian"
               && attrs["highway"].ToLowerInvariant() != "footway"
               && attrs["highway"].ToLowerInvariant() != "steps"
               && attrs["highway"].ToLowerInvariant() != "service");
            });
        }
    }

}
