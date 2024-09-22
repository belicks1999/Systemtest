using EASendMail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmtpClient = EASendMail.SmtpClient;

namespace InventoryMng
{
    public partial class MainForm : Form
    {

        String fromEmail, toEmail, appPassword;
        public MainForm()
        {
            InitializeComponent();
        }

        private void addStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItem newItem     = new AddItem();
            newItem.TopLevel    = false;
            newItem.AutoScroll  = true;

            panel1.Controls.Clear();
            this.panel1.Controls.Add(newItem);

            newItem.Show();
        }

        private void newSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sales newItem       = new Sales();
            newItem.TopLevel    = false;
            newItem.AutoScroll  = true;

            panel1.Controls.Clear();
            this.panel1.Controls.Add(newItem);

            newItem.Show();
        }

        private void dailySalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportDailySalesToXML(DateTime.Now);
            ReportViewerForm reportViewerForm = new ReportViewerForm();
            reportViewerForm.LoadDailySalesReport();
            reportViewerForm.ShowDialog();
        }
        public void ExportDailySalesToXML(DateTime reportDate)
        {
            try
            {
                // Query to get the sales data for the specific date
                string query = @"SELECT sale_id, product_id, product_name, quantity, unit_price, total_price, sale_date
                             FROM Sales
                             WHERE DATE(sale_date) = @reportDate";
                using (SQLiteConnection sqlite_conn = DBCon.CreateConnection())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, sqlite_conn))
                    {
                        cmd.Parameters.AddWithValue("@reportDate", reportDate.ToString("yyyy-MM-dd"));

                        SQLiteDataAdapter da    = new SQLiteDataAdapter(cmd);
                        DataTable dt            = new DataTable("SalesReport"); // Explicitly name the DataTable
                        da.Fill(dt);

                        // Specify the file path where XML will be saved
                        string xmlFilePath = Application.StartupPath + "\\DailySalesReport.xml";

                        // Write DataTable to XML
                        dt.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data to XML: {ex.Message}");
            }
        }

        private void barcodePrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportBarCodesToXML(DateTime.Now);
            ReportViewerForm reportViewerForm = new ReportViewerForm();
            reportViewerForm.LoadBarCodePrint();
            reportViewerForm.ShowDialog();
        }
        public void ExportBarCodesToXML(DateTime reportDate)
        {
            try
            {
                // Query to get the barcode data
                string query = @"SELECT barcode
                             FROM Products";
                using (SQLiteConnection sqlite_conn = DBCon.CreateConnection())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, sqlite_conn))
                    {
                        cmd.Parameters.AddWithValue("@reportDate", reportDate.ToString("yyyy-MM-dd"));

                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("BarCodeReport"); // Explicitly name the DataTable
                        da.Fill(dt);

                        // Specify the file path where XML will be saved
                        string xmlFilePath = Application.StartupPath + "\\BarCodeReport.xml";

                        // Write DataTable to XML
                        dt.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data to XML: {ex.Message}");
            }
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void barcodePrintSingleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BarcodeSingle newObj    = new BarcodeSingle();
            newObj.TopLevel         = false;
            newObj.AutoScroll       = true;

            panel1.Controls.Clear();
            this.panel1.Controls.Add(newObj);

            newObj.Show();
        }

        private void sendLowStockMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckStockAndSendEmail();
        }
        public void CheckStockAndSendEmail()
        {
            // Check for products with stock below a certain threshold (e.g., 10)
            List<string> lowStockItems = CheckLowStock(10);

            // If there are items with low stock, send an email
            SendLowStockEmail(lowStockItems);
        }
        public List<string> CheckLowStock(int threshold)
        {
            List<string> lowStockItems = new List<string>();

            using (SQLiteConnection sqlite_conn = DBCon.CreateConnection())
            {
                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                {
                    // Query to check stock below the threshold
                    sqlite_cmd.CommandText = "SELECT name, quantity FROM Products WHERE quantity < @threshold";
                    sqlite_cmd.Parameters.AddWithValue("@threshold", threshold);

                    using (SQLiteDataReader reader = sqlite_cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string productName  = reader["name"].ToString();
                            int quantity        = Convert.ToInt32(reader["quantity"]);

                            lowStockItems.Add($"{productName} (Available stock: {quantity})");
                        }
                    }
                }
            }

            return lowStockItems;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            Color PanelColor = Color.FromArgb(40, 116, 166);
            panel1.BackColor = PanelColor;
        }

        private void dBBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartDBBackup();
        }
        public static void CreateBackup(string sourceDbPath, string backupDbPath)
        {
            // Check if the source database file exists
            if (!File.Exists(sourceDbPath))
            {
                MessageBox.Show("Source database not found.");
                return;
            }

            try
            {
                // Open the source database connection
                using (SQLiteConnection sourceConn = new SQLiteConnection($"Data Source={sourceDbPath};"))
                {
                    sourceConn.Open();

                    // Open the destination database connection (backup)
                    using (SQLiteConnection backupConn = new SQLiteConnection($"Data Source={backupDbPath};"))
                    {
                        backupConn.Open();

                        // Create a backup of the source database
                        sourceConn.BackupDatabase(backupConn, "main", "main", -1, null, 0);

                        MessageBox.Show($"Backup created successfully at: {backupDbPath}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create backup: {ex.Message}");
            }
        }

        public void StartDBBackup()
        {
            // Specify the source database and backup file paths
            string sourceDbPath = Application.StartupPath + "\\InventoryMngDB.db"; // Original DB
            string backupDbPath = Application.StartupPath + "\\Backup\\Inventory_Backup.db"; // Backup location

            // Create the directory for backup if it doesn't exist
            string backupDirectory = Path.GetDirectoryName(backupDbPath);
            if (!Directory.Exists(backupDirectory))
            {
                Directory.CreateDirectory(backupDirectory);
            }

            // Call the method to create a backup
            CreateBackup(sourceDbPath, backupDbPath);
        }
        public void SendLowStockEmail(List<string> lowStockItems)
        {

            if (lowStockItems.Count == 0)
            {
                MessageBox.Show("No items are low in stock.");
                return;
            }

            try
            {
                using (SQLiteConnection sqlite_conn = DBCon.CreateConnection())
                {
                    using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                    {
                        sqlite_cmd.CommandText = "SELECT FromEmail, ToEmail, AppPassword FROM EmailSettings";
                    
                        using (SQLiteDataReader reader = sqlite_cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                fromEmail   = reader["FromEmail"].ToString();
                                toEmail     = reader["ToEmail"].ToString();
                                appPassword = reader["AppPassword"].ToString();
                            }
                        }
                    }
                }

                string emailBody = "The following items are low in stock:\n\n" + string.Join("\n", lowStockItems);

                SmtpMail oMail = new SmtpMail("TryIt");

                // Your gmail email address
                oMail.From = fromEmail;

                // Set recipient email address
                oMail.To = toEmail;

                // Set email subject
                oMail.Subject = "Low stock details";

                // Set email body
                oMail.TextBody = emailBody;

                // Gmail SMTP server address
                SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                // Gmail user authentication
                // For example: your email is "gmailid@gmail.com", then the user should be the same
                oServer.User = fromEmail;

                // Create app password in Google account
                // https://support.google.com/accounts/answer/185833?hl=en
                oServer.Password = appPassword;

                // If you want to use direct SSL 465 port,
                // please add this line, otherwise TLS will be used.
                // oServer.Port = 465;

                // set 587 TLS port;
                oServer.Port = 587;

                // detect SSL/TLS automatically
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                Console.WriteLine("start to send email over SSL ...");

                SmtpClient oSmtp = new SmtpClient();
                oSmtp.SendMail(oServer, oMail);

                Console.WriteLine("email was sent successfully!");
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }
        }

        private void emailSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmailConfig newObj  = new EmailConfig();
            newObj.TopLevel     = false;
            newObj.AutoScroll   = true;

            panel1.Controls.Clear();
            this.panel1.Controls.Add(newObj);

            newObj.Show();
        }
    }
}
