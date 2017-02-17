using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bt=BarTender;

namespace UseBarTender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bt.Application btApp;
        bt.Format btnFormat;
        private void button1_Click(object sender, EventArgs e)
        {
            string msg = "hello world";
            btApp = new bt.Application();
            btnFormat = btApp.Formats.Open(@"C:\Users\newli\Documents\BarTender\BarTender Documents\10070.btw", false, "");
            btnFormat.PrintSetup.IdenticalCopiesOfLabel = 1;
            btnFormat.PrintSetup.NumberSerializedLabels = 1;
            btnFormat.SetNamedSubStringValue("Content", msg);

            btnFormat.PrintOut(true, true);
            btnFormat.Close(bt.BtSaveOptions.btSaveChanges);

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            btApp.Quit(bt.BtSaveOptions.btSaveChanges);
            base.OnClosing(e);
        }
    }
}
