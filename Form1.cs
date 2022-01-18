using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompE361Project
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        // store usernames & passwords in list of strings
        List<string> users = new List<string>();
        List<string> pass = new List<string>();

        private void button_login_Click(object sender, EventArgs e)
        {
            // if nothing is typed
            if (textBox_username.Text == "" || textBox_password.Text == "")
            {
                MessageBox.Show("Please Provide Username and Password");
                return;
            }
            else
            {
                // check if admin credentials match
                if (users[0].Contains(textBox_username.Text) && pass[0].Contains(textBox_password.Text))
                {
                    // Show Admin form
                    FormAdmin adminForm = new FormAdmin();
                    adminForm.Show();
                    this.Hide();
                }
                // check if customer credentials match
                else if (users[1].Contains(textBox_username.Text) && pass[1].Contains(textBox_password.Text))
                {
                    // Show Costumer form
                    FormCustomer costumerForm = new FormCustomer();
                    costumerForm.Show();
                    this.Hide();
                }
                else
                {
                    // if credentials do not match
                    MessageBox.Show("The username and/or password is incorrect");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Read from txt file when form starts
            StreamReader reader = new StreamReader("accts.txt");
            string line = ""; // read line by line
            while ((line=reader.ReadLine()) != null) // until the end of it
            {
                // usernames & passwords seperated by comma
                string[] col = line.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                users.Add(col[0]); // first column
                pass.Add(col[1]); // second column
            }
        }
    }
}
