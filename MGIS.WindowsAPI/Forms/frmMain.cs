using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using NGAT.Business;
using NGAT.Business.Contracts.IO;
using NGAT.Services.IO.MapFiles;
using NGAT.Services.IO.Osm;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System.Net;
using NGAT.Business.Contracts.Services.Algorithms;
using NGAT.Services.IO;
using NGAT.WindowsAPI.Forms;
using GMap.NET.WindowsForms.Markers;
using NGAT.Geo.Geometries;
using NGAT.Geo;
using NGAT.Business.Domain.Core;
using NGAT.Services.ForbiddenTurns;
using OsmSharp;
using OsmSharp.Streams;
using System.Drawing;
using Node = NGAT.Business.Domain.Core.Node;
using NGAT.Visual.Forms;
using NGAT.Business.Contracts.Services.ResultDisplayers;
using NGAT.Business.Contracts.Services.ResultExporters;
using NGAT.Services.ResultDisplayers;
using NGAT.Services.Algorithms;
using Utils = NGAT.Services.Utils;

namespace NGAT.WindowsAPI
{
    public partial class frmMain : Form
    {

        #region Graph & IO related fields

        Business.Contracts.IO.File file; // A generic file
        GraphFile graphFile;             // the current loaded graph file
        IShortestPathProblemAlgorithm algorithm;            // the selected algorithm to run
        ITurnProhibitionsAssociatedGraph turnProhibitionsGraphModifier; // the graph modifier used to consider turn prohibitions
        bool readyToRunAlgorithm;// some algorithms need extra data input from users
        ShortestPathProblemOutput lastResult;               // The result of the last computed algorithm
        RoutingProblemFile resultFile;   // the selected result file implementation to be loaded
        (Business.Domain.Core.Node, Business.Domain.Core.Node) link;
        (Business.Domain.Core.Node, Business.Domain.Core.Node, Business.Domain.Core.Node) turnProhibitionNodes;
        #endregion

        #region GMap Related fields

        enum ItemToLoad { Graph, Map, Result }
        ItemToLoad itemToLoad;

        private List<PointLatLng> points;
        private GMapOverlay globalOverlay;
        MapActions mapAction;
        Dictionary<Layer, bool> Layers;

        #endregion


        public frmMain()
        {
            InitializeComponent();
            mapAction = MapActions.None;
            globalOverlay = new GMapOverlay("global");
            points = new List<PointLatLng>();
            gmap.Overlays.Add(globalOverlay);
            Layers = new Dictionary<Layer, bool>();
            readyToRunAlgorithm = false;
        }

        #region Map Buttons Events

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            gmap.Zoom += 1;
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            gmap.Zoom -= 1;
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            gmap.Position = new PointLatLng(23.122648000717167, -82.38653898239136); // MATCOM coordinates
            gmap.Zoom = 14;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            var point = gmap.FromLatLngToLocal(gmap.Position);
            point.Y -= 20;
            gmap.Position = gmap.FromLocalToLatLng((int)point.X, (int)point.Y);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            var point = gmap.FromLatLngToLocal(gmap.Position);
            point.Y += 20;
            gmap.Position = gmap.FromLocalToLatLng((int)point.X, (int)point.Y);
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            var point = gmap.FromLatLngToLocal(gmap.Position);
            point.X -= 20;
            gmap.Position = gmap.FromLocalToLatLng((int)point.X, (int)point.Y);
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            var point = gmap.FromLatLngToLocal(gmap.Position);
            point.X += 20;
            gmap.Position = gmap.FromLocalToLatLng((int)point.X, (int)point.Y);
        }

