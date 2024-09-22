using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryMng
{
    public class DBCon
    {
        private static SQLiteConnection sqlite_conn;

        // Method to create a connection
        public static SQLiteConnection CreateConnection()
        {
            string databasePath = Application.StartupPath + "\\InventoryMngDB.db";

            if (!File.Exists(databasePath))
            {
                MessageBox.Show($"Database file not found at: {databasePath}");
                return null;
            }

            try
            {
                sqlite_conn = new SQLiteConnection($"Data Source={databasePath}; Foreign Keys=true; FailIfMissing=True;");
                sqlite_conn.Open();
                Console.WriteLine("Connection established successfully.");
                CreateTable();
                return sqlite_conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open the database connection: {ex.Message}");
                return null;
            }
        }

        // Method to create the Products table if it doesn't exist
        public static void CreateTable()
        {
            try
            {
                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                {
                    sqlite_cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Products (
                                                product_id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                name TEXT NOT NULL,
                                                description TEXT,
                                                price REAL NOT NULL,
                                                quantity INTEGER NOT NULL
                                              );";

                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_cmd.CommandText = @"CREATE TABLE IF NOT EXISTS [Sales] (
                                              [sale_id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                                            , [product_id] bigint NOT NULL
                                            , [product_name] text NOT NULL
                                            , [barcode] text NOT NULL
                                            , [quantity] bigint NOT NULL
                                            , [unit_price] real NOT NULL
                                            , [total_price] real NOT NULL
                                            , [sale_date] datetime DEFAULT (CURRENT_TIMESTAMP) NULL
                                            , CONSTRAINT [FK_Sales_0_0] FOREIGN KEY ([product_id]) REFERENCES [Products] ([product_id]) ON DELETE NO ACTION ON UPDATE NO ACTION
                                            );";

                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_cmd.CommandText = @"CREATE TABLE IF NOT EXISTS [EmailSettings] (
                                              [FromEmail] text NULL
                                            , [ToEmail] text NULL
                                            , [AppPassword] text NULL);";

                    sqlite_cmd.ExecuteNonQuery();

                    Console.WriteLine("Tables created or already exists.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create the Products table: {ex.Message}");
            }
        }

        internal void updateDataItem(int productId, string name, string description, decimal price, int quantity)
        {
            try
            {
                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                {
                    sqlite_cmd.CommandText = "UPDATE Products SET name = @name, description = @description, price = @price, quantity = @quantity, barcode = @barcode where product_id = @productId";
                    sqlite_cmd.Parameters.AddWithValue("@productId", productId);
                    sqlite_cmd.Parameters.AddWithValue("@name", name);
                    sqlite_cmd.Parameters.AddWithValue("@description", description);
                    sqlite_cmd.Parameters.AddWithValue("@price", price);
                    sqlite_cmd.Parameters.AddWithValue("@quantity", quantity);
                    sqlite_cmd.Parameters.AddWithValue("@barcode", name + "IT" + description);

                    sqlite_cmd.ExecuteNonQuery();
                    MessageBox.Show("Item updated successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update data: {ex.Message}");
            }
        }
        internal void deleteDataItem(int productId)
        {
            try
            {
                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                {
                    sqlite_cmd.CommandText = "delete Products where product_id = @productId";
                    sqlite_cmd.Parameters.AddWithValue("@productId", productId);

                    sqlite_cmd.ExecuteNonQuery();
                    MessageBox.Show("Item updated successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update data: {ex.Message}");
            }
        }

        // Method to insert data into the Products table
        public void InsertDataProduct(string name, string description, decimal price, int quantity)
        {
            try
            {
                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                {
                    sqlite_cmd.CommandText = "INSERT INTO Products(name, description, price, quantity, barcode) VALUES (@name, @description, @price, @quantity, @barcode)";
                    sqlite_cmd.Parameters.AddWithValue("@name", name);
                    sqlite_cmd.Parameters.AddWithValue("@description", description);
                    sqlite_cmd.Parameters.AddWithValue("@price", price);
                    sqlite_cmd.Parameters.AddWithValue("@quantity", quantity);
                    sqlite_cmd.Parameters.AddWithValue("@barcode", name + "IT" + description);

                    sqlite_cmd.ExecuteNonQuery();
                    MessageBox.Show("Item deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to insert data: {ex.Message}");
            }
        }
        public void InsertDataEmail(string fromEmail, string toEmail, string password)
        {
            try
            {
                // Ensure that we have a valid connection before proceeding
                using (SQLiteConnection sqlite_conn = DBCon.CreateConnection())  // Open connection for each operation
                {
                    if (sqlite_conn == null)
                    {
                        MessageBox.Show("Failed to establish a database connection.");
                        return;
                    }

                    using (SQLiteCommand sqlite_cmd     = sqlite_conn.CreateCommand())
                    {
                        SQLiteCommand sqlite_cmdDelete  = sqlite_conn.CreateCommand();
                        sqlite_cmdDelete.CommandText    = "Delete  FROM EmailSettings";
                        sqlite_cmdDelete.ExecuteNonQuery();

                        sqlite_cmd.CommandText = "INSERT INTO EmailSettings (FromEmail, ToEmail, AppPassword) VALUES (@FromEmail, @ToEmail, @AppPassword)";
                        
                        sqlite_cmd.Parameters.AddWithValue("@FromEmail", fromEmail);
                        sqlite_cmd.Parameters.AddWithValue("@ToEmail", toEmail);
                        sqlite_cmd.Parameters.AddWithValue("@AppPassword", password);

                        sqlite_cmd.ExecuteNonQuery();
                        MessageBox.Show("Email setup successful!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to insert data: {ex.Message}");
            }
        }


        // Method to retrieve data from the Products table
        public DataTable GetData()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string query = "SELECT product_id, name, description, price, quantity FROM Products";

                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, sqlite_conn))
                {
                    dataAdapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve data: {ex.Message}");
            }
            return dataTable;
        }
    }

}
