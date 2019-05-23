using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sanita.Utility;

namespace Sanita.Utility.UI
{
    /// <summary>
    /// This autocomplete item appears after dot
    /// </summary>
    public class MethodAutocompleteItem : AutocompleteItem
    {
        string firstPart;
        string lowercaseText;

        public MethodAutocompleteItem(string text)
            : base(text)
        {
            lowercaseText = Text.ToLower();
        }

        public override CompareResult Compare(string fragmentText)
        {
            int i = fragmentText.LastIndexOf('.');
            if (i < 0)
                return CompareResult.Hidden;
            string lastPart = fragmentText.Substring(i + 1);
            firstPart = fragmentText.Substring(0, i);

            if (lastPart == "") return CompareResult.Visible;
            if (Text.StartsWith(lastPart, StringComparison.InvariantCultureIgnoreCase))
                return CompareResult.VisibleAndSelected;
            if (lowercaseText.Contains(lastPart.ToLower()))
                return CompareResult.Visible;

            return CompareResult.Hidden;
        }

        public override string GetTextForReplace()
        {
            return firstPart + "." + Text;
        }
    }

    /// <summary>
    /// Autocomplete item for code snippets
    /// </summary>
    /// <remarks>Snippet can contain special char ^ for caret position.</remarks>
    public class SnippetAutocompleteItem : AutocompleteItem
    {
        public SnippetAutocompleteItem(string snippet)
        {
            Text = snippet.Replace("\r", "");
            ToolTipTitle = "Code snippet:";
            ToolTipText = Text;
        }

        public override string ToString()
        {
            return MenuText ?? Text.Replace("\n", " ").Replace("^", "");
        }

        public override string GetTextForReplace()
        {
            return Text;
        }

        public override void OnSelected(SelectedEventArgs e)
        {
            var tb = Parent.TargetControlWrapper;
            //
            if (!Text.Contains("^"))
                return;
            var text = tb.Text;
            for (int i = Parent.Fragment.Start; i < text.Length; i++)
                if (text[i] == '^')
                {
                    tb.SelectionStart = i;
                    tb.SelectionLength = 1;
                    tb.SelectedText = "";
                    return;
                }
        }

        /// <summary>
        /// Compares fragment text with this item
        /// </summary>
        public override CompareResult Compare(string fragmentText)
        {
            if (Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase) &&
                   Text != fragmentText)
                return CompareResult.Visible;

