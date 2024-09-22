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
    public partial class Sales : Form
    {
        private List<SaleItem> saleItems = new List<SaleItem>(); 
        DBCon con = new DBCon();
        int productId;
        public Sales()
        {
            InitializeComponent();
        }
        private void AddProductToSale(string barcode, string name, decimal price, int quantity, int productId)
        {
            // Check if product is already in the sale
            SaleItem existingItem = saleItems.Find(item => item.Barcode == barcode);
            if (existingItem != null)
            {
                // If product exists, update the quantity
                existingItem.Quantity += quantity;
            }
            else
            {
                // Add new product to the sale
                SaleItem saleItem = new SaleItem
                {
                    Barcode     = barcode,
                    Name        = name,
                    Price       = price,
                    Quantity    = quantity,
                    ProductId   = productId
                };
                saleItems.Add(saleItem);
            }

            UpdateSaleList(); // Update the list to display in the UI
        }

        // Method to update the UI list (e.g., DataGridView) with current sale items
        private void UpdateSaleList()
        {
            dataGridViewSaleItems.DataSource = null;
            dataGridViewSaleItems.DataSource = saleItems;
            UpdateTotalSaleAmount();
        }

        // Method to calculate and display the total sale amount
        private void UpdateTotalSaleAmount()
        {
            decimal totalAmount = 0;
            foreach (var item in saleItems)
            {
                totalAmount += item.TotalPrice;
            }

            lblTotalAmount.Text = totalAmount.ToString();
        }

        // Method to finalize the sale (write data to the DB)
        private void FinalizeSale()
        {
            using (SQLiteConnection sqlite_conn = DBCon.CreateConnection())
            {
                if (sqlite_conn == null)
                {
                    MessageBox.Show("Database connection not established.");
                    return;
                }

                SQLiteTransaction transaction = sqlite_conn.BeginTransaction(); // Begin transaction
                try
                {
                    foreach (var saleItem in saleItems)
                    {
                        // Insert each sale item into the Sales table
                        using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                        {
                            sqlite_cmd.CommandText = @"INSERT INTO Sales (product_id, product_name, quantity, sale_date, barcode, unit_price, total_price) 
                                                       VALUES (@productId, @productName, @quantity, @saleDate, @barcode, @unitprice, @totalPrice)";
                            
                            sqlite_cmd.Parameters.AddWithValue("@productId", saleItem.ProductId);
                            sqlite_cmd.Parameters.AddWithValue("@productName", saleItem.Name);
                            sqlite_cmd.Parameters.AddWithValue("@quantity", saleItem.Quantity);
                            sqlite_cmd.Parameters.AddWithValue("@saleDate", DateTime.Now);
                            sqlite_cmd.Parameters.AddWithValue("@barcode", saleItem.Barcode);
                            sqlite_cmd.Parameters.AddWithValue("@unitprice", saleItem.Price);
                            sqlite_cmd.Parameters.AddWithValue("@totalPrice", saleItem.TotalPrice);

                            sqlite_cmd.ExecuteNonQuery();
                        }

                        // Update the stock quantity in the Products table
                        using (SQLiteCommand updateCmd = sqlite_conn.CreateCommand())
                        {
                            updateCmd.CommandText = @"UPDATE Products SET quantity = quantity - @quantity 
                                                      WHERE product_id = @productId";
                            
                            updateCmd.Parameters.AddWithValue("@quantity", saleItem.Quantity);
                            updateCmd.Parameters.AddWithValue("@productId", saleItem.ProductId);

                            updateCmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit(); // Commit the transaction
                    MessageBox.Show("Sale completed successfully!");
                    saleItems.Clear(); // Clear the sale list after successful transaction
                    UpdateSaleList(); // Refresh the UI

                    txtBarcode.Clear();
                    txtProductName.Clear();
                    txtUnitPrice.Clear();
                    numQuantity.ResetText();
                    lblTotalAmount.ResetText();

                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback in case of error
                    MessageBox.Show($"Failed to finalize the sale: {ex.Message}");
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string barcode = txtBarcode.Text.Trim(); // Get the entered barcode

            if (!string.IsNullOrEmpty(barcode)) // Ensure that barcode is not empty
            {
                FetchProductDetailsByBarcode(barcode); // Fetch product details from the database
            }
        }
        private void FetchProductDetailsByBarcode(string barcode)
        {
            try
            {
                using (SQLiteConnection sqlite_conn = DBCon.CreateConnection())
                {
                    using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                    {
                        // SQL query to get the product name and price based on the barcode
                        sqlite_cmd.CommandText = "SELECT name, price, product_id FROM Products WHERE barcode = @barcode";
                        sqlite_cmd.Parameters.AddWithValue("@barcode", barcode);

                        using (SQLiteDataReader reader = sqlite_cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // If product is found, update the name and price text fields
                                string productName = reader["name"].ToString();
                                decimal productPrice = Convert.ToDecimal(reader["price"]);

                                // Update UI fields
                                txtProductName.Text = productName;
                                txtUnitPrice.Text   = productPrice.ToString("F2");
                                productId           = Int32.Parse(reader["product_id"].ToString());
                                numQuantity.Focus();
                            }
                            else
                            {
                                // If no product is found, clear the fields and show a message
                                txtProductName.Text = "Product Not Found";
                                txtUnitPrice.Text   = "0.00";
                                MessageBox.Show("Product not found for this barcode!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve product details: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtBarcode.Text) && decimal.TryParse(txtUnitPrice.Text, out decimal productPrice))
            {
                int quantity = (int)numQuantity.Value;
                string productName = txtProductName.Text;

                AddProductToSale(txtBarcode.Text, productName, productPrice, quantity, productId);
                
            }
            else
            {
                MessageBox.Show("Please enter a valid barcode and ensure price is valid.");
            }

        }
        private string FetchProductName(string productId)
        {
            try
            {
                using (SQLiteConnection sqlite_conn = DBCon.CreateConnection())
                {
                    using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                    {
                        sqlite_cmd.CommandText = "SELECT name FROM Products WHERE product_id = @productId";
                        
                        sqlite_cmd.Parameters.AddWithValue("@productId", productId);

                        object result = sqlite_cmd.ExecuteScalar();

                        return result != null ? result.ToString() : "Unknown Product";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve product name: {ex.Message}");
                return "Unknown Product";
            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FinalizeSale();
            txtBarcode.Clear();
            txtProductName.Clear();
            txtUnitPrice.Clear();
            numQuantity.ResetText();
            txtBarcode.Focus();
        }

        private void numQuantity_ValueChanged(object sender, EventArgs e)
        {
            CheckStockAvailability(productId, Int32.Parse(numQuantity.Value.ToString()));
        }
        bool CheckStockAvailability(int productId, int quantityRequested)
        {
            try
            {
                using (SQLiteConnection sqlite_conn = DBCon.CreateConnection())
                {
                    using (SQLiteCommand checkStockCmd = sqlite_conn.CreateCommand())
                    {
                        // SQL query to get the available stock for the product
                        checkStockCmd.CommandText = "SELECT quantity FROM Products WHERE product_id = @product_id";
                        
                        checkStockCmd.Parameters.AddWithValue("@product_id", productId);

                        object result = checkStockCmd.ExecuteScalar();

                        if (result != null)
                        {
                            int availableStock = Convert.ToInt32(result);

                            // Check if the requested quantity is greater than the available stock
                            if (quantityRequested > availableStock)
                            {
                                MessageBox.Show($"Insufficient stock. Available: {availableStock}, Requested: {quantityRequested}");
                                return false;  // Not enough stock
                            }

                            return true;  // Stock is sufficient
                        }
                        else
                        {
                            MessageBox.Show("Product not found in stock.");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking stock: {ex.Message}");
                return false;
            }
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            txtBarcode.Focus();
        }
    }
}
