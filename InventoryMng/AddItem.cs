using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryMng
{
    public partial class AddItem : Form
    {
        static  SQLiteConnection    sqlite_conn;
        static  DBCon               con = new DBCon();
        int     productId;
        public AddItem()
        {
            InitializeComponent();
            DBCon.CreateConnection();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtName.Text) && valuePrice.Value > 0 && valueQty.Value > 0)
            {
                con.InsertDataProduct(txtName.Text, txtDescription.Text, valuePrice.Value, (int)valueQty.Value);
                LoadData(); // Refresh the data after insertion
            }
            else
            {
                MessageBox.Show("Please fill all the fields with valid data.");
            }
        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            DataTable dataTable         = con.GetData();
            dataGridView1.DataSource    = dataTable;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Assign the cell values to the textboxes
                txtName.Text = selectedRow.Cells["name"].Value.ToString();
                txtDescription.Text = selectedRow.Cells["description"].Value.ToString();
                valuePrice.Value = decimal.Parse(selectedRow.Cells["price"].Value.ToString());
                valueQty.Value = decimal.Parse(selectedRow.Cells["quantity"].Value.ToString());
                productId = int.Parse(selectedRow.Cells["product_id"].Value.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && valuePrice.Value > 0 && valueQty.Value > 0)
            {
                con.updateDataItem(productId, txtName.Text, txtDescription.Text, valuePrice.Value, (int)valueQty.Value);
                LoadData(); // Refresh the data after insertion
            }
            else
            {
                MessageBox.Show("Please fill all the fields with valid data.");
            }
        }
    }
}
