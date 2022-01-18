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

namespace CompE361Project
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            // logout
            FormLogin form = new FormLogin();
            form.Show();
            this.Hide();
        }
        public class Product
        {
            public string name;
            public double price;
            public int quantity;
        }

        public class Sales
        {
            public string customerName;
            public string product;
            public double price;
            public int quantity;
        }

        public class Order
        {
            public string customerName;
            public string product;
            public double price;
            public int quantity;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

            ////////////////////////////////////////////////
            //               Inventory tab              //
            //////////////////////////////////////////////

            FileStream file = new FileStream("Products.txt", FileMode.Open, FileAccess.Read); // open text file
            StreamReader reader = new StreamReader(file); // reader created
            string text = reader.ReadToEnd(); // store entire data in text string
            string[] lines = text.Split(','); // each column seperated by comma

            Product[] products = new Product[lines.Length / 3]; // Product class has 3 variables

            int i, j = 0;
            for (i = 0; i < lines.Length / 3; i++)
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
                listViewProdInv.Items.Add(item);
            }
            // close reader & file
            reader.Close();
            file.Close();

            // reset counters
            i = 0;
            j = 0;


            ////////////////////////////////////////////////
            //               Sales tab                  //
            //////////////////////////////////////////////

            FileStream file1 = new FileStream("sales.txt", FileMode.Open, FileAccess.Read); // open text file
            StreamReader reader1 = new StreamReader(file1); // reader created
            string text1 = reader1.ReadToEnd(); // store entire data in text
            string[] lines1 = text1.Split(','); // each column seperated by comma

            Sales[] sales = new Sales[lines1.Length / 4]; // Sales class has 4 variables


            for (i = 0; i < lines1.Length / 4; i++)
            {
                sales[i] = new Sales(); // initialize with default constructor
                // assign variables
                sales[i].customerName = lines1[j];
                sales[i].product = lines1[j + 1];
                sales[i].price = Convert.ToDouble(lines1[j + 2]);
                sales[i].quantity = Convert.ToInt32(lines1[j + 3]);
                j += 4; // iterate through rows

                // import values in listview table
                ListViewItem item1 = new ListViewItem(sales[i].customerName);
                item1.SubItems.Add(sales[i].product);
                item1.SubItems.Add(sales[i].price.ToString("C2"));
                item1.SubItems.Add(sales[i].quantity.ToString());
                listViewSales.Items.Add(item1);
            }
            // close reader & file
            reader1.Close();
            file1.Close();

            // reset counters
            i = 0;
            j = 0;


            ////////////////////////////////////////////////
            //              Orders tab                  //
            //////////////////////////////////////////////

            FileStream file2 = new FileStream("orders.txt", FileMode.Open, FileAccess.Read); // open text file
            StreamReader reader2 = new StreamReader(file2); // reader created
            string text2 = reader2.ReadToEnd(); // store entire data in text
            string[] lines2 = text2.Split(','); // each column seperated by comma

            Order[] orders = new Order[lines2.Length / 4]; // Order class has 4 variables

            for (i = 0; i < lines2.Length / 4; i++)
            {
                orders[i] = new Order(); // initialize with default constructor
                // assign variables
                orders[i].customerName = lines2[j];
                orders[i].product = lines2[j + 1];
                orders[i].price = Convert.ToDouble(lines2[j + 2]);
                orders[i].quantity = Convert.ToInt32(lines2[j + 3]);
                j += 4; // iterate through rows

                // import values in listview table
                ListViewItem item2 = new ListViewItem(orders[i].customerName);
                item2.SubItems.Add(orders[i].product);
                item2.SubItems.Add(orders[i].price.ToString("C2"));
                item2.SubItems.Add(orders[i].quantity.ToString());
                listViewOrders.Items.Add(item2);
            }
            // close reader & file
            reader2.Close();
            file2.Close();

        }

        // add method
        private void add(String prod, String prc, String quan) // takes 3 parameters corresponding to each column
        {
            String[] row = { prod, prc, quan }; // create a row array of the 3 columns
            ListViewItem item = new ListViewItem(row); // create object
            listViewProdInv.Items.Add(item); // add item inside listview
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // call add method to add values typed in each text box
            add(textBoxProduct.Text, textBoxPrice.Text, textBoxQuantity.Text);

            // clear each text box after
            textBoxProduct.Text = "";
            textBoxPrice.Text = "";
            textBoxQuantity.Text = "";
        }

        // update method
        private void update()
        {
            // set each element in the row according to what is typed in each text box 
            listViewProdInv.SelectedItems[0].SubItems[0].Text = textBoxProduct.Text;
            listViewProdInv.SelectedItems[0].SubItems[1].Text = textBoxPrice.Text;
            listViewProdInv.SelectedItems[0].SubItems[2].Text = textBoxQuantity.Text;

            // clear each text box after
            textBoxProduct.Text = "";
            textBoxPrice.Text = "";
            textBoxQuantity.Text = "";
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // call update method
            update();
        }

        // delete method
        private void delete()
        {
            if (MessageBox.Show("Are you sure to delete?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // remove selected row
                listViewProdInv.Items.RemoveAt(listViewProdInv.SelectedIndices[0]);

                // clear each text box after
                textBoxProduct.Text = "";
                textBoxPrice.Text = "";
                textBoxQuantity.Text = "";
            }

        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // call delete method
            delete();
        }

        private void listViewProdInv_MouseClick(object sender, MouseEventArgs e)
        {
            // mouse click selects the entire row
            textBoxProduct.Text = listViewProdInv.SelectedItems[0].SubItems[0].Text;
            textBoxPrice.Text = listViewProdInv.SelectedItems[0].SubItems[1].Text;
            textBoxQuantity.Text = listViewProdInv.SelectedItems[0].SubItems[2].Text;
        }

    }
}
