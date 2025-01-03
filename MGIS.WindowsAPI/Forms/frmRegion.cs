using NGAT.Business.Domain.Core;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using NGAT.Geo.Geometries;
using NGAT.Services.IO.MapFiles;
using GMap.NET.WindowsForms.Markers;
using NGAT.Services.Algorithms;
using DotSpatial.Data;
using NGAT.Services.Layers;
using OsmSharp.Streams;
using OsmSharp;
using NGAT.Services.ForbiddenTurns;
using System.IO;
using NGAT.Business.Contracts.IO;
using NGAT.Services.IO;
using NGAT.Business.Contracts.Services.DataPointsImportFormat;
using ExcelDataReader;
using NGAT.Business.Contracts.Services.ResultDisplayers;
using NGAT.Services.ResultDisplayers;
using NGAT.Business.Contracts.Services.Algorithms;
using Node = NGAT.Business.Domain.Core.Node;
using NGAT.Services;

namespace NGAT.WindowsAPI.Forms
{
    public partial class frmRegion : Form
    {

        #region Private Fields
        Graph graph;
        Polygon polygon;
        PointLatLng homePosition;
        double homeZoom;
        RoutingProblemFile resultFile;
        MapActions mapAction;
        List<PointLatLng> inputPoints;
        (Business.Domain.Core.Node, Business.Domain.Core.Node) link;
        List<IFeature> features;
        GMapMarker arpMarker;
        bool checkConnectivity;
        List<string> nodesData;
        IDataPointsImporter dataPointsImporter;
        bool readyToExport;
        (Business.Domain.Core.Node, Business.Domain.Core.Node, Business.Domain.Core.Node) turnProhibitionNodes;


        private class Route
        {
            public List<int> Nodes { get; }
            public int Id { get; }

            public Route(List<int> nodes, int id)
            {
                Nodes = nodes;
                Id = id;
            }

            public override string ToString()
            {
                return "Route " + Id;
            }
        }

        Route route;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor to select the input.
        /// </summary>
        /// <param name="subGraph"></param>
        /// <param name="gMap"></param>
        /// <param name="region"></param>
        public frmRegion(Graph subGraph, GMapControl gMap, Polygon region, bool checkConnectivity = false)
        {
            InitializeComponent();
            mapAction = MapActions.None;
            graph = subGraph;
            polygon = region;
            inputPoints = new List<PointLatLng>();
            features = new List<IFeature>();
            this.checkConnectivity = checkConnectivity;
            readyToExport = false;

            // Initial GMap Control settings
            gmap.Manager.Mode = gMap.Manager.Mode;
            gmap.CacheLocation = gMap.CacheLocation;
            gmap.ShowCenter = false;
            gmap.DragButton = MouseButtons.Left;
            gmap.CanDragMap = true;
            gmap.MapProvider = GMapProviders.GoogleMap;
            gmap.AutoScroll = false;
            gmap.Position = gMap.Position;
            gmap.Zoom = gMap.Zoom;

            foreach (var item in gMap.Overlays)
            {
                gmap.Overlays.Add(item);
            }
            menuViewNone.Checked = true;

        }

        /// <summary>
        /// Constructor to display the output.
        /// </summary>
        /// <param name="resultFile"></param>
        /// <param name="gMap"></param>
        public frmRegion(RoutingProblemFile resultFile, GMapControl gMap)
        {
            InitializeComponent();
            mapAction = MapActions.None;
            this.resultFile = resultFile;
            graph = resultFile.Graph;

            // Initial GMap Control settings
            gmap.Manager.Mode = gMap.Manager.Mode;
            gmap.CacheLocation = gMap.CacheLocation;
            gmap.ShowCenter = false;
            gmap.DragButton = MouseButtons.Left;
            gmap.CanDragMap = true;
            gmap.MapProvider = GMapProviders.GoogleMap;
            gmap.AutoScroll = false;
            gmap.Position = gMap.Position;
            gmap.Zoom = gMap.Zoom;
            lblClear.Visible = false;
            lblExportVRP.Visible = false;
            lblExportARP.Visible = false;
            lblExportTSP.Visible = false;
            gmap.Overlays.Add(new GMapOverlay("vrpInput"));

            dynamic fileOutput = resultFile.Output;
            if (resultFile.Output != null && fileOutput.Count > 0)
            {
                lblSelectRoute.Visible = true;
                cbxSelectVehicle.Visible = true;
                lblShowRoute.Visible = true;
                lblDisplayRoute.Visible = true;

                for (int i = 0; i < fileOutput.Count; i++)
                {
                    cbxSelectVehicle.Items.Add(new Route(fileOutput[i], i+1));
                }

            }
            else if(resultFile.ProblemType == RoutingProblemType.VRP)
            {
                mapAction = MapActions.SelectingVRPInput;
                lblExportVRP.Visible = true;
                lblExportARP.Visible = false;
                lblExportVRP.Text = "Done";
                inputPoints = new List<PointLatLng>();
                foreach (var item in (List<int>)resultFile.Input)
                {
                    var point = new PointLatLng(graph.NodesId[item].Latitude,
                        graph.NodesId[item].Longitude);
                    var marker = new GMarkerGoogle(point, new Bitmap("Resources\\BitMaps\\blue_point.png"));
                    gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(marker);
                    inputPoints.Add(point);
                }
                lblAddPoint.Visible = true;
                tbxLat.Visible = true;
                tbxLong.Visible = true;
            }
            else if(resultFile.ProblemType == RoutingProblemType.ARP)
            {
                mapAction = MapActions.SelectingARPInput;
                lblExportARP.Visible = true;
                lblExportVRP.Visible = false;
                lblExportARP.Text = "Done";
                int nodeId = (int)resultFile.Input;
                if (nodeId > -1)
                {
                    var node = graph.NodesId[nodeId];
                    arpMarker = new GMarkerGoogle(new PointLatLng(node.Latitude, node.Longitude), new Bitmap("Resources\\BitMaps\\blue_point.png"));
                }
            }

        }

        #endregion

        #region Form Related Events

