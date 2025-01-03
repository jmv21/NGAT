namespace NGAT.Visual.Forms
{
    partial class frmArcEdgeCosts
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
            this.tbxCost = new System.Windows.Forms.TextBox();
            this.lblAddedCosts = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxCost
            // 
            this.tbxCost.Location = new System.Drawing.Point(12, 12);
            this.tbxCost.Name = "tbxCost";
            this.tbxCost.Size = new System.Drawing.Size(100, 22);
            this.tbxCost.TabIndex = 0;
            // 
            // lblAddedCosts
            // 
            this.lblAddedCosts.AutoSize = true;
            this.lblAddedCosts.Location = new System.Drawing.Point(12, 47);
            this.lblAddedCosts.Name = "lblAddedCosts";
            this.lblAddedCosts.Size = new System.Drawing.Size(0, 16);
            this.lblAddedCosts.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(159, 11);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Enabled = false;
            this.btnAccept.Location = new System.Drawing.Point(159, 80);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(159, 44);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frmArcEdgeCosts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 115);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblAddedCosts);
            this.Controls.Add(this.tbxCost);
            this.Name = "frmArcEdgeCosts";
            this.Text = "frmArcEdgeCosts";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxCost;
        private System.Windows.Forms.Label lblAddedCosts;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnRemove;
    }
}