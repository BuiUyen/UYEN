using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;
using DevComponents.Editors.DateTimeAdv;
using Sanita.Utility;
using Sanita.Utility.UI;

namespace System
{
    public static class LanguageExtensions
    {
        public static String Translate(this String data_control)
        {
            if (SanitaLanguage.CurrentLanguage == SanitaLanguage.OldLanguage)
            {
                return data_control;
            }

            return SanitaLanguage.GetLanguage(data_control);
        }

        public static void UpdateUI(this Control data_control)
        {
            //Set default culture
            Thread.CurrentThread.CurrentCulture = SystemInfo.CULTURE_INFO;

#if true
            if (SystemInfo.THEME_STYLE == 0)
            {
                return;
            }

            if (data_control is Control)
            {
                IList<Object> mListControl = GetAll(data_control).ToList();
                mListControl = mListControl.Where(c =>
                         c is FormBase ||                         
                         c is Office2007Form ||
                         c is UserControl ||
                         c is GroupPanel ||
                         c is SuperTabControl ||
                         c is Bar ||
                         c is CircularProgress ||
                         c is PanelEx ||
                         c is Panel ||
                         c is ButtonX ||
                         c is TextBoxX ||
                         c is LabelX ||
                         c is LabelItem ||
                         c is DateTimeInput ||
                         c is IntegerInput ||
                         c is DoubleInput ||
                         c is ButtonItem ||
                         c is ComboBoxEx ||
                         c is Office2007StartButton ||
                         c is ProgressBarX ||
                         c is ObjectListView ||
                         c is SuperTabItem ||
                         c is ProgressSteps ||
                         c is Line ||
                         c is RibbonTabItem
                         ).ToList();
                foreach (Object control in mListControl)
                {
                    if (control is FormBase)
                    {
                        (control as FormBase).BackColor = SystemInfo.m_PanelColor;
                    }
                    else if (control is PanelEx)
                    {
                        if ((control as PanelEx).Style.BackColor1.Color != System.Drawing.Color.Transparent)
                        {
                            (control as PanelEx).Style.BackColor1.Color = SystemInfo.m_PanelColor;
                            (control as PanelEx).Style.BackColor2.Color = SystemInfo.m_PanelColor;
                        }
                    }
                    else if (control is GroupPanel)
                    {
                        (control as GroupPanel).BackColor = SystemInfo.m_PanelColor;
                        (control as GroupPanel).Style.BackColor = SystemInfo.m_PanelColor;
                        (control as GroupPanel).Style.BackColor2 = SystemInfo.m_PanelColor;
                    }
                    else if (control is Bar)
                    {
                        (control as Bar).BackColor = SystemInfo.m_PanelColor;
                    }
                    else if (control is Line)
                    {
                        (control as Line).BackColor = SystemInfo.m_PanelColor;
                    }
                    else if (control is Panel)
                    {
                        (control as Panel).BackColor = SystemInfo.m_PanelColor;
                    }
                    else if (control is LabelX)
                    {
                        (control as LabelX).BackColor = SystemInfo.m_PanelColor;
                    }
                    else if (control is LabelItem)
                    {
                        (control as LabelItem).BackColor = SystemInfo.m_PanelColor;
                    }
                    else if (control is ButtonX)
                    {
                        (control as ButtonX).BackColor = SystemInfo.m_PanelColor;
                    }
                    else if (control is SuperTabControl)
                    {
                        DevComponents.DotNetBar.Rendering.SuperTabLinearGradientColorTable SuperTabLinearGradientColorTable = new DevComponents.DotNetBar.Rendering.SuperTabLinearGradientColorTable();
                        SuperTabLinearGradientColorTable.Colors = new System.Drawing.Color[] { SystemInfo.m_PanelColor };

                        DevComponents.DotNetBar.Rendering.SuperTabColorTable mSuperTabColorTable = new DevComponents.DotNetBar.Rendering.SuperTabColorTable();
                        mSuperTabColorTable.Background = SuperTabLinearGradientColorTable;

                        (control as SuperTabControl).TabStripColor = mSuperTabColorTable;
                    }
                    else if (control is ProgressSteps)
                    {
                        (control as ProgressSteps).BackColor = SystemInfo.m_PanelColor;
                        (control as ProgressSteps).BackgroundStyle.BackColor = SystemInfo.m_PanelColor;
                        (control as ProgressSteps).BackgroundStyle.BackColor2 = SystemInfo.m_PanelColor;
                    }
                    else if (control is CircularProgress)
                    {
                        (control as CircularProgress).BackColor = SystemInfo.m_PanelColor;
                        (control as CircularProgress).BackgroundStyle.BackColor = SystemInfo.m_PanelColor;
                        (control as CircularProgress).BackgroundStyle.BackColor2 = SystemInfo.m_PanelColor;
                    }
                    else if (control is ObjectListView)
                    {
                        (control as ObjectListView).BackColor = SystemInfo.m_PanelColor;
                        (control as ObjectListView).AlternateRowBackColor = SystemInfo.m_ListviewAlterRowBackColor;
                    }
                }
            }
#endif
        }

