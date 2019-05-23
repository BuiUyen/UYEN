using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sanita.Utility.UI;
using System.Windows.Forms;
using System.Collections;

namespace Sanita.Utility
{
    public class UtilityListView
    {
        public static void ListViewRefresh(ObjectListView list, IEnumerable datas)
        {
            list.BeginUpdate();
            int previousTopIndex = list.TopItemIndex;
            int indexSelected = list.SelectedIndex;

            list.SetObjects(datas);

            if (list is TreeListView)
            {
                ((TreeListView)list).RebuildAll(false);
                ((TreeListView)list).ExpandAll();
            }

            if (indexSelected >= 0 && indexSelected < list.Items.Count)
            {
                list.isAutoSelected = true;
                list.Items[indexSelected].Selected = true;
                list.isAutoSelected = false;
            }

            if (previousTopIndex >= 0 && previousTopIndex < list.Items.Count)
            {
                list.TopItemIndex = previousTopIndex;
            }

            list.EndUpdate();
        }

        public static void ListViewRefresh(ObjectListView list, IEnumerable datas, String strText, int index)
        {
            list.BeginUpdate();
            list.SetObjects(datas);

            if (list is TreeListView)
            {
                ((TreeListView)list).RebuildAll(true);
                ((TreeListView)list).ExpandAll();
            }

            if (!String.IsNullOrEmpty(strText))
            {
                for (int i = 0; i < list.Items.Count; i++)
                {
                    if (list.Items[i].SubItems.Count > index)
                    {
                        if (list.Items[i].SubItems[index].Text.Trim() == strText.Trim())
                        {
                            list.Focus();
                            list.isAutoSelected = true;
                            list.Items[i].Selected = true;
                            list.SelectedIndex = i;
                            list.isAutoSelected = false;

                            list.EnsureModelVisible(list.GetModelObject(i));
                            break;
                        }
                    }
                }
            }

            list.EndUpdate();
        }

        public static void DoListViewFilter(ObjectListView olv, String txtInput)
        {
            try
            {
                TextMatchFilter filter = null;
                if (!String.IsNullOrEmpty(txtInput.Trim()))
                {
                    filter = new TextMatchFilter(olv, txtInput, StringComparison.CurrentCultureIgnoreCase);
                }
                // Setup a default renderer to draw the filter matches
                if (filter == null)
                {
                    olv.DefaultRenderer = null;
                }
                else
                {
                    olv.DefaultRenderer = new HighlightTextRenderer(filter);
                }
                // Some lists have renderers already installed
                HighlightTextRenderer highlightingRenderer = olv.GetColumn(0).Renderer as HighlightTextRenderer;
                if (highlightingRenderer != null)
                {
                    highlightingRenderer.Filter = filter;
                }

                olv.ModelFilter = filter;
            }
            catch { }
        }
    }
}
