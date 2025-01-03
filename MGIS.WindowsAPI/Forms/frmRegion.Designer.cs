namespace NGAT.WindowsAPI.Forms
{
    partial class frmRegion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegion));
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.pnlToolsBar = new System.Windows.Forms.Panel();
            this.cbxTurnProhibitionsGraphModifier = new System.Windows.Forms.ComboBox();
            this.chbTurnProhibitions = new System.Windows.Forms.CheckBox();
            this.chbTrafficRestrictions = new System.Windows.Forms.CheckBox();
            this.lblAlgorithm = new System.Windows.Forms.Label();
            this.cbxSelectAlgorithm = new System.Windows.Forms.ComboBox();
            this.lblExportTSP = new System.Windows.Forms.Label();
            this.lblExportARP = new System.Windows.Forms.Label();
            this.lblClear = new System.Windows.Forms.Label();
            this.lblExportVRP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuSettings = new System.Windows.Forms.MenuStrip();
            this.menuViewGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewNone = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewNodes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewEdges = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewArcs = new System.Windows.Forms.ToolStripMenuItem();
            this.editGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddNode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddEdge = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddArc = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGraphAsGRFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.dlgSaveAs = new System.Windows.Forms.SaveFileDialog();
            this.cbxSelectVehicle = new System.Windows.Forms.ComboBox();
            this.lblSelectRoute = new System.Windows.Forms.Label();
            this.lblShowRoute = new System.Windows.Forms.Label();
            this.lblDisplayRoute = new System.Windows.Forms.Label();
            this.tbxLat = new System.Windows.Forms.TextBox();
            this.tbxLong = new System.Windows.Forms.TextBox();
            this.lblAddPoint = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxStreet2 = new System.Windows.Forms.TextBox();
            this.tbxStreet1 = new System.Windows.Forms.TextBox();
            this.btnImportData = new System.Windows.Forms.Button();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.cbxKeepLocation = new System.Windows.Forms.CheckBox();
            this.cbxDataFormat = new System.Windows.Forms.ComboBox();
            this.lblImportDataFormat = new System.Windows.Forms.Label();
            this.btnAddProhibition = new System.Windows.Forms.Button();
            this.btnDeleteProhibition = new System.Windows.Forms.Button();
            this.pnlToolsBar.SuspendLayout();
            this.menuSettings.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(3, 160);
            this.gmap.Margin = new System.Windows.Forms.Padding(4);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 20;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(1173, 645);
            this.gmap.TabIndex = 1;
            this.gmap.Zoom = 13D;
            this.gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmap_OnMarkerClick);
            this.gmap.Load += new System.EventHandler(this.gmap_Load);
            this.gmap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gmap_KeyDown);
            this.gmap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseClick);
            // 
            // pnlToolsBar
            // 
            this.pnlToolsBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToolsBar.Controls.Add(this.cbxTurnProhibitionsGraphModifier);
            this.pnlToolsBar.Controls.Add(this.chbTurnProhibitions);
            this.pnlToolsBar.Controls.Add(this.chbTrafficRestrictions);
            this.pnlToolsBar.Controls.Add(this.lblAlgorithm);
            this.pnlToolsBar.Controls.Add(this.cbxSelectAlgorithm);
            this.pnlToolsBar.Controls.Add(this.lblExportTSP);
            this.pnlToolsBar.Controls.Add(this.lblExportARP);
            this.pnlToolsBar.Controls.Add(this.lblClear);
            this.pnlToolsBar.Controls.Add(this.lblExportVRP);
            this.pnlToolsBar.Controls.Add(this.label1);
            this.pnlToolsBar.Controls.Add(this.menuSettings);
            this.pnlToolsBar.Controls.Add(this.btnZoomOut);
            this.pnlToolsBar.Controls.Add(this.btnZoomIn);
            this.pnlToolsBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlToolsBar.ForeColor = System.Drawing.SystemColors.Control;
            this.pnlToolsBar.Location = new System.Drawing.Point(3, 1);
            this.pnlToolsBar.Margin = new System.Windows.Forms.Padding(4);
            this.pnlToolsBar.Name = "pnlToolsBar";
            this.pnlToolsBar.Size = new System.Drawing.Size(1171, 86);
            this.pnlToolsBar.TabIndex = 3;
            // 
            // cbxTurnProhibitionsGraphModifier
            // 
            this.cbxTurnProhibitionsGraphModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTurnProhibitionsGraphModifier.FormattingEnabled = true;
            this.cbxTurnProhibitionsGraphModifier.Location = new System.Drawing.Point(623, 43);
            this.cbxTurnProhibitionsGraphModifier.Name = "cbxTurnProhibitionsGraphModifier";
            this.cbxTurnProhibitionsGraphModifier.Size = new System.Drawing.Size(154, 28);
            this.cbxTurnProhibitionsGraphModifier.TabIndex = 18;
            this.cbxTurnProhibitionsGraphModifier.Visible = false;
            // 
            // chbTurnProhibitions
            // 
            this.chbTurnProhibitions.AutoSize = true;
            this.chbTurnProhibitions.Location = new System.Drawing.Point(408, 61);
            this.chbTurnProhibitions.Name = "chbTurnProhibitions";
            this.chbTurnProhibitions.Size = new System.Drawing.Size(193, 24);
            this.chbTurnProhibitions.TabIndex = 17;
            this.chbTurnProhibitions.Text = "Use Turn Prohibitions";
            this.chbTurnProhibitions.UseVisualStyleBackColor = true;
            this.chbTurnProhibitions.Visible = false;
            this.chbTurnProhibitions.CheckedChanged += new System.EventHandler(this.chbTurnProhibitions_CheckedChanged);
            // 
            // chbTrafficRestrictions
            // 
            this.chbTrafficRestrictions.AutoSize = true;
            this.chbTrafficRestrictions.Location = new System.Drawing.Point(408, 37);
            this.chbTrafficRestrictions.Name = "chbTrafficRestrictions";
            this.chbTrafficRestrictions.Size = new System.Drawing.Size(209, 24);
            this.chbTrafficRestrictions.TabIndex = 14;
            this.chbTrafficRestrictions.Text = "Use Traffic Restrictions";
            this.chbTrafficRestrictions.UseVisualStyleBackColor = true;
            this.chbTrafficRestrictions.Visible = false;
            this.chbTrafficRestrictions.CheckedChanged += new System.EventHandler(this.chbTrafficRestrictions_CheckedChanged);
            // 
            // lblAlgorithm
            // 
            this.lblAlgorithm.AutoSize = true;
            this.lblAlgorithm.Location = new System.Drawing.Point(95, 46);
            this.lblAlgorithm.Name = "lblAlgorithm";
            this.lblAlgorithm.Size = new System.Drawing.Size(80, 20);
            this.lblAlgorithm.TabIndex = 13;
            this.lblAlgorithm.Text = "Algorithm";
            this.lblAlgorithm.Visible = false;
            // 
            // cbxSelectAlgorithm
            // 
            this.cbxSelectAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectAlgorithm.FormattingEnabled = true;
            this.cbxSelectAlgorithm.Location = new System.Drawing.Point(178, 43);
            this.cbxSelectAlgorithm.Name = "cbxSelectAlgorithm";
            this.cbxSelectAlgorithm.Size = new System.Drawing.Size(223, 28);
            this.cbxSelectAlgorithm.TabIndex = 12;
            this.cbxSelectAlgorithm.Visible = false;
            // 
            // lblExportTSP
            // 
            this.lblExportTSP.AutoSize = true;
            this.lblExportTSP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblExportTSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportTSP.Location = new System.Drawing.Point(688, 7);
            this.lblExportTSP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExportTSP.Name = "lblExportTSP";
            this.lblExportTSP.Size = new System.Drawing.Size(154, 27);
            this.lblExportTSP.TabIndex = 11;
            this.lblExportTSP.Text = "Export as TSP";
            this.lblExportTSP.Click += new System.EventHandler(this.lblExportTSP_Click);
            // 
            // lblExportARP
            // 
            this.lblExportARP.AutoSize = true;
            this.lblExportARP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblExportARP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportARP.Location = new System.Drawing.Point(486, 7);
            this.lblExportARP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExportARP.Name = "lblExportARP";
            this.lblExportARP.Size = new System.Drawing.Size(154, 27);
            this.lblExportARP.TabIndex = 10;
            this.lblExportARP.Text = "Export as ARP";
            this.lblExportARP.Click += new System.EventHandler(this.lblExportARP_Click);
            // 
            // lblClear
            // 
            this.lblClear.AutoSize = true;
            this.lblClear.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClear.Location = new System.Drawing.Point(879, 7);
            this.lblClear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClear.Name = "lblClear";
            this.lblClear.Size = new System.Drawing.Size(66, 27);
            this.lblClear.TabIndex = 9;
            this.lblClear.Text = "Clear";
            this.lblClear.Click += new System.EventHandler(this.lblCancel_Click);
            // 
            // lblExportVRP
            // 
            this.lblExportVRP.AutoSize = true;
            this.lblExportVRP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblExportVRP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportVRP.Location = new System.Drawing.Point(247, 7);
            this.lblExportVRP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExportVRP.Name = "lblExportVRP";
            this.lblExportVRP.Size = new System.Drawing.Size(154, 27);
            this.lblExportVRP.TabIndex = 8;
            this.lblExportVRP.Text = "Export as NRP";
            this.lblExportVRP.Click += new System.EventHandler(this.lblExportVRP_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1002, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 27);
            this.label1.TabIndex = 6;
            this.label1.Text = "Home";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // menuSettings
            // 
            this.menuSettings.Dock = System.Windows.Forms.DockStyle.None;
            this.menuSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuSettings.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuViewGraph,
            this.editGraphToolStripMenuItem});
            this.menuSettings.Location = new System.Drawing.Point(5, 6);
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuSettings.Size = new System.Drawing.Size(204, 28);
            this.menuSettings.TabIndex = 5;
            // 
            // menuViewGraph
            // 
            this.menuViewGraph.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuViewNone,
            this.menuViewNodes,
            this.menuViewEdges,
            this.menuViewArcs});
            this.menuViewGraph.Name = "menuViewGraph";
            this.menuViewGraph.Size = new System.Drawing.Size(102, 24);
            this.menuViewGraph.Text = "View Graph";
            // 
            // menuViewNone
            // 
            this.menuViewNone.Checked = true;
            this.menuViewNone.CheckOnClick = true;
            this.menuViewNone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuViewNone.Name = "menuViewNone";
            this.menuViewNone.Size = new System.Drawing.Size(136, 26);
            this.menuViewNone.Text = "None";
            this.menuViewNone.CheckedChanged += new System.EventHandler(this.menuViewNone_CheckedChanged);
            // 
            // menuViewNodes
            // 
            this.menuViewNodes.CheckOnClick = true;
            this.menuViewNodes.Name = "menuViewNodes";
            this.menuViewNodes.Size = new System.Drawing.Size(136, 26);
            this.menuViewNodes.Text = "Nodes";
            this.menuViewNodes.CheckedChanged += new System.EventHandler(this.menuViewNodes_CheckedChanged);
            // 
            // menuViewEdges
            // 
            this.menuViewEdges.CheckOnClick = true;
            this.menuViewEdges.Name = "menuViewEdges";
            this.menuViewEdges.Size = new System.Drawing.Size(136, 26);
            this.menuViewEdges.Text = "Edges";
            this.menuViewEdges.CheckedChanged += new System.EventHandler(this.menuViewEdges_CheckedChanged);
            // 
            // menuViewArcs
            // 
            this.menuViewArcs.CheckOnClick = true;
            this.menuViewArcs.Name = "menuViewArcs";
            this.menuViewArcs.Size = new System.Drawing.Size(136, 26);
            this.menuViewArcs.Text = "Arcs";
            this.menuViewArcs.CheckedChanged += new System.EventHandler(this.menuViewArcs_CheckedChanged);
            // 
            // editGraphToolStripMenuItem
            // 
            this.editGraphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddNode,
            this.menuAddEdge,
            this.menuAddArc,
            this.saveGraphAsGRFToolStripMenuItem});
            this.editGraphToolStripMenuItem.Name = "editGraphToolStripMenuItem";
            this.editGraphToolStripMenuItem.Size = new System.Drawing.Size(95, 24);
            this.editGraphToolStripMenuItem.Text = "Edit Graph";
            // 
            // menuAddNode
            // 
            this.menuAddNode.CheckOnClick = true;
            this.menuAddNode.Name = "menuAddNode";
            this.menuAddNode.Size = new System.Drawing.Size(219, 26);
            this.menuAddNode.Text = "Add Nodes";
            this.menuAddNode.CheckedChanged += new System.EventHandler(this.MenuAddNode_CheckedChanged);
            // 
            // menuAddEdge
            // 
            this.menuAddEdge.CheckOnClick = true;
            this.menuAddEdge.Name = "menuAddEdge";
            this.menuAddEdge.Size = new System.Drawing.Size(219, 26);
            this.menuAddEdge.Text = "Add Edges";
            this.menuAddEdge.CheckedChanged += new System.EventHandler(this.MenuAddEdge_CheckedChanged);
            // 
            // menuAddArc
            // 
            this.menuAddArc.CheckOnClick = true;
            this.menuAddArc.Name = "menuAddArc";
            this.menuAddArc.Size = new System.Drawing.Size(219, 26);
            this.menuAddArc.Text = "Add Arcs";
            this.menuAddArc.CheckedChanged += new System.EventHandler(this.MenuAddArc_CheckedChanged);
            // 
            // saveGraphAsGRFToolStripMenuItem
            // 
            this.saveGraphAsGRFToolStripMenuItem.Name = "saveGraphAsGRFToolStripMenuItem";
            this.saveGraphAsGRFToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.saveGraphAsGRFToolStripMenuItem.Text = "Save Graph as GRF";
            this.saveGraphAsGRFToolStripMenuItem.Click += new System.EventHandler(this.saveGraphAsGRFToolStripMenuItem_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomOut.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnZoomOut.Location = new System.Drawing.Point(1127, 0);
            this.btnZoomOut.Margin = new System.Windows.Forms.Padding(4);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(39, 37);
            this.btnZoomOut.TabIndex = 3;
            this.btnZoomOut.Text = "-";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomIn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnZoomIn.Location = new System.Drawing.Point(1080, 0);
            this.btnZoomIn.Margin = new System.Windows.Forms.Padding(4);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(39, 37);
            this.btnZoomIn.TabIndex = 2;
            this.btnZoomIn.Text = "+";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // cbxSelectVehicle
            // 
            this.cbxSelectVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectVehicle.FormattingEnabled = true;
            this.cbxSelectVehicle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxSelectVehicle.Location = new System.Drawing.Point(145, 814);
            this.cbxSelectVehicle.Margin = new System.Windows.Forms.Padding(4);
            this.cbxSelectVehicle.Name = "cbxSelectVehicle";
            this.cbxSelectVehicle.Size = new System.Drawing.Size(157, 24);
            this.cbxSelectVehicle.Sorted = true;
            this.cbxSelectVehicle.TabIndex = 4;
            this.cbxSelectVehicle.Visible = false;
            this.cbxSelectVehicle.SelectedIndexChanged += new System.EventHandler(this.cbxSelectVehicle_SelectedIndexChanged);
            // 
            // lblSelectRoute
            // 
            this.lblSelectRoute.AutoSize = true;
            this.lblSelectRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectRoute.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSelectRoute.Location = new System.Drawing.Point(3, 817);
            this.lblSelectRoute.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectRoute.Name = "lblSelectRoute";
            this.lblSelectRoute.Size = new System.Drawing.Size(117, 20);
            this.lblSelectRoute.TabIndex = 5;
            this.lblSelectRoute.Text = "Select Route";
            this.lblSelectRoute.Visible = false;
            // 
            // lblShowRoute
            // 
            this.lblShowRoute.AutoSize = true;
            this.lblShowRoute.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShowRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowRoute.ForeColor = System.Drawing.SystemColors.Control;
            this.lblShowRoute.Location = new System.Drawing.Point(319, 814);
            this.lblShowRoute.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShowRoute.Name = "lblShowRoute";
            this.lblShowRoute.Size = new System.Drawing.Size(111, 22);
            this.lblShowRoute.TabIndex = 6;
            this.lblShowRoute.Text = "Show Route";
            this.lblShowRoute.Visible = false;
            this.lblShowRoute.Click += new System.EventHandler(this.lblShowRoute_Click);
            // 
            // lblDisplayRoute
            // 
            this.lblDisplayRoute.AutoSize = true;
            this.lblDisplayRoute.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDisplayRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayRoute.ForeColor = System.Drawing.SystemColors.Control;
            this.lblDisplayRoute.Location = new System.Drawing.Point(455, 814);
            this.lblDisplayRoute.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDisplayRoute.Name = "lblDisplayRoute";
            this.lblDisplayRoute.Size = new System.Drawing.Size(129, 22);
            this.lblDisplayRoute.TabIndex = 7;
            this.lblDisplayRoute.Text = "Display Route";
            this.lblDisplayRoute.Visible = false;
            this.lblDisplayRoute.Click += new System.EventHandler(this.lblDisplayRoute_Click);
            // 
            // tbxLat
            // 
            this.tbxLat.Location = new System.Drawing.Point(883, 814);
            this.tbxLat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbxLat.Name = "tbxLat";
            this.tbxLat.Size = new System.Drawing.Size(141, 22);
            this.tbxLat.TabIndex = 8;
            this.tbxLat.Visible = false;
            // 
            // tbxLong
            // 
            this.tbxLong.Location = new System.Drawing.Point(1029, 814);
            this.tbxLong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbxLong.Name = "tbxLong";
            this.tbxLong.Size = new System.Drawing.Size(141, 22);
            this.tbxLong.TabIndex = 9;
            this.tbxLong.Visible = false;
            // 
            // lblAddPoint
            // 
            this.lblAddPoint.AutoSize = true;
            this.lblAddPoint.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAddPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddPoint.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAddPoint.Location = new System.Drawing.Point(771, 814);
            this.lblAddPoint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAddPoint.Name = "lblAddPoint";
            this.lblAddPoint.Size = new System.Drawing.Size(92, 22);
            this.lblAddPoint.TabIndex = 10;
            this.lblAddPoint.Text = "Add Point";
            this.lblAddPoint.Visible = false;
            this.lblAddPoint.Click += new System.EventHandler(this.lblAddPoint_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.tbxStreet2);
            this.groupBox4.Controls.Add(this.tbxStreet1);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Location = new System.Drawing.Point(7, 98);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(491, 50);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Search Address";
            // 
            // btnSearch
            // 
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearch.Location = new System.Drawing.Point(377, 18);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(87, 28);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(171, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "&&";
            // 
            // tbxStreet2
            // 
            this.tbxStreet2.Location = new System.Drawing.Point(197, 18);
            this.tbxStreet2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbxStreet2.Name = "tbxStreet2";
            this.tbxStreet2.Size = new System.Drawing.Size(156, 26);
            this.tbxStreet2.TabIndex = 1;
            // 
            // tbxStreet1
            // 
            this.tbxStreet1.Location = new System.Drawing.Point(7, 18);
            this.tbxStreet1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbxStreet1.Name = "tbxStreet1";
            this.tbxStreet1.Size = new System.Drawing.Size(156, 26);
            this.tbxStreet1.TabIndex = 0;
            // 
            // btnImportData
            // 
            this.btnImportData.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnImportData.Location = new System.Drawing.Point(1042, 109);
            this.btnImportData.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportData.Name = "btnImportData";
            this.btnImportData.Size = new System.Drawing.Size(122, 28);
            this.btnImportData.TabIndex = 5;
            this.btnImportData.Text = "Import Data";
            this.btnImportData.UseVisualStyleBackColor = true;
            this.btnImportData.Visible = false;
            this.btnImportData.Click += new System.EventHandler(this.btnImportData_Click);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.FileName = "openFileDialog1";
            // 
            // cbxKeepLocation
            // 
            this.cbxKeepLocation.AutoSize = true;
            this.cbxKeepLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxKeepLocation.Location = new System.Drawing.Point(538, 118);
            this.cbxKeepLocation.Name = "cbxKeepLocation";
            this.cbxKeepLocation.Size = new System.Drawing.Size(202, 24);
            this.cbxKeepLocation.TabIndex = 12;
            this.cbxKeepLocation.Text = "Keep points location";
            this.cbxKeepLocation.UseVisualStyleBackColor = true;
            this.cbxKeepLocation.Visible = false;
            // 
            // cbxDataFormat
            // 
            this.cbxDataFormat.FormattingEnabled = true;
            this.cbxDataFormat.Location = new System.Drawing.Point(902, 113);
            this.cbxDataFormat.Name = "cbxDataFormat";
            this.cbxDataFormat.Size = new System.Drawing.Size(122, 24);
            this.cbxDataFormat.TabIndex = 13;
            this.cbxDataFormat.Visible = false;
            this.cbxDataFormat.SelectedIndexChanged += new System.EventHandler(this.cbxDataFormat_SelectedIndexChanged);
            // 
            // lblImportDataFormat
            // 
            this.lblImportDataFormat.AutoSize = true;
            this.lblImportDataFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportDataFormat.Location = new System.Drawing.Point(914, 91);
            this.lblImportDataFormat.Name = "lblImportDataFormat";
            this.lblImportDataFormat.Size = new System.Drawing.Size(92, 16);
            this.lblImportDataFormat.TabIndex = 14;
            this.lblImportDataFormat.Text = "Data Format";
            this.lblImportDataFormat.Visible = false;
            // 
            // btnAddProhibition
            // 
            this.btnAddProhibition.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProhibition.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAddProhibition.Location = new System.Drawing.Point(538, 113);
            this.btnAddProhibition.Name = "btnAddProhibition";
            this.btnAddProhibition.Size = new System.Drawing.Size(130, 28);
            this.btnAddProhibition.TabIndex = 15;
            this.btnAddProhibition.Text = "Add Prohibition";
            this.btnAddProhibition.UseVisualStyleBackColor = true;
            this.btnAddProhibition.Visible = false;
            this.btnAddProhibition.Click += new System.EventHandler(this.btnAddProhibition_Click);
            // 
            // btnDeleteProhibition
            // 
            this.btnDeleteProhibition.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteProhibition.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDeleteProhibition.Location = new System.Drawing.Point(692, 113);
            this.btnDeleteProhibition.Name = "btnDeleteProhibition";
            this.btnDeleteProhibition.Size = new System.Drawing.Size(161, 26);
            this.btnDeleteProhibition.TabIndex = 16;
            this.btnDeleteProhibition.Text = "Delete Prohibition";
            this.btnDeleteProhibition.UseVisualStyleBackColor = true;
            this.btnDeleteProhibition.Visible = false;
            this.btnDeleteProhibition.Click += new System.EventHandler(this.btnDeleteProhibition_Click);
            // 
            // frmRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1177, 844);
            this.Controls.Add(this.btnDeleteProhibition);
            this.Controls.Add(this.btnAddProhibition);
            this.Controls.Add(this.lblImportDataFormat);
            this.Controls.Add(this.cbxDataFormat);
            this.Controls.Add(this.cbxKeepLocation);
            this.Controls.Add(this.btnImportData);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.lblAddPoint);
            this.Controls.Add(this.tbxLong);
            this.Controls.Add(this.tbxLat);
            this.Controls.Add(this.lblDisplayRoute);
            this.Controls.Add(this.lblShowRoute);
            this.Controls.Add(this.lblSelectRoute);
            this.Controls.Add(this.cbxSelectVehicle);
            this.Controls.Add(this.pnlToolsBar);
            this.Controls.Add(this.gmap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1195, 891);
            this.MinimumSize = new System.Drawing.Size(1195, 891);
            this.Name = "frmRegion";
            this.Text = "Visual NGAT";
            this.Load += new System.EventHandler(this.frmRegion_Load);
            this.pnlToolsBar.ResumeLayout(false);
            this.pnlToolsBar.PerformLayout();
            this.menuSettings.ResumeLayout(false);
            this.menuSettings.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.Panel pnlToolsBar;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuViewGraph;
        private System.Windows.Forms.ToolStripMenuItem menuViewNone;
        private System.Windows.Forms.ToolStripMenuItem menuViewNodes;
        private System.Windows.Forms.ToolStripMenuItem menuViewEdges;
        private System.Windows.Forms.ToolStripMenuItem menuViewArcs;
        private System.Windows.Forms.ToolStripMenuItem editGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddNode;
        private System.Windows.Forms.ToolStripMenuItem menuAddEdge;
        private System.Windows.Forms.ToolStripMenuItem menuAddArc;
        private System.Windows.Forms.SaveFileDialog dlgSaveAs;
        private System.Windows.Forms.Label lblExportVRP;
        private System.Windows.Forms.Label lblClear;
        private System.Windows.Forms.ComboBox cbxSelectVehicle;
        private System.Windows.Forms.Label lblSelectRoute;
        private System.Windows.Forms.Label lblShowRoute;
        private System.Windows.Forms.Label lblDisplayRoute;
        private System.Windows.Forms.Label lblExportARP;
        private System.Windows.Forms.TextBox tbxLat;
        private System.Windows.Forms.TextBox tbxLong;
        private System.Windows.Forms.Label lblAddPoint;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxStreet2;
        private System.Windows.Forms.TextBox tbxStreet1;
        private System.Windows.Forms.Button btnImportData;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.Label lblExportTSP;
        private System.Windows.Forms.CheckBox cbxKeepLocation;
        private System.Windows.Forms.ToolStripMenuItem saveGraphAsGRFToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbxDataFormat;
        private System.Windows.Forms.Label lblImportDataFormat;
        private System.Windows.Forms.Button btnAddProhibition;
        private System.Windows.Forms.Button btnDeleteProhibition;
        private System.Windows.Forms.CheckBox chbTrafficRestrictions;
        private System.Windows.Forms.Label lblAlgorithm;
        private System.Windows.Forms.ComboBox cbxSelectAlgorithm;
        private System.Windows.Forms.CheckBox chbTurnProhibitions;
        private System.Windows.Forms.ComboBox cbxTurnProhibitionsGraphModifier;
    }
}