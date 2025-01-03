﻿using NGAT.Business.Domain.Core;
using System.IO;
using System.Threading.Tasks;

namespace NGAT.Business.Contracts.IO
{
    /// <summary>
    /// Represents an object that exports (or saves) a graph to a specific format
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    public interface IGraphExporter
    {
        /// <summary>
        /// Exports only the subgraph within this range
        /// </summary>
        /// <param name="minLat">Minimum latitude</param>
        /// <param name="MinLong">Minimum longitude</param>
        /// <param name="maxLat">Maximum latitude</param>
        /// <param name="MaxLong">Maximum longitude</param>
        void ExportInRange(Stream stream, double minLat, double MinLong, double maxLat, double MaxLong, Graph graph);

        /// <summary>
        /// Exports only the subgraph within this range
        /// </summary>
        /// <param name="minLat"></param>
        /// <param name="MinLong"></param>
        /// <param name="maxLat"></param>
        /// <param name="MaxLong"></param>
        Task ExportInRangeAsync(Stream stream, double minLat, double MinLong, double maxLat, double MaxLong, Graph graph);

        /// <summary>
        /// Exports a graph synchronously
        /// </summary>
        /// <param name="input">The input for this export, including the graph to export</param>
        void Export(Stream stream, Graph graph);

        /// <summary>
        /// Exports a Graph asynchronously
        /// </summary>
        /// <param name="input">The input for this export, including the graph to export</param>
        /// <returns>A task that exports the graph</returns>
        Task ExportAsync(Stream stream, Graph graph);

        /// <summary>
        /// Gets a unique id for the format. i.e: GRF, GeoJSON, GraphML...
        /// </summary>
        string FormatID { get; }
    }


}
