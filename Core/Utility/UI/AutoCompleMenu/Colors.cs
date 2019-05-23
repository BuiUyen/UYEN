using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Sanita.Utility.UI
{
    [Serializable]
    public class Colors
    {
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }
        public Color SelectedForeColor { get; set; }
        public Color SelectedBackColor { get; set; }
        public Color SelectedBackColor2 { get; set; }
        public Color HighlightingColor { get; set; }

        public Colors()
        {
            ForeColor = Color.Black;
            BackColor = Color.White;
            SelectedForeColor = Color.White;
            SelectedBackColor = Color.FromArgb(51, 153, 255);
            SelectedBackColor2 = Color.FromArgb(51, 153, 255);
            HighlightingColor = Color.FromArgb(51, 153, 255);

            //ForeColor = Color.Black;
            //BackColor = Color.White;
            //SelectedForeColor = Color.Black;
            //SelectedBackColor = Color.FromArgb(253, 230, 144);
            //SelectedBackColor2 = Color.FromArgb(253, 230, 144);
            //HighlightingColor = Color.FromArgb(253, 230, 144);
        }
    }
}
