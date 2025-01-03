using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using NGAT.Business.Contracts.Services.DataPointsImportFormat;
using GMap.NET;
using ExcelDataReader;
using NGAT.Services.DataImporters.DataImportXLSX;
using NGAT.Visual.Forms;
using System.Windows.Forms;

namespace NGAT.Services.DataImporters.DataImportXLSX
{
    public class DataImportXLSX : IDataPointsImporter
    {
        public string Filter => "Files XLSX (*.xlsx)|*xlsx";

        public (List<PointLatLng>, List<string>) Import(string path, Form owner)
        {
            var stream = File.Open(path, FileMode.Open, FileAccess.Read);

            var reader = ExcelReaderFactory.CreateReader(stream);

            var result = reader.AsDataSet();

            stream.Close();

            DataTable table = result.Tables[0];

            int rows = table.Rows.Count;
            int columns = table.Columns.Count;

            int dataInitialColumn = 0;
            int dataInitialRow = 0;
            int latitudeInitialColumn = 0;
            int latitudeInitialRow = 0;
            int longitudeInitialColumn = 0;
            int longitudeInitialRow = 0;


            frmSelectFromExcel testDialog = new frmSelectFromExcel(table);

            //  Putting the available columns and rows into the comboBoxs
            for (int i = 0; i < rows; i++)
            {
                testDialog.comboBox2.Items.Add(i);
            }
            for (int i = 0; i < columns; i++)
            {
                testDialog.comboBox1.Items.Add(i);
                testDialog.comboBox3.Items.Add(i);
                testDialog.comboBox5.Items.Add(i);
            }
            testDialog.comboBox2.SelectedIndex = 0;
            testDialog.comboBox1.SelectedIndex = 0;
            testDialog.comboBox3.SelectedIndex = 0;
            testDialog.comboBox5.SelectedIndex = 0;


            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(owner) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                dataInitialColumn = (int)testDialog.comboBox1.SelectedItem;
                dataInitialRow = (int)testDialog.comboBox2.SelectedItem;
                latitudeInitialColumn = (int)testDialog.comboBox3.SelectedItem;
                latitudeInitialRow = (int)testDialog.comboBox2.SelectedItem;
                longitudeInitialColumn = (int)testDialog.comboBox5.SelectedItem;
                longitudeInitialRow = (int)testDialog.comboBox2.SelectedItem;

            }
            else
            {
                MessageBox.Show("Incorrect input.");
                return ((new List<PointLatLng>(),new List<string>()));
            }
            testDialog.Dispose();

            List<string> nodesData = new List<string>();
            List<PointLatLng> inputPoints = new List<PointLatLng>();

            for (int i = dataInitialRow; i < rows; i++)
            {
                DataRow dataRow = table.Rows[i];
                nodesData.Add((string)(dataRow[dataInitialColumn]));
            }
            for (int i = latitudeInitialRow; i < rows; i++)
            {
                DataRow dataRow = table.Rows[i];
                double lat = (double)dataRow[latitudeInitialColumn];
                double lng = (double)dataRow[longitudeInitialColumn];
                inputPoints.Add(new PointLatLng(lat, lng));
            }
            
            return (inputPoints, nodesData);

        }

        public override string ToString()
        {
            return "XLSX";
        }
    }
}
