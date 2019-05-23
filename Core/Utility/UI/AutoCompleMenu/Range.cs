using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Sanita.Utility.UI
{
    public class Range
    {
        public ITextBoxWrapper TargetWrapper { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

        public Range(ITextBoxWrapper targetWrapper)
        {
            this.TargetWrapper = targetWrapper;
        }

        public string Text
        {
            get
            {
                var text = TargetWrapper.Text;
                {
                    if (string.IsNullOrEmpty(text))
                        return "";
                    if (Start >= text.Length)
                        return "";
                    if (End > text.Length)
                        return "";

                    return TargetWrapper.Text.Substring(Start, End - Start);
                }
            }
            set
            {
                TextBoxWrapper mTextBoxWrapper = TargetWrapper as TextBoxWrapper;
                if (mTextBoxWrapper.IsMultiData)
                {
                    TargetWrapper.SelectionStart = Start;
                    TargetWrapper.SelectionLength = mTextBoxWrapper.Text.Length;
                    TargetWrapper.SelectedText = value + ";";
                }
                else
                {
                    TargetWrapper.SelectionStart = Start;
                    TargetWrapper.SelectionLength = End - Start;
                    TargetWrapper.SelectedText = value;
                }
            }
        }
    }
}
