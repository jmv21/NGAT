using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NGAT.WindowsAPI.Forms
{
    public partial class frmOsmPbfSettings : Form
    {
        public List<string> NodeAttributes { get; private set; }
        public List<string> LinkAttributes { get; private set; }
        public List<string> LinkAttrFiltrer { get; private set; }
        public bool Pedestrian { get; private set; }    

        public frmOsmPbfSettings()
        {
            InitializeComponent();
            NodeAttributes = new List<string>();
            LinkAttrFiltrer = new List<string>();
            LinkAttributes = new List<string>();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if(tbxAttribute.Text != "")
            {
                checkedListBox1.Items.Add(tbxAttribute.Text.ToLowerInvariant());
            }
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            foreach (var item in checkedListBox1.CheckedItems)
            {
                NodeAttributes.Add((string)item);
            }
            foreach(var item in checkedListBox2.CheckedItems)
            {
                LinkAttributes.Add((string)item);
            }
            Pedestrian = cbxPedestrian.Checked;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            NodeAttributes.Clear();
            Close();
        }

        private void BtnAddLinkAttr_Click(object sender, EventArgs e)
        {
            if(tbxLinkAttr.Text != "")
            {
                checkedListBox2.Items.Add(tbxLinkAttr.Text.ToLowerInvariant());
            }
        }

        private void frmOsmPbfSettings_Leave(object sender, EventArgs e)
        {
            NodeAttributes.Clear();
            Close();
        }
    }
}