        private void frmRegion_Load(object sender, EventArgs e)
        {
            foreach (var imp in Utils.GetSubClasses<IDataPointsImporter>())
                cbxDataFormat.Items.Add(imp);
            foreach (var imp in Utils.GetSubClasses<IShortestPathProblemAlgorithm>()) 
                cbxSelectAlgorithm.Items.Add(imp);
            foreach (var imp in Utils.GetSubClasses<ITurnProhibitionsAssociatedGraph>())
            {
                cbxTurnProhibitionsGraphModifier.Items.Add(imp);
            }
            if (polygon != null)
                gmap.ShowRegion(polygon);
            else if(resultFile != null)
            {
                gmap.ShowRegion(graph);
            }
                

            homePosition = gmap.Position;
            homeZoom = gmap.Zoom;

            //Check Connectivity
            try
            {
                if (graph != null && checkConnectivity)
                {
                    var connected = new CheckConectivity();
                    List<(Business.Domain.Core.Node, Business.Domain.Core.Node)> disconnected = (List<(Business.Domain.Core.Node, Business.Domain.Core.Node)>)connected.Run(graph.Clone() as Graph);
                    if (disconnected.Count > 0 // Subgraph is not strongly connected            
                       && MessageBox.Show("The subgraph is not strongly connected. Do you want to edit It?", "Disconnected Graph",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MessageBox.Show("Disconnected nodes has been highlighted with a blue ring.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MostDisconnectedNode(disconnected);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error ocurred while selecting region.");
            }
            if(features != null)
            {
                Shapefile shp = Shapefile.OpenFile("Resources\\ShapeFiles\\cub_admbnda_adm2_2019.shp");
                var filter = new HavanaMunicipalitiesFilter();
                foreach (var feature in shp.Features)
                    if (filter.Match(feature))
                        features.Add(feature);
                shp.Close();
            }
            

        }

        #endregion

        #region GMap Related Events

        private void gmap_Load(object sender, EventArgs e)
        {
            var overlay = new GMapOverlay("vrpInput");
            gmap.Overlays.Add(overlay);
            if (resultFile != null && resultFile.ProblemType == RoutingProblemType.VRP)
            {
                dynamic input = resultFile.Input;
                foreach (int item in input)
                {
                    var node = graph.Nodes[(int)item - 1];
                    var marker = new GMarkerGoogle(new PointLatLng(node.Latitude, node.Longitude), new Bitmap("Resources\\BitMaps\\blue_point.png"));
                    overlay.Markers.Add(marker);
                }
                gmap.Refresh();
            }
        }

        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && (mapAction == MapActions.SelectingVRPInput || mapAction == MapActions.AddingArc || mapAction == MapActions.AddingEdge || mapAction == MapActions.AddingNode))
            {
                if(item.Overlay.Id == "vrpInput" && mapAction == MapActions.SelectingVRPInput) // deselect point
                {
                    inputPoints.Remove(item.Position);
                    item.Overlay.Markers.Remove(item);
                    if (inputPoints.Count <= 2)
                    {
                        lblExportVRP.Text = "Export as VRP";
                        lblExportVRP.Enabled = false;
                    }
                    gmap.Refresh();
                }
                else if(item.Overlay.Id == "nodes" && (mapAction == MapActions.AddingArc || mapAction == MapActions.AddingEdge || mapAction == MapActions.AddingNode)) // remove the node
                {
                    // remove its links
                    menuViewArcs.Checked = true;
                    menuViewEdges.Checked = true;
                    var edges = gmap.Overlays.First(o => o.Id == "edges");
                    for (int i = 0; i < edges.Routes.Count; i++)
                        if (edges.Routes[i].Points.Contains(item.Position))
                        {
                            edges.Routes.RemoveAt(i);
                            i = 0;
                        }

                    var arcs = gmap.Overlays.First(o => o.Id == "arcs");
                    for (int i = 0; i < arcs.Routes.Count; i++)
                        if (arcs.Routes[i].Points.Contains(item.Position))
                        {
                            arcs.Routes.RemoveAt(i);
                            i = 0;
                        }
                    // remove the node
                    Business.Domain.Core.Node node = graph.NodesId[(int)item.Tag];
                    graph.DeleteNode(node);
                    item.Overlay.Markers.Remove(item);
                }
            }
            else if (e.Button == MouseButtons.Right && (mapAction == MapActions.AddTurnProhibition || mapAction == MapActions.DeleteTurningProhibition) && item.Tag != null && item.Tag is int)
            {
                if (turnProhibitionNodes.Item1 == graph.NodesId[(int)item.Tag])
                {
                    turnProhibitionNodes.Item1 = null;
                    item.Size = new Size(item.Size.Width / 2, item.Size.Height / 2);
                    MessageBox.Show($"First marked node has been unmarked. Next node you marked will fill the smallest empty position");
                }
                else if (turnProhibitionNodes.Item2 == graph.NodesId[(int)item.Tag])
                {
                    turnProhibitionNodes.Item2 = null;
                    item.Size = new Size(item.Size.Width / 2, item.Size.Height / 2);
                    MessageBox.Show($"Second marked node has been unmarked. Next node you marked will fill the smallest empty position");
                }
                else if (turnProhibitionNodes.Item3 == graph.NodesId[(int)item.Tag])
                {
                    turnProhibitionNodes.Item3 = null;
                    item.Size = new Size(item.Size.Width / 2, item.Size.Height / 2);
                    MessageBox.Show($"Third marked node has been unmarked. Next node you marked will fill the smallest empty position");
                }
            }
            else if((mapAction == MapActions.AddingArc || mapAction == MapActions.AddingEdge) && e.Button == MouseButtons.Left)
            {
                if (link.Item1 == null) link.Item1 = graph.NodesId[(int)(item.Tag)];
                else
                {
                    bool directed = mapAction == MapActions.AddingArc;
                    link.Item2 = graph.NodesId[(int)(item.Tag)];
                    graph.AddLink(link.Item1.Id, link.Item2.Id, new LinkData(), directed);
                    GMapRoute route = new GMapRoute(new PointLatLng[] { new PointLatLng(link.Item1.Latitude, link.Item1.Longitude), new PointLatLng(link.Item2.Latitude, link.Item2.Longitude)}, "");
                    if (directed) gmap.Overlays.First(o => o.Id == "arcs").Routes.Add(route);
                    else gmap.Overlays.First(o => o.Id == "edges").Routes.Add(route);
                    //mapAction = MapActions.None;
                    link.Item1 = null;
                    link.Item2 = null;
                    var connected = new CheckConectivity();
                    List<(Business.Domain.Core.Node, Business.Domain.Core.Node)> disconnected = (List<(Business.Domain.Core.Node, Business.Domain.Core.Node)>)connected.Run(graph);
                    MostDisconnectedNode(disconnected);
                }
            }
            else if(mapAction == MapActions.SelectingARPInput && e.Button == MouseButtons.Left)
            {
                lblExportARP.Enabled = true;
                lblExportARP.Text = "Done";
                var overlay = gmap.Overlays.First(o => o.Id == "nodes");
                if (arpMarker != null)
                    overlay.Markers.Remove(arpMarker);
                arpMarker = new GMarkerGoogle(new PointLatLng(item.Position.Lat, item.Position.Lng), new Bitmap("Resources\\BitMaps\\blue_point.png")) { Tag = item.Tag};
                arpMarker.ToolTipText = arpMarker.Tag.ToString();
                overlay.Markers.Add(arpMarker);
            }
            else if ((mapAction == MapActions.AddTurnProhibition || mapAction == MapActions.DeleteTurningProhibition) && e.Button == MouseButtons.Left  && item.Tag != null && item.Tag is int)
            {
                ////Checking not double marked nodes
                //if (turnProhibitionNodes.Item1 == graph.NodesId[(int)item.Tag]
                //    || turnProhibitionNodes.Item2 == graph.NodesId[(int)item.Tag]
                //    || turnProhibitionNodes.Item3 == graph.NodesId[(int)item.Tag]
                //    )
                //    return;

                if (turnProhibitionNodes.Item1 == null)
                {
                    turnProhibitionNodes.Item1 = graph.NodesId[(int)item.Tag];
                    item.Size = new Size(2 * item.Size.Width, 2 * item.Size.Height);
                }
                else if (turnProhibitionNodes.Item2 == null)
                {
                    //Checking not double marked nodes
                    if (turnProhibitionNodes.Item1 == graph.NodesId[(int)item.Tag])
                        return;
                    turnProhibitionNodes.Item2 = graph.NodesId[(int)item.Tag];
                    item.Size = new Size(2 * item.Size.Width, 2 * item.Size.Height);
                }
                else if (turnProhibitionNodes.Item3 == null)
                {
                    //Checking not double marked nodes
                    if (turnProhibitionNodes.Item2 == graph.NodesId[(int)item.Tag])
                        return;
                    turnProhibitionNodes.Item3 = graph.NodesId[(int)item.Tag];
                    item.Size = new Size(2 * item.Size.Width, 2 * item.Size.Height);

                }
            }
        }

        private void gmap_MouseClick(object sender, MouseEventArgs e)
        {

            PointLatLng clickLatLong = gmap.FromLocalToLatLng(e.X, e.Y);

            if (mapAction == MapActions.SelectingVRPInput && e.Button == MouseButtons.Left)
            {
                if(inputPoints.Count > 1)
                {
                    lblExportVRP.Text = "Done";
                    lblExportVRP.Enabled = true;
                }
                // draw the marker and store the point
                inputPoints.Add(clickLatLong);
                var marker = new GMarkerGoogle(clickLatLong, new Bitmap("Resources\\BitMaps\\blue_point.png"));
                
                foreach (var overlay in gmap.Overlays)
                    if (overlay.Id == "vrpInput")
                    {
                        overlay.Markers.Add(marker);
                        break;
                    }
            }
            else if(mapAction == MapActions.AddingNode && e.Button == MouseButtons.Left)
            {
                int id = graph.AddNode(clickLatLong.Lat, clickLatLong.Lng, new Dictionary<string, string>());
                gmap.Overlays.First(o => o.Id == "nodes").Markers.Add(new GMarkerGoogle(clickLatLong, new Bitmap("Resources\\BitMaps\\blue_border_red_point.png")) { Tag = id});
            }
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

        #endregion

        #region Map Controls Related Events

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            gmap.Zoom += 1;
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            gmap.Zoom -= 1;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            gmap.Position = homePosition;
            gmap.Zoom = homeZoom;
        }

        private void lblExportVRP_Click(object sender, EventArgs e)
        {
            bool TraficRestrictions = true;
            if (mapAction == MapActions.AddTurnProhibition || mapAction == MapActions.DeleteTurningProhibition)
            {
                if (MessageBox.Show("Are you done editing prohibitions?", "Done?", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                else
                {
                    turnProhibitionNodes = (null, null, null);
                    mapAction = MapActions.None;
                    btnAddProhibition.Visible = false;
                    btnDeleteProhibition.Visible = false;
                }
            }
            
            if (mapAction != MapActions.SelectingVRPInput &&
                mapAction != MapActions.AddTurnProhibition &&
                mapAction != MapActions.DeleteTurningProhibition 
                && inputPoints.Count == 0)
            {
                MessageBox.Show("Please, select the input points and set the algorithm settings.", "Select input");
                mapAction = MapActions.SelectingVRPInput;
                btnImportData.Visible = true;
                cbxDataFormat.Visible = true;
                lblImportDataFormat.Visible = true;
                lblAddPoint.Visible = true;
                cbxKeepLocation.Visible = true;
                tbxLat.Visible = true;
                tbxLong.Visible = true;
                inputPoints = new List<PointLatLng>();
                lblExportVRP.Enabled = false;
                lblExportARP.Enabled = false;
                cbxSelectAlgorithm.Visible = true;
                cbxSelectAlgorithm.SelectedIndex = 0;
                chbTrafficRestrictions.Visible = true;
                chbTurnProhibitions.Visible = true;
                lblExportVRP.Text = "Export as NRP file";
                //if (!menuViewNodes.Checked) menuViewNodes.Checked = true;
                return;
            }

            ///////////////////////////////////////////////////////

            if (mapAction == MapActions.SelectingVRPInput)
            {
                chbTrafficRestrictions.Enabled = false;
                chbTurnProhibitions.Enabled = false;
                cbxSelectAlgorithm.Enabled = false;
                cbxTurnProhibitionsGraphModifier.Enabled = false;
                if (chbTrafficRestrictions.Checked)
                {
                    if (chbTurnProhibitions.Checked)
                    {
                        if (!readyToExport)
                        {
                            bool automaticAddition = MessageBox.Show("Do you want to add automatic prohibitions?", "Add automatic prohibitions?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                            if (automaticAddition)
                                graph.AutomaticProhibitions();
                            readyToExport = MessageBox.Show("Do you want to edit the prohibitions?", "Edit prohibitions?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
                            if (!readyToExport)
                            {
                                mapAction = MapActions.AddTurnProhibition;
                                btnImportData.Visible = false;
                                cbxDataFormat.Visible = false;
                                lblImportDataFormat.Visible = false;
                                lblAddPoint.Visible = false;
                                cbxKeepLocation.Visible = false;
                                tbxLat.Visible = false;
                                tbxLong.Visible = false;

                                mapAction = MapActions.AddTurnProhibition;
                                menuViewNodes.Checked = true;
                                btnAddProhibition.Visible = true;
                                btnDeleteProhibition.Visible = true;
                                ViewTurnProhibitions();


                                //CODE FOR EDIT PROHIBITIONS
                                readyToExport = true;
                                return;
                            }
                        }
 
                    }
                    else
                    {
                        readyToExport = true;
                    }
                }
                else
                {
                    readyToExport = true;
                }
            }
            ///////////////////////////////////////////////////////

            //if (!readyToExport)
            //{
            //    TraficRestrictions = chbTrafficRestrictions.Checked;

            //    if (TraficRestrictions)
            //    {
            //        bool AutomaticTurnProhibitions = MessageBox.Show("Do you want to add automatic prohibitions?", "Add automatic prohibitions?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            //        if (AutomaticTurnProhibitions)
            //            graph.AutomaticProhibitions();

            //        readyToExport = MessageBox.Show("Do you want to edit the prohibitions?", "Edit prohibitions?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
            //        if (!readyToExport)
            //        {
            //            mapAction = MapActions.AddTurnProhibition;
            //            btnImportData.Visible = false;
            //            cbxDataFormat.Visible = false;
            //            lblImportDataFormat.Visible = false;
            //            lblAddPoint.Visible = false;
            //            cbxKeepLocation.Visible = false;
            //            tbxLat.Visible = false;
            //            tbxLong.Visible = false;

            //            mapAction = MapActions.AddTurnProhibition;
            //            menuViewNodes.Checked = true;
            //            btnAddProhibition.Visible = true;
            //            btnDeleteProhibition.Visible = true;
            //            ViewTurnProhibitions();
                        

            //            //CODE FOR EDIT PROHIBITIONS
            //            readyToExport = true;
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        readyToExport = true;
            //    }
            //}

            if (readyToExport)
            {
                List<int> inputNodesIndex = new List<int>();
                if (!cbxKeepLocation.Checked) //// Insert the new nodes in the graph
                {
                    foreach (var point in inputPoints)
                    {
                        int id = graph.InsertPoint(point.Lat, point.Lng);
                        inputNodesIndex.Add(graph.Nodes.IndexOf(graph.NodesId[id]));
                    }
                }
                else  //// Keep their location and connect them to the nearest node
                {
                    foreach (var point in inputPoints)
                    {
                        int nearestID = graph.GetNearestNode(point.Lat, point.Lng);
                        int Id = graph.AddNode(point.Lat, point.Lng, new Dictionary<string, string>());
                        graph.AddLink(Id, nearestID, new LinkData(), false);
                        inputNodesIndex.Add(graph.Nodes.Count - 1);
                    }
                }



                //// Check connectivity among the input nodes

                bool disconnected = false;
                try
                {
                    var floydWarshall = new FloydWarshall();
                    var distances = floydWarshall.Run(graph) as double[,];
                    for (int i = 0; i < inputNodesIndex.Count; i++)
                    {
                        for (int j = 0; j < inputNodesIndex.Count; j++)
                        {
                            if (distances[inputNodesIndex[i], inputNodesIndex[j]] == double.PositiveInfinity)
                            {
                                disconnected = true;
                                break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    disconnected = false;
                }


                if (disconnected)
                    MessageBox.Show("One or more of the input nodes are disconnected from the graph. Edit the graph to avoid disconnections");
                //throw new Exception("One or more of the input nodes are disconnected from the graph. Edit the graph to avoid disconnections");

                RoutingProblemFile file = new RoutingProblemFile(graph, chbTrafficRestrictions.Checked ? RoutingProblemType.VRP : RoutingProblemType.NRP, (IShortestPathProblemAlgorithm)cbxSelectAlgorithm.SelectedItem, chbTurnProhibitions.Checked? (ITurnProhibitionsAssociatedGraph)cbxTurnProhibitionsGraphModifier.SelectedItem : null, features, nodesData);
                dynamic fileInput = file.Input;
                foreach (var index in inputNodesIndex)
                    fileInput.Add(graph.Nodes[index].Id);
                Save(file);
                lblAddPoint.Visible = false;
                tbxLat.Visible = false;
                tbxLong.Visible = false;


                // Readjusting parameters
                mapAction = MapActions.None;
                btnImportData.Visible = false;
                cbxDataFormat.Visible = false;
                lblImportDataFormat.Visible = false;
                lblAddPoint.Visible = false;
                cbxKeepLocation.Visible = false;
                inputPoints = new List<PointLatLng>();
                lblExportVRP.Enabled = true;
                lblExportARP.Enabled = true;

                chbTrafficRestrictions.Enabled = true;
                chbTurnProhibitions.Enabled = true;
                cbxSelectAlgorithm.Enabled = true;
                cbxSelectAlgorithm.Visible = false;
                chbTrafficRestrictions.Visible = false;
                chbTurnProhibitions.Visible = false;
                chbTrafficRestrictions.Checked = false;

                graph.TurnProhibitions.Clear();
                gmap.Overlays.Clear();

            }
        }

        private void lblExportARP_Click(object sender, EventArgs e)
        {
            if (mapAction != MapActions.SelectingARPInput)
            {                
                if (MessageBox.Show("Do you want to select the initial node?", "Select Input", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    mapAction = MapActions.SelectingARPInput;
                    lblExportVRP.Enabled = false;
                    lblExportARP.Enabled = false;
                    menuViewNodes.Checked = true;
                    if (arpMarker != null)
                    { 
                        gmap.Overlays.First(o => o.Id == "nodes").Markers.Add(arpMarker);
                        lblExportARP.Enabled = true;
                        lblExportARP.Text = "Done";
                    }
                }
                else
                {
                    RoutingProblemFile file = new RoutingProblemFile(graph, RoutingProblemType.ARP, null, null, features);
                    file.Input = -1;
                    Save(file);
                }
                return;
            }
            int id = arpMarker == null ? -1 : (int)arpMarker.Tag;
            Save(new RoutingProblemFile(graph, RoutingProblemType.ARP, null, null, features) { Input = id });
        }

        private void lblExportTSP_Click(object sender, EventArgs e)
        {
            if (mapAction != MapActions.SelectingTSPInput && inputPoints.Count == 0)
            {
                MessageBox.Show("Please, select the input points.", "Select input");
                mapAction = MapActions.SelectingTSPInput;
                btnImportData.Visible = true;
                //lblAddPoint.Visible = true;
                //tbxLat.Visible = true;
                //tbxLong.Visible = true;
                inputPoints = new List<PointLatLng>();
                lblExportVRP.Enabled = false;
                lblExportARP.Enabled = false;
                lblExportTSP.Enabled = false;
                lblExportTSP.Text = "Export as TSP";
                //if (!menuViewNodes.Checked) menuViewNodes.Checked = true;
                return;
            }

            //// Create a new graph from the input points
            graph = new Graph();

            //// Add nodes
            List<int> inputNodesIndex = new List<int>();
            for (int i = 0; i < inputPoints.Count(); i++)
            {
                var attrs = new Dictionary<string, string>();
                attrs.Add("Name", nodesData[i]);
                graph.AddNode(inputPoints[i].Lat, inputPoints[i].Lng, attrs);
            }

            //// Ad edges to build complete graph
            for (int i = 0; i < graph.Nodes.Count() - 1; i++)
            {
                for (int j = i+1; j < graph.Nodes.Count() ; j++)
                {
                    graph.AddLink(i + 1, j + 1, new LinkData(), false);
                }
            }

            RoutingProblemFile file = new RoutingProblemFile(graph, RoutingProblemType.VRP, null, null, features, nodesData);
            dynamic fileInput = file.Input;
            foreach (var node in graph.Nodes)
                fileInput.Add(node.Id);
            Save(file);
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            menuViewNone.Checked = true;
            gmap.Overlays.FirstOrDefault(o => o.Id == "vrpInput").Clear();
            gmap.Refresh();
            inputPoints = new List<PointLatLng>();
            arpMarker = null;
            mapAction = MapActions.None;
            lblExportVRP.Text = "Export as VRP";
            lblExportARP.Text = "Export as ARP";
            lblExportTSP.Text = "Export as TSP";
            lblExportVRP.Enabled = true;
            lblExportARP.Enabled = true;
            mapAction = MapActions.None;
        }

        private void cbxSelectVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            route = (Route)cbxSelectVehicle.SelectedItem;
        }

        private void lblShowRoute_Click(object sender, EventArgs e)
        {
            if (route != null)
            {
                gmap.Overlays[gmap.Overlays.Count - 1].Clear();
                if (resultFile.ProblemType == RoutingProblemType.VRP) DrawOutputVRP(route.Nodes);
                else DrawOutputARP(route.Nodes);
            }
        }

        private void lblDisplayRoute_Click(object sender, EventArgs e)
        {
            if (route != null)
            {
                if(resultFile.ProblemType == RoutingProblemType.VRP)
                {
                    DisplayRoute display = new DisplayRoute();
                    Thread thread = new Thread(DrawOutputVRPAsync);
                    thread.Start(route.Nodes);
                }
                else
                {
                    Thread thread = new Thread(DrawOutputARPAsync);
                    thread.Start(route.Nodes);
                }
                
            }
        }

        private void lblAddPoint_Click(object sender, EventArgs e)
        {
            double lat, lng;
            if (!(double.TryParse(tbxLat.Text, out lat) && double.TryParse(tbxLong.Text, out lng)))
                MessageBox.Show("The format of latitude or longitude is invalid.", "Invalid data format", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                PointLatLng point = new PointLatLng(lat, lng);
                if (inputPoints.Count > 1)
                {
                    lblExportVRP.Text = "Done";
                    lblExportVRP.Enabled = true;
                }
                // draw the marker and store the point
                inputPoints.Add(point);
                var marker = new GMarkerGoogle(point, new Bitmap("Resources\\BitMaps\\blue_point.png"));
                foreach (var overlay in gmap.Overlays)
                    if (overlay.Id == "vrpInput")
                    {
                        overlay.Markers.Add(marker);
                        break;
                    }
            }
            tbxLat.Clear();
            tbxLong.Clear();
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

        private void btnAddProhibition_Click(object sender, EventArgs e)
        {
            if (mapAction == MapActions.AddTurnProhibition)
            {
                if (turnProhibitionNodes.Item1 != null && turnProhibitionNodes.Item2 != null && turnProhibitionNodes.Item3 != null)
                {
                    if (!turnProhibitionNodes.Item1.Edges.Where(edge => (edge.ToNode == turnProhibitionNodes.Item2) || edge.FromNode == turnProhibitionNodes.Item2).Any() && !turnProhibitionNodes.Item1.OutgoingArcs.Where(arc => arc.ToNode == turnProhibitionNodes.Item2).Any()
                        || !turnProhibitionNodes.Item2.Edges.Where(edge => (edge.ToNode == turnProhibitionNodes.Item3) || edge.FromNode == turnProhibitionNodes.Item3).Any() && !turnProhibitionNodes.Item2.OutgoingArcs.Where(arc => arc.ToNode == turnProhibitionNodes.Item3).Any())
                    {
                        MessageBox.Show($"Invalid Turn Prohibition");
                        menuViewNone.Checked = true;
                        menuViewNodes.Checked = true;
                        turnProhibitionNodes = (null, null, null);
                        ViewTurnProhibitions();//Refresh Turn prohibitions overlay

                    }
                    else
                    {
                        if (!graph.TurnProhibitions.Contains(turnProhibitionNodes))
                        { graph.TurnProhibitions.Add(turnProhibitionNodes); }
                        //graphFile.Graph.SplitNode(turnProhibitionNodes.Item1, turnProhibitionNodes.Item2, turnProhibitionNodes.Item3);
                        MessageBox.Show($"Turn Prohibition Added");
                        menuViewNone.Checked = true;
                        menuViewNodes.Checked = true;
                        turnProhibitionNodes = (null, null, null);
                        ViewTurnProhibitions();//Refresh Turn prohibitions overlay
                    }
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
        private void btnDeleteProhibition_Click(object sender, EventArgs e)
        {
            if (mapAction == MapActions.DeleteTurningProhibition)
            {
                if (turnProhibitionNodes.Item1 != null && turnProhibitionNodes.Item2 != null && turnProhibitionNodes.Item3 != null)
                {
                    graph.TurnProhibitions.Remove(turnProhibitionNodes);
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

        #region Menu Related Events

        private void menuViewNone_CheckedChanged(object sender, EventArgs e)
        {
            if (menuViewNone.Checked)
            {
                gmap.Overlays.Remove(gmap.Overlays.FirstOrDefault(o => o.Id == "nodes"));
                gmap.Overlays.Remove(gmap.Overlays.FirstOrDefault(o => o.Id == "edges"));
                gmap.Overlays.Remove(gmap.Overlays.FirstOrDefault(o => o.Id == "arcs"));
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
                if (!gmap.Overlays.Contains(nodesOverlay))
                {
                    nodesOverlay = (polygon != null) ? gmap.GetNodesOverlay(graph, polygon) : gmap.GetNodesOverlay(graph, gmap.GetEnvelope());
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
                    edgesOverlay = (polygon != null) ? gmap.GetEdgesOverlay(graph, polygon) : gmap.GetEdgesOverlay(graph, gmap.GetEnvelope());
                    gmap.Overlays.Add(edgesOverlay);
                }
                else edgesOverlay.IsVisibile = true;
                gmap.Zoom += 1;
                gmap.Zoom -= 1;
                menuViewNone.Checked = false;
            }
            else if (edgesOverlay != null)
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
                    arcsOverlay = (polygon != null) ? gmap.GetArcsOverlay(graph, polygon) : gmap.GetArcsOverlay(graph, gmap.GetEnvelope());
                    gmap.Overlays.Add(arcsOverlay);
                }
                else arcsOverlay.IsVisibile = true;
                gmap.Zoom += 1;
                gmap.Zoom -= 1;
                menuViewNone.Checked = false;
            }
            else if (arcsOverlay != null)
            {
                arcsOverlay.IsVisibile = false;
            }
        }

        private void MenuAddNode_CheckedChanged(object sender, EventArgs e)
        {
            if (menuAddNode.Checked)
            {                
                menuViewNodes.Checked = true;
                menuAddArc.Checked = false;
                menuAddEdge.Checked = false;
                mapAction = MapActions.AddingNode;
            }
            else mapAction = MapActions.None;
        }

        private void MenuAddEdge_CheckedChanged(object sender, EventArgs e)
        {
            if (menuAddEdge.Checked)
            {                
                menuViewEdges.Checked = true;
                menuAddArc.Checked = false;
                menuAddNode.Checked = false;
                mapAction = MapActions.AddingEdge;
            }
            else mapAction = MapActions.None;
        }

        private void MenuAddArc_CheckedChanged(object sender, EventArgs e)
        {
            if (menuAddArc.Checked)
            {
                menuViewArcs.Checked = true;
                menuAddEdge.Checked = false;
                menuAddNode.Checked = false;
                mapAction = MapActions.AddingArc;
            }
            else mapAction = MapActions.None;
        }

        #endregion

        #region Others Methods

        private void Save(RoutingProblemFile file)
        {
            dlgSaveAs.AddExtension = true;
            dlgSaveAs.DefaultExt = file.Extension;
            dlgSaveAs.Filter = "File " + file.FormatID + " (*" + dlgSaveAs.DefaultExt + ")|*" + dlgSaveAs.DefaultExt;
            dlgSaveAs.FileName = "result." + dlgSaveAs.DefaultExt;

            if (dlgSaveAs.ShowDialog() == DialogResult.OK)
            {
                //file.Export(dlgSaveAs.FileName);
                //MessageBox.Show("Results has been successfully exported.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
               //try
               // {
                    file.Export(dlgSaveAs.FileName);
                    MessageBox.Show("Results has been successfully exported.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
               // }
               // catch (Exception ex)
               // {
               //     string msg = ex.Message;
               //     if (ex is NullReferenceException)
               //         msg = "One or more of the input points are out of the selected region, the file could be broken.";
               //     MessageBox.Show("Error saving " + file.FormatID + ": " +
               //         msg, "Error ocurred while saving " + file.FormatID + " file",
               //         MessageBoxButtons.OK, MessageBoxIcon.Error);
               // }


                mapAction = MapActions.None;
                lblExportVRP.Text = "Export as VRP";
                lblExportARP.Text = "Export as ARP";
                lblExportTSP.Text = "Export as TSP";
                lblExportVRP.Enabled = true;
                lblExportARP.Enabled = true;
                lblExportTSP.Enabled = true;
                menuViewNodes.Checked = false;
                gmap.Overlays.FirstOrDefault(o => o.Id == "vrpInput").Clear();
                gmap.Refresh();
            }
        }

        private void MostDisconnectedNode(List<(Business.Domain.Core.Node, Business.Domain.Core.Node)> pairs)
        {

            menuViewNodes.Checked = true;
            menuViewArcs.Checked = true;
            menuViewEdges.Checked = true;

            int count = 0;
            Business.Domain.Core.Node mostDisconnected = null;
            foreach (var (node, _) in pairs)
            {
                int d = int.Parse(node.NodeData);
                if (d > count) { count = d; mostDisconnected = node; }
            }
            GMapOverlay nodes = gmap.Overlays.First(o => o.Id == "nodes");
            count = nodes.Markers.Count;
            if (mostDisconnected != null)
            {   
                var oldMarker = nodes.Markers.First(m => (int)m.Tag == mostDisconnected.Id);
                var marker = new GMarkerGoogle(oldMarker.Position, new Bitmap("Resources\\BitMaps\\blue_border_red_point.png")) { Tag = mostDisconnected.Id };
                nodes.Markers.Remove(oldMarker);
                nodes.Markers.Add(marker);
                count--;
            }
            for (int i = 0; i < count; i++)
            {
                var newMarker = new GMarkerGoogle(nodes.Markers[i].Position, new Bitmap("Resources\\BitMaps\\red_point.png")) { Tag = nodes.Markers[i].Tag };
                nodes.Markers[i] = newMarker;
            }
        }

        private void DrawOutputVRP(List<int> nodes)
        {
            List<PointLatLng> road = new List<PointLatLng>();
            var dijkstra = new Dijkstra(); //ForbiddenTurnsDijkstra();
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                Node sp = resultFile.Graph.NodesId[nodes[i]];
                Node ep = resultFile.Graph.NodesId[nodes[i + 1]];
                dynamic result = dijkstra.Run(resultFile.Graph, sp, ep);
                foreach (var node in result.Points)
                {
                    road.Add(new PointLatLng(node.Latitude, node.Longitude));
                }
                road.RemoveAt(road.Count - 1); // remove the last one of the path to avoid repeat it.
            }
            // Add the last node of the road.
            road.Add(new PointLatLng(resultFile.Graph.NodesId[nodes[nodes.Count - 1]].Latitude,
                                     resultFile.Graph.NodesId[nodes[nodes.Count - 1]].Longitude));
            int tag = 1;
            dynamic input = resultFile.Input;
            foreach (var nodeId in nodes)
            {
                if (input.Contains(nodeId))
                {
                    var lat = graph.NodesId[nodeId].Latitude;
                    var lng = graph.NodesId[nodeId].Longitude;
                    var marker = new GMarkerGoogle(new PointLatLng(lat, lng), new Bitmap("Resources\\BitMaps\\blue_point.png"));
                    marker.ToolTipText = tag.ToString();
                    marker.ToolTipMode = MarkerTooltipMode.Always;
                    gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(marker);           
                    tag++;
                }
            }
            var route = new GMapRoute(road, "path");
            gmap.Overlays[gmap.Overlays.Count - 1].Routes.Add(route);
        }

        private void DrawOutputVRPAsync(object list)
        {
            gmap.Overlays[gmap.Overlays.Count - 1].Markers.Clear();
            List<int> nodes = (List<int>)list;
            List<PointLatLng> road = new List<PointLatLng>();
            var dijkstra = new Dijkstra(); // ForbiddenTurnsDijkstra();
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                Node sp = resultFile.Graph.NodesId[nodes[i]];
                Node ep = resultFile.Graph.NodesId[nodes[i + 1]];
                dynamic result = dijkstra.Run(resultFile.Graph, sp, ep);
                foreach (var node in result.Points)
                {
                    road.Add(new PointLatLng(node.Latitude, node.Longitude));
                }
                road.RemoveAt(road.Count - 1); // remove the last one of the path to avoid repeat it.
            }
            // Add the last node of the road.
            road.Add(new PointLatLng(resultFile.Graph.NodesId[nodes[nodes.Count - 1]].Latitude,
                                     resultFile.Graph.NodesId[nodes[nodes.Count - 1]].Longitude));
            int tag = 1;
            dynamic input = resultFile.Input;
            foreach (var nodeId in nodes)
            {
                if (input.Contains(nodeId))
                {
                    var marker = new GMarkerGoogle(new PointLatLng(graph.NodesId[nodeId].Latitude,
                        graph.NodesId[nodeId].Longitude), new Bitmap("Resources\\BitMaps\\blue_point.png"));
                    marker.ToolTipText = tag.ToString();
                    tag++;
                    gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(marker);
                }
            }
            gmap.Overlays[gmap.Overlays.Count - 1].Routes.Clear();
            List<PointLatLng> tmp = new List<PointLatLng> { road[0] };
            var route = new GMapRoute(tmp, "path");            
            gmap.Overlays[gmap.Overlays.Count - 1].Routes.Add(route);
            GMapMarker car = new GMarkerGoogle(road[0], new Bitmap("Resources\\BitMaps\\purple_car.png"));
            gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(car);

            for (int i = 1; i < road.Count; i++)
            {
                Thread.Sleep(300);
                tmp.Add(road[i]);
                gmap.Overlays[gmap.Overlays.Count - 1].Routes.Clear();
                route = new GMapRoute(tmp, "path");
                gmap.Overlays[gmap.Overlays.Count - 1].Routes.Add(route);
                gmap.Overlays[gmap.Overlays.Count - 1].Markers.Remove(car);
                car = new GMarkerGoogle(road[i], new Bitmap("Resources\\BitMaps\\purple_car.png"));
                gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(car);
            }
        }

        private void DrawOutputARP(List<int> nodes)
        {
            List<PointLatLng> road = new List<PointLatLng>();
            Dictionary<int, GMapMarker> dict = new Dictionary<int, GMapMarker>();
            int count = 0;
            foreach (var item in nodes)
            {
                Business.Domain.Core.Node node = graph.NodesId[item];
                PointLatLng point = new PointLatLng(node.Latitude, node.Longitude);
                road.Add(point);
                if (dict.ContainsKey(item))
                    dict[item].ToolTipText += "-" + (++count).ToString();
                else
                {
                    GMapMarker marker = new GMarkerGoogle(point, new Bitmap("Resources\\BitMaps\\small_point.png"));
                    marker.ToolTipText = (++count).ToString();
                    dict.Add(item, marker);
                    marker.ToolTipMode = MarkerTooltipMode.Always;
                    gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(marker);
                }
            }

            var route = new GMapRoute(road, "path");
            gmap.Overlays[gmap.Overlays.Count - 1].Routes.Add(route);
        }

        private void DrawOutputARPAsync(object list)
        {
            gmap.Overlays[gmap.Overlays.Count - 1].Markers.Clear();
            List<int> nodes = (List<int>)list;
            List<PointLatLng> road = new List<PointLatLng>();
            Dictionary<int, GMapMarker> dict = new Dictionary<int, GMapMarker>();
            int count = 0;
            for (int i = 0; i < nodes.Count; i++)
            {
                int item = nodes[i];
                Business.Domain.Core.Node node = graph.NodesId[item];
                PointLatLng point = new PointLatLng(node.Latitude, node.Longitude);
                road.Add(point);
                if (dict.ContainsKey(item))
                    dict[item].ToolTipText += "-" + (++count).ToString();
                else
                {
                    GMapMarker marker = new GMarkerGoogle(point, new Bitmap("Resources\\BitMaps\\small_point.png"));
                    marker.ToolTipText = (++count).ToString();
                    dict.Add(item, marker);
                    gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(marker);
                }
            }
            var tmp = new List<PointLatLng> { road[0] };
            var route = new GMapRoute(tmp, "path");
            gmap.Overlays[gmap.Overlays.Count - 1].Routes.Add(route);
            GMapMarker car = new GMarkerGoogle(road[0], new Bitmap("Resources\\BitMaps\\purple_car.png"));
            gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(car);
            for (int i = 1; i < road.Count; i++)
            {
                Thread.Sleep(300);
                tmp.Add(road[i]);
                gmap.Overlays[gmap.Overlays.Count - 1].Routes.Clear();
                route = new GMapRoute(tmp, "path");
                gmap.Overlays[gmap.Overlays.Count - 1].Routes.Add(route);
                gmap.Overlays[gmap.Overlays.Count - 1].Markers.Remove(car);
                car = new GMarkerGoogle(road[i], new Bitmap("Resources\\BitMaps\\purple_car.png"));
                gmap.Overlays[gmap.Overlays.Count - 1].Markers.Add(car);
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
            if (prohibitionsOverlayIndex != gmap.Overlays.Count - 1)
            {
                gmap.Overlays.RemoveAt(prohibitionsOverlayIndex);
                gmap.Overlays.Add(prohibitionsOverlay);
            }
            //menuViewNodes.Checked = false;
            //menuViewNodes.Checked = true;
            Dictionary<(double, double), List<(Node, Node, Node)>> prohibitions = new Dictionary<(double, double), List<(Node, Node, Node)>>();
            foreach (var tp in graph.TurnProhibitions)
            {
                double lat = tp.Item2.Latitude;
                double lon = tp.Item2.Longitude;
                if (prohibitions.ContainsKey((lat, lon)))
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


        #endregion

        private void btnImportData_Click(object sender, EventArgs e)
        {
            if (dataPointsImporter==null)
            {
                return;
            }
            dlgOpenFile.CheckPathExists = true;
            dlgOpenFile.CheckFileExists = false;
            dlgOpenFile.AddExtension = true;
            dlgOpenFile.DefaultExt = "RPD";
            dlgOpenFile.ValidateNames = true;
            dlgOpenFile.Title = "Load Data File";
            dlgOpenFile.Filter = dataPointsImporter.Filter;
            dlgOpenFile.FilterIndex = 1;
            dlgOpenFile.RestoreDirectory = true;

            nodesData = new List<string>();
            inputPoints = new List<PointLatLng>();

            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                string path = dlgOpenFile.FileName;

                (List<PointLatLng>, List<string>) result = dataPointsImporter.Import(path, this);

                nodesData = result.Item2;
                inputPoints = result.Item1;
                if (inputPoints.Count == 0)
                    return;

                
                //// Read data from Excel File
                //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //Workbook wbexcel = excelApp.Workbooks.Open(path);
                //Worksheet sheet = (Worksheet)wbexcel.Worksheets[1];
                //Range range = sheet.UsedRange;
                //int rows = range.Rows.Count;
                //nodesData = new List<string>();
                //inputPoints = new List<PointLatLng>();
                //for (int i = 1; i < rows; i++)
                //{
                //    nodesData.Add((string)(range.Cells[i, 1] as Range).Value2);
                //    double lat = (double)(sheet.Cells[i, 2] as Range).Value2;
                //    double lng = (double)(sheet.Cells[i, 3] as Range).Value2;
                //    inputPoints.Add(new PointLatLng(lat, lng));
                //}
                //wbexcel.Close(true, null, null);
                //excelApp.Quit();

                // Show Input Points
                foreach (var overlay in gmap.Overlays)
                    if (overlay.Id == "vrpInput")
                    {
                        foreach (var point in inputPoints)
                        {
                            var marker = new GMarkerGoogle(point, new Bitmap("Resources\\BitMaps\\blue_point.png"));
                            overlay.Markers.Add(marker);
                        }
                        break;
                    }
                if(mapAction == MapActions.SelectingVRPInput)
                {
                    lblExportVRP.Text = "Done";
                    lblExportVRP.Enabled = true;
                }
                else if(mapAction == MapActions.SelectingTSPInput)
                {
                    lblExportTSP.Text = "Done";
                    lblExportTSP.Enabled = true;
                }
                
            }

            
        }

        private void saveGraphAsGRFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgSaveAs.AddExtension = true;
            dlgSaveAs.DefaultExt = ".grf";
            if (dlgSaveAs.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    GRFGraphExporter exporter = new GRFGraphExporter();
                    exporter.Export(new FileStream(dlgSaveAs.FileName, FileMode.CreateNew), graph);
                    MessageBox.Show("Results has been successfully exported.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving GRF: " +
                        ex.Message, "Error ocurred while saving GRF file",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbxDataFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataPointsImporter = (IDataPointsImporter)cbxDataFormat.SelectedItem;
        }

        private void chbTrafficRestrictions_CheckedChanged(object sender, EventArgs e)
        {
            if (!chbTrafficRestrictions.Checked)
                chbTurnProhibitions.Checked = false;
        }

        private void chbTurnProhibitions_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTurnProhibitions.Checked)
            {
                chbTrafficRestrictions.Checked = true;
                cbxTurnProhibitionsGraphModifier.Visible = true;
            }
            else
                cbxTurnProhibitionsGraphModifier.Visible = false;
        }
    }
}