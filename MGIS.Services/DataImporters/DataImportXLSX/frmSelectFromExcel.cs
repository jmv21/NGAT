using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace NGAT.Visual.Forms
{
    public partial class frmSelectFromExcel : Form
    {
        public frmSelectFromExcel(DataTable data)
        {
            InitializeComponent();
            BindingSource bindingSource1 = new BindingSource();
            bindingSource1.DataSource = data;
            dataGridView1.DataSource = bindingSource1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
