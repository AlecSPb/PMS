using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CSCANCalcualtor
{
    public class UltraSoundDb
    {
        public UltraSoundDb()
        {
            Materials = new List<Material>();
        }
        public List<Material> Materials;

        public void LoadDataFiles()
        {
            try
            {
                string[] lines = File.ReadAllLines("Data.txt");

                Materials.Clear();
                foreach (var line in lines)
                {
                    string[] data = line.Split('+');
                    var temp = new Material();
                    temp.Composition = data[0];
                    temp.UltraSoundSpeed = double.Parse(data[1]);
                    Materials.Add(temp);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