        private void lblSelectRegion_Click(object sender, EventArgs e)
        {
            if (graphFile == null || graphFile.Graph == null)
            {
                MessageBox.Show("No graph file has been loaded yet.", "No graph", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(globalOverlay.Polygons.Count == 1)
            {
                Polygon polygon = new Polygon(globalOverlay.Polygons[0].Points.Select(p => new Coordinate(p.Lat, p.Lng)).ToArray());
                // build subgraph
                Lock(lblSelectRegion);
                Lock(null, "Building subgraph...");
                polygon.CoordinateSystem = new GeoCoordinateSystem();
                var subgraph = graphFile.Graph.SubGraphInRegion(polygon, polygon.CoordinateSystem);
                Lock(null);
                Lock(lblSelectRegion);
                try
                {
                    bool checkConnectivity = MessageBox.Show("Do you want to check connectivity in this region?", "Check Connectivity?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    frmRegion frmRegion = new frmRegion(subgraph, gmap, polygon, checkConnectivity);
                    frmRegion.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error ocurred while selecting region.");
                }
                globalOverlay.Polygons.Clear();
                gmap.Refresh();
            }
            else
            {
                mapAction = MapActions.SelectingVRPInput;
                Lock(lblSelectRegion, "Buildind polygon ...");
            }
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            if (mapAction == MapActions.AddTurnProhibition || mapAction == MapActions.DeleteTurningProhibition)
                return;
            menuViewNone.Checked = true;
            gmap.Overlays.Clear();
            globalOverlay = new GMapOverlay("global");
            gmap.Overlays.Add(globalOverlay);
            gmap.Cursor = Cursors.Default;
            gmap.Refresh();
            points = new List<PointLatLng>();
        }

        private void menuProxySettings_Click(object sender, EventArgs e)
        {
            frmProxySettings frmProxy = new frmProxySettings();
            frmProxy.ShowDialog();
            if (frmProxy.UseProxy)
            {
                GMapProvider.WebProxy = new WebProxy(frmProxy.ProxyServer, frmProxy.Port);
                GMapProvider.WebProxy.Credentials = new NetworkCredential(frmProxy.UserName, frmProxy.Password);
            }
            frmProxy.Dispose();
        }

        private void menuCacheLocation_Click(object sender, EventArgs e)
        {
            dlgFolderBrowser.SelectedPath = gmap.CacheLocation;
            if (dlgFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                gmap.CacheLocation = dlgFolderBrowser.SelectedPath;
                gmap.Refresh();
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
            if (algorithm != null)
            {
                lblMessage.Text = Align(algorithm.Description);
            }
        }

        private void groupBox3_Leave(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }

        #region View Graph Related Events

        private void menuViewNone_CheckedChanged(object sender, EventArgs e)
        {
            if (menuViewNone.Checked)
            {
                gmap.Overlays.Remove(gmap.Overlays.FirstOrDefault(o => o.Id == "nodes"));
                gmap.Overlays.Remove(gmap.Overlays.FirstOrDefault(o => o.Id == "edges"));
                gmap.Overlays.Remove(gmap.Overlays.FirstOrDefault(o => o.Id == "arcs"));
                gmap.Overlays.Remove(gmap.Overlays.FirstOrDefault(o => o.Id == "prohibitions"));
                gmap.Refresh();
                menuViewArcs.Checked = false;
                menuViewEdges.Checked = false;
                menuViewNodes.Checked = false;
            }
        }

        private void menuViewNodes_CheckedChanged(object sender, EventArgs e)
        {
            GMapOverlay nodesOverlay = gmap.Overlays.FirstOrDefault(o => o.Id == "nodes");
            if (menuViewNodes.Checked)
            {
                menuViewNone.Checked = false;
                if (!gmap.Overlays.Contains(nodesOverlay))
                {
                    Envelope envelope = gmap.GetEnvelope();
                    nodesOverlay = gmap.GetNodesOverlay(graphFile.Graph, envelope);
                    gmap.Overlays.Add(nodesOverlay);
                    
                }
                else nodesOverlay.IsVisibile = true;
                gmap.Zoom += 1;
                gmap.Zoom -= 1;
                menuViewNone.Checked = false;
            }
            else if (nodesOverlay != null)
            {
                nodesOverlay.IsVisibile = false;
            }
        }

        private void menuViewEdges_CheckedChanged(object sender, EventArgs e)
        {
            GMapOverlay edgesOverlay = gmap.Overlays.FirstOrDefault(o => o.Id == "edges");
            if (menuViewEdges.Checked)
            {
                if (!gmap.Overlays.Contains(edgesOverlay))
                {
                    Envelope envelope = gmap.GetEnvelope();
                    edgesOverlay = gmap.GetEdgesOverlay(graphFile.Graph, envelope);
                    gmap.Overlays.Add(edgesOverlay);
                }
                else edgesOverlay.IsVisibile = true;
                gmap.Zoom += 1;
                gmap.Zoom -= 1;
                menuViewNone.Checked = false;
            }
            else if(edgesOverlay != null)
            {
                edgesOverlay.IsVisibile = false;
            }
        }

        private void menuViewArcs_CheckedChanged(object sender, EventArgs e)
        {
            GMapOverlay arcsOverlay = gmap.Overlays.FirstOrDefault(o => o.Id == "arcs");
            if (menuViewArcs.Checked)
            {
                if (!gmap.Overlays.Contains(arcsOverlay))
                {
                    Envelope envelope = gmap.GetEnvelope();
                    arcsOverlay = gmap.GetArcsOverlay(graphFile.Graph, envelope);
                    gmap.Overlays.Add(arcsOverlay);
                }
                else arcsOverlay.IsVisibile = true;
                gmap.Zoom += 1;
                gmap.Zoom -= 1;
                menuViewNone.Checked = false;
            }
            else if(arcsOverlay != null)
            {
                arcsOverlay.IsVisibile = false;
            }
        }

        #endregion

        private void MenuLayers_Click(object sender, EventArgs e)
        {
            frmLayers layers = new frmLayers(Layers);
            layers.ShowDialog();

            foreach (var layer in Layers.Keys)
            {
                if (Layers[layer]) // Layer is activated
                {
                    if (!gmap.Overlays.Any(o => o.Id == layer.Name)) // gMap does not contains this layer
                    {
                        var ol = gmap.GetOverlay(layer);
                        gmap.Overlays.Add(ol);
                        //gmap.Refresh();
                        gmap.Zoom += 1;
                        gmap.Zoom -= 1;
                    }
                    else
                    {
                        gmap.Overlays.First(l => l.Id == layer.Name).IsVisibile = true;
                    }
                }
                else
                {
                    var ol = gmap.Overlays.FirstOrDefault(l => l.Id == layer.Name);
                    if (ol != null) ol.IsVisibile = false;
                }

            }
            gmap.Refresh();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!(tbxStreet1.Text == "" && tbxStreet2.Text == ""))
            {
                List<Way> street1 = new List<Way>();
                List<Way> street2 = new List<Way>();
                List<PointLatLng> corners = new List<PointLatLng>();

                using (var fileStream = System.IO.File.OpenRead("Resources/IO/cuba-latest.osm.pbf"))
                {
                    var source = new PBFOsmStreamSource(fileStream);

                    // Search all matching ways
                    foreach (Way way in source.Where(w => w.Type == OsmGeoType.Way && w.Tags.ContainsKey("name")))
                    {
                        string name = way.Tags["name"].ToLower();
                        if (name.Contains(tbxStreet1.Text.ToLower()))
                            street1.Add(way);
                        if (name.Contains(tbxStreet2.Text.ToLower()))
                            street2.Add(way);
                    }

                    // Find the matching corners
                    foreach (var st1 in street1)
                    {
                        foreach (var st2 in street2)
                        {
                            foreach (var nodeId1 in st1.Nodes)
                            {
                                foreach (var nodeId2 in st2.Nodes)
                                {
                                    if (nodeId1 == nodeId2)
                                    {
                                        OsmSharp.Node node = (OsmSharp.Node)source.First(n => n.Type == OsmGeoType.Node && n.Id == nodeId1);
                                        corners.Add(new PointLatLng(node.Latitude.Value, node.Longitude.Value));
                                    }
                                }
                            }
                        }
                    }

                }
                if (corners.Count == 0)
                    MessageBox.Show("The specified address was not found", "Address Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    // find the nearest corner
                    double distance = double.PositiveInfinity;
                    PointLatLng nearest = default;
                    foreach (var point in corners)
                    {
                        double d = Math.Sqrt(Math.Pow(gmap.Position.Lat - point.Lat, 2) + Math.Pow(gmap.Position.Lng - point.Lng, 2));
                        if (d < distance)
                        {
                            distance = d;
                            nearest = point;
                        }
                    }
                    gmap.Position = nearest;
                }
                tbxStreet1.Clear();
                tbxStreet2.Clear();
            }
        }

        #region Edit Graph Related Events

        private void menuAddNode_CheckedChanged(object sender, EventArgs e)
        {
            if (menuAddNode.Checked)
            {
                menuViewNodes.Checked = true;
                menuAddArc.Checked = false;
                menuAddEdge.Checked = false;
                menuDeleteArc.Checked = false;
                menuDeleteEdge.Checked = false;
                menuDeleteNode.Checked = false;
                menuEditArc.Checked = false;
                menuEditEdge.Checked = false;
                mapAction = MapActions.AddingNode;
            }
            else
            {
                mapAction = MapActions.None;
                menuViewNodes.Checked = false;
            }
        }

        private void menuAddEdge_CheckedChanged(object sender, EventArgs e)
        {
            if (menuAddEdge.Checked)
            {
                menuViewEdges.Checked = true;
                menuViewNodes.Checked = true;
                menuAddArc.Checked = false;
                menuAddNode.Checked = false;
                menuDeleteArc.Checked = false;
                menuDeleteEdge.Checked = false;
                menuDeleteNode.Checked = false;
                menuEditArc.Checked = false;
                menuEditEdge.Checked = false;
                mapAction = MapActions.AddingEdge;
            }
            else
            {
                mapAction = MapActions.None;
                menuViewNodes.Checked = false;
                menuViewEdges.Checked = false;

            }
        }

        private void menuAddArc_CheckedChanged(object sender, EventArgs e)
        {
            if (menuAddArc.Checked)
            {
                menuViewNodes.Checked = true;
                menuViewArcs.Checked = true;
                menuAddEdge.Checked = false;
                menuAddNode.Checked = false;
                menuDeleteArc.Checked = false;
                menuDeleteEdge.Checked = false;
                menuDeleteNode.Checked = false;
                menuEditArc.Checked = false;
                menuEditEdge.Checked = false;
                mapAction = MapActions.AddingArc;
            }
            else
            {
                mapAction = MapActions.None;
                menuViewNodes.Checked = false;
                menuViewArcs.Checked= false;
            }
        }

        private void menuDeleteArc_CheckedChanged(object sender, EventArgs e)
        {
            if (menuDeleteArc.Checked)
            {
                menuViewNodes.Checked = true;
                menuViewArcs.Checked = true;
                menuAddEdge.Checked = false;
                menuAddNode.Checked = false;
                menuAddArc.Checked = false;
                menuDeleteEdge.Checked = false;
                menuDeleteNode.Checked = false;
                menuEditArc.Checked = false;
                menuEditEdge.Checked = false;
                mapAction = MapActions.DeleteArc;
            }
            else
            {
                mapAction = MapActions.None;
                menuViewNodes.Checked = false;
                menuViewArcs.Checked = false;
            }
        }

        private void menuDeleteEdge_CheckedChanged(object sender, EventArgs e)
        {
            if (menuDeleteEdge.Checked)
            {
                menuViewNodes.Checked = true;
                menuViewEdges.Checked = true;
                menuAddEdge.Checked = false;
                menuAddNode.Checked = false;
                menuAddArc.Checked = false;
                menuDeleteArc.Checked = false;
                menuDeleteNode.Checked = false;
                menuEditArc.Checked = false;
                menuEditEdge.Checked = false;
                mapAction = MapActions.DeleteEdge;
            }
            else
            {
                mapAction = MapActions.None;
                menuViewNodes.Checked = false;
                menuViewArcs.Checked = false;
            }
        }
        private void menuDeleteNode_CheckedChanged(object sender, EventArgs e)
        {
            if (menuDeleteNode.Checked)
            {
                menuViewNodes.Checked = true;
                menuAddEdge.Checked = false;
                menuAddNode.Checked = false;
                menuAddArc.Checked = false;
                menuDeleteArc.Checked = false;
                menuDeleteEdge.Checked = false;
                menuEditArc.Checked = false;
                menuEditEdge.Checked = false;
                mapAction = MapActions.DeleteNode;
            }
            else
            {
                mapAction = MapActions.None;
                menuViewNodes.Checked = false;
            }
        }

        private void menuScenarios_Click(object sender, EventArgs e)
        {
            frmScenariosCount modal = new frmScenariosCount(graphFile.Graph.Scenarios_Count);
            if (modal.ShowDialog() == DialogResult.OK)
            {
                int newScenariosCount = modal.selectedScenariosCount;
                if (newScenariosCount > graphFile.Graph.Scenarios_Count)
                {
                    foreach (Link link in graphFile.Graph.Links)
                    {
                        link.Distance.IsMultiScenario = true;
                        for (int i = 0; i < newScenariosCount - graphFile.Graph.Scenarios_Count; i++)
                        {
                            link.Distance.Costs.Add(0);
                        }
                    }
                }
                else if (newScenariosCount < graphFile.Graph.Scenarios_Count)
                {
                    foreach (Link link in graphFile.Graph.Links)
                    {
                        link.Distance.Costs.RemoveRange(newScenariosCount, link.Distance.Costs.Count - newScenariosCount);
                    }
                }

                graphFile.Graph.Scenarios_Count = newScenariosCount;
            }
        }

        private void menuEditArc_CheckedChanged(object sender, EventArgs e)
        {
            if (menuEditArc.Checked)
            {
                menuViewNodes.Checked = true;
                menuViewArcs.Checked = true;
                menuAddEdge.Checked = false;
                menuAddNode.Checked = false;
                menuAddArc.Checked = false;
                menuDeleteArc.Checked = false;
                menuDeleteEdge.Checked = false;
                menuEditEdge.Checked = false;
                mapAction = MapActions.EditArc;
            }
            else 
            {
                mapAction = MapActions.None;
                menuViewNodes.Checked = false;
                menuViewArcs.Checked = false;
            }
        }

        private void menuEditEdge_CheckedChanged(object sender, EventArgs e)
        {
            if (menuEditEdge.Checked)
            {
                menuViewNodes.Checked = true;
                menuViewEdges.Checked = true;
                menuAddEdge.Checked = false;
                menuAddNode.Checked = false;
                menuAddArc.Checked = false;
                menuDeleteArc.Checked = false;
                menuDeleteEdge.Checked = false;
                menuEditArc.Checked = false;
                mapAction = MapActions.EditEdge;
            }
            else
            {
                mapAction = MapActions.EditEdge;
                menuViewNodes.Checked = false;
                menuViewEdges.Checked = false;
            }
        }
        #endregion

        #endregion

        #region GMap Events

        private void gmap_Load(object sender, EventArgs e)
        {
            gmap.Manager.Mode = AccessMode.ServerAndCache; // to work offline and online
            gmap.CacheLocation = "Resources\\gmapcache\\GMap.NET";
            gmap.ShowCenter = false;
            gmap.DragButton = MouseButtons.Left;
            gmap.CanDragMap = true;
            gmap.MapProvider = GMapProviders.GoogleMap;
            gmap.MinZoom = 0;
            gmap.MaxZoom = 20;
            gmap.AutoScroll = false;

            try
            {
                using (StreamReader sr = new StreamReader(new FileStream("Resources\\IO\\GmapSettings", FileMode.Open)))
                {
                    string[] input = sr.ReadLine().Split();
                    gmap.Zoom = double.Parse(input[1]);
                    input = sr.ReadLine().Split();
                    gmap.Position = new PointLatLng(double.Parse(input[2]), double.Parse(input[4]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " The map settings will take default values.", "Error Loading Gmap Settings");
                gmap.CacheLocation = "Resources\\gmapcache\\GMap.NET";
                gmap.Position = new PointLatLng(23.122648000717167, -82.38653898239136);
                gmap.Zoom = 14;
            }

        }

        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (points.Count > 2 && item.Position == points[0]) // close the polygon
                {
                    GMapPolygon pol = new GMapPolygon(points, "Region");
                    globalOverlay.Polygons.Clear();
                    globalOverlay.Polygons.Add(pol);
                    globalOverlay.Markers.Clear();
                    gmap.Refresh();
                    Polygon polygon = new Polygon(points.Select(p => new Coordinate(p.Lat, p.Lng)).ToArray());
                    points = new List<PointLatLng>();
                    if(mapAction == MapActions.SelectingVRPInput)
                    {
                        // build subgraph
                        Lock(lblSelectRegion);
                        Lock(null, "Building subgraph...");
                        polygon.CoordinateSystem = new GeoCoordinateSystem();
                        var subgraph = graphFile.Graph.SubGraphInRegion(polygon, polygon.CoordinateSystem);
                        Lock(null);
                        try
                        {
                            bool checkConnectivity = MessageBox.Show("Do you want to check connectivity in this region?", "Check Connectivity?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                            frmRegion frmRegion = new frmRegion(subgraph, gmap, polygon, checkConnectivity);
                            frmRegion.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error ocurred while selecting region.");
                        }
                        globalOverlay.Polygons.Clear();

                    }
                }
                else if ((mapAction == MapActions.AddingArc || mapAction == MapActions.AddingEdge))
                {
                    
                    if (link.Item1 == null) link.Item1 = graphFile.Graph.NodesId[(int)(item.Tag)];
                    else
                    {
                        bool directed = mapAction == MapActions.AddingArc;
                        link.Item2 = graphFile.Graph.NodesId[(int)(item.Tag)];
                        if(graphFile.Graph.Scenarios_Count==1)
                        {
                            graphFile.Graph.AddLink(link.Item1.Id, link.Item2.Id, new LinkData(), directed);
                        }
                        else 
                        {
                            frmArcEdgeCosts modal = new frmArcEdgeCosts(graphFile.Graph.Scenarios_Count);
                            List<double> costs = new List<double>();
                            if (modal.ShowDialog() == DialogResult.OK)
                            {
                                for (int i = 0; i < modal.scenariosCost.Length; i++)
                                {
                                    costs.Add(modal.scenariosCost[i]);
                                }
                            }
                            else 
                            {
                                MessageBox.Show("Incorrect input");
                                return;
                            }
                            Distance distance = new Distance(costs, true);
                            graphFile.Graph.AddLink(link.Item1.Id, link.Item2.Id, distance, new LinkData(), directed);
                        }
                        GMapRoute route = new GMapRoute(new PointLatLng[] { new PointLatLng(link.Item1.Latitude, link.Item1.Longitude), new PointLatLng(link.Item2.Latitude, link.Item2.Longitude) }, "");
                        if (directed) gmap.Overlays.First(o => o.Id == "arcs").Routes.Add(route);
                        else gmap.Overlays.First(o => o.Id == "edges").Routes.Add(route);
                        //mapAction = MapActions.None;
                        link.Item1 = null;
                        link.Item2 = null;
                    }
                }
                else if ((mapAction == MapActions.AddTurnProhibition || mapAction == MapActions.DeleteTurningProhibition) && item.Tag != null && item.Tag is int)
                {
                    //Checking not double marked nodes
                    //if (turnProhibitionNodes.Item1 == graphFile.Graph.NodesId[(int)item.Tag]
                    //    || turnProhibitionNodes.Item2 == graphFile.Graph.NodesId[(int)item.Tag]
                    //    || turnProhibitionNodes.Item3 == graphFile.Graph.NodesId[(int)item.Tag]
                    //    )
                    //    return;

                    if (turnProhibitionNodes.Item1 == null)
                    {
                        turnProhibitionNodes.Item1 = graphFile.Graph.NodesId[(int)item.Tag];
                        item.Size = new Size(2*item.Size.Width, 2*item.Size.Height);
                    }
                    else if (turnProhibitionNodes.Item2 == null)
                    {
                        //Checking not double marked nodes
                        if (turnProhibitionNodes.Item1 == graphFile.Graph.NodesId[(int)item.Tag])
                                return;
                        turnProhibitionNodes.Item2 = graphFile.Graph.NodesId[(int)item.Tag];
                        item.Size = new Size(2 * item.Size.Width, 2 * item.Size.Height);
                    }
                    else if (turnProhibitionNodes.Item3 == null)
                    {
                        //Checking not double marked nodes
                        if (turnProhibitionNodes.Item2 == graphFile.Graph.NodesId[(int)item.Tag])
                            return;
                        turnProhibitionNodes.Item3 = graphFile.Graph.NodesId[(int)item.Tag];
                        item.Size = new Size(2 * item.Size.Width, 2 * item.Size.Height);
                        
                    }
                }
                else if (mapAction==MapActions.DeleteArc)
                {
                    if (link.Item1 == null) link.Item1 = graphFile.Graph.NodesId[(int)(item.Tag)];
                    else
                    {
                        link.Item2 = graphFile.Graph.NodesId[(int)(item.Tag)];
                        Link toDeleteLink = link.Item1.OutgoingArcs.FirstOrDefault(l => l.ToNode == link.Item2);

                        if (toDeleteLink != null)
                        {
                            graphFile.Graph.DeleteLink(toDeleteLink);
                            GMapRoute routeToDelete = gmap.Overlays.FirstOrDefault(o => o.Id == "arcs").Routes.FirstOrDefault(r => (r.Points[0].Lat == link.Item1.Latitude && r.Points[0].Lng == link.Item1.Longitude && r.Points[1].Lat == link.Item2.Latitude && r.Points[1].Lng == link.Item2.Longitude));
                            if (routeToDelete != null)
                            gmap.Overlays.First(o => o.Id == "arcs").Routes.Remove(routeToDelete);
                            //mapAction = MapActions.None;
                        }
                        else
                        {
                            MessageBox.Show("Non existent link");
                        }
                        link.Item1 = null;
                        link.Item2 = null;

                    }
                }
                else if (mapAction == MapActions.DeleteEdge)
                {
                    if (link.Item1 == null) link.Item1 = graphFile.Graph.NodesId[(int)(item.Tag)];
                    else
                    {
                        link.Item2 = graphFile.Graph.NodesId[(int)(item.Tag)];
                        Link toDeleteLink = link.Item1.Edges.FirstOrDefault(l => (l.ToNode == link.Item2 || l.FromNode == link.Item2) );

                        if (toDeleteLink != null)
                        {
                            graphFile.Graph.DeleteLink(toDeleteLink);
                            GMapRoute routeToDelete = gmap.Overlays.First(o => o.Id == "edges").Routes.FirstOrDefault(r =>
                            (r.Points[0].Lat == link.Item1.Latitude && r.Points[0].Lng == link.Item1.Longitude && r.Points[1].Lat == link.Item2.Latitude && r.Points[1].Lng == link.Item2.Longitude)
                            || (r.Points[0].Lat == link.Item2.Latitude && r.Points[0].Lng == link.Item2.Longitude && r.Points[1].Lat == link.Item1.Latitude && r.Points[1].Lng == link.Item1.Longitude));
                            gmap.Overlays.First(o => o.Id == "edges").Routes.Remove(routeToDelete);
                            //mapAction = MapActions.None;
                        }
                        else
                        {
                            MessageBox.Show("Non existent link");
                        }
                        link.Item1 = null;
                        link.Item2 = null;

                    }
                }
                else if (mapAction == MapActions.EditArc)
                {
                    if (link.Item1 == null) link.Item1 = graphFile.Graph.NodesId[(int)(item.Tag)];
                    else
                    {
                        link.Item2 = graphFile.Graph.NodesId[(int)(item.Tag)];
                        Link toEditLink = link.Item1.OutgoingArcs.FirstOrDefault(l => l.ToNode == link.Item2);
                        link.Item1 = null;
                        link.Item2 = null;
                        if (toEditLink != null)
                        {
                            double [] costs = new double[toEditLink.Distance.Costs.Count];
                            for (int i = 0; i < costs.Length; i++)
                            {
                                costs[i] = toEditLink.Distance.Costs[i];
                            }
                            frmArcEdgeCosts modal = new frmArcEdgeCosts(costs);
                            List<double> distances = new List<double>();
                            if (modal.ShowDialog() == DialogResult.OK)
                            {
                                for (int i = 0; i < modal.scenariosCost.Length; i++)
                                {
                                    distances.Add(modal.scenariosCost[i]);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Incorrect input");
                                return;
                            }
                            Distance distance = new Distance(distances, true);
                            toEditLink.Distance = distance;
                        }
                        else
                        {
                            MessageBox.Show("Non existent link");
                        }
                    }
                }
                else if (mapAction == MapActions.EditEdge)
                {
                    if (link.Item1 == null) link.Item1 = graphFile.Graph.NodesId[(int)(item.Tag)];
                    else
                    {
                        link.Item2 = graphFile.Graph.NodesId[(int)(item.Tag)];
                        Link toEditLink = link.Item1.Edges.FirstOrDefault(l => (l.ToNode == link.Item2 || l.FromNode == link.Item2));
                        link.Item1 = null;
                        link.Item2 = null;
                        if (toEditLink != null)
                        {
                            double[] costs = new double[toEditLink.Distance.Costs.Count];
                            for (int i = 0; i < costs.Length; i++)
                            {
                                costs[i] = toEditLink.Distance.Costs[i];
                            }
                            frmArcEdgeCosts modal = new frmArcEdgeCosts(costs);
                            List<double> distances = new List<double>();
                            if (modal.ShowDialog() == DialogResult.OK)
                            {
                                for (int i = 0; i < modal.scenariosCost.Length; i++)
                                {
                                    distances.Add(modal.scenariosCost[i]);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Incorrect input");
                                return;
                            }
                            Distance distance = new Distance(distances, true);
                            toEditLink.Distance = distance;
                        }
                        else
                        {
                            MessageBox.Show("Non existent link");
                        }

                    }
                }
                else if (mapAction == MapActions.DeleteNode)
                {
                    Node node = graphFile.Graph.NodesId[(int)item.Tag];
                    bool delete = MessageBox.Show("Remove Node " + item.Tag + "?", "Remove?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (delete)
                    {
                        graphFile.Graph.DeleteNode(node);
                        menuViewNone.Checked = true;
                        menuViewNodes.Checked = true;
                    }

                }
            }
            else //When e.Button == MouseButtons.Right
            {
                if ((mapAction == MapActions.AddTurnProhibition || mapAction == MapActions.DeleteTurningProhibition) && item.Tag != null && item.Tag is int)
                {
                    if (turnProhibitionNodes.Item1 == graphFile.Graph.NodesId[(int)item.Tag])
                    {
                        turnProhibitionNodes.Item1 = null;
                        item.Size = new Size(item.Size.Width/2, item.Size.Height/2);
                        MessageBox.Show($"First marked node has been unmarked. Next node you marked will fill the smallest empty position");
                    }
                    else if (turnProhibitionNodes.Item2 == graphFile.Graph.NodesId[(int)item.Tag])
                    {
                        turnProhibitionNodes.Item2 = null;
                        item.Size = new Size(item.Size.Width / 2, item.Size.Height / 2);
                        MessageBox.Show($"Second marked node has been unmarked. Next node you marked will fill the smallest empty position");
                    }
                    else if (turnProhibitionNodes.Item3 == graphFile.Graph.NodesId[(int)item.Tag])
                    {
                        turnProhibitionNodes.Item3 = null;
                        item.Size = new Size(item.Size.Width / 2, item.Size.Height / 2);
                        MessageBox.Show($"Third marked node has been unmarked. Next node you marked will fill the smallest empty position");
                    }
                }
            }
        }

        private void gmap_MouseClick(object sender, MouseEventArgs e)
        {
            PointLatLng clickLocation = gmap.FromLocalToLatLng(e.Location.X, e.Location.Y);
            if (e.Button == MouseButtons.Right && mapAction != MapActions.AddTurnProhibition && mapAction != MapActions.DeleteTurningProhibition && mapAction != MapActions.DeleteArc)
            {
                GMapMarker marker = new GMarkerGoogle(clickLocation, GMarkerGoogleType.blue_dot);
                globalOverlay.Markers.Add(marker);
                points.Add(clickLocation);
            }

            else if (mapAction == MapActions.AddingNode && e.Button == MouseButtons.Left)
            {
                int id = graphFile.Graph.AddNode(clickLocation.Lat, clickLocation.Lng, new Dictionary<string, string>());
                gmap.Overlays.First(o => o.Id == "nodes").Markers.Add(new GMarkerGoogle(clickLocation, new Bitmap("Resources\\BitMaps\\blue_border_red_point.png")) { Tag = id });
            }
        }

        private void gmap_OnMarkerEnter(GMapMarker item)
        {
            //GMapOverlay nodesOverlay = gmap.Overlays.FirstOrDefault(o => o.Id == "nodes");
            //if (nodesOverlay != null && nodesOverlay.Markers.Contains(item))
            //{
            //    GMapOverlay arcs = new GMapOverlay("nodeArcs");
            //    Node node = null;
            //    double distance = double.PositiveInfinity;
            //    foreach (var v in graphFile.Graph.Nodes)
            //    {
            //        double d = v.Coordinate.GetDistanceTo(new Coordinate(item.Position.Lat, item.Position.Lng));
            //        if (d < distance)
            //        {
            //            distance = d;
            //            node = v;
            //        }
            //    }
            //    foreach (var arc in node.OutgoingArcs)
            //    {
            //        var route = new GMapRoute(new PointLatLng[] { new PointLatLng(arc.FromNode.Latitude, arc.FromNode.Longitude),
            //                                                           new PointLatLng(arc.ToNode.Latitude, arc.ToNode.Longitude)}, "");
            //        arcs.Routes.Add(route);
            //    }

            //    gmap.Overlays.Add(arcs);
            //    gmap.Zoom += 1;
            //    gmap.Zoom -= 1;
            //    gmap.Refresh();
            //}
        }

        private void gmap_OnMarkerLeave(GMapMarker item)
        {
            //GMapOverlay nodesOverlay = gmap.Overlays.FirstOrDefault(o => o.Id == "nodes");
            //if (nodesOverlay != null && nodesOverlay.Markers.Contains(item))
            //{
            //    gmap.Overlays.Remove(gmap.Overlays.FirstOrDefault(o => o.Id == "nodeArcs"));
            //    gmap.Refresh();
            //}
        }

        private void gmap_KeyDown(object sender, KeyEventArgs e)
        {
            var point = gmap.FromLatLngToLocal(gmap.Position);
            switch (e.KeyCode)
            {
                case Keys.Left:
                    point.X -= 20;
                    gmap.Position = gmap.FromLocalToLatLng((int)point.X, (int)point.Y);
                    break;
                case Keys.Right:
                    point.X += 20;
                    gmap.Position = gmap.FromLocalToLatLng((int)point.X, (int)point.Y);
                    break;
                case Keys.Up:
                    point.Y -= 20;
                    gmap.Position = gmap.FromLocalToLatLng((int)point.X, (int)point.Y);
                    break;
                case Keys.Down:
                    point.Y += 20;
                    gmap.Position = gmap.FromLocalToLatLng((int)point.X, (int)point.Y);
                    break;
                default:
                    break;
            }
        }

        private void gmap_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng clickLocation = gmap.FromLocalToLatLng(e.Location.X, e.Location.Y);
            lblLat.Text = clickLocation.Lat.ToString();
            lblLong.Text = clickLocation.Lng.ToString();
        }

        #endregion

        #region Form Events

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Load all types which implements IGraphFile
            foreach (var imp in Utils.GetSubClasses<GraphFile>())
                cbxGraphFormat.Items.Add(imp);
            // Load all types which implements IResultFile
            foreach (var imp in Utils.GetSubClasses<GraphAlgorithmFile>())
                cbxResultFormat.Items.Add(imp);
            // Load all types which implements IGraphExporter
            foreach (var imp in Utils.GetSubClasses<IGraphExporter>())
                cbxSaveAs.Items.Add(imp);
            // Load all types which implements IAlgorithm
            foreach (var imp in Utils.GetSubClasses<IShortestPathProblemAlgorithm>())
                cbxRunAlgorithm.Items.Add(imp);
            // Load showing routes options
            foreach (var imp in Utils.GetSubClasses<IResultDisplayer>())
                cbxVisualizationForm.Items.Add(imp);
            cbxVisualizationForm.SelectedIndex = 1;
            // Load export result options
            foreach (var imp in Utils.GetSubClasses<IResultExporter>())
                cbxExportResult.Items.Add(imp);
            cbxExportResult.SelectedIndex = 1;
            // Load Turn Prohibitions Graph Modifiers availables
            foreach (var imp in Utils.GetSubClasses<ITurnProhibitionsAssociatedGraph>())
                cbxTurnProhibitionsGraphModifiers.Items.Add(imp);
            cbxTurnProhibitionsGraphModifiers.SelectedIndex = 0;

            lblMessage.Text = "";
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var stream = new FileStream("Resources\\IO\\GmapSettings", FileMode.Truncate);
                using (var tw = new StreamWriter(stream))
                {
                    tw.WriteLine("Zoom: {0}", gmap.Zoom);
                    tw.WriteLine("Position: Lat: {0} Long: {1}", gmap.Position.Lat, gmap.Position.Lng);
                    tw.WriteLine("Cache Location: {0}", gmap.CacheLocation);
                    //bool useProxy = (GMapProvider.WebProxy == null) ? false : true;
                    //tw.WriteLine("Use Proxy: {0}", (useProxy) ? "No" : "Yes");
                    //tw.WriteLine("Proxy: Server: {0} Port: {1} User: {2} Password: {3}");

                }
                stream.Close();
            }


        }

        #endregion

        #region Load File Related Events

        private void cbxFilesKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            graphFile = (GraphFile)cbxGraphFormat.SelectedItem;
            file = graphFile;
            btnLoadFile.Enabled = true;
            itemToLoad = ItemToLoad.Graph;
        }

        private void cbxResultFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemToLoad = ItemToLoad.Result;
            resultFile = (RoutingProblemFile)cbxResultFormat.SelectedItem;
            //file = resultFile;
            btnLoadResultFile.Enabled = true;
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            
            dlgOpenFile.CheckPathExists = true;
            dlgOpenFile.CheckFileExists = false;
            dlgOpenFile.AddExtension = true;
            dlgOpenFile.DefaultExt = file.Extension;
            dlgOpenFile.ValidateNames = true;
            dlgOpenFile.Title = "Load " + file.FormatID + " file";
            // Example: "Files GRF (*.grf)|*.grf"
            dlgOpenFile.Filter = "Files " + file.FormatID + " (*" + file.Extension + ")|*" + file.Extension;
            dlgOpenFile.FilterIndex = 1;
            dlgOpenFile.RestoreDirectory = true;

            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                //save the file uri
                file.FileUri = new Uri(Path.GetFullPath(dlgOpenFile.FileName));
                try
                {
                    // build the graph ....

                    if (graphFile is OsmPbfFile)
                    {
                        var graphSettings = new frmOsmPbfSettings();
                        graphSettings.ShowDialog();
                        var nodeAttrs = graphSettings.NodeAttributes.ToArray();
                        var linkAttrs = graphSettings.LinkAttributes.ToArray();
                        OsmPbfGraphBuilder builder = new OsmPbfGraphBuilder(new OsmRoadLinksFilterCollection(),
                                                                            new AttributesFetcherCollection(nodeAttrs),
                                                                            new AttributesFetcherCollection(linkAttrs));
                        builder.Pedestrian = graphSettings.Pedestrian;
                        Lock(null, "Building graph ...");
                        graphFile.Graph = builder.Build(graphFile.FileUri);
                        Lock(null);
                    }
                    else
                    {
                        Lock(null, "Building graph ...");
                        graphFile.Graph = graphFile.GraphBuilder.Build(graphFile.FileUri);
                        Lock(null);
                    }
                    MessageBox.Show("Graph has been successfully built.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    gmap.ShowRegion(graphFile.Graph);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading " + graphFile.FormatID + ": " +
                    ex.Message, "Error ocurred while loading " + graphFile.FormatID + " file",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblMessage.Text = "";
                    Enabled = true;
                }
            }

        }
        private void btnLoadResultFile_Click(object sender, EventArgs e)
        {
            dlgOpenFile.CheckPathExists = true;
            dlgOpenFile.CheckFileExists = false;
            dlgOpenFile.AddExtension = true;
            dlgOpenFile.DefaultExt = resultFile.Extension;
            dlgOpenFile.ValidateNames = true;
            dlgOpenFile.Title = "Load " + resultFile.FormatID + " file";
            // Example: "Files GRF (*.grf)|*.grf"
            dlgOpenFile.Filter = "Files " + resultFile.FormatID + " (*" + resultFile.Extension + ")|*" + resultFile.Extension;
            dlgOpenFile.FilterIndex = 1;
            dlgOpenFile.RestoreDirectory = true;

            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                resultFile.FileUri = new Uri(Path.GetFullPath(dlgOpenFile.FileName));
                Lock(null, "Importing file ...");
                resultFile.Import();
                Lock(null);
                frmRegion frmRegion = new frmRegion(resultFile, gmap);
                frmRegion.Show();
            }


        }


        #endregion

        #region Save File Related Events

        private void cbxSaveAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (graphFile.Graph != null) btnSave.Enabled = true;
            graphFile.GraphExporter = (IGraphExporter)cbxSaveAs.SelectedItem;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dlgSaveAs.AddExtension = true;
            dlgSaveAs.DefaultExt = graphFile.GraphExporter.FormatID.ToLower();
            dlgSaveAs.Filter = "File " + graphFile.GraphExporter.FormatID + " (*" + dlgSaveAs.DefaultExt + ")|*" + dlgSaveAs.DefaultExt;
            dlgSaveAs.FileName = "graph." + dlgSaveAs.DefaultExt;

            if (graphFile.Graph != null && globalOverlay.Polygons.Count == 1)
            {
                Polygon polygon = new Polygon(globalOverlay.Polygons[0].Points.Select(p => new Coordinate(p.Lat, p.Lng)).ToArray());
                polygon.CoordinateSystem = new GeoCoordinateSystem();
                var subgraph = graphFile.Graph.SubGraphInRegion(polygon, polygon.CoordinateSystem);
                graphFile.Graph = subgraph;
            }

            if (graphFile.GraphExporter != null && dlgSaveAs.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //MessageBox.Show("Please wait while results are exported", "Building graph from file");
                    lblMessage.Text = "Exporting results ...";
                    Lock(null);
                    graphFile.GraphExporter.Export(new FileStream(dlgSaveAs.FileName, FileMode.CreateNew), graphFile.Graph);
                    MessageBox.Show("Results has been successfully exported.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblMessage.Text = "";
                    Lock(null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving " + graphFile.GraphExporter.FormatID + ": " +
                        ex.Message, "Error ocurred while saving " + graphFile.GraphExporter.FormatID + " file",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Run Algorithm Related Events

        private void cbxRunAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (graphFile != null && graphFile.Graph != null) btnRun.Enabled = true;
            algorithm = (IShortestPathProblemAlgorithm)cbxRunAlgorithm.SelectedItem;
            lblMessage.Text = Align(algorithm.Description);
            lastResult = null;
        }
        private void cbxTurnProhibitionsGraphModifiers_SelectedIndexChanged(object sender, EventArgs e)
        {
            turnProhibitionsGraphModifier = (ITurnProhibitionsAssociatedGraph)cbxTurnProhibitionsGraphModifiers.SelectedItem;
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            if(algorithm != null)
            {
                if (points.Count != 2)
                {
                    // throw an Exception, show a message, or do nothing.
                    MessageBox.Show("Select two markers to run algorithm", "Incorrect input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                
                {
                    #region Getting Start and End Nodes
                    var sp = new PointLatLong(points[0].Lat, points[0].Lng);
                    var ep = new PointLatLong(points[1].Lat, points[1].Lng);
                    (Node, Node) starEndNodes = Services.Algorithms.AlgorithmUtils.NearestStarEndPoints(graphFile.Graph, sp, ep);
                    Node startNode = starEndNodes.Item1;
                    Node endNode = starEndNodes.Item2;
                    #endregion

                    Graph dual;
                    if (chbTrafficRestrictions.Checked)
                    {
                        if(chbUseTurnProhibitions.Checked)
                        {
                            if(!readyToRunAlgorithm)
                            {
                                bool automaticAddition = MessageBox.Show("Do you want to add automatic prohibitions?", "Add automatic prohibitions?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                                if (automaticAddition)
                                    graphFile.Graph.AutomaticProhibitions();
                                readyToRunAlgorithm = MessageBox.Show("Do you want to edit the prohibitions?", "Edit prohibitions?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
                                if (!readyToRunAlgorithm)
                                {

                                    mapAction = MapActions.AddTurnProhibition;
                                    menuViewNodes.Checked = true;
                                    btnRun.Enabled = false;
                                    btnAddProhibition.Visible = true;
                                    btnDeleteProhibition.Visible = true;
                                    btnDoneEditingTurnProhibitions.Visible = true;
                                    ViewTurnProhibitions();
                                    chbUseTurnProhibitions.Enabled = false;
                                    chbTrafficRestrictions.Enabled = false;
                                    return;
                                }
                            }
                            dual = turnProhibitionsGraphModifier.DualGraphBuilder(graphFile.Graph, startNode.Id, endNode.Id);
                            startNode = dual.NodesId[turnProhibitionsGraphModifier.dualStartNodeId];
                            endNode = dual.NodesId[turnProhibitionsGraphModifier.dualEndNodeId];
                        }
                        else
                        {
                            dual = graphFile.Graph;
                        }
                    }
                    else
                    {
                        //readyToRunAlgorithm = true;
                        dual = Services.Algorithms.AlgorithmUtils.NonDirectedGraph(graphFile.Graph);
                        startNode = dual.NodesId[startNode.Id];
                        endNode = dual.NodesId[endNode.Id];
                    }

                    readyToRunAlgorithm = false;
                    
                    points = new List<PointLatLng>();
                    ShortestPathProblemOutput result = algorithm.Run(dual.Clone() as Graph, startNode, endNode);
                    graphFile.Graph.TurnProhibitions.Clear();
                    if (chbUseTurnProhibitions.Checked)
                    {
                        List<int> path = new List<int>();
                        foreach (int node in (result as ShortestPathProblemOutput).NodesId)
                        {
                            //path.Add(node >= ppg.Item2 ? ppg.Item3[node - ppg.Item2] : node);
                            path.Add(node);
                        }
                        path = turnProhibitionsGraphModifier.EquivalentPath(path);
                        result = new ShortestPathProblemOutput(result.Distance, path, path.Select(n => new PointLatLong(graphFile.Graph.NodesId[n].Latitude, graphFile.Graph.NodesId[n].Longitude)), result.startPoint, result.endPoint);
                    }
                    result = new ShortestPathProblemOutput(result.Distance, result.NodesId, result.Points, sp, ep);
                    lastResult = result;

                    //Show Result

                    IResultDisplayer resultDisplayer = cbxVisualizationForm.SelectedItem as IResultDisplayer;
                    resultDisplayer.Display(result, gmap);
                    
                }
            }
           
        }

        private void btnAddProhibition_Click(object sender, EventArgs e)
        {
            if (mapAction == MapActions.AddTurnProhibition)
            {
                if (turnProhibitionNodes.Item1 != null && turnProhibitionNodes.Item2 != null && turnProhibitionNodes.Item3 != null)
                {
                    if (!turnProhibitionNodes.Item1.Edges.Where(edge => (edge.ToNode == turnProhibitionNodes.Item2) || edge.FromNode == turnProhibitionNodes.Item2).Any()  && !turnProhibitionNodes.Item1.OutgoingArcs.Where(arc => arc.ToNode == turnProhibitionNodes.Item2).Any())
                        if (!turnProhibitionNodes.Item2.Edges.Where(edge => (edge.ToNode == turnProhibitionNodes.Item3) || edge.FromNode == turnProhibitionNodes.Item3).Any() && !turnProhibitionNodes.Item2.OutgoingArcs.Where(arc => arc.ToNode == turnProhibitionNodes.Item3).Any())
                        {
                            MessageBox.Show($"Invalid Turn Prohibition");
                            menuViewNone.Checked = true;
                            menuViewNodes.Checked = true;
                            turnProhibitionNodes = (null, null, null);
                            ViewTurnProhibitions();//Refresh Turn prohibitions overlay
                            return;
                        }
                    
                    if (!graphFile.Graph.TurnProhibitions.Contains(turnProhibitionNodes))
                    { graphFile.Graph.TurnProhibitions.Add(turnProhibitionNodes); }
                    //graphFile.Graph.SplitNode(turnProhibitionNodes.Item1, turnProhibitionNodes.Item2, turnProhibitionNodes.Item3);
                    MessageBox.Show($"Turn Prohibition Added");
                    menuViewNone.Checked = true;
                    menuViewNodes.Checked = true;

                    turnProhibitionNodes = (null, null, null);
                    ViewTurnProhibitions();//Refresh Turn prohibitions overlay
                }
                else
                    MessageBox.Show($"You need to mark the three nodes that make up the prohibition");

            }
            else 
            {
                mapAction = MapActions.AddTurnProhibition;
                ViewTurnProhibitions();
            }

        }
        private void btnDoneEditingTurnProhibitions_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you done editing the prohibitions?", "Done?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                turnProhibitionNodes = (null, null, null);
                readyToRunAlgorithm = true;
                mapAction = MapActions.None;
                btnAddProhibition.Visible = false;
                btnDeleteProhibition.Visible = false;
                btnDoneEditingTurnProhibitions.Visible = false;
                btnRun.Enabled = true;
                HideTurnProhibitions();
                chbTrafficRestrictions.Enabled = true;
                chbUseTurnProhibitions.Enabled = true;
                btnRun_Click(sender, e);
                
            }

        }

        private void btnDeleteProhibition_Click(object sender, EventArgs e)
        {
            if (mapAction == MapActions.DeleteTurningProhibition)
            {
                if (turnProhibitionNodes.Item1 != null && turnProhibitionNodes.Item2 != null && turnProhibitionNodes.Item3 != null)
                {
                    graphFile.Graph.TurnProhibitions.Remove(turnProhibitionNodes);
                    //graphFile.Graph.SplitNode(turnProhibitionNodes.Item1, turnProhibitionNodes.Item2, turnProhibitionNodes.Item3);
                    MessageBox.Show($"Turn Prohibition Deleted");
                    menuViewNone.Checked = true;
                    menuViewNodes.Checked = true;

                    turnProhibitionNodes = (null, null, null);
                    ViewTurnProhibitions();//Refresh Turn prohibitions overlay
                }
                else
                    MessageBox.Show($"You need to mark the three nodes that make up the prohibition");
            }
            else
            {
                mapAction = MapActions.DeleteTurningProhibition;
                ViewTurnProhibitions();
            }
        }

        #endregion

        #region Other Methods

        void Lock(Control control, string message = "")
        {
            lblMessage.Text = message;
            if (control != null) control.Enabled = !control.Enabled;
            else
            {
                pnlMap.Enabled = !pnlMap.Enabled;
                pnlMenu.Enabled = !pnlMenu.Enabled;
                pnlToolsBar.Enabled = !pnlToolsBar.Enabled;
            }
        }

        string Align(string s, int width = 30)
        {
            List<int> spacesPosition = new List<int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ') spacesPosition.Add(i);
            }
            spacesPosition.Add(s.Length);

            string resp = "";
            int length = 0;
            int lineCount = 0;
            foreach (var item in spacesPosition)
            {
                if(lineCount + item - length > width)
                {
                    resp += "\n";
                    length++;
                    lineCount = 0;
                }
                resp += s.Substring(length, item - length);
                lineCount += item - length;
                length = item;
            }
            return resp;
        }

        void AsyncDrawM(object s)
        {
            
            var tmp = new List<PointLatLng> { points[0] };
            var route = new GMapRoute(tmp, "path");
            gmap.Overlays[gmap.Overlays.Count - 1].Routes.Add(route);
            GMapMarker car = new GMarkerGoogle(points[0], new Bitmap("Resources\\BitMaps\\purple_car.png"));
            gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(car);
            for (int i = 1; i < points.Count; i++)
            {
                Thread.Sleep(300);
                tmp.Add(points[i]);
                gmap.Overlays[gmap.Overlays.Count - 1].Routes.Clear();
                route = new GMapRoute(tmp, "path");
                gmap.Overlays[gmap.Overlays.Count - 1].Routes.Add(route);
                gmap.Overlays[gmap.Overlays.Count - 1].Markers.Remove(car);
                car = new GMarkerGoogle(points[i], new Bitmap("Resources\\BitMaps\\purple_car.png"));
                gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(car);
            }
            globalOverlay.Markers[globalOverlay.Markers.Count - 1].ToolTipText = "End(" + s.ToString();
        }


        #endregion

        private void cbxVisualizationForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lastResult != null)
            {
                IResultDisplayer resultDisplayer = cbxVisualizationForm.SelectedItem as IResultDisplayer;
                resultDisplayer.Display(lastResult, gmap);
            }
        }

        private void ViewTurnProhibitions()
        {
            GMapOverlay prohibitionsOverlay = gmap.Overlays.FirstOrDefault(o => o.Id == "prohibitions");

            if (!gmap.Overlays.Contains(prohibitionsOverlay))
            {
                prohibitionsOverlay = new GMapOverlay();
                prohibitionsOverlay.Id = "prohibitions";
                Envelope envelope = gmap.GetEnvelope();
                //prohibitionsOverlay = gmap.GetNodesOverlay(graphFile.Graph, envelope);
                gmap.Overlays.Add(prohibitionsOverlay);
            }
            prohibitionsOverlay.Markers.Clear();
            int prohibitionsOverlayIndex = gmap.Overlays.IndexOf(prohibitionsOverlay);
            if (prohibitionsOverlayIndex != gmap.Overlays.Count-1)
            {
                gmap.Overlays.RemoveAt(prohibitionsOverlayIndex);
                gmap.Overlays.Add(prohibitionsOverlay);
            }
            //menuViewNodes.Checked = false;
            //menuViewNodes.Checked = true;
            Dictionary<(double,double), List<(Node,Node,Node)>> prohibitions = new Dictionary<(double, double), List<(Node,Node,Node)>>();
            foreach (var tp in graphFile.Graph.TurnProhibitions)
            {
                double lat = tp.Item2.Latitude;
                double lon = tp.Item2.Longitude;
                if (prohibitions.ContainsKey((lat,lon)))
                {
                    prohibitions[(lat, lon)].Add(tp);
                    //prohibitions[(lat, lon)].Add($"{tp.Item1.Id} - {tp.Item2.Id} - {tp.Item3.Id}");
                }
                else
                {
                    prohibitions.Add((lat, lon), new List<(Node, Node, Node)>() { tp });
                    //prohibitions.Add((lat, lon), new List<string> { $"{tp.Item1.Id} - {tp.Item2.Id} - {tp.Item3.Id}" });
                }
            }
            foreach (var prohibition in prohibitions)
            {
                double lat = prohibition.Key.Item1;
                double lon = prohibition.Key.Item2;
                string tag = "";
                foreach (var nodes in prohibition.Value)
                {
                    tag += $"{nodes.Item1.Id} - {nodes.Item2.Id} - {nodes.Item3.Id}" + "\n ";
                }
                GMapMarker mark = new GMarkerGoogle(new PointLatLng(lat, lon), new Bitmap("Resources\\BitMaps\\red_cross.png"));
                mark.ToolTipText = tag;
                mark.Tag = prohibition.Value;
                //gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(mark);
                prohibitionsOverlay.Markers.Add(mark);
                //gmap.globalOverlay.Markers.Add(mark);
                
            }


        }

        private void HideTurnProhibitions()
        {
            GMapOverlay prohibitionsOverlay = gmap.Overlays.FirstOrDefault(o => o.Id == "prohibitions");
            if (prohibitionsOverlay != null)
            {
                gmap.Overlays.Remove(prohibitionsOverlay);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (lastResult!=null)
            {
                IResultExporter resultExporter = cbxExportResult.SelectedItem as IResultExporter;
                resultExporter.Export(graphFile.Graph, lastResult, gmap);
            }
            else
            {
                MessageBox.Show("There is no result available");
            }


        }

        private void chbUseTurnProhibitions_CheckedChanged(object sender, EventArgs e)
        {
            if (chbUseTurnProhibitions.Checked)
            {
                chbTrafficRestrictions.Checked = true;
                cbxTurnProhibitionsGraphModifiers.Visible = true;
            }
            else
                cbxTurnProhibitionsGraphModifiers.Visible = false;
        }

        private void chbTrafficRestrictions_CheckedChanged(object sender, EventArgs e)
        {
            if (!chbTrafficRestrictions.Checked)
                chbUseTurnProhibitions.Checked = false;
        }


    }


    /// <summary>
    /// Draw strings marker
    /// </summary>
   


}
