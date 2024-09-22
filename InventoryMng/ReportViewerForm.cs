using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryMng
{
    public partial class ReportViewerForm : Form
    {
        public ReportViewerForm()
        {
            InitializeComponent();
        }
        public void LoadBarCodePrintSingle()
        {
            try
            {
                // Load the Crystal Report
                ReportDocument reportDocument = new ReportDocument();

                // Load the report file (assuming you created a report named BarcodePrintSingle.rpt)
                string reportPath = Application.StartupPath + "\\BarcodePrintSingle.rpt";
                reportDocument.Load(reportPath);

                // Load the XML data source for the report
                string xmlFilePath = Application.StartupPath + "\\BarCodeReportSingle.xml";

                DataSet ds = new DataSet();
                ds.ReadXml(xmlFilePath);

                // Check if the DataTable contains the expected columns
                if (ds.Tables.Contains("BarCodeReport"))
                {
                    DataTable salesReportTable = ds.Tables["BarCodeReport"];
                    // Check if the columns exist
                    foreach (DataColumn column in salesReportTable.Columns)
                    {
                        Console.WriteLine(column.ColumnName); // Print column names for debugging
                    }
                    reportDocument.SetDataSource(salesReportTable);
                }
                else
                {
                    MessageBox.Show("DataTable 'BarCodeReport' not found in XML.");
                    return;
                }

                // Assign the report to the CrystalReportViewer control
                crystalReportViewer1.ReportSource = reportDocument;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report: {ex.Message}");
            }
        }
        public void LoadBarCodePrint()
        {
            try
            {
                // Load the Crystal Report
                ReportDocument reportDocument = new ReportDocument();

                // Load the report file (assuming you created a report named BarcodeMultiple.rpt)
                string reportPath = Application.StartupPath + "\\BarcodeMultiple.rpt";
                reportDocument.Load(reportPath);

                // Load the XML data source for the report
                string xmlFilePath = Application.StartupPath + "\\BarCodeReport.xml";

                DataSet ds = new DataSet();
                ds.ReadXml(xmlFilePath);

                // Check if the DataTable contains the expected columns
                if (ds.Tables.Contains("BarCodeReport"))
                {
                    DataTable salesReportTable = ds.Tables["BarCodeReport"];
                    // Check if the columns exist
                    foreach (DataColumn column in salesReportTable.Columns)
                    {
                        Console.WriteLine(column.ColumnName); // Print column names for debugging
                    }
                    reportDocument.SetDataSource(salesReportTable);
                }
                else
                {
                    MessageBox.Show("DataTable 'BarCodeReport' not found in XML.");
                    return;
                }

                // Assign the report to the CrystalReportViewer control
                crystalReportViewer1.ReportSource = reportDocument;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report: {ex.Message}");
            }
        }
        public void LoadDailySalesReport()
        {
            try
            {
                // Load the Crystal Report
                ReportDocument reportDocument = new ReportDocument();

                // Load the report file (assuming you created a report named DailySalesReport.rpt)
                string reportPath = Application.StartupPath + "\\DailySalesReport.rpt";
                reportDocument.Load(reportPath);

                // Load the XML data source for the report
                string xmlFilePath = Application.StartupPath + "\\DailySalesReport.xml";

                DataSet ds = new DataSet();
                ds.ReadXml(xmlFilePath);

                // Check if the DataTable contains the expected columns
                if (ds.Tables.Contains("SalesReport"))
                {
                    DataTable salesReportTable = ds.Tables["SalesReport"];
                    // Check if the columns exist
                    foreach (DataColumn column in salesReportTable.Columns)
                    {
                        Console.WriteLine(column.ColumnName); // Print column names for debugging
                    }
                    reportDocument.SetDataSource(salesReportTable);
                }
                else
                {
                    MessageBox.Show("DataTable 'SalesReport' not found in XML.");
                    return;
                }

                // Assign the report to the CrystalReportViewer control
                crystalReportViewer1.ReportSource = reportDocument;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report: {ex.Message}");
            }
        }
    }
}
