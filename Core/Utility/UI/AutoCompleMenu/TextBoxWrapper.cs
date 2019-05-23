using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;

namespace Sanita.Utility.UI
{
    /// <summary>
    /// Wrapper over the control like TextBox.
    /// </summary>
    public class TextBoxWrapper : ITextBoxWrapper
    {
        private Control target;
        private PropertyInfo selectionStart;
        private PropertyInfo selectionLength;
        private PropertyInfo selectedText;
        private PropertyInfo readonlyProperty;
        private MethodInfo getPositionFromCharIndex;
        private event ScrollEventHandler RTBScroll;
        public bool IsMultiData { get; set; }

        private TextBoxWrapper(Control targetControl)
        {
            this.target = targetControl;
            Init();
        }

        protected virtual void Init()
        {
            var t = target.GetType();
            selectedText = t.GetProperty("SelectedText");
            selectionLength = t.GetProperty("SelectionLength");
            selectionStart = t.GetProperty("SelectionStart");
            readonlyProperty = t.GetProperty("ReadOnly");
            getPositionFromCharIndex = t.GetMethod("GetPositionFromCharIndex") ?? t.GetMethod("PositionToPoint");

            if (target is RichTextBox)
            {
                (target as RichTextBox).VScroll += new EventHandler(TextBoxWrapper_VScroll);
            }
        }

        void TextBoxWrapper_VScroll(object sender, EventArgs e)
        {
            if (RTBScroll != null)
            {
                RTBScroll(sender, new ScrollEventArgs(ScrollEventType.EndScroll, 0, 1));
            }
        }

        public static TextBoxWrapper Create(Control targetControl, bool muti_data)
        {
            var result = new TextBoxWrapper(targetControl);
            result.IsMultiData = muti_data;
            if (targetControl is TokenEditor)
            {
                return result;
            }
            else
            {
                if (result.selectedText == null || result.selectionLength == null || result.selectionStart == null || result.getPositionFromCharIndex == null)
                {
                    return null;
                }
            }

            return result;
        }

        public virtual string Text
        {
            get
            {
                if (IsMultiData && target is TextBoxX)
                {
                    TextBoxX textbox = target as TextBoxX;
                    String[] list_data = textbox.Text.Split(';');
                    if (list_data.Length >= 1)
                    {
                        return list_data[list_data.Length - 1];
                    }
                    else
                    {
                        return "";
                    }
                }
                else if (target is TokenEditor)
                {
                    return "";
                }

                return target.Text;
            }
            set
            {
                if (IsMultiData && target is TextBoxX)
                {
                    TextBoxX textbox = target as TextBoxX;
                    String[] list_data = textbox.Text.Split(';');
                    if (list_data.Length >= 1)
                    {
                        list_data[list_data.Length - 1] = value;
                    }
                    else
                    {
                        target.Text = value;
                    }
                }
                else
                {
                    target.Text = value;
                }
            }
        }

        public virtual string SelectedText
        {
            get
            {
                return (string)selectedText.GetValue(target, null);
            }
            set
            {                
                {
                    if (selectedText != null)
                    {
                        selectedText.SetValue(target, value, null);
                    }
                }
            }
        }

        public virtual int SelectionLength
        {
            get
            {
                if (target is TokenEditor)
                {
                    return 0;
                }

                return (int)selectionLength.GetValue(target, null);
            }
            set
            {
                if (selectionLength != null)
                {
                    selectionLength.SetValue(target, value, null);
                }
            }
        }

        public virtual int SelectionStart
        {
            get
            {
                if (IsMultiData && target is TextBoxX)
                {
                    TextBoxX textbox = target as TextBoxX;
                    int start = textbox.Text.LastIndexOf(";");
                    if (start < 0)
                    {
                        start = 0;
                    }
                    else
                    {
                        start = start + 1;
                    }
                    return start;
                }

                if (selectionStart != null)
                {
                    return (int)selectionStart.GetValue(target, null);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (selectionStart != null)
                {
                    selectionStart.SetValue(target, value, null);
                }
            }
        }

        public virtual Point GetPositionFromCharIndex(int pos)
        {
            if (target is TokenEditor)
            {
                return new Point(100, 100);
            }
            else
            {
                return (Point)getPositionFromCharIndex.Invoke(target, new object[] { pos });
            }
        }


        public virtual Form FindForm()
        {
            return target.FindForm();
        }

        public virtual event EventHandler LostFocus
        {
            add { target.LostFocus += value; }
            remove { target.LostFocus -= value; }
        }

        public virtual event ScrollEventHandler Scroll
        {
            add
            {
                if (target is RichTextBox)
                    RTBScroll += value;
                else
                    if (target is ScrollableControl) (target as ScrollableControl).Scroll += value;

            }
            remove
            {
                if (target is RichTextBox)
                    RTBScroll -= value;
                else
                    if (target is ScrollableControl) (target as ScrollableControl).Scroll -= value;
            }
        }

        public virtual event KeyEventHandler KeyDown
        {
            add { target.KeyDown += value; }
            remove { target.KeyDown -= value; }
        }

        public virtual event MouseEventHandler MouseDown
        {
            add { target.MouseDown += value; }
            remove { target.MouseDown -= value; }
        }

        public virtual Control TargetControl
        {
            get { return target; }
        }


        public bool Readonly
        {
            get
            {
                if (readonlyProperty == null)
                {
                    return false;
                }
                return (bool)readonlyProperty.GetValue(target, null);
            }
        }
    }
}
