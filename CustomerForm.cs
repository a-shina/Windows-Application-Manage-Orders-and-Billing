using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CompE361Project
{

    public partial class FormCustomer : Form
    {
        // global variables
        Product[] products; // product class
        // toolbox arrays
        CheckBox[] checkBoxes;
        NumericUpDown[] numericUpDowns;
        Label[] labels;

        // create delegate for custom event handler
        public delegate void Discount(object sender, EventArgs e);
        public event Discount MsgDiscount;

        public FormCustomer()
        {
            InitializeComponent();
        }
        
        // Create Product class
        public class Product
        {
            public string name;
            public double price;
            public int quantity;
        }


        private void CustomerForm_Load(object sender, EventArgs e)
        {
            // Create arrays of checkBoxes, numericUpDowns, and labels
            checkBoxes = new CheckBox[] { checkBox0, checkBox1, checkBox2, checkBox3, checkBox4 };
            numericUpDowns = new NumericUpDown[] { numericUpDown0, numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4 };
            labels = new Label[] { label0, label1, label2, label3, label4 };

            // open text file
            FileStream file = new FileStream("Products.txt", FileMode.Open, FileAccess.Read); 
            StreamReader reader = new StreamReader(file); // reader created
            string text = reader.ReadToEnd(); // store entire data in text
            string[] lines = text.Split(','); // each column seperated by comma

            products = new Product[lines.Length / 3]; // Product class has 3 variables

            int j = 0;
            for (int i = 0; i < lines.Length / 3; i++)
            {
                products[i] = new Product(); // initialize with default constructor
                // assign variables
                products[i].name = lines[j];
                products[i].price = Convert.ToDouble(lines[j + 1]);
                products[i].quantity = Convert.ToInt32(lines[j + 2]);
                j += 3; // iterate through rows

                // import values in listview table
                ListViewItem item = new ListViewItem(products[i].name);
                item.SubItems.Add(products[i].price.ToString("C2"));
                item.SubItems.Add(products[i].quantity.ToString());
                listView1.Items.Add(item);

                // check if quantity is less than 5
                if (products[i].quantity < 5)
                {
                    labels[i].Text = "Less than 5 left in stock!";
                }

                // set all numericUpDowns maximum values according to products' quantities
                numericUpDowns[i].Maximum = new decimal(new int[] {products[i].quantity,0,0,0});

                
            }

            // close reader & file
            reader.Close();
            file.Close();     
        }

        private void buttonPlaceOrder_Click(object sender, EventArgs e)
        {
            double total = 0;
            double discount = 0;
            MsgDiscount += new Discount(DiscountHandler); // display message
            
            for (int i = 0; i < checkBoxes.Length; i++)
            {
                // check if checkboxes have been checked
                if (checkBoxes[i].Checked && checkBoxes[i].Enabled)
                {
                    // calculate total
                    total += (products[i].price * Convert.ToDouble(numericUpDowns[i].Value));
                }

            }

            // check if total is more than $1000, give 10% discount on total
            if (total >= 1000)
            {
                EventArgs arg = new EventArgs();
                MsgDiscount(this, arg); 
                discount = 0.1;
                total -= (total * discount);
            }

            // display total
            MessageBox.Show(string.Format("Total: {0:C2}", total), "Bill");

            // logout
            FormLogin form = new FormLogin();
            form.Show();
            this.Hide();

        }


        // custom event handler
        public void DiscountHandler(object sender, EventArgs e)
        {
            MessageBox.Show("You get 10% off total!", "Discount");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            // logout
            FormLogin form = new FormLogin();
            form.Show();
            this.Hide();
        }
    }
}
