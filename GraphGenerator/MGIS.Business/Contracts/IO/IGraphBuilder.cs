using NGAT.Business.Domain.Core;
using NGAT.Geo.Geometries;
using System;
using System.Threading.Tasks;

namespace NGAT.Business.Contracts.IO
{
    /// <summary>
    /// Represents a graph builder
    /// </summary>
    /// <typeparam name="TInput">Type to wrap the arguments needed by this graph builder</typeparam>
    public interface IGraphBuilder
    {
        /// <summary>
        /// The universal resource identifier for the digital map data source
        /// </summary>
        Uri DigitalMapURI { get; set; }

        /// <summary>
        /// A collection of filters to apply to the links based on its attributes
        /// </summary>
        IAttributeFilterCollection LinkFilters { get; set; }

        /// <summary>
        /// A collection of filters to apply to the nodes based on its attributes
        /// </summary>
        IAttributesFetcherCollection NodeAttributesFetchers { get; set; }

        /// <summary>
        /// A collection of attributes fetchers for the links
        /// </summary>
        IAttributesFetcherCollection LinkAttributesFetchers { get; set; }

        /// <summary>
        /// If true, all links in the graph shall be undirected and no filters shall be applied to any link
        /// </summary>
        bool Pedestrian { get; set; }

        /// <summary>
        /// Builds a graph synchronously from <paramref name="input"/> source
        /// </summary>
        /// <param name="input">Arguments needed to build the graph</param>
        /// <returns>A Graph</returns>
        Graph Build(Uri fileUri);

        /// <summary>
        /// Builds a graph asynchronously from <paramref name="input"/> source
        /// </summary>
        /// <param name="input">Arguments needed to build the graph</param>
        /// <returns>A Task for building the Graph</returns>
        Task<Graph> BuildAsync(Uri fileUri);

        /// <summary>
        /// A unique string identifier for the map format. i.e: PBF, ShapeFile, GeoJSON...
        /// </summary>
        string DigitalMapFormatID { get; }

        /// <summary>
        /// Builds the graph contained in the region
        /// </summary>
        /// <param name="fileUri">The Graph Uri</param>
        /// <param name="region">The region</param>
        /// <returns></returns>
        Graph BuildInRegion(Uri fileUri, Polygon region);
    }

}
