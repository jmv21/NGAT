using System;
using System.Windows.Forms;

namespace NGAT.WindowsAPI.Forms
{
    public partial class frmProxySettings : Form
    {

        public bool UseProxy { get; private set; }
        public string ProxyServer { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public int Port { get; private set; }

        public frmProxySettings()
        {
            InitializeComponent();
            UseProxy = false;
        }

        private void rdbtnAtomatic_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbtnAtomatic.Checked)
            {
                tbxPassword.Enabled = true;
                tbxPort.Enabled = true;
                tbxServer.Enabled = true;
                tbxUserName.Enabled = true;
            }
            else
            {
                tbxPassword.Enabled = false;
                tbxPort.Enabled = false;
                tbxServer.Enabled = false;
                tbxUserName.Enabled = false;
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbtnAtomatic.Checked)
                {
                    UseProxy = true;
                    ProxyServer = tbxServer.Text;
                    Port = int.Parse(tbxPort.Text);
                    UserName = tbxUserName.Text;
                    Password = tbxPassword.Text;
                }
                else UseProxy = false;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error ocurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
