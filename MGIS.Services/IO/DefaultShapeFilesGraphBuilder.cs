using NGAT.Business.Contracts.IO;
using NGAT.Business.Domain.Core;
using NGAT.Geo.Geometries;
using System;
using System.Threading.Tasks;

namespace NGAT.Services.IO
{
    public class DefaultShapeFilesGraphBuilder : IGraphBuilder
    {
        public Uri DigitalMapURI { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IAttributeFilterCollection LinkFilters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IAttributesFetcherCollection NodeAttributesFetchers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IAttributesFetcherCollection LinkAttributesFetchers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Pedestrian { get; set; }

        public string DigitalMapFormatID => throw new NotImplementedException();

        public Graph Build()
        {
            throw new NotImplementedException();
        }

        public Graph Build(Uri fileUri)
        {
            throw new NotImplementedException();
        }

        public Task<Graph> BuildAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Graph> BuildAsync(Uri fileUri)
        {
            throw new NotImplementedException();
        }

        public Graph BuildInRegion(Uri fileUri, Polygon region)
        {
            throw new NotImplementedException();
        }

    }

}
