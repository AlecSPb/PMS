using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntermolecularLotParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TxtInput.Text = System.IO.File.ReadAllText("Example1.txt");

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            var pp = new LotProcess();
            pp.InputString = TxtInput.Text;
            pp.Process();
        }
    }
}
