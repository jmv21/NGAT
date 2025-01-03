namespace NGAT.Visual.Forms
{
    partial class frmScenariosCount
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
            this.btnAccept = new System.Windows.Forms.Button();
            this.cbxScenariosCount = new System.Windows.Forms.ComboBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(204, 68);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // cbxScenariosCount
            // 
            this.cbxScenariosCount.FormattingEnabled = true;
            this.cbxScenariosCount.Location = new System.Drawing.Point(12, 12);
            this.cbxScenariosCount.Name = "cbxScenariosCount";
            this.cbxScenariosCount.Size = new System.Drawing.Size(129, 24);
            this.cbxScenariosCount.TabIndex = 1;
            this.cbxScenariosCount.SelectedIndexChanged += new System.EventHandler(this.cbxScenariosCount_SelectedIndexChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(9, 50);
            this.lblMessage.MaximumSize = new System.Drawing.Size(180, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 16);
            this.lblMessage.TabIndex = 2;
            // 
            // frmScenariosCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(291, 103);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.cbxScenariosCount);
            this.Controls.Add(this.btnAccept);
            this.Name = "frmScenariosCount";
            this.Text = "frmScenariosCount";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.ComboBox cbxScenariosCount;
        private System.Windows.Forms.Label lblMessage;
    }
}