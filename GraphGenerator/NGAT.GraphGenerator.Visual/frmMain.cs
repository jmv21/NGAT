using NGAT.GraphGenerator.Business.Contracts;
using NGAT.Business.Contracts.IO;
using NGAT.Business.Domain.Core;
using System.Data;

namespace NGAT.GraphGenerator.Visual
{
    public partial class frmMain : Form
    {
        public List<Graph> GeneratedGraphs { get; set; }
        List<List<DataTable>> table = new List<List<DataTable>>();
        int selectedGraph;
        int selectedScenario;
        public IGraphGenerator generator { get; set; }
        public frmMain()
        {
            InitializeComponent();
            GeneratedGraphs = new List<Graph>();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Load all types that inherit from GraphGenerator
            foreach (var imp in Utils.GetSubClasses<IGraphGenerator>())
                cbxSelectGenerator.Items.Add(imp);
            //Put the available amount of graph to generate
            for (int i = 1; i < 101; i++)
                cbxAmount.Items.Add(i);
            //Load all types that implemenst IGraphExporter
            foreach (var imp in Utils.GetSubClasses<IGraphExporter>())
            {
                cbxExportAs.Items.Add(imp);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GeneratedGraphs.Clear();
            cbxSelectGraph.Items.Clear();
            cbxSelectScenario.Items.Clear();
            List<(Type, string)> parameters = new List<(Type, string)>();
            parameters = generator.UsedParameters();
            frmNeededParameters modal = new frmNeededParameters(parameters);
            object [] setParameters;
            if (modal.ShowDialog() == DialogResult.OK)
            {
                setParameters = modal.setParameters;
                generator.SetParameters(setParameters);
            }

            if ((int)cbxAmount.SelectedItem > 1)
            {
                foreach(Graph graph in generator.GenerateAll((int)cbxAmount.SelectedItem))
                    GeneratedGraphs.Add(graph);
            }
            else
            {
                GeneratedGraphs.Add(generator.Generate());
            }
            #region Update Visualization
            UpdateTable();

            for (int i = 0; i < GeneratedGraphs.Count; i++)
            {
                cbxSelectGraph.Items.Add(i+1);
            }
            cbxSelectGraph.SelectedIndex = 0;
            #endregion
        }

        private void cbxSelectGenerator_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDescription.Text = (cbxSelectGenerator.SelectedItem as IGraphGenerator).Description;
            this.generator = cbxSelectGenerator.SelectedItem as IGraphGenerator;
            btnGenerate.Enabled = true;
        }

        private void UpdateTable()
        {
            table.Clear();
            foreach (Graph graph in GeneratedGraphs)
            {
                List<DataTable> graphTable = new List<DataTable>();
                for(int i = 0; i < graph.Scenarios_Count; i++)
                {
                    DataTable dataTable = new DataTable();
                    for (int j = 0; j < graph.Nodes.Count; j++)
                    {
                        dataTable.Columns.Add((j + 1).ToString(), typeof(double));
                    }
                    for (int j = 0; j < graph.Nodes.Count; j++)
                    {
                        DataRow row = dataTable.NewRow();
                        for (int k = 0; k < graph.Nodes.Count; k++)
                        {
                            if (k==j)
                            {
                                row[(k+1).ToString()] = -1;
                                continue;
                            }
                            Arc arc = graph.NodesId[j + 1].OutgoingArcs.FirstOrDefault(arc => arc.ToNodeId == k + 1);
                            Edge edge = graph.NodesId[j+1].Edges.FirstOrDefault(edge => (edge.ToNodeId == k + 1)|| edge.FromNodeId == k+1);
                            if (arc != null)
                            {
                                row[(k + 1).ToString()] = arc.Distance.Costs[i];
                            }
                            else if (edge != null)
                            {
                            row[(k + 1).ToString()] = edge.Distance.Costs[i];
                            }
                            else
                            {
                                row[(k + 1).ToString()] = -1;
                            }
                        }
                        //Add name to the rows;

                        dataTable.Rows.Add(row);
                    }
                    graphTable.Add(dataTable);
                }
                table.Add(graphTable);
            }
        }
        private void cbxSelectGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxSelectScenario.Items.Clear();
            for (int i = 0; i < GeneratedGraphs[cbxSelectGraph.SelectedIndex].Scenarios_Count; i++)
            {
                cbxSelectScenario.Items.Add(i + 1);
            }
            cbxSelectScenario.SelectedIndex = 0;
            UpdateDataGrid();
        }
        private void cbxSelectScenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void btnNextGraph_Click(object sender, EventArgs e)
        {
            int newIndex = cbxSelectGraph.SelectedIndex + 1;
            if (newIndex == cbxSelectGraph.Items.Count)
                newIndex = 0;
            cbxSelectGraph.SelectedIndex = newIndex;
        }

