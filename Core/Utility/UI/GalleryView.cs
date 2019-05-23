using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sanita.Utility.UI;
using System.Runtime.InteropServices;

namespace Medibox.Utility.UI
{
    public partial class GalleryView : UserControl
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }

        public void ListView_SetSpacing(ListView listview, short cx, short cy)
        {
            const int LVM_FIRST = 0x1000;
            const int LVM_SETICONSPACING = LVM_FIRST + 53;
            // http://msdn.microsoft.com/en-us/library/bb761176(VS.85).aspx
            // minimum spacing = 4
            SendMessage(listview.Handle, LVM_SETICONSPACING,
            IntPtr.Zero, (IntPtr)MakeLong(cx, cy));

            // http://msdn.microsoft.com/en-us/library/bb775085(VS.85).aspx
            // DOESN'T WORK!
            // can't find ListView_SetIconSpacing in dll comctl32.dll
            //ListView_SetIconSpacing(listView.Handle, 5, 5);
        }

        public GalleryView()
        {
            InitializeComponent();

            short width = 100;
            short height = 100;

            mListView.LargeImageList = new ImageList();
            mListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            mListView.LargeImageList.ImageSize = new Size(width, height);
            mListView.View = System.Windows.Forms.View.LargeIcon;
            mListView.BackColor = Color.FromArgb(246, 246, 246);
            mListView.ForeColor = Color.Black;
            ListView_SetSpacing(mListView, (short)(width + 12), (short)(height + 4 + 20));
        }

        public GalleryView(short width, short height)
        {
            InitializeComponent();

            mListView.LargeImageList = new ImageList();
            mListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            mListView.LargeImageList.ImageSize = new Size(width, height);
            mListView.View = System.Windows.Forms.View.LargeIcon;
            mListView.BackColor = Color.FromArgb(246, 246, 246);
            mListView.ForeColor = Color.Black;
            ListView_SetSpacing(mListView, (short)(width + 12), (short)(height + 4 + 20));
        }

        public void AddItem(String name, Image item)
        {
            if (mListView.LargeImageList != null)
            {
                string imageKey = name;
                mListView.LargeImageList.Images.Add(imageKey, item);
                ListViewItem lvi = new ListViewItem(name, imageKey);
                lvi.Tag = item;                 
                mListView.Items.Insert(0, lvi);
                mListView.Sorting = SortOrder.Descending;
                mListView.Sort();
            }
        }

        public void RemoveItem(int index)
        {
            if (mListView.LargeImageList != null)
            {
                ListViewItem lvi = mListView.Items[index];
                mListView.LargeImageList.Images.RemoveByKey(lvi.ImageKey);
                mListView.Items.RemoveAt(index);

                if (mListView.Items.Count > 0)
                {
                    int i = Math.Min(index, mListView.Items.Count - 1);
                    mListView.Items[i].Selected = true;
                }
            }
        }

        public Image GetSelectedItem()
        {
            if (mListView.SelectedItems.Count > 0)
            {
                ListViewItem lvi = mListView.SelectedItems[0];
                return lvi.Tag as Image;
            }

            return null;
        }

        public String GetSelectedItemID()
        {
            if (mListView.SelectedItems.Count > 0)
            {
                ListViewItem lvi = mListView.SelectedItems[0];
                return lvi.ImageKey;
            }

            return null;
        }

        public int GetSelectedItemIndex()
        {
            if (mListView.SelectedItems.Count > 0)
            {
                ListViewItem lvi = mListView.SelectedItems[0];
                return lvi.Index;
            }

            return 0;
        }

        public void ResetList()
        {
            try
            {
                if (mListView.Items != null)
                {
                    mListView.Items.Clear();
                }
                if (mListView.LargeImageList != null && mListView.LargeImageList.Images != null)
                {
                    mListView.LargeImageList.Images.Clear();
                }
            }
            catch { }
        }

        private void mListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.OnMouseDoubleClick(e);
        }
    }
}
