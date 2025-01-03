using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NGAT.Visual.Forms
{
    public partial class frmScenariosCount : Form
    {
        int actualScenariosCount;
        public int selectedScenariosCount;
        public frmScenariosCount(int actualScenariosCount)
        {
            InitializeComponent();
            this.actualScenariosCount = actualScenariosCount;
            for (int i = 1; i < 11; i++)
            {
                cbxScenariosCount.Items.Add(i.ToString());
            }
                cbxScenariosCount.SelectedIndex = actualScenariosCount-1;
        }

        private void cbxScenariosCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedScenariosCount = cbxScenariosCount.SelectedIndex + 1;
            if (int.Parse(cbxScenariosCount.SelectedItem as string) == actualScenariosCount)
                lblMessage.Text = "";
            else if (int.Parse(cbxScenariosCount.SelectedItem as string) > actualScenariosCount)
            {
                lblMessage.Text = "Costs with zero value will be added to all the new scenarios of the edges and arcs of the graph";
            }
            else
            {
                lblMessage.Text = "All scenarios with an index greater than the new number of scenarios will be eliminated on all edges and arcs of the graph.";
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
