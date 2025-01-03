namespace NGAT.WindowsAPI
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbxTurnProhibitionsGraphModifiers = new System.Windows.Forms.ComboBox();
            this.chbUseTurnProhibitions = new System.Windows.Forms.CheckBox();
            this.chbTrafficRestrictions = new System.Windows.Forms.CheckBox();
            this.btnDoneEditingTurnProhibitions = new System.Windows.Forms.Button();
            this.btnDeleteProhibition = new System.Windows.Forms.Button();
            this.btnAddProhibition = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.cbxExportResult = new System.Windows.Forms.ComboBox();
            this.cbxVisualizationForm = new System.Windows.Forms.ComboBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.cbxRunAlgorithm = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbxSaveAs = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoadResultFile = new System.Windows.Forms.Button();
            this.cbxResultFormat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.cbxGraphFormat = new System.Windows.Forms.ComboBox();
            this.lblMenu = new System.Windows.Forms.Label();
            this.menuSettings = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProxySettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCacheLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewNone = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewNodes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewEdges = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewArcs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLayers = new System.Windows.Forms.ToolStripMenuItem();
            this.editGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddNode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddEdge = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddArc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteArc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteEdge = new System.Windows.Forms.ToolStripMenuItem();
            this.menuScenarios = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditArc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditEdge = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteNode = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMap = new System.Windows.Forms.Panel();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveAs = new System.Windows.Forms.SaveFileDialog();
            this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlToolsBar = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbxStreet2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxStreet1 = new System.Windows.Forms.TextBox();
            this.lblClear = new System.Windows.Forms.Label();
            this.lblSelectRegion = new System.Windows.Forms.Label();
            this.lblHome = new System.Windows.Forms.Label();
            this.btnMoveRight = new System.Windows.Forms.Button();
            this.btnMoveLeft = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.lblLat = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLong = new System.Windows.Forms.Label();
            this.pnlMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuSettings.SuspendLayout();
            this.pnlMap.SuspendLayout();
            this.pnlToolsBar.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenu.Controls.Add(this.lblMessage);
            this.pnlMenu.Controls.Add(this.groupBox3);
            this.pnlMenu.Controls.Add(this.groupBox2);
            this.pnlMenu.Controls.Add(this.groupBox1);
            this.pnlMenu.Controls.Add(this.lblMenu);
            this.pnlMenu.Location = new System.Drawing.Point(3, 15);
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(287, 775);
            this.pnlMenu.TabIndex = 0;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMessage.Location = new System.Drawing.Point(4, 703);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(77, 20);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.Text = "message";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbxTurnProhibitionsGraphModifiers);
            this.groupBox3.Controls.Add(this.chbUseTurnProhibitions);
            this.groupBox3.Controls.Add(this.chbTrafficRestrictions);
            this.groupBox3.Controls.Add(this.btnDoneEditingTurnProhibitions);
            this.groupBox3.Controls.Add(this.btnDeleteProhibition);
            this.groupBox3.Controls.Add(this.btnAddProhibition);
            this.groupBox3.Controls.Add(this.btnExport);
            this.groupBox3.Controls.Add(this.cbxExportResult);
            this.groupBox3.Controls.Add(this.cbxVisualizationForm);
            this.groupBox3.Controls.Add(this.btnRun);
            this.groupBox3.Controls.Add(this.cbxRunAlgorithm);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Location = new System.Drawing.Point(4, 351);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(277, 340);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Run Algorithm";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            this.groupBox3.Leave += new System.EventHandler(this.groupBox3_Leave);
            // 
            // cbxTurnProhibitionsGraphModifiers
            // 
            this.cbxTurnProhibitionsGraphModifiers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTurnProhibitionsGraphModifiers.FormattingEnabled = true;
            this.cbxTurnProhibitionsGraphModifiers.Location = new System.Drawing.Point(11, 113);
            this.cbxTurnProhibitionsGraphModifiers.Name = "cbxTurnProhibitionsGraphModifiers";
            this.cbxTurnProhibitionsGraphModifiers.Size = new System.Drawing.Size(259, 28);
            this.cbxTurnProhibitionsGraphModifiers.TabIndex = 10;
            this.cbxTurnProhibitionsGraphModifiers.Visible = false;
            this.cbxTurnProhibitionsGraphModifiers.SelectedIndexChanged += new System.EventHandler(this.cbxTurnProhibitionsGraphModifiers_SelectedIndexChanged);
            // 
            // chbUseTurnProhibitions
            // 
            this.chbUseTurnProhibitions.AutoSize = true;
            this.chbUseTurnProhibitions.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chbUseTurnProhibitions.Location = new System.Drawing.Point(11, 83);
            this.chbUseTurnProhibitions.Name = "chbUseTurnProhibitions";
            this.chbUseTurnProhibitions.Size = new System.Drawing.Size(214, 24);
            this.chbUseTurnProhibitions.TabIndex = 9;
            this.chbUseTurnProhibitions.Text = "Use Turn Prohibitions";
            this.chbUseTurnProhibitions.UseVisualStyleBackColor = true;
            this.chbUseTurnProhibitions.CheckedChanged += new System.EventHandler(this.chbUseTurnProhibitions_CheckedChanged);
            // 
            // chbTrafficRestrictions
            // 
            this.chbTrafficRestrictions.AutoSize = true;
            this.chbTrafficRestrictions.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chbTrafficRestrictions.Location = new System.Drawing.Point(11, 61);
            this.chbTrafficRestrictions.Name = "chbTrafficRestrictions";
            this.chbTrafficRestrictions.Size = new System.Drawing.Size(233, 24);
            this.chbTrafficRestrictions.TabIndex = 8;
            this.chbTrafficRestrictions.Text = "Use Traffic Restrictions";
            this.chbTrafficRestrictions.UseVisualStyleBackColor = true;
            this.chbTrafficRestrictions.CheckedChanged += new System.EventHandler(this.chbTrafficRestrictions_CheckedChanged);
            // 
            // btnDoneEditingTurnProhibitions
            // 
            this.btnDoneEditingTurnProhibitions.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDoneEditingTurnProhibitions.Location = new System.Drawing.Point(182, 304);
            this.btnDoneEditingTurnProhibitions.Name = "btnDoneEditingTurnProhibitions";
            this.btnDoneEditingTurnProhibitions.Size = new System.Drawing.Size(92, 29);
            this.btnDoneEditingTurnProhibitions.TabIndex = 7;
            this.btnDoneEditingTurnProhibitions.Text = "Done";
            this.btnDoneEditingTurnProhibitions.UseVisualStyleBackColor = true;
            this.btnDoneEditingTurnProhibitions.Visible = false;
            this.btnDoneEditingTurnProhibitions.Click += new System.EventHandler(this.btnDoneEditingTurnProhibitions_Click);
            // 
            // btnDeleteProhibition
            // 
            this.btnDeleteProhibition.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDeleteProhibition.Location = new System.Drawing.Point(92, 304);
            this.btnDeleteProhibition.Name = "btnDeleteProhibition";
            this.btnDeleteProhibition.Size = new System.Drawing.Size(90, 28);
            this.btnDeleteProhibition.TabIndex = 6;
            this.btnDeleteProhibition.Text = "Delete";
            this.btnDeleteProhibition.UseVisualStyleBackColor = true;
            this.btnDeleteProhibition.Visible = false;
            this.btnDeleteProhibition.Click += new System.EventHandler(this.btnDeleteProhibition_Click);
            // 
            // btnAddProhibition
            // 
            this.btnAddProhibition.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAddProhibition.Location = new System.Drawing.Point(2, 304);
            this.btnAddProhibition.Name = "btnAddProhibition";
            this.btnAddProhibition.Size = new System.Drawing.Size(90, 28);
            this.btnAddProhibition.TabIndex = 5;
            this.btnAddProhibition.Text = "Add";
            this.btnAddProhibition.UseVisualStyleBackColor = true;
            this.btnAddProhibition.Visible = false;
            this.btnAddProhibition.Click += new System.EventHandler(this.btnAddProhibition_Click);
            // 
            // btnExport
            // 
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExport.Location = new System.Drawing.Point(89, 270);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 28);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "Export Image";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cbxExportResult
            // 
            this.cbxExportResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxExportResult.FormattingEnabled = true;
            this.cbxExportResult.Location = new System.Drawing.Point(11, 200);
            this.cbxExportResult.Name = "cbxExportResult";
            this.cbxExportResult.Size = new System.Drawing.Size(259, 28);
            this.cbxExportResult.TabIndex = 3;
            // 
            // cbxVisualizationForm
            // 
            this.cbxVisualizationForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVisualizationForm.FormattingEnabled = true;
            this.cbxVisualizationForm.Location = new System.Drawing.Point(11, 166);
            this.cbxVisualizationForm.Name = "cbxVisualizationForm";
            this.cbxVisualizationForm.Size = new System.Drawing.Size(259, 28);
            this.cbxVisualizationForm.TabIndex = 2;
            this.cbxVisualizationForm.SelectedIndexChanged += new System.EventHandler(this.cbxVisualizationForm_SelectedIndexChanged);
            // 
            // btnRun
            // 
            this.btnRun.Enabled = false;
            this.btnRun.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRun.Location = new System.Drawing.Point(89, 235);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(100, 28);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // cbxRunAlgorithm
            // 
            this.cbxRunAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRunAlgorithm.FormattingEnabled = true;
            this.cbxRunAlgorithm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxRunAlgorithm.Location = new System.Drawing.Point(13, 26);
            this.cbxRunAlgorithm.Margin = new System.Windows.Forms.Padding(4);
            this.cbxRunAlgorithm.Name = "cbxRunAlgorithm";
            this.cbxRunAlgorithm.Size = new System.Drawing.Size(253, 28);
            this.cbxRunAlgorithm.Sorted = true;
            this.cbxRunAlgorithm.TabIndex = 0;
            this.cbxRunAlgorithm.SelectedIndexChanged += new System.EventHandler(this.cbxRunAlgorithm_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.cbxSaveAs);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Location = new System.Drawing.Point(4, 237);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(277, 114);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Save as";
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSave.Location = new System.Drawing.Point(89, 78);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbxSaveAs
            // 
            this.cbxSaveAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSaveAs.FormattingEnabled = true;
            this.cbxSaveAs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxSaveAs.Location = new System.Drawing.Point(9, 26);
            this.cbxSaveAs.Margin = new System.Windows.Forms.Padding(4);
            this.cbxSaveAs.Name = "cbxSaveAs";
            this.cbxSaveAs.Size = new System.Drawing.Size(259, 28);
            this.cbxSaveAs.Sorted = true;
            this.cbxSaveAs.TabIndex = 0;
            this.cbxSaveAs.SelectedIndexChanged += new System.EventHandler(this.cbxSaveAs_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadResultFile);
            this.groupBox1.Controls.Add(this.cbxResultFormat);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnLoadFile);
            this.groupBox1.Controls.Add(this.cbxGraphFormat);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(4, 49);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(277, 188);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load file";
            // 
            // btnLoadResultFile
            // 
            this.btnLoadResultFile.Enabled = false;
            this.btnLoadResultFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLoadResultFile.Location = new System.Drawing.Point(13, 153);
            this.btnLoadResultFile.Name = "btnLoadResultFile";
            this.btnLoadResultFile.Size = new System.Drawing.Size(195, 28);
            this.btnLoadResultFile.TabIndex = 7;
            this.btnLoadResultFile.Text = "Load algorithm file";
            this.btnLoadResultFile.UseVisualStyleBackColor = true;
            this.btnLoadResultFile.Click += new System.EventHandler(this.btnLoadResultFile_Click);
            // 
            // cbxResultFormat
            // 
            this.cbxResultFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxResultFormat.FormattingEnabled = true;
            this.cbxResultFormat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxResultFormat.Location = new System.Drawing.Point(129, 114);
            this.cbxResultFormat.Margin = new System.Windows.Forms.Padding(4);
            this.cbxResultFormat.Name = "cbxResultFormat";
            this.cbxResultFormat.Size = new System.Drawing.Size(139, 28);
            this.cbxResultFormat.Sorted = true;
            this.cbxResultFormat.TabIndex = 6;
            this.cbxResultFormat.SelectedIndexChanged += new System.EventHandler(this.cbxResultFormat_SelectedIndexChanged);
            this.cbxResultFormat.Click += new System.EventHandler(this.cbxResultFormat_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Algorithm file";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Graph file";
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Enabled = false;
            this.btnLoadFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLoadFile.Location = new System.Drawing.Point(13, 62);
            this.btnLoadFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(195, 28);
            this.btnLoadFile.TabIndex = 1;
            this.btnLoadFile.Text = "Load graph file";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // cbxGraphFormat
            // 
            this.cbxGraphFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGraphFormat.FormattingEnabled = true;
            this.cbxGraphFormat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxGraphFormat.Location = new System.Drawing.Point(129, 26);
            this.cbxGraphFormat.Margin = new System.Windows.Forms.Padding(4);
            this.cbxGraphFormat.Name = "cbxGraphFormat";
            this.cbxGraphFormat.Size = new System.Drawing.Size(139, 28);
            this.cbxGraphFormat.Sorted = true;
            this.cbxGraphFormat.TabIndex = 0;
            this.cbxGraphFormat.SelectedIndexChanged += new System.EventHandler(this.cbxFilesKind_SelectedIndexChanged);
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMenu.Location = new System.Drawing.Point(109, 4);
            this.lblMenu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(66, 25);
            this.lblMenu.TabIndex = 0;
            this.lblMenu.Text = "Menu";
            // 
            // menuSettings
            // 
            this.menuSettings.Dock = System.Windows.Forms.DockStyle.None;
            this.menuSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuSettings.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.menuViewGraph,
            this.menuLayers,
            this.editGraphToolStripMenuItem});
            this.menuSettings.Location = new System.Drawing.Point(0, 4);
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuSettings.Size = new System.Drawing.Size(392, 30);
            this.menuSettings.TabIndex = 4;
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProxySettings,
            this.menuCacheLocation});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.settingsToolStripMenuItem.Text = "GMap Settings";
            // 
            // menuProxySettings
            // 
            this.menuProxySettings.Name = "menuProxySettings";
            this.menuProxySettings.Size = new System.Drawing.Size(195, 26);
            this.menuProxySettings.Text = "Proxy Settings";
            this.menuProxySettings.Click += new System.EventHandler(this.menuProxySettings_Click);
            // 
            // menuCacheLocation
            // 
            this.menuCacheLocation.Name = "menuCacheLocation";
            this.menuCacheLocation.Size = new System.Drawing.Size(195, 26);
            this.menuCacheLocation.Text = "Cache Location";
            this.menuCacheLocation.Click += new System.EventHandler(this.menuCacheLocation_Click);
            // 
            // menuViewGraph
            // 
            this.menuViewGraph.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuViewNone,
            this.menuViewNodes,
            this.menuViewEdges,
            this.menuViewArcs});
            this.menuViewGraph.Name = "menuViewGraph";
            this.menuViewGraph.Size = new System.Drawing.Size(102, 26);
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
            // menuLayers
            // 
            this.menuLayers.Name = "menuLayers";
            this.menuLayers.Size = new System.Drawing.Size(66, 26);
            this.menuLayers.Text = "Layers";
            this.menuLayers.Click += new System.EventHandler(this.MenuLayers_Click);
            // 
            // editGraphToolStripMenuItem
            // 
            this.editGraphToolStripMenuItem.CheckOnClick = true;
            this.editGraphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddNode,
            this.menuAddEdge,
            this.menuAddArc,
            this.menuDeleteArc,
            this.menuDeleteEdge,
            this.menuScenarios,
            this.menuEditArc,
            this.menuEditEdge,
            this.menuDeleteNode});
            this.editGraphToolStripMenuItem.Name = "editGraphToolStripMenuItem";
            this.editGraphToolStripMenuItem.Size = new System.Drawing.Size(95, 26);
            this.editGraphToolStripMenuItem.Text = "Edit Graph";
            // 
            // menuAddNode
            // 
            this.menuAddNode.CheckOnClick = true;
            this.menuAddNode.Name = "menuAddNode";
            this.menuAddNode.Size = new System.Drawing.Size(289, 26);
            this.menuAddNode.Text = "Add Node";
            this.menuAddNode.CheckedChanged += new System.EventHandler(this.menuAddNode_CheckedChanged);
            // 
            // menuAddEdge
            // 
            this.menuAddEdge.CheckOnClick = true;
            this.menuAddEdge.Name = "menuAddEdge";
            this.menuAddEdge.Size = new System.Drawing.Size(289, 26);
            this.menuAddEdge.Text = "Add Edge";
            this.menuAddEdge.CheckedChanged += new System.EventHandler(this.menuAddEdge_CheckedChanged);
            // 
            // menuAddArc
            // 
            this.menuAddArc.CheckOnClick = true;
            this.menuAddArc.Name = "menuAddArc";
            this.menuAddArc.Size = new System.Drawing.Size(289, 26);
            this.menuAddArc.Text = "Add Arc";
            this.menuAddArc.CheckedChanged += new System.EventHandler(this.menuAddArc_CheckedChanged);
            // 
            // menuDeleteArc
            // 
            this.menuDeleteArc.CheckOnClick = true;
            this.menuDeleteArc.Name = "menuDeleteArc";
            this.menuDeleteArc.Size = new System.Drawing.Size(289, 26);
            this.menuDeleteArc.Text = "Delete Arc";
            this.menuDeleteArc.CheckedChanged += new System.EventHandler(this.menuDeleteArc_CheckedChanged);
            // 
            // menuDeleteEdge
            // 
            this.menuDeleteEdge.CheckOnClick = true;
            this.menuDeleteEdge.Name = "menuDeleteEdge";
            this.menuDeleteEdge.Size = new System.Drawing.Size(289, 26);
            this.menuDeleteEdge.Text = "Delete Edge";
            this.menuDeleteEdge.CheckedChanged += new System.EventHandler(this.menuDeleteEdge_CheckedChanged);
            // 
            // menuScenarios
            // 
            this.menuScenarios.Name = "menuScenarios";
            this.menuScenarios.Size = new System.Drawing.Size(289, 26);
            this.menuScenarios.Text = "Modify Number of Scenarios";
            this.menuScenarios.Click += new System.EventHandler(this.menuScenarios_Click);
            // 
            // menuEditArc
            // 
            this.menuEditArc.CheckOnClick = true;
            this.menuEditArc.Name = "menuEditArc";
            this.menuEditArc.Size = new System.Drawing.Size(289, 26);
            this.menuEditArc.Text = "Edit Arc";
            this.menuEditArc.CheckedChanged += new System.EventHandler(this.menuEditArc_CheckedChanged);
            // 
            // menuEditEdge
            // 
            this.menuEditEdge.CheckOnClick = true;
            this.menuEditEdge.Name = "menuEditEdge";
            this.menuEditEdge.Size = new System.Drawing.Size(289, 26);
            this.menuEditEdge.Text = "Edit Edge";
            this.menuEditEdge.CheckedChanged += new System.EventHandler(this.menuEditEdge_CheckedChanged);
            // 
            // menuDeleteNode
            // 
            this.menuDeleteNode.CheckOnClick = true;
            this.menuDeleteNode.Name = "menuDeleteNode";
            this.menuDeleteNode.Size = new System.Drawing.Size(289, 26);
            this.menuDeleteNode.Text = "Delete Node";
            this.menuDeleteNode.CheckedChanged += new System.EventHandler(this.menuDeleteNode_CheckedChanged);
            // 
            // pnlMap
            // 
            this.pnlMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMap.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMap.Controls.Add(this.gmap);
            this.pnlMap.Location = new System.Drawing.Point(297, 101);
            this.pnlMap.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(1078, 662);
            this.pnlMap.TabIndex = 1;
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.SystemColors.ActiveCaption;
            this.gmap.GrayScaleMode = true;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(-1, -1);
            this.gmap.Margin = new System.Windows.Forms.Padding(4);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 2;
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
            this.gmap.Size = new System.Drawing.Size(1079, 665);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 0D;
            this.gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmap_OnMarkerClick);
            this.gmap.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gmap_OnMarkerEnter);
            this.gmap.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.gmap_OnMarkerLeave);
            this.gmap.Load += new System.EventHandler(this.gmap_Load);
            this.gmap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gmap_KeyDown);
            this.gmap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseClick);
            this.gmap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseMove);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomIn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnZoomIn.Location = new System.Drawing.Point(1033, 4);
            this.btnZoomIn.Margin = new System.Windows.Forms.Padding(4);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(39, 37);
            this.btnZoomIn.TabIndex = 2;
            this.btnZoomIn.Text = "+";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomOut.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnZoomOut.Location = new System.Drawing.Point(1033, 38);
            this.btnZoomOut.Margin = new System.Windows.Forms.Padding(4);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(39, 37);
            this.btnZoomOut.TabIndex = 3;
            this.btnZoomOut.Text = "-";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // pnlToolsBar
            // 
            this.pnlToolsBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToolsBar.Controls.Add(this.groupBox4);
            this.pnlToolsBar.Controls.Add(this.lblClear);
            this.pnlToolsBar.Controls.Add(this.lblSelectRegion);
            this.pnlToolsBar.Controls.Add(this.lblHome);
            this.pnlToolsBar.Controls.Add(this.btnMoveRight);
            this.pnlToolsBar.Controls.Add(this.btnMoveLeft);
            this.pnlToolsBar.Controls.Add(this.btnMoveDown);
            this.pnlToolsBar.Controls.Add(this.btnMoveUp);
            this.pnlToolsBar.Controls.Add(this.btnZoomOut);
            this.pnlToolsBar.Controls.Add(this.btnZoomIn);
            this.pnlToolsBar.Controls.Add(this.menuSettings);
            this.pnlToolsBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlToolsBar.ForeColor = System.Drawing.SystemColors.Control;
            this.pnlToolsBar.Location = new System.Drawing.Point(297, 15);
            this.pnlToolsBar.Margin = new System.Windows.Forms.Padding(4);
            this.pnlToolsBar.Name = "pnlToolsBar";
            this.pnlToolsBar.Size = new System.Drawing.Size(1078, 78);
            this.pnlToolsBar.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.tbxStreet2);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.tbxStreet1);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Location = new System.Drawing.Point(435, 1);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(436, 71);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Search Address";
            // 
            // btnSearch
            // 
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearch.Location = new System.Drawing.Point(338, 33);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(87, 28);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbxStreet2
            // 
            this.tbxStreet2.Location = new System.Drawing.Point(181, 35);
            this.tbxStreet2.Name = "tbxStreet2";
            this.tbxStreet2.Size = new System.Drawing.Size(139, 26);
            this.tbxStreet2.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(153, 37);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "&&";
            // 
            // tbxStreet1
            // 
            this.tbxStreet1.Location = new System.Drawing.Point(7, 35);
            this.tbxStreet1.Name = "tbxStreet1";
            this.tbxStreet1.Size = new System.Drawing.Size(139, 26);
            this.tbxStreet1.TabIndex = 0;
            // 
            // lblClear
            // 
            this.lblClear.AutoSize = true;
            this.lblClear.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClear.Location = new System.Drawing.Point(165, 47);
            this.lblClear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClear.Name = "lblClear";
            this.lblClear.Size = new System.Drawing.Size(61, 26);
            this.lblClear.TabIndex = 11;
            this.lblClear.Text = "Clear";
            this.lblClear.Click += new System.EventHandler(this.lblClear_Click);
            // 
            // lblSelectRegion
            // 
            this.lblSelectRegion.AutoSize = true;
            this.lblSelectRegion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSelectRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectRegion.Location = new System.Drawing.Point(4, 47);
            this.lblSelectRegion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectRegion.Name = "lblSelectRegion";
            this.lblSelectRegion.Size = new System.Drawing.Size(143, 26);
            this.lblSelectRegion.TabIndex = 10;
            this.lblSelectRegion.Text = "Select Region";
            this.lblSelectRegion.Click += new System.EventHandler(this.lblSelectRegion_Click);
            // 
            // lblHome
            // 
            this.lblHome.AutoSize = true;
            this.lblHome.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHome.Location = new System.Drawing.Point(918, 27);
            this.lblHome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(68, 26);
            this.lblHome.TabIndex = 9;
            this.lblHome.Text = "Home";
            this.lblHome.Click += new System.EventHandler(this.lblHome_Click);
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveRight.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMoveRight.Location = new System.Drawing.Point(994, 25);
            this.btnMoveRight.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(31, 28);
            this.btnMoveRight.TabIndex = 8;
            this.btnMoveRight.Text = ">";
            this.btnMoveRight.UseVisualStyleBackColor = true;
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveLeft.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMoveLeft.Location = new System.Drawing.Point(879, 25);
            this.btnMoveLeft.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(31, 28);
            this.btnMoveLeft.TabIndex = 7;
            this.btnMoveLeft.Text = "<";
            this.btnMoveLeft.UseVisualStyleBackColor = true;
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveDown.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMoveDown.Location = new System.Drawing.Point(938, 49);
            this.btnMoveDown.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(31, 28);
            this.btnMoveDown.TabIndex = 6;
            this.btnMoveDown.Text = "v";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveUp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMoveUp.Location = new System.Drawing.Point(938, -1);
            this.btnMoveUp.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(31, 28);
            this.btnMoveUp.TabIndex = 5;
            this.btnMoveUp.Text = "^";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // lblLat
            // 
            this.lblLat.AutoSize = true;
            this.lblLat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLat.ForeColor = System.Drawing.SystemColors.Control;
            this.lblLat.Location = new System.Drawing.Point(364, 767);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new System.Drawing.Size(18, 20);
            this.lblLat.TabIndex = 12;
            this.lblLat.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(320, 767);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Lat:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(928, 767);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Lng:";
            // 
            // lblLong
            // 
            this.lblLong.AutoSize = true;
            this.lblLong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLong.ForeColor = System.Drawing.SystemColors.Control;
            this.lblLong.Location = new System.Drawing.Point(976, 767);
            this.lblLong.Name = "lblLong";
            this.lblLong.Size = new System.Drawing.Size(18, 20);
            this.lblLong.TabIndex = 15;
            this.lblLong.Text = "0";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1376, 791);
            this.Controls.Add(this.lblLong);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlToolsBar);
            this.Controls.Add(this.lblLat);
            this.Controls.Add(this.pnlMap);
            this.Controls.Add(this.pnlMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1394, 838);
            this.MinimumSize = new System.Drawing.Size(1394, 819);
            this.Name = "frmMain";
            this.Text = "Visual NGAT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuSettings.ResumeLayout(false);
            this.menuSettings.PerformLayout();
            this.pnlMap.ResumeLayout(false);
            this.pnlToolsBar.ResumeLayout(false);
            this.pnlToolsBar.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlMap;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.ComboBox cbxGraphFormat;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbxSaveAs;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.SaveFileDialog dlgSaveAs;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ComboBox cbxRunAlgorithm;
        private System.Windows.Forms.MenuStrip menuSettings;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuProxySettings;
        private System.Windows.Forms.ToolStripMenuItem menuCacheLocation;
        private System.Windows.Forms.Panel pnlToolsBar;
        private System.Windows.Forms.ToolStripMenuItem menuViewGraph;
        private System.Windows.Forms.ToolStripMenuItem menuViewNone;
        private System.Windows.Forms.ToolStripMenuItem menuViewNodes;
        private System.Windows.Forms.ToolStripMenuItem menuViewEdges;
        private System.Windows.Forms.ToolStripMenuItem menuViewArcs;
        private System.Windows.Forms.ComboBox cbxResultFormat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMoveRight;
        private System.Windows.Forms.Button btnMoveLeft;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Label lblHome;
        private System.Windows.Forms.Label lblClear;
        private System.Windows.Forms.Label lblSelectRegion;
        private System.Windows.Forms.ToolStripMenuItem menuLayers;
        private System.Windows.Forms.Label lblLat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLong;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxStreet2;
        private System.Windows.Forms.TextBox tbxStreet1;
        private System.Windows.Forms.Button btnLoadResultFile;
        private System.Windows.Forms.ComboBox cbxVisualizationForm;
        private System.Windows.Forms.ComboBox cbxExportResult;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ToolStripMenuItem editGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddNode;
        private System.Windows.Forms.ToolStripMenuItem menuAddEdge;
        private System.Windows.Forms.ToolStripMenuItem menuAddArc;
        private System.Windows.Forms.Button btnDoneEditingTurnProhibitions;
        private System.Windows.Forms.Button btnDeleteProhibition;
        private System.Windows.Forms.Button btnAddProhibition;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteArc;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteEdge;
        private System.Windows.Forms.ToolStripMenuItem menuScenarios;
        private System.Windows.Forms.ToolStripMenuItem menuEditArc;
        private System.Windows.Forms.ToolStripMenuItem menuEditEdge;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteNode;
        private System.Windows.Forms.CheckBox chbUseTurnProhibitions;
        private System.Windows.Forms.CheckBox chbTrafficRestrictions;
        private System.Windows.Forms.ComboBox cbxTurnProhibitionsGraphModifiers;
    }
}

