using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NGAT.Business.Contracts.Services.Layers;
using NGAT.Business.Domain.Core;
using NGAT.Services;

namespace NGAT.WindowsAPI.Forms
{
    public partial class frmLayers : Form
    {

        public Dictionary<Layer, bool> Layers { get; private set; }

        public frmLayers(Dictionary<Layer, bool> layers)
        {
            InitializeComponent();
            Layers = layers;
        }

        private void FrmLayers_Load(object sender, EventArgs e)
        {
            // Load Layer Providers
            foreach (var item in Utils.GetSubClasses<FileLayerProvider>())
                cbxLayerProvider.Items.Add(item);
            cbxLayerFilter.Enabled = false;

            var keys = Layers.Keys.ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                clbActiveLayers.Items.Add(keys[i]);
                clbActiveLayers.SetItemChecked(i, Layers[keys[i]]);
            }
        }

        private void CbxLayerProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLayerProvider.SelectedItem != null)
            {
                cbxLayerFilter.Enabled = true;
                btnLoadProvider.Enabled = true;
                // Load Layer Filters
                cbxLayerFilter.Items.Clear();
                foreach (var item in Utils.GetSubClasses<ILayerFilter>().Where(f => f.ProviderType == typeof(object) 
                        || f.ProviderType == cbxLayerProvider.SelectedItem.GetType()
                        || cbxLayerProvider.SelectedItem.GetType().IsSubclassOf(f.ProviderType)))
                    cbxLayerFilter.Items.Add(item);
            }
            else
            {
                cbxLayerFilter.Enabled = false;
                btnLoadProvider.Enabled = false;
            }
            
        }

        private void PbxLayerMarquer_Click(object sender, EventArgs e)
        {
            dlgLoadIcon.CheckPathExists = true;
            dlgLoadIcon.CheckFileExists = false;
            dlgLoadIcon.AddExtension = true;
            dlgLoadIcon.ValidateNames = true;
            dlgLoadIcon.Title = "Load icon file";
            // Example: "Files GRF (*.grf)|*.grf"
            dlgLoadIcon.Filter = "Image Files (*.jpg, *.png, *.bmp, *.ico | *.JPG; *.PNG; *.BMP; *.ICO| All files (*.*)|*.*";
            dlgLoadIcon.FilterIndex = 1;
            dlgLoadIcon.RestoreDirectory = true;
            dlgLoadIcon.InitialDirectory = "Resources\\ShapeFiles";

            if (dlgLoadIcon.ShowDialog() == DialogResult.OK)
            {
                (sender as PictureBox).ImageLocation = dlgLoadIcon.FileName;
            }
        }

        private void BtnLoadProvider_Click(object sender, EventArgs e)
        {
            dlgOpenProvider.CheckPathExists = true;
            dlgOpenProvider.CheckFileExists = false;
            dlgOpenProvider.AddExtension = true;
            dlgOpenProvider.ValidateNames = true;
            dlgOpenProvider.Title = "Select provider file";
            dlgOpenProvider.InitialDirectory = "Resources\\BitMaps\\";
            // Example: "Files GRF (*.grf)|*.grf"
            var provider = (FileLayerProvider)cbxLayerProvider.SelectedItem;
            dlgOpenProvider.Filter = provider.FormatID + " Files (*" + provider.Extension + ")|*." + provider.FormatID;
            dlgOpenProvider.ShowDialog();
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxLayerProvider.SelectedItem == null)
                    MessageBox.Show("You must select a  layer provider.", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (dlgOpenProvider.FileName == "")
                    MessageBox.Show("You must select a provider file.", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (cbxLayerFilter.SelectedItem == null)
                    MessageBox.Show("You must select a filter.", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (tbxLayername.Text == "")
                    MessageBox.Show("Invalid layer name.", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (dlgLoadIcon.FileName == "")
                    MessageBox.Show("You must select a layer marker file.", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    btnCreate.Enabled = false;
                    ILayerProvider provider = (ILayerProvider)cbxLayerProvider.SelectedItem;
                    Layer layer = provider.GetLayer(dlgOpenProvider.FileName, (ILayerFilter)cbxLayerFilter.SelectedItem, tbxLayername.Text, dlgLoadIcon.FileName);
                    Layers.Add(layer, false);
                    clbActiveLayers.Items.Add(layer);
                    cbxLayerProvider.SelectedItem = null;
                    dlgOpenProvider.FileName = "";
                    cbxLayerFilter.SelectedItem = null;
                    tbxLayername.Text = "";
                    dlgLoadIcon.FileName = "";
                    pbxLayerMarquer.Image = null;
                    btnCreate.Enabled = true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error ocurred while creating layer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCreate.Enabled = true;
            }
            

        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (clbActiveLayers.SelectedItems.Count > 0 &&
                MessageBox.Show("Selected layers will be deleted. Are you sure?", "Remove Layers", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                for (int i = 0; i < clbActiveLayers.SelectedItems.Count; i++)
                {
                    Layer layer = (Layer)clbActiveLayers.SelectedItems[i];
                    Layers.Remove(layer);
                    clbActiveLayers.Items.Remove(clbActiveLayers.SelectedItems[i]);
                }
            }
        }

        private void ClbActiveLayers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(clbActiveLayers.SelectedItem != null)
                Layers[(Layer)clbActiveLayers.SelectedItem] = !Layers[(Layer)clbActiveLayers.SelectedItem];
        }

        private void cbxLayerFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxLayerFilter.SelectedItem != null)
            {
                ILayerFilter filter = (ILayerFilter)cbxLayerFilter.SelectedItem;
                try
                {
                    string source = (filter.DefaultDataSource != null && filter.DefaultDataSource != string.Empty) ? Path.GetFullPath(filter.DefaultDataSource) : null;
                    string marker = (filter.DefaultMarkerPath != null && filter.DefaultMarkerPath != string.Empty) ? Path.GetFullPath(filter.DefaultMarkerPath) : null;
                    if (source != null && Directory.GetFiles(Path.GetDirectoryName(source)).ToList().Contains(source)) // check existency
                    {
                        dlgOpenProvider.FileName = source;
                    }
                    if (marker != null && Directory.GetFiles(Path.GetDirectoryName(marker)).ToList().Contains(marker))
                    {
                        // *.jpg, *.png, *.bmp, *.ico
                        if (marker.EndsWith(".jpg") || marker.EndsWith(".png") || marker.EndsWith(".bmp") || marker.EndsWith(".ico"))
                        {
                            dlgLoadIcon.FileName = marker;
                            pbxLayerMarquer.ImageLocation = marker;
                        }
                    }
                }
                catch (NotImplementedException)
                {
                }
            }

            
        }
    }
}
