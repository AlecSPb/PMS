using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace XSHelper.Helpers
{
    public class XSDialogResult
    {
        public XSDialogResult()
        {
            HasSelected = false;
        }
        public bool HasSelected { get; set; }
        //public string FileName { get; set; }

        public string SelectPath { get; set; }
        public Font SelectedFont { get; set; }
        public Color SelectedColor { get; set; }
    }
}
