using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace Medibox.Utility.UI
{
    public class SanitaButton : ButtonX
    {
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                return true;
            }

            return base.IsInputKey(keyData);
        }
    }
}
