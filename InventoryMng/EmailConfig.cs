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
    public partial class EmailConfig : Form
    {
        static DBCon con = new DBCon();
        public EmailConfig()
        {
            InitializeComponent();
            DBCon.CreateConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFromEmail.Text) && !string.IsNullOrEmpty(txtToEmail.Text) && !string.IsNullOrEmpty(txtAppPassword.Text))
            {
                con.InsertDataEmail(txtFromEmail.Text, txtToEmail.Text, txtAppPassword.Text);
            }
            else
            {
                MessageBox.Show("Please fill all the fields with valid data.");
            }
        }

        private void EmailConfig_Load(object sender, EventArgs e)
        {
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
                                // Update UI fields
                                txtFromEmail.Text   = reader["FromEmail"].ToString(); 
                                txtToEmail.Text     = reader["ToEmail"].ToString();
                                txtAppPassword.Text = reader["AppPassword"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve email details: {ex.Message}");
            }
        }
    }
}