        static void LanguageExtensions_EnabledChanged(object sender, EventArgs e)
        {
            if (sender is IntegerInput)
            {
                if ((sender as IntegerInput).Enabled)
                {
                    (sender as IntegerInput).BackgroundStyle.BackColor = Color.White;
                    (sender as IntegerInput).BackgroundStyle.BackColor2 = Color.White;
                }
                else
                {
                    (sender as IntegerInput).BackgroundStyle.BackColor = Color.WhiteSmoke;
                    (sender as IntegerInput).BackgroundStyle.BackColor2 = Color.WhiteSmoke;
                }
            }
            else if (sender is DoubleInput)
            {
                if ((sender as DoubleInput).Enabled)
                {
                    (sender as DoubleInput).BackgroundStyle.BackColor = Color.White;
                    (sender as DoubleInput).BackgroundStyle.BackColor2 = Color.White;
                }
                else
                {
                    (sender as DoubleInput).BackgroundStyle.BackColor = Color.WhiteSmoke;
                    (sender as DoubleInput).BackgroundStyle.BackColor2 = Color.WhiteSmoke;
                }
            }
            else if (sender is DateTimeInput)
            {
                if ((sender as DateTimeInput).Enabled)
                {
                    (sender as DateTimeInput).BackgroundStyle.BackColor = Color.White;
                    (sender as DateTimeInput).BackgroundStyle.BackColor2 = Color.White;
                }
                else
                {
                    (sender as DateTimeInput).BackgroundStyle.BackColor = Color.WhiteSmoke;
                    (sender as DateTimeInput).BackgroundStyle.BackColor2 = Color.WhiteSmoke;
                }
            }
            else if (sender is ComboBox)
            {
                if ((sender as ComboBoxEx).Enabled)
                {
                    (sender as ComboBoxEx).BackColor = Color.White;
                }
                else
                {
                    (sender as ComboBoxEx).BackColor = Color.WhiteSmoke;
                }
            }
        }

        static void LanguageExtensions_ReadOnlyChanged(object sender, EventArgs e)
        {
            if (sender is TextBoxX)
            {
                if ((sender as TextBoxX).ReadOnly)
                {
                    (sender as TextBoxX).BackColor = Color.WhiteSmoke;
                }
                else
                {
                    (sender as TextBoxX).BackColor = Color.White;
                }
            }
        }

        public static void Translate(this Object data_control)
        {
            //Updat language
            if (SanitaLanguage.CurrentLanguage == SanitaLanguage.OldLanguage)
            {
                return;
            }

            //Do translate
            if (data_control is Control)
            {
                IList<Object> mListControl = GetAll(data_control).ToList();
                mListControl =
                    (from c in mListControl
                     .Where(c =>
                         c is Office2007Form ||
                         c is Office2007RibbonForm ||
                         c is GroupPanel ||
                         c is Label ||
                         c is TextBox ||
                         c is LabelX ||
                         c is CheckBoxX ||
                         c is ButtonX ||
                         c is RibbonTabItem ||
                         c is TextBoxItem ||
                         c is CheckBoxItem ||
                         c is LabelItem ||
                         c is ButtonItem ||
                         c is SuperTabItem ||
                         c is OLVColumn
                         )
                     select c).ToList();
                foreach (Object control in mListControl)
                {
                    if (control is TextBoxX)
                    {
                        (control as TextBoxX).WatermarkText = SanitaLanguage.GetLanguage((control as TextBoxX).WatermarkText);
                        (control as TextBoxX).Text = SanitaLanguage.GetLanguage((control as TextBoxX).Text);
                    }
                    else if (control is TextBoxItem)
                    {
                        (control as TextBoxItem).WatermarkText = SanitaLanguage.GetLanguage((control as TextBoxItem).WatermarkText);
                        (control as TextBoxItem).Text = SanitaLanguage.GetLanguage((control as TextBoxItem).Text);
                    }
                    else if (control is Control)
                    {
                        (control as Control).Text = SanitaLanguage.GetLanguage((control as Control).Text);
                    }
                    else if (control is BaseItem)
                    {
                        (control as BaseItem).Text = SanitaLanguage.GetLanguage((control as BaseItem).Text);
                    }
                    else if (control is OLVColumn)
                    {
                        (control as OLVColumn).Text = SanitaLanguage.GetLanguage((control as OLVColumn).Text);
                    }
                }
            }
        }

