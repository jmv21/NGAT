namespace NGAT.GraphGenerator.Visual
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cbxSelectGenerator = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblExportAs = new System.Windows.Forms.Label();
            this.cbxExportAs = new System.Windows.Forms.ComboBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.cbxAmount = new System.Windows.Forms.ComboBox();
            this.lblSelectGenerator = new System.Windows.Forms.Label();
            this.dgvVisualizer = new System.Windows.Forms.DataGridView();
            this.btnNextGraph = new System.Windows.Forms.Button();
            this.btnPreviusGraph = new System.Windows.Forms.Button();
            this.cbxSelectGraph = new System.Windows.Forms.ComboBox();
            this.lblSelectGraph = new System.Windows.Forms.Label();
            this.btnNextScenario = new System.Windows.Forms.Button();
            this.btnPreviusScenario = new System.Windows.Forms.Button();
            this.cbxSelectScenario = new System.Windows.Forms.ComboBox();
            this.lblSelectScenario = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisualizer)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxSelectGenerator
            // 
            this.cbxSelectGenerator.FormattingEnabled = true;
            this.cbxSelectGenerator.Location = new System.Drawing.Point(10, 34);
            this.cbxSelectGenerator.Name = "cbxSelectGenerator";
            this.cbxSelectGenerator.Size = new System.Drawing.Size(166, 28);
            this.cbxSelectGenerator.TabIndex = 0;
            this.cbxSelectGenerator.SelectedIndexChanged += new System.EventHandler(this.cbxSelectGenerator_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.lblDescription);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.lblExportAs);
            this.panel1.Controls.Add(this.cbxExportAs);
            this.panel1.Controls.Add(this.lblAmount);
            this.panel1.Controls.Add(this.cbxAmount);
            this.panel1.Controls.Add(this.lblSelectGenerator);
            this.panel1.Controls.Add(this.cbxSelectGenerator);
            this.panel1.Location = new System.Drawing.Point(2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(197, 490);
            this.panel1.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(10, 298);
            this.lblDescription.MaximumSize = new System.Drawing.Size(200, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(0, 20);
            this.lblDescription.TabIndex = 7;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGenerate.Location = new System.Drawing.Point(10, 125);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(94, 29);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExport.Location = new System.Drawing.Point(10, 262);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(94, 29);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblExportAs
            // 
            this.lblExportAs.AutoSize = true;
            this.lblExportAs.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblExportAs.Location = new System.Drawing.Point(10, 202);
            this.lblExportAs.Name = "lblExportAs";
            this.lblExportAs.Size = new System.Drawing.Size(87, 23);
            this.lblExportAs.TabIndex = 4;
            this.lblExportAs.Text = "Export As";
            // 
            // cbxExportAs
            // 
            this.cbxExportAs.FormattingEnabled = true;
            this.cbxExportAs.Location = new System.Drawing.Point(10, 228);
            this.cbxExportAs.Name = "cbxExportAs";
            this.cbxExportAs.Size = new System.Drawing.Size(166, 28);
            this.cbxExportAs.TabIndex = 3;
            this.cbxExportAs.SelectedIndexChanged += new System.EventHandler(this.cbxExportAs_SelectedIndexChanged);
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAmount.Location = new System.Drawing.Point(10, 65);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(128, 23);
            this.lblAmount.TabIndex = 2;
            this.lblAmount.Text = "Select Amount";
            // 
            // cbxAmount
            // 
            this.cbxAmount.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxAmount.FormattingEnabled = true;
            this.cbxAmount.IntegralHeight = false;
            this.cbxAmount.Location = new System.Drawing.Point(10, 91);
            this.cbxAmount.Name = "cbxAmount";
            this.cbxAmount.Size = new System.Drawing.Size(166, 28);
            this.cbxAmount.TabIndex = 2;
            // 
            // lblSelectGenerator
            // 
            this.lblSelectGenerator.AutoSize = true;
            this.lblSelectGenerator.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSelectGenerator.Location = new System.Drawing.Point(10, 8);
            this.lblSelectGenerator.Name = "lblSelectGenerator";
            this.lblSelectGenerator.Size = new System.Drawing.Size(143, 23);
            this.lblSelectGenerator.TabIndex = 1;
            this.lblSelectGenerator.Text = "Select Generator";
            // 
            // dgvVisualizer
            // 
            this.dgvVisualizer.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvVisualizer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVisualizer.Location = new System.Drawing.Point(205, 66);
            this.dgvVisualizer.Name = "dgvVisualizer";
            this.dgvVisualizer.RowHeadersWidth = 51;
            this.dgvVisualizer.RowTemplate.Height = 29;
            this.dgvVisualizer.Size = new System.Drawing.Size(591, 357);
            this.dgvVisualizer.TabIndex = 2;
            // 
            // btnNextGraph
            // 
            this.btnNextGraph.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNextGraph.Location = new System.Drawing.Point(720, 30);
            this.btnNextGraph.Name = "btnNextGraph";
            this.btnNextGraph.Size = new System.Drawing.Size(68, 29);
            this.btnNextGraph.TabIndex = 3;
            this.btnNextGraph.Text = "Next";
            this.btnNextGraph.UseVisualStyleBackColor = true;
            this.btnNextGraph.Click += new System.EventHandler(this.btnNextGraph_Click);
            // 
            // btnPreviusGraph
            // 
            this.btnPreviusGraph.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPreviusGraph.Location = new System.Drawing.Point(489, 30);
            this.btnPreviusGraph.Name = "btnPreviusGraph";
            this.btnPreviusGraph.Size = new System.Drawing.Size(68, 29);
            this.btnPreviusGraph.TabIndex = 4;
            this.btnPreviusGraph.Text = "Previus";
            this.btnPreviusGraph.UseVisualStyleBackColor = true;
            this.btnPreviusGraph.Click += new System.EventHandler(this.btnPreviusGraph_Click);
            // 
            // cbxSelectGraph
            // 
            this.cbxSelectGraph.FormattingEnabled = true;
            this.cbxSelectGraph.Location = new System.Drawing.Point(563, 31);
            this.cbxSelectGraph.Name = "cbxSelectGraph";
            this.cbxSelectGraph.Size = new System.Drawing.Size(151, 28);
            this.cbxSelectGraph.TabIndex = 5;
            this.cbxSelectGraph.SelectedIndexChanged += new System.EventHandler(this.cbxSelectGraph_SelectedIndexChanged);
            // 
            // lblSelectGraph
            // 
            this.lblSelectGraph.AutoSize = true;
            this.lblSelectGraph.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSelectGraph.Location = new System.Drawing.Point(563, 8);
            this.lblSelectGraph.Name = "lblSelectGraph";
            this.lblSelectGraph.Size = new System.Drawing.Size(112, 23);
            this.lblSelectGraph.TabIndex = 6;
            this.lblSelectGraph.Text = "Select Graph";
            // 
            // btnNextScenario
            // 
            this.btnNextScenario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNextScenario.Location = new System.Drawing.Point(720, 451);
            this.btnNextScenario.Name = "btnNextScenario";
            this.btnNextScenario.Size = new System.Drawing.Size(68, 29);
            this.btnNextScenario.TabIndex = 7;
            this.btnNextScenario.Text = "Next";
            this.btnNextScenario.UseVisualStyleBackColor = true;
            this.btnNextScenario.Click += new System.EventHandler(this.btnNextScenario_Click);
            // 
            // btnPreviusScenario
            // 
            this.btnPreviusScenario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPreviusScenario.Location = new System.Drawing.Point(489, 451);
            this.btnPreviusScenario.Name = "btnPreviusScenario";
            this.btnPreviusScenario.Size = new System.Drawing.Size(68, 29);
            this.btnPreviusScenario.TabIndex = 8;
            this.btnPreviusScenario.Text = "Previus";
            this.btnPreviusScenario.UseVisualStyleBackColor = true;
            this.btnPreviusScenario.Click += new System.EventHandler(this.btnPreviusScenario_Click);
            // 
            // cbxSelectScenario
            // 
            this.cbxSelectScenario.FormattingEnabled = true;
            this.cbxSelectScenario.Location = new System.Drawing.Point(563, 451);
            this.cbxSelectScenario.Name = "cbxSelectScenario";
            this.cbxSelectScenario.Size = new System.Drawing.Size(151, 28);
            this.cbxSelectScenario.TabIndex = 9;
            this.cbxSelectScenario.SelectedIndexChanged += new System.EventHandler(this.cbxSelectScenario_SelectedIndexChanged);
            // 
            // lblSelectScenario
            // 
            this.lblSelectScenario.AutoSize = true;
            this.lblSelectScenario.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSelectScenario.Location = new System.Drawing.Point(563, 428);
            this.lblSelectScenario.Name = "lblSelectScenario";
            this.lblSelectScenario.Size = new System.Drawing.Size(131, 23);
            this.lblSelectScenario.TabIndex = 10;
            this.lblSelectScenario.Text = "Select Scenario";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 492);
            this.Controls.Add(this.lblSelectScenario);
            this.Controls.Add(this.cbxSelectScenario);
            this.Controls.Add(this.btnPreviusScenario);
            this.Controls.Add(this.btnNextScenario);
            this.Controls.Add(this.lblSelectGraph);
            this.Controls.Add(this.cbxSelectGraph);
            this.Controls.Add(this.btnPreviusGraph);
            this.Controls.Add(this.btnNextGraph);
            this.Controls.Add(this.dgvVisualizer);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "GraphGenerator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisualizer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox cbxSelectGenerator;
        private Panel panel1;
        private Button btnGenerate;
        private Button btnExport;
        private Label lblExportAs;
        private ComboBox cbxExportAs;
        private Label lblAmount;
        private ComboBox cbxAmount;
        private Label lblSelectGenerator;
        private DataGridView dgvVisualizer;
        private Button btnNextGraph;
        private Button btnPreviusGraph;
        private ComboBox cbxSelectGraph;
        private Label lblSelectGraph;
        private Button btnNextScenario;
        private Button btnPreviusScenario;
        private ComboBox cbxSelectScenario;
        private Label lblSelectScenario;
        private Label lblDescription;
    }
}