namespace NGAT.WindowsAPI.Forms
{
    partial class frmLayers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayers));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.clbActiveLayers = new System.Windows.Forms.CheckedListBox();
            this.lblMenu = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxLayerProvider = new System.Windows.Forms.ComboBox();
            this.cbxLayerFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxLayername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pbxLayerMarquer = new System.Windows.Forms.PictureBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.dlgLoadIcon = new System.Windows.Forms.OpenFileDialog();
            this.btnLoadProvider = new System.Windows.Forms.Button();
            this.dlgOpenProvider = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLayerMarquer)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.clbActiveLayers);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(13, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(317, 423);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Created Layers";
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRemove.Location = new System.Drawing.Point(123, 385);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(83, 28);
            this.btnRemove.TabIndex = 19;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // clbActiveLayers
            // 
            this.clbActiveLayers.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.clbActiveLayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbActiveLayers.FormattingEnabled = true;
            this.clbActiveLayers.Location = new System.Drawing.Point(12, 25);
            this.clbActiveLayers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clbActiveLayers.Name = "clbActiveLayers";
            this.clbActiveLayers.Size = new System.Drawing.Size(299, 336);
            this.clbActiveLayers.TabIndex = 3;
            this.clbActiveLayers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ClbActiveLayers_ItemCheck);
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMenu.Location = new System.Drawing.Point(487, 14);
            this.lblMenu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(185, 25);
            this.lblMenu.TabIndex = 8;
            this.lblMenu.Text = "Create New Layer";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(381, 66);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Layer Provider";
            // 
            // cbxLayerProvider
            // 
            this.cbxLayerProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLayerProvider.FormattingEnabled = true;
            this.cbxLayerProvider.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxLayerProvider.Location = new System.Drawing.Point(532, 66);
            this.cbxLayerProvider.Margin = new System.Windows.Forms.Padding(4);
            this.cbxLayerProvider.Name = "cbxLayerProvider";
            this.cbxLayerProvider.Size = new System.Drawing.Size(197, 24);
            this.cbxLayerProvider.Sorted = true;
            this.cbxLayerProvider.TabIndex = 10;
            this.cbxLayerProvider.SelectedIndexChanged += new System.EventHandler(this.CbxLayerProvider_SelectedIndexChanged);
            // 
            // cbxLayerFilter
            // 
            this.cbxLayerFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLayerFilter.FormattingEnabled = true;
            this.cbxLayerFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxLayerFilter.Location = new System.Drawing.Point(532, 134);
            this.cbxLayerFilter.Margin = new System.Windows.Forms.Padding(4);
            this.cbxLayerFilter.Name = "cbxLayerFilter";
            this.cbxLayerFilter.Size = new System.Drawing.Size(256, 24);
            this.cbxLayerFilter.Sorted = true;
            this.cbxLayerFilter.TabIndex = 12;
            this.cbxLayerFilter.SelectedIndexChanged += new System.EventHandler(this.cbxLayerFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(381, 134);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Layer Filter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(381, 207);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Layer Name";
            // 
            // tbxLayername
            // 
            this.tbxLayername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxLayername.Location = new System.Drawing.Point(532, 204);
            this.tbxLayername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbxLayername.Name = "tbxLayername";
            this.tbxLayername.Size = new System.Drawing.Size(256, 27);
            this.tbxLayername.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(381, 284);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Layer Marker";
            // 
            // pbxLayerMarquer
            // 
            this.pbxLayerMarquer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxLayerMarquer.Location = new System.Drawing.Point(540, 262);
            this.pbxLayerMarquer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbxLayerMarquer.Name = "pbxLayerMarquer";
            this.pbxLayerMarquer.Size = new System.Drawing.Size(79, 64);
            this.pbxLayerMarquer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxLayerMarquer.TabIndex = 16;
            this.pbxLayerMarquer.TabStop = false;
            this.pbxLayerMarquer.Click += new System.EventHandler(this.PbxLayerMarquer_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCreate.Location = new System.Drawing.Point(537, 393);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(83, 34);
            this.btnCreate.TabIndex = 17;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // btnLoadProvider
            // 
            this.btnLoadProvider.Enabled = false;
            this.btnLoadProvider.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadProvider.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLoadProvider.Location = new System.Drawing.Point(744, 66);
            this.btnLoadProvider.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadProvider.Name = "btnLoadProvider";
            this.btnLoadProvider.Size = new System.Drawing.Size(44, 26);
            this.btnLoadProvider.TabIndex = 18;
            this.btnLoadProvider.Text = "...";
            this.btnLoadProvider.UseVisualStyleBackColor = true;
            this.btnLoadProvider.Click += new System.EventHandler(this.BtnLoadProvider_Click);
            // 
            // dlgOpenProvider
            // 
            this.dlgOpenProvider.InitialDirectory = "Resources\\\\BitMaps\\\\";
            // 
            // frmLayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 448);
            this.Controls.Add(this.btnLoadProvider);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.pbxLayerMarquer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxLayername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxLayerFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxLayerProvider);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMenu);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(818, 495);
            this.MinimumSize = new System.Drawing.Size(818, 495);
            this.Name = "frmLayers";
            this.Text = "Manage Layers";
            this.Load += new System.EventHandler(this.FrmLayers_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxLayerMarquer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox clbActiveLayers;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxLayerProvider;
        private System.Windows.Forms.ComboBox cbxLayerFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxLayername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbxLayerMarquer;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.OpenFileDialog dlgLoadIcon;
        private System.Windows.Forms.Button btnLoadProvider;
        private System.Windows.Forms.OpenFileDialog dlgOpenProvider;
        private System.Windows.Forms.Button btnRemove;
    }
}