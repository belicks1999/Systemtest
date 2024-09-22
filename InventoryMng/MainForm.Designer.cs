
namespace InventoryMng
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.inventoryManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailySalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barcodePrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barcodePrintSingleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendLowStockMailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dBBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inventoryManagementToolStripMenuItem,
            this.salesToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1391, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // inventoryManagementToolStripMenuItem
            // 
            this.inventoryManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addStockToolStripMenuItem});
            this.inventoryManagementToolStripMenuItem.Name = "inventoryManagementToolStripMenuItem";
            this.inventoryManagementToolStripMenuItem.Size = new System.Drawing.Size(143, 19);
            this.inventoryManagementToolStripMenuItem.Text = "Inventory management";
            // 
            // addStockToolStripMenuItem
            // 
            this.addStockToolStripMenuItem.Name = "addStockToolStripMenuItem";
            this.addStockToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.addStockToolStripMenuItem.Text = "Add item";
            this.addStockToolStripMenuItem.Click += new System.EventHandler(this.addStockToolStripMenuItem_Click);
            // 
            // salesToolStripMenuItem
            // 
            this.salesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSaleToolStripMenuItem});
            this.salesToolStripMenuItem.Name = "salesToolStripMenuItem";
            this.salesToolStripMenuItem.Size = new System.Drawing.Size(45, 19);
            this.salesToolStripMenuItem.Text = "Sales";
            // 
            // newSaleToolStripMenuItem
            // 
            this.newSaleToolStripMenuItem.Name = "newSaleToolStripMenuItem";
            this.newSaleToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.newSaleToolStripMenuItem.Text = "New Sale";
            this.newSaleToolStripMenuItem.Click += new System.EventHandler(this.newSaleToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dailySalesToolStripMenuItem,
            this.barcodePrintToolStripMenuItem,
            this.barcodePrintSingleToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 19);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // dailySalesToolStripMenuItem
            // 
            this.dailySalesToolStripMenuItem.Name = "dailySalesToolStripMenuItem";
            this.dailySalesToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.dailySalesToolStripMenuItem.Text = "Daily sales";
            this.dailySalesToolStripMenuItem.Click += new System.EventHandler(this.dailySalesToolStripMenuItem_Click);
            // 
            // barcodePrintToolStripMenuItem
            // 
            this.barcodePrintToolStripMenuItem.Name = "barcodePrintToolStripMenuItem";
            this.barcodePrintToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.barcodePrintToolStripMenuItem.Text = "Barcode print (All)";
            this.barcodePrintToolStripMenuItem.Click += new System.EventHandler(this.barcodePrintToolStripMenuItem_Click);
            // 
            // barcodePrintSingleToolStripMenuItem
            // 
            this.barcodePrintSingleToolStripMenuItem.Name = "barcodePrintSingleToolStripMenuItem";
            this.barcodePrintSingleToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.barcodePrintSingleToolStripMenuItem.Text = "Barcode print (Single)";
            this.barcodePrintSingleToolStripMenuItem.Click += new System.EventHandler(this.barcodePrintSingleToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emailSetupToolStripMenuItem,
            this.sendLowStockMailToolStripMenuItem,
            this.dBBackupToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 19);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // emailSetupToolStripMenuItem
            // 
            this.emailSetupToolStripMenuItem.Name = "emailSetupToolStripMenuItem";
            this.emailSetupToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.emailSetupToolStripMenuItem.Text = "Email setup";
            this.emailSetupToolStripMenuItem.Click += new System.EventHandler(this.emailSetupToolStripMenuItem_Click);
            // 
            // sendLowStockMailToolStripMenuItem
            // 
            this.sendLowStockMailToolStripMenuItem.Name = "sendLowStockMailToolStripMenuItem";
            this.sendLowStockMailToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sendLowStockMailToolStripMenuItem.Text = "Send low stock Mail";
            this.sendLowStockMailToolStripMenuItem.Click += new System.EventHandler(this.sendLowStockMailToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1391, 929);
            this.panel1.TabIndex = 1;
            // 
            // dBBackupToolStripMenuItem
            // 
            this.dBBackupToolStripMenuItem.Name = "dBBackupToolStripMenuItem";
            this.dBBackupToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dBBackupToolStripMenuItem.Text = "DB backup";
            this.dBBackupToolStripMenuItem.Click += new System.EventHandler(this.dBBackupToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1391, 954);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory management";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem inventoryManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dailySalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barcodePrintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailSetupToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem barcodePrintSingleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendLowStockMailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dBBackupToolStripMenuItem;
    }
}