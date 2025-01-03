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
    public partial class frmArcEdgeCosts : Form
    {
        public double[] scenariosCost;
        int index = 0;
        int [] lblIndex;
        public frmArcEdgeCosts(int scenarioCount)
        {
            InitializeComponent();
            scenariosCost = new double[scenarioCount];
            lblIndex = new int[scenarioCount];
        }

        public frmArcEdgeCosts(double [] scenariosCosts)
        {
            InitializeComponent();
            this.scenariosCost = scenariosCosts;
            index = scenariosCost.Length;
            lblIndex = new int[scenariosCost.Length];
            for (int i = 0; i < scenariosCost.Length; i++)
            {
                lblIndex[i] = lblAddedCosts.Text.Length == 0 ? 0 : lblAddedCosts.Text.Length;
                lblAddedCosts.Text = lblAddedCosts.Text + "R" + i + "-" + scenariosCost[i] + "\n";
            }
            btnAdd.Enabled = false;
            btnAccept.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (double.TryParse(tbxCost.Text, out double input))
            {
                lblIndex[index] = lblAddedCosts.Text.Length;
                scenariosCost[index] = input;
                lblAddedCosts.Text = lblAddedCosts.Text + "R" + index + "-" + scenariosCost[index] + "\n";
                index++;
                tbxCost.Clear();
                if (index == scenariosCost.Length)
                {
                    btnAdd.Enabled = false;
                    btnAccept.Enabled = true;
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (index>0)
            {
                index--;
                scenariosCost[index] = 0;
                lblAddedCosts.Text = lblAddedCosts.Text.Remove(lblIndex[index]);
                btnAdd.Enabled = true;
                btnAccept.Enabled = false;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
