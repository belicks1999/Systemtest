using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryMng
{
    public partial class BarcodeSingle : Form
    {
        public BarcodeSingle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportBarCodesToXMLSingle(int.Parse(txtProductId.Text), int.Parse(Qty.Value.ToString()));
            ReportViewerForm reportViewerForm = new ReportViewerForm();
            reportViewerForm.LoadBarCodePrintSingle();
            reportViewerForm.ShowDialog();
        }
        public void ExportBarCodesToXMLSingle(int productId, int qty)
        {
            try
            {
                using (SQLiteConnection sqlite_conn = DBCon.CreateConnection())
                {
                    using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                    {
                        // SQL query to get the barcode based on the productId
                        sqlite_cmd.CommandText = "SELECT barcode FROM Products WHERE product_id = @productId";
                        sqlite_cmd.Parameters.AddWithValue("@productId", productId);

                        using (SQLiteDataReader reader = sqlite_cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Create a DataTable with a single column for barcodes
                                DataTable dt = new DataTable("BarCodeReport");
                                dt.Columns.Add("barcode", typeof(string)); // Explicitly name and type the column

                                string barcode = reader["barcode"].ToString();

                                // Add rows based on the quantity
                                for (int i = 0; i < qty; i++)
                                {
                                    DataRow row = dt.NewRow();
                                    row["barcode"] = barcode;
                                    dt.Rows.Add(row);
                                }

                                // Specify the file path where XML will be saved
                                string xmlFilePath = Application.StartupPath + "\\BarCodeReportSingle.xml";

                                // Write DataTable to XML
                                dt.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);

                            }
                            else
                            {
                                MessageBox.Show("No barcode found for the provided product ID.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data to XML: {ex.Message}");
            }
        }

    }
}
