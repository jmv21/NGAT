using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET.WindowsForms;
using NGAT.Business.Contracts.Services.ResultExporters;
using NGAT.Business.Domain.Core;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace NGAT.Services.ResultExporters
{
    public class MapImageExporter : IResultExporter
    {
        public void Export(Graph graph, ShortestPathProblemOutput result, GMapControl gmap)
        {
            Image image = gmap.ToImage();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "png";
            saveFileDialog.Filter = "PNG File" + " (*" + saveFileDialog.DefaultExt + ")|*" + saveFileDialog.DefaultExt;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image.Save(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving image", "Error ocurred while saving",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public override string ToString()
        {
            return "Export Image";
        }
    }
}