        private void btnPreviusGraph_Click(object sender, EventArgs e)
        {
            int newIndex = cbxSelectGraph.SelectedIndex - 1;
            if (newIndex == -1)
                newIndex = cbxSelectGraph.Items.Count-1;
            cbxSelectGraph.SelectedIndex = newIndex;
        }

        private void btnNextScenario_Click(object sender, EventArgs e)
        {
            int newIndex = cbxSelectScenario.SelectedIndex + 1;
            if (newIndex == cbxSelectScenario.Items.Count)
                newIndex = 0;
            cbxSelectScenario.SelectedIndex = newIndex;
        }

        private void btnPreviusScenario_Click(object sender, EventArgs e)
        {
            int newIndex = cbxSelectScenario.SelectedIndex - 1;
            if (newIndex == -1)
                newIndex = cbxSelectScenario.Items.Count - 1;
            cbxSelectScenario.SelectedIndex = newIndex;
        }

        private void UpdateDataGrid()
        {
            BindingSource bindingSource1 = new BindingSource();
            bindingSource1.DataSource = table[cbxSelectGraph.SelectedIndex][cbxSelectScenario.SelectedIndex];
            dgvVisualizer.DataSource = bindingSource1;

            for (int i = 0; i < dgvVisualizer.Rows.Count-1; i++)
            {
                dgvVisualizer.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void cbxExportAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GeneratedGraphs.Count > 0)
                btnExport.Enabled = true;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            IGraphExporter exporter = (cbxExportAs.SelectedItem as IGraphExporter);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = exporter.FormatID.ToLower();
            saveFileDialog.Filter = "File " + exporter.FormatID + " (*" + saveFileDialog.DefaultExt + ")|*" + saveFileDialog.DefaultExt;
            saveFileDialog.FileName = "graph." + saveFileDialog.DefaultExt;

            bool saveOption = true;
            if (GeneratedGraphs.Count>1)
            saveOption = MessageBox.Show("Do you want to save the graphs one by one?", "Save options", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

            if (saveOption)
            {
                for (int i = 0; i < GeneratedGraphs.Count; i++)
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            exporter.Export(new FileStream(saveFileDialog.FileName, FileMode.CreateNew), GeneratedGraphs[i]);
                            MessageBox.Show("Results has been successfully exported.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error saving " + exporter.FormatID + ": " +
                                ex.Message, "Error ocurred while saving " + exporter.FormatID + " file",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                if (saveFileDialog.ShowDialog()==DialogResult.OK)
                {
                    int index = saveFileDialog.FileName.LastIndexOf('.');
                    for (int i = 0; i < GeneratedGraphs.Count; i++)
                    {
                        
                        try
                        {
                            exporter.Export(new FileStream(saveFileDialog.FileName.Insert(index,(i+1).ToString()), FileMode.CreateNew), GeneratedGraphs[i]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error saving " + exporter.FormatID + ": " +
                                ex.Message, "Error ocurred while saving " + exporter.FormatID + " file",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    MessageBox.Show("Results has been successfully exported.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}