        public static IList<Object> GetAll(Object control)
        {
            IList<Object> ret = new List<Object>();
            ret.Add(control);

            if (control is RibbonControl)
            {
                {
                    var controls = (control as RibbonControl).Items.Cast<BaseItem>();
                    ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
                }
                {
                    var controls = (control as Control).Controls.Cast<Control>();
                    ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
                }
            }
            else if (control is Bar)
            {
                var controls = (control as Bar).Items.Cast<BaseItem>();
                ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
            }
            else if (control is SuperTabControl)
            {
                {
                    var controls = (control as SuperTabControl).Tabs.Cast<BaseItem>();
                    ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
                }
                {
                    var controls = (control as Control).Controls.Cast<Control>();
                    ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
                }
            }
            else if (control is ContextMenuBar)
            {
                var controls = (control as ContextMenuBar).Items.Cast<BaseItem>();
                ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
            }
            else if (control is RibbonBar)
            {
                var controls = (control as RibbonBar).Items.Cast<BaseItem>();
                ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
            }
            else if (control is RibbonTabItem)
            {
                var controls = (control as RibbonTabItem).SubItems.Cast<RibbonTabItem>();
                ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
            }
            else if (control is ButtonX)
            {
                var controls = (control as ButtonX).SubItems.Cast<BaseItem>();
                ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
            }
            else if (control is BaseItem)
            {
                var controls = (control as BaseItem).SubItems.Cast<BaseItem>();
                ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
            }
            else if (control is ObjectListView)
            {
                var controls = (control as ObjectListView).Columns.Cast<OLVColumn>();
                ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
            }
            else if (control is DataGridViewX)
            {
                var controls = (control as DataGridViewX).Columns.Cast<DataGridViewColumn>();
                ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
            }
            else if (control is Control)
            {
                var controls = (control as Control).Controls.Cast<Control>();
                ret = ret.Concat(controls.SelectMany(ctrl => GetAll(ctrl))).ToList();
            }

            return ret;
        }
    }

    public sealed class SanitaLanguage
    {
        //Constant
        public const string VietNam = "VI";
        public const string English = "EN";
        public const string Japanese = "JA";
        public const string Chinese = "ZH";

        //Public
        public static string OldLanguage = "VI";
        public static string CurrentLanguage = "VI";

        //Private
        private static IList<SanitaLanguageData> mListSanitaLanguageData = new List<SanitaLanguageData>();
        private static IList<Object> mListControlLanguage = new List<Object>();

        public static void Init(IList<SanitaLanguageData> listlanguage)
        {
            mListSanitaLanguageData = listlanguage;
        }

        public static void ChangeLanguage(String New_Language)
        {
            if (New_Language != CurrentLanguage)
            {
                OldLanguage = CurrentLanguage;
                CurrentLanguage = New_Language;
            }
        }

        public static String GetLanguage(String VietNam_Text)
        {
            if (OldLanguage == CurrentLanguage)
            {
                return VietNam_Text;
            }
            if (String.IsNullOrEmpty(VietNam_Text))
            {
                return VietNam_Text;
            }

            try
            {
                SanitaLanguageData data = mListSanitaLanguageData.FirstOrDefault(p => p.Value[OldLanguage].Trim() == VietNam_Text.Trim());
                if (data != null)
                {
                    return data.Value[CurrentLanguage];
                }
            }
            catch { }

            //Default
            return VietNam_Text;
        }

        public class SanitaLanguageData
        {
            public MultilingualString Value { get; set; }

            public SanitaLanguageData()
            {
                Value = new MultilingualString();
            }
        }
    }

    public class MediboxLanguage
    {
        public String Code { get; set; }
        public String Name { get; set; }

        public MediboxLanguage(String code, String name)
        {
            Code = code;
            Name = name;
        }

        public static MediboxLanguage GetDefault(Object list, String _code)
        {
            if (list == null)
            {
                return null;
            }

            if (!(list is IList<MediboxLanguage>))
            {
                return null;
            }

            IList<MediboxLanguage> list_data = list as IList<MediboxLanguage>;
            return list_data.FirstOrDefault(p => p.Code == _code);
        }

        public static MediboxLanguage GetDefault(String _code)
        {
            MediboxLanguage data = GetDefaultList().FirstOrDefault(p => p.Code == _code);
            data = data ?? new MediboxLanguage("", "");
            return data;
        }

        public static MediboxLanguage GetDefault_Name(String _name)
        {
            MediboxLanguage data = GetDefaultList().FirstOrDefault(p => p.Name == _name);
            data = data ?? new MediboxLanguage("", "");
            return data;
        }

        public static String GetCode(Object data)
        {
            if (data == null)
            {
                return "";
            }
            if (!(data is MediboxLanguage))
            {
                return "";
            }
            return (data as MediboxLanguage).Code;
        }

        public static IList<MediboxLanguage> GetDefaultList()
        {
            IList<MediboxLanguage> list_data = new List<MediboxLanguage>();

            list_data.Add(new MediboxLanguage("EN", "English"));
            list_data.Add(new MediboxLanguage("MS", "Malay"));
            list_data.Add(new MediboxLanguage("ZH", "Chinese"));
            list_data.Add(new MediboxLanguage("VI", "Vietnamese"));
            list_data.Add(new MediboxLanguage("JA", "Japanese"));
            list_data.Add(new MediboxLanguage("KM", "Cambodian"));
            list_data.Add(new MediboxLanguage("ID", "Indonesia"));

            return list_data;
        }
    }
}