            return CompareResult.Hidden;
        }
    }

    /// <summary>
    /// This class finds items by substring
    /// </summary>
    public class SubstringAutocompleteItem : AutocompleteItem
    {
        protected readonly string lowercaseText;
        protected readonly string lowercaseKey;
        protected readonly string lowercaseData;
        protected readonly bool ignoreCase;

        public SubstringAutocompleteItem(string text, bool ignoreCase = true)
            : base(text)
        {
            this.ignoreCase = ignoreCase;
            if (ignoreCase)
            {
                lowercaseText = text.ToLower();
            }
        }

        public SubstringAutocompleteItem(string text, String key, String data, bool ignoreCase = true)
            : base(text, key, data)
        {
            this.ignoreCase = ignoreCase;
            if (ignoreCase)
            {
                lowercaseText = text == null ? "" : text.ToLower();
                lowercaseKey = key == null ? "" : key.ToLower();
                lowercaseData = data == null ? "" : data.ToLower();
            }
        }

        public override CompareResult Compare(string fragmentText)
        {
            if (Parent.SearchTinhHuyenXa)
            {
                if (fragmentText.Length == 2 || fragmentText.Length == 3)
                {
                    if (lowercaseKey.ToUpper().StartsWith(fragmentText + "KX", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return CompareResult.VisibleAndSelected;
                    }
                }
                else
                {
                    if (lowercaseKey.ToUpper().StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return CompareResult.VisibleAndSelected;
                    }
                }
            }
            else
            {
                if (ignoreCase)
                {
#if true
                    //if (!String.IsNullOrEmpty(fragmentText.Trim()))
                    //{
                    //    String[] list_data = fragmentText.TrimSpace().Trim().Split(' ');
                    //    foreach (String data in list_data)
                    //    {
                    //        if (!String.IsNullOrEmpty(data))
                    //        {
                    //            if (!SanitaUtility.RemoveSign4VN(lowercaseText).Contains(SanitaUtility.RemoveSign4VN(data.ToLower())))
                    //            {
                    //                return CompareResult.Hidden;
                    //            }
                    //        }
                    //    }
                    //    return CompareResult.Visible;
                    //}
                    if (lowercaseText.ToLower() == fragmentText.ToLower())
                    {
                        return CompareResult.VisibleAndSelected;
                    }
                    if (lowercaseText != null && SanitaUtility.RemoveSign4VN(lowercaseText).Contains(SanitaUtility.RemoveSign4VN(fragmentText.ToLower())))
                    {
                        return CompareResult.Visible;
                    }
                    if (lowercaseKey != null && lowercaseKey.Contains(fragmentText.ToLower()))
                    {
                        return CompareResult.Visible;
                    }
                    if (lowercaseData != null && lowercaseData.Contains(fragmentText.ToLower()))
                    {
                        return CompareResult.Visible;
                    }
#else
                if (lowercaseText.Contains(fragmentText.ToLower()))
                {
                    return CompareResult.Visible;
                }
#endif
                }
                else
                {
                    if (!String.IsNullOrEmpty(Text) && Text.Contains(fragmentText))
                    {
                        return CompareResult.Visible;
                    }
                    if (!String.IsNullOrEmpty(Key) && Key.Contains(fragmentText))
                    {
                        return CompareResult.Visible;
                    }
                    if (!String.IsNullOrEmpty(Data) && Data.Contains(fragmentText))
                    {
                        return CompareResult.Visible;
                    }
                }
            }

            return CompareResult.Hidden;
        }
    }

    /// <summary>
    /// This item draws multicolumn menu
    /// </summary>
    public class MulticolumnAutocompleteItem : SubstringAutocompleteItem
    {
        public bool CompareBySubstring { get; set; }
        public string[] MenuTextByColumns { get; set; }
        public int[] ColumnWidth { get; set; }

        public MulticolumnAutocompleteItem(string[] menuTextByColumns, string insertingText, bool compareBySubstring = true, bool ignoreCase = true)
            : base(insertingText, ignoreCase)
        {
            this.CompareBySubstring = compareBySubstring;
            this.MenuTextByColumns = menuTextByColumns;
        }

        public override CompareResult Compare(string fragmentText)
        {
            if (CompareBySubstring)
                return base.Compare(fragmentText);

            if (ignoreCase)
            {
                if (Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase))
                    return CompareResult.VisibleAndSelected;
            }
            else
                if (Text.StartsWith(fragmentText))
                    return CompareResult.VisibleAndSelected;

            return CompareResult.Hidden;
        }

        public override void OnPaint(PaintItemEventArgs e)
        {
            if (ColumnWidth != null && ColumnWidth.Length != MenuTextByColumns.Length)
                throw new Exception("ColumnWidth.Length != MenuTextByColumns.Length");

            int[] columnWidth = ColumnWidth;
            if (columnWidth == null)
            {
                columnWidth = new int[MenuTextByColumns.Length];
                float step = e.TextRect.Width / MenuTextByColumns.Length;
                for (int i = 0; i < MenuTextByColumns.Length; i++)
                    columnWidth[i] = (int)step;
            }

            //draw columns
            Pen pen = Pens.Silver;
            float x = e.TextRect.X;
            e.StringFormat.FormatFlags = e.StringFormat.FormatFlags | StringFormatFlags.NoWrap;

            using (var brush = new SolidBrush(e.IsSelected ? e.Colors.SelectedForeColor : e.Colors.ForeColor))
                for (int i = 0; i < MenuTextByColumns.Length; i++)
                {
                    var width = columnWidth[i];
                    var rect = new RectangleF(x, e.TextRect.Top, width, e.TextRect.Height);
                    e.Graphics.DrawLine(pen, new PointF(x, e.TextRect.Top), new PointF(x, e.TextRect.Bottom));
                    e.Graphics.DrawString(MenuTextByColumns[i], e.Font, brush, rect, e.StringFormat);
                    x += width;
                }
        }
    }
}