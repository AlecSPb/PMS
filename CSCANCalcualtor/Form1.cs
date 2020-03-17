using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSCANCalcualtor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            v1 = v2 = l1 = l2;
            LoadData();
        }

        public void LoadData()
        {
            var db = new UltraSoundDb();
            db.LoadDataFiles();
            var lv_items = new List<ListViewItem>();
            foreach (var item in db.Materials)
            {
                string[] str = new string[2];
                str[0] = item.Composition;
                str[1] = item.UltraSoundSpeed.ToString();
                var temp = new ListViewItem(str);
                lv_items.Add(temp);
            }
            LVSoundSpeed.Items.Clear();
            LVSoundSpeed.Items.AddRange(lv_items.ToArray());
        }

        private double v1, v2, l1, l2;

        private void LVSoundSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LVSoundSpeed.SelectedItems.Count > 0)
            {
                var item = LVSoundSpeed.SelectedItems[0].SubItems[1].Text;
                TxtSpeedA.Text = item;
                string material = LVSoundSpeed.SelectedItems[0].Text;
                groupBox1.Text = $"介质A({material})";
            }
        }

        private void BtnDataFile_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("数据格式:每行一条数据，用+将名称和数值隔开，结束后保存", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start("Data.txt");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LVSoundSpeed_Click(object sender, EventArgs e)
        {

        }

        private void LVSoundSpeed_DoubleClick(object sender, EventArgs e)
        {

        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                double.TryParse(TxtSpeedA.Text.Trim(), out v1);
                double.TryParse(TxtSpeedB.Text.Trim(), out v2);
                double.TryParse(TxtRangeA.Text.Trim(), out l1);

                l2 = v1 * l1 / v2;
                TxtRangeB.Text = l2.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
