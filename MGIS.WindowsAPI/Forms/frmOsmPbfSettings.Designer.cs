namespace NGAT.WindowsAPI.Forms
{
    partial class frmOsmPbfSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOsmPbfSettings));
            this.btnDone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxAttribute = new System.Windows.Forms.TextBox();
            this.btnAddNodeAttr = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbxLinkAttr = new System.Windows.Forms.TextBox();
            this.btnAddLinkAttr = new System.Windows.Forms.Button();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.cbxPedestrian = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDone
            // 
            this.btnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDone.Location = new System.Drawing.Point(93, 316);
            this.btnDone.Margin = new System.Windows.Forms.Padding(4);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(100, 28);
            this.btnDone.TabIndex = 2;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.BtnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Location = new System.Drawing.Point(402, 316);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxAttribute);
            this.groupBox1.Controls.Add(this.btnAddNodeAttr);
            this.groupBox1.Controls.Add(this.checkedListBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(13, 1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(274, 269);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nodes Attributes";
            // 
            // tbxAttribute
            // 
            this.tbxAttribute.Location = new System.Drawing.Point(51, 220);
            this.tbxAttribute.Name = "tbxAttribute";
            this.tbxAttribute.Size = new System.Drawing.Size(191, 26);
            this.tbxAttribute.TabIndex = 5;
            // 
            // btnAddNodeAttr
            // 
            this.btnAddNodeAttr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNodeAttr.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAddNodeAttr.Location = new System.Drawing.Point(8, 220);
            this.btnAddNodeAttr.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddNodeAttr.Name = "btnAddNodeAttr";
            this.btnAddNodeAttr.Size = new System.Drawing.Size(36, 28);
            this.btnAddNodeAttr.TabIndex = 4;
            this.btnAddNodeAttr.Text = "+";
            this.btnAddNodeAttr.UseVisualStyleBackColor = true;
            this.btnAddNodeAttr.Click += new System.EventHandler(this.Button2_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "amenity",
            "building",
            "shop",
            "highway"});
            this.checkedListBox1.Location = new System.Drawing.Point(12, 25);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(255, 189);
            this.checkedListBox1.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbxLinkAttr);
            this.groupBox2.Controls.Add(this.btnAddLinkAttr);
            this.groupBox2.Controls.Add(this.checkedListBox2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Location = new System.Drawing.Point(304, 1);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(270, 269);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Links Attributes";
            // 
            // tbxLinkAttr
            // 
            this.tbxLinkAttr.Location = new System.Drawing.Point(51, 220);
            this.tbxLinkAttr.Name = "tbxLinkAttr";
            this.tbxLinkAttr.Size = new System.Drawing.Size(191, 26);
            this.tbxLinkAttr.TabIndex = 5;
            // 
            // btnAddLinkAttr
            // 
            this.btnAddLinkAttr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddLinkAttr.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAddLinkAttr.Location = new System.Drawing.Point(8, 220);
            this.btnAddLinkAttr.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddLinkAttr.Name = "btnAddLinkAttr";
            this.btnAddLinkAttr.Size = new System.Drawing.Size(36, 28);
            this.btnAddLinkAttr.TabIndex = 4;
            this.btnAddLinkAttr.Text = "+";
            this.btnAddLinkAttr.UseVisualStyleBackColor = true;
            this.btnAddLinkAttr.Click += new System.EventHandler(this.BtnAddLinkAttr_Click);
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkedListBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Items.AddRange(new object[] {
            "amenity",
            "highway",
            "railway",
            "waterway"});
            this.checkedListBox2.Location = new System.Drawing.Point(12, 25);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(241, 189);
            this.checkedListBox2.TabIndex = 3;
            // 
            // cbxPedestrian
            // 
            this.cbxPedestrian.AutoSize = true;
            this.cbxPedestrian.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPedestrian.ForeColor = System.Drawing.Color.Black;
            this.cbxPedestrian.Location = new System.Drawing.Point(25, 278);
            this.cbxPedestrian.Name = "cbxPedestrian";
            this.cbxPedestrian.Size = new System.Drawing.Size(187, 22);
            this.cbxPedestrian.TabIndex = 7;
            this.cbxPedestrian.Text = "Pedestrian Network?";
            this.cbxPedestrian.UseVisualStyleBackColor = true;
            // 
            // frmOsmPbfSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(590, 357);
            this.Controls.Add(this.cbxPedestrian);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDone);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOsmPbfSettings";
            this.Text = "OsmPbf Graph Settings";
            this.Leave += new System.EventHandler(this.frmOsmPbfSettings_Leave);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TextBox tbxAttribute;
        private System.Windows.Forms.Button btnAddNodeAttr;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbxLinkAttr;
        private System.Windows.Forms.Button btnAddLinkAttr;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.CheckBox cbxPedestrian;
    }
}