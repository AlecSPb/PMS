using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace PMSOrderEmailHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            model.Customer = "Midsummer";
            model.PO = "PO#124578";
            TxtCustomer.DataBindings.Add("Text", model, "Customer",false,DataSourceUpdateMode.OnPropertyChanged);
            TxtPO.DataBindings.Add("Text", model, "PO");

            List<string> compositionList = new List<string>();
            compositionList.Add("CuInGaSe");
            compositionList.Add("CuGaSe");
            compositionList.Add("InSe");
            compositionList.Add("InS");
            compositionList.Add("GaSe");
            compositionList.Add("CuSe");
            BindingList<string> ds = new BindingList<string>(compositionList);
            listBox1.DataSource = ds;

        }
        OrderModel model = new OrderModel();

        private void button1_Click(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(model);
            richTextBox1.Text = json;
        }
    }
}
