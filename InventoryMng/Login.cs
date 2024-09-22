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
    public partial class Login : Form
    {
        Color PanelColor = Color.FromArgb(40, 116, 166);
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BackColor = PanelColor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "superadmin" && txtPassword.Text == "superadmin")
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid credentials");
            }
        }
    }
}
