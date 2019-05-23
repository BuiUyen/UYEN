using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Sanita.Utility.UI
{
    public class AutoData
    {
        public String Text { get; set; }
        public String Key { get; set; }
        public String Data { get; set; }

        public AutoData()
        {

        }

        public AutoData(String text, String key)
        {
            Text = text;
            Key = key;
            Data = "";
        }

        public AutoData(String text, String key, String data)
        {
            Text = text;
            Key = key;
            Data = data;
        }
    }
}
