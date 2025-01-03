using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NGAT.GraphGenerator.Visual
{
    public partial class frmNeededParameters : Form
    {
        List<(Type, string)> parameters;
        int index;
        public object[] setParameters;
        Type a;
        public frmNeededParameters(List<(Type,string)> parameters)
        {
            InitializeComponent();
            this.parameters = parameters;
            setParameters = new object[parameters.Count];
            lblParameter.Text = parameters[0].Item2;
            a = typeof(int);
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            setParameters[index] = Convert.ChangeType(tbxInput.Text, parameters[index].Item1);
            index++;
            if (index == setParameters.Length)
            {
                btnEnter.Enabled = false;
                btnDone.Enabled = true;
                lblParameter.Text = "";
            }
            else 
            {
            lblParameter.Text = parameters[index].Item2;
            }
            tbxInput.Text = "";
            tbxInput.Focus();
                
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
