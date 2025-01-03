using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using GMap.NET.WindowsForms;
using NGAT.Business.Contracts.Services.ResultExporters;
using NGAT.Business.Domain.Core;
using System.Windows.Forms;

namespace NGAT.Services.ResultExportes
{
    public class RouteSheetExporter : IResultExporter
    {
        public void Export(Graph graph, ShortestPathProblemOutput result, GMapControl gmap)
        {

            #region Getting Links
            List<Link> links = new List<Link>();
            List<Node> nodes = new List<Node>();
            foreach (var nodeId in result.NodesId)
            {
                nodes.Add(graph.NodesId[nodeId]);
            }
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                foreach (Link item in nodes[i].OutgoingArcs.Where(l => l.ToNode == nodes[i + 1]))
                {
                    links.Add(item);
                }
                foreach (Link item in nodes[i].Edges.Where(l => l.ToNode == nodes[i + 1] || l.FromNode == nodes[i + 1]))
                {
                    links.Add(item);
                }

            }
            #endregion

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "TXT File" + " (*" + saveFileDialog.DefaultExt + ")|*" + saveFileDialog.DefaultExt;

            List<string> linksNames = new List<string>();
            for (int i = 0; i < links.Count; i++)
            {
                if (links[i].LinkData.Attributes != null && links[i].LinkData.Attributes.ContainsKey("name"))
                {
                    if (linksNames.Count == 0)
                        linksNames.Add(links[i].LinkData.Attributes["name"]);
                    if (linksNames[linksNames.Count - 1] != links[i].LinkData.Attributes["name"])
                    {
                        Node v1;
                        Node v2;
                        Node v3;

                        if (links[i].Directed)
                        {
                            v2 = links[i].FromNode;
                            v3 = links[i].ToNode;
                            v1 = links[i - 1].ToNode == v2 ? links[i - 1].FromNode : links[i - 1].ToNode;
                        }
                        else
                        {
                            v2 = links[i].FromNode == links[i - 1].ToNode || links[i].FromNode == links[i - 1].FromNode ? links[i].FromNode : links[i].ToNode;
                            v3 = links[i].FromNode == v2 ? links[i].ToNode : links[i].FromNode;
                            v1 = links[i - 1].ToNode == v2 ? links[i - 1].FromNode : links[i - 1].ToNode;
                        }

                        double v1X = v2.Coordinate[1] - v1.Coordinate[1];
                        double v1Y = v2.Coordinate[0] - v1.Coordinate[0];
                        double v2X = v3.Coordinate[1] - v2.Coordinate[1];
                        double v2Y = v3.Coordinate[0] - v2.Coordinate[0];

                        string turn = v1X * v2Y > v1Y * v2X ? "Turn Left" : "Turn Right";
                        turn = turn + "(" + (i+1) + "-" + (i+2) + ")";
                        linksNames.Add(turn);
                        linksNames.Add(links[i].LinkData.Attributes["name"]);
                    }
                }
                else
                {
                    linksNames.Add("Unknow street");
                }
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.WriteLine("Route Sheet");
                    try
                    {
                        for (int i = 0; i < linksNames.Count; i++)
                        {
                            writer.WriteLine(linksNames[i]);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving txt", "Error ocurred while saving",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public override string ToString()
        {
            return "Export Route Sheet";
        }
    }
}
