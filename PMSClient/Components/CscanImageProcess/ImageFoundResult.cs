using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Components.CscanImageProcess
{
    public class ImageFoundResult
    {
        public ImageFoundResult()
        {
            IsFound = false;
        }
        public bool IsFound { get; set; }
        public string ImagePath { get; set; }
        public string InfoMessage { get; set; }
    }
}
