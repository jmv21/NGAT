namespace NGAT.WindowsAPI.Forms
{
    partial class frmProxySettings
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
            this.rdbtnNoProxy = new System.Windows.Forms.RadioButton();
            this.lblMenu = new System.Windows.Forms.Label();
            this.rdbtnAtomatic = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxUserName = new System.Windows.Forms.TextBox();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rdbtnNoProxy
            // 
            this.rdbtnNoProxy.AutoSize = true;
            this.rdbtnNoProxy.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnNoProxy.Location = new System.Drawing.Point(12, 21);
            this.rdbtnNoProxy.Name = "rdbtnNoProxy";
            this.rdbtnNoProxy.Size = new System.Drawing.Size(96, 22);
            this.rdbtnNoProxy.TabIndex = 0;
            this.rdbtnNoProxy.TabStop = true;
            this.rdbtnNoProxy.Text = "No Proxy";
            this.rdbtnNoProxy.UseVisualStyleBackColor = true;
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMenu.Location = new System.Drawing.Point(8, 60);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(170, 20);
            this.lblMenu.TabIndex = 1;
            this.lblMenu.Text = "Proxy Configuration:";
            // 
            // rdbtnAtomatic
            // 
            this.rdbtnAtomatic.AutoSize = true;
            this.rdbtnAtomatic.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnAtomatic.Location = new System.Drawing.Point(330, 21);
            this.rdbtnAtomatic.Name = "rdbtnAtomatic";
            this.rdbtnAtomatic.Size = new System.Drawing.Size(149, 22);
            this.rdbtnAtomatic.TabIndex = 2;
            this.rdbtnAtomatic.TabStop = true;
            this.rdbtnAtomatic.Text = "Automatic Proxy";
            this.rdbtnAtomatic.UseVisualStyleBackColor = true;
            this.rdbtnAtomatic.CheckedChanged += new System.EventHandler(this.rdbtnAtomatic_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(13, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Proxy Server";
            // 
            // tbxServer
            // 
            this.tbxServer.Enabled = false;
            this.tbxServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxServer.Location = new System.Drawing.Point(116, 92);
            this.tbxServer.Name = "tbxServer";
            this.tbxServer.Size = new System.Drawing.Size(171, 22);
            this.tbxServer.TabIndex = 4;
            this.tbxServer.Text = "proxy-est.uh.cu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(9, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "User Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(19, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // tbxUserName
            // 
            this.tbxUserName.Enabled = false;
            this.tbxUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUserName.Location = new System.Drawing.Point(101, 131);
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new System.Drawing.Size(262, 22);
            this.tbxUserName.TabIndex = 7;
            // 
            // tbxPassword
            // 
            this.tbxPassword.Enabled = false;
            this.tbxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPassword.Location = new System.Drawing.Point(101, 168);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '*';
            this.tbxPassword.Size = new System.Drawing.Size(262, 22);
            this.tbxPassword.TabIndex = 8;
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLoadFile.Location = new System.Drawing.Point(232, 213);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(75, 29);
            this.btnLoadFile.TabIndex = 9;
            this.btnLoadFile.Text = "Save";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(327, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Port";
            // 
            // tbxPort
            // 
            this.tbxPort.Enabled = false;
            this.tbxPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPort.Location = new System.Drawing.Point(369, 92);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(116, 22);
            this.tbxPort.TabIndex = 11;
            this.tbxPort.Text = "3128";
            // 
            // frmProxySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(537, 254);
            this.Controls.Add(this.tbxPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.tbxPassword);
            this.Controls.Add(this.tbxUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdbtnAtomatic);
            this.Controls.Add(this.lblMenu);
            this.Controls.Add(this.rdbtnNoProxy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmProxySettings";
            this.Text = "Proxy Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbtnNoProxy;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.RadioButton rdbtnAtomatic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxUserName;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxPort;
    }
}