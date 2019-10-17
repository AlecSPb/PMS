using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator.XMLGenerator
{
    public class ElementFullNameDict
    {
        private Dictionary<string, string> elements;
        public ElementFullNameDict()
        {
            elements = new Dictionary<string, string>();
            Init();
        }

        private void Init()
        {
            elements.Add("Se", "Selenium");
            elements.Add("As", "Arsenic");
            elements.Add("Ge", "Germanium");
            elements.Add("Si", "Silicon");
            elements.Add("Al", "Aluminum");
            elements.Add("Bi", "Bismuth");
            elements.Add("Cr", "Chromium");
            elements.Add("Cu", "Copper");
            elements.Add("Fe", "Iron");
            elements.Add("Ga", "Gallium");
            elements.Add("Mg", "Magnesium");
            elements.Add("Mn", "Manganese");
            elements.Add("Ni", "Nickel");
            elements.Add("Pb", "Lead");
            elements.Add("Sc", "Scandium");
            elements.Add("S", "Sulfur");
            elements.Add("Ti", "Titanium");
            elements.Add("W", "Tungsten");
            elements.Add("Y", "Yttrium");
            elements.Add("O", "Oxygen");
            elements.Add("N", "Nitrogen");
        }

        public string GetShortName(string key)
        {
            if (elements.ContainsKey(key))
            {
                return elements[key];
            }
            else
            {
                return key;
            }
        }

    }
}
