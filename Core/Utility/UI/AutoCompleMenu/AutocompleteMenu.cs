//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE.
//
//  License: GNU Lesser General Public License (LGPLv3)
//
//  Email: p_torgashov@ukr.net.
//
//  Copyright (C) Pavel Torgashov, 2012-2015. 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;

namespace Sanita.Utility.UI
{
    //[ProvideProperty("AutocompleteMenu", typeof(Control))]
    public class AutocompleteMenu : Component
    {
        //Map
        private static readonly Dictionary<Control, AutocompleteMenu> mListAutoCompleteMenu = new Dictionary<Control, AutocompleteMenu>();
        private static readonly Dictionary<Control, ITextBoxWrapper> mListTargetControlWrapper = new Dictionary<Control, ITextBoxWrapper>();

        //Private
        private ITextBoxWrapper mTargetControlWrapper;
        private Timer timer = new Timer();
        private AutocompleteMenuHost mHost { get; set; }
        private IEnumerable<AutocompleteItem> mListData = new List<AutocompleteItem>();
        private Size mMaximumSize;

        //Public
        public IList<AutocompleteItem> VisibleItems
        {
            get
            {
                return mHost.ListView.VisibleItems;
            }
            private set
            {
                mHost.ListView.VisibleItems = value;
            }
        }
        public int ToolTipDuration
        {
            get { return mHost.ListView.ToolTipDuration; }
            set { mHost.ListView.ToolTipDuration = value; }
        }
        public int SelectedItemIndex
        {
            get
            {
                return mHost.ListView.SelectedItemIndex;
            }
            internal set
            {
                mHost.ListView.SelectedItemIndex = value;
            }
        }
        public ITextBoxWrapper TargetControlWrapper
        {
            get
            {
                return mTargetControlWrapper;
            }
            set
            {
                mTargetControlWrapper = value;
                if (value != null && !mListTargetControlWrapper.ContainsKey(value.TargetControl))
                {
                    mListTargetControlWrapper[value.TargetControl] = value;
                    SetAutocompleteMenu(value.TargetControl);
                }
            }
        }
        public Size MaximumSize
        {
            get
            {
                return mMaximumSize;
            }
            set
            {
                mMaximumSize = value;
                (mHost.ListView as Control).MaximumSize = mMaximumSize;
                (mHost.ListView as Control).Size = mMaximumSize;
                mHost.CalcSize();
            }
        }
        public Font Font
        {
            get
            {
                return (mHost.ListView as Control).Font;
            }
            set
            {
                (mHost.ListView as Control).Font = value;
                //(mHost.ListView as AutocompleteListView).ItemHeight = value.Height + 20;
            }
        }
        public int LeftPadding
        {
            get
            {
                if (mHost.ListView is AutocompleteListView)
                {
                    return (mHost.ListView as AutocompleteListView).LeftPadding;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (mHost.ListView is AutocompleteListView)
                {
                    (mHost.ListView as AutocompleteListView).LeftPadding = value;
                }
            }
        }
        public Colors Colors
        {
            get
            {
                return (mHost.ListView as IAutocompleteListView).Colors;
            }
            set
            {
                (mHost.ListView as IAutocompleteListView).Colors = value;
            }
        }

        public bool ComboboxExMode { get; set; }
        public bool ComboboxMode { get; set; }
        public bool SearchTinhHuyenXa { get; set; }
        public bool MultiDataMode { get; set; }

        //Constructor
        public AutocompleteMenu()
        {
            mHost = new AutocompleteMenuHost(this);
            mHost.ListView.ItemSelected += new EventHandler(ListView_ItemSelected);
            mHost.ListView.ItemHovered += new EventHandler<HoveredEventArgs>(ListView_ItemHovered);
            VisibleItems = new List<AutocompleteItem>();
            Enabled = true;
            AppearInterval = 500;
            timer.Tick += timer_Tick;
            MaximumSize = new Size(400, 250);
            AutoPopup = true;
            LeftPadding = 0;
            Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            SearchPattern = @".";
            MinFragmentLength = 2;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timer.Dispose();
                mHost.Dispose();
            }
            base.Dispose(disposing);
        }

        void ListView_ItemSelected(object sender, EventArgs e)
        {
            OnSelecting();
        }

        void ListView_ItemHovered(object sender, HoveredEventArgs e)
        {
            OnHovered(e);
        }

        public void OnHovered(HoveredEventArgs e)
        {
            if (Hovered != null)
            {
                Hovered(this, e);
            }
        }

        /// <summary>
        /// Called when user selected the control and needed wrapper over it.
        /// You can assign own Wrapper for target control.
        /// </summary>
        [Description("Called when user selected the control and needed wrapper over it. You can assign own Wrapper for target control.")]
        public event EventHandler<WrapperNeededEventArgs> WrapperNeeded;

        protected void OnWrapperNeeded(WrapperNeededEventArgs args)
        {
            if (WrapperNeeded != null)
            {
                WrapperNeeded(this, args);
            }
            if (args.Wrapper == null)
            {
                args.Wrapper = TextBoxWrapper.Create(args.TargetControl, MultiDataMode);
            }
        }

        ITextBoxWrapper CreateWrapper(Control control)
        {
            if (mListTargetControlWrapper.ContainsKey(control))
            {
                return mListTargetControlWrapper[control];
            }

            var args = new WrapperNeededEventArgs(control);
            OnWrapperNeeded(args);
            if (args.Wrapper != null)
            {
                mListTargetControlWrapper[control] = args.Wrapper;
            }

            return args.Wrapper;
        }

        /// <summary>
        /// AutocompleteMenu will popup automatically (when user writes text). Otherwise it will popup only programmatically or by Ctrl-Space.
        /// </summary>
        [DefaultValue(true)]
        [Description("AutocompleteMenu will popup automatically (when user writes text). Otherwise it will popup only programmatically or by Ctrl-Space.")]
        public bool AutoPopup { get; set; }

        /// <summary>
        /// AutocompleteMenu will capture focus when opening.
        /// </summary>
        [DefaultValue(false)]
        [Description("AutocompleteMenu will capture focus when opening.")]
        public bool CaptureFocus { get; set; }

        /// <summary>
        /// Indicates whether the component should draw right-to-left for RTL languages.
        /// </summary>
        [DefaultValue(typeof(RightToLeft), "No")]
        [Description("Indicates whether the component should draw right-to-left for RTL languages.")]
        public RightToLeft RightToLeft
        {
            get { return mHost.RightToLeft; }
            set { mHost.RightToLeft = value; }
        }

        /// <summary>
        /// Image list
        /// </summary>
        public ImageList ImageList
        {
            get { return mHost.ListView.ImageList; }
            set { mHost.ListView.ImageList = value; }
        }

        /// <summary>
        /// Fragment
        /// </summary>
        [Browsable(false)]
        public Range Fragment { get; internal set; }

        /// <summary>
        /// Regex pattern for serach fragment around caret
        /// </summary>
        [Description("Regex pattern for serach fragment around caret")]
        [DefaultValue(@"[\w\.]")]
        public string SearchPattern { get; set; }

        /// <summary>
        /// Minimum fragment length for popup
        /// </summary>
        [Description("Minimum fragment length for popup")]
        [DefaultValue(2)]
        public int MinFragmentLength { get; set; }

        /// <summary>
        /// Allows TAB for select menu item
        /// </summary>
        [Description("Allows TAB for select menu item")]
        [DefaultValue(false)]
        public bool AllowsTabKey { get; set; }

        /// <summary>
        /// Interval of menu appear (ms)
        /// </summary>
        [Description("Interval of menu appear (ms)")]
        [DefaultValue(500)]
        public int AppearInterval { get; set; }

        [DefaultValue(null)]
        public string[] Items
        {
            get
            {
                if (mListData == null)
                    return null;
                var list = new List<string>();
                foreach (AutocompleteItem item in mListData)
                    list.Add(item.ToString());
                return list.ToArray();
            }
            set { SetAutocompleteItems(value); }
        }

        /// <summary>
        /// The control for menu displaying.
        /// Set to null for restore default ListView (AutocompleteListView).
        /// </summary>
        [Browsable(false)]
        public IAutocompleteListView ListView
        {
            get { return mHost.ListView; }
            set
            {
                if (ListView != null)
                {
                    var ctrl = value as Control;
                    value.ImageList = ImageList;
                    ctrl.RightToLeft = RightToLeft;
                    ctrl.Font = Font;
                    ctrl.MaximumSize = MaximumSize;
                }
                mHost.ListView = value;
                mHost.ListView.ItemSelected += new EventHandler(ListView_ItemSelected);
                mHost.ListView.ItemHovered += new EventHandler<HoveredEventArgs>(ListView_ItemHovered);
            }
        }

        [DefaultValue(true)]
        public bool Enabled { get; set; }

        public bool IsOpen
        {
            get
            {
                return mHost.IsOpen;
            }
        }

        /// <summary>
        /// Updates size of the menu
        /// </summary>
        public void Update()
        {
            mHost.CalcSize();
        }

        #region IExtenderProvider Members

        public void SetAutocompleteMenu(bool IsMultiData, Control control)
        {
            this.MultiDataMode = IsMultiData;
            SetAutocompleteMenu(control);
            this.MultiDataMode = false;
        }

        public void SetAutocompleteMenu(Control control)
        {
            //Check map control-menu
            if (mListTargetControlWrapper.ContainsKey(control))
            {
                return;
            }

            //Create new control
            var wrapper = CreateWrapper(control);
            if (wrapper == null)
            {
                return;
            }

            //Hook parent from of control
            if (control.IsHandleCreated)
            {
                SubscribeForm(wrapper);
            }
            else
            {
                control.HandleCreated += (o, e) => SubscribeForm(wrapper);
            }

            //Add map control-menu
            mListAutoCompleteMenu[control] = this;

            //Hook event from control
            wrapper.LostFocus += control_LostFocus;
            wrapper.Scroll += control_Scroll;
            wrapper.KeyDown += control_KeyDown;
            wrapper.MouseDown += control_MouseDown;
        }

        #endregion

        /// <summary>
        /// User selects item
        /// </summary>
        [Description("Occurs when user selects item.")]
        public event EventHandler<SelectingEventArgs> Selecting;

        /// <summary>
        /// It fires after item was inserting
        /// </summary>
        [Description("Occurs after user selected item.")]
        public event EventHandler<SelectedEventArgs> Selected;

        /// <summary>
        /// It fires when item was hovered
        /// </summary>
        [Description("Occurs when user hovered item.")]
        public event EventHandler<HoveredEventArgs> Hovered;

        /// <summary>
        /// Occurs when popup menu is opening
        /// </summary>
        public event EventHandler<CancelEventArgs> Opening;

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if (TargetControlWrapper != null)
            {
                ShowAutocomplete(false, false);
            }
        }

        private Form myForm;

        void SubscribeForm(ITextBoxWrapper wrapper)
        {
            if (wrapper == null)
            {
                return;
            }
            var form = wrapper.TargetControl.FindForm();
            if (form == null)
            {
                return;
            }
            if (myForm != null)
            {
                if (myForm == form)
                {
                    return;
                }
                UnsubscribeForm(wrapper);
            }

            myForm = form;

            form.LocationChanged += new EventHandler(form_LocationChanged);
            form.ResizeBegin += new EventHandler(form_LocationChanged);
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.LostFocus += new EventHandler(form_LocationChanged);
        }

        void UnsubscribeForm(ITextBoxWrapper wrapper)
        {
            if (wrapper == null) return;
            var form = wrapper.TargetControl.FindForm();
            if (form == null) return;

            form.LocationChanged -= new EventHandler(form_LocationChanged);
            form.ResizeBegin -= new EventHandler(form_LocationChanged);
            form.FormClosing -= new FormClosingEventHandler(form_FormClosing);
            form.LostFocus -= new EventHandler(form_LocationChanged);
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Close();
        }

        private void form_LocationChanged(object sender, EventArgs e)
        {
            Close();
        }

        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            if ((ComboboxMode || ComboboxExMode) && sender is Control)
            {
                Control control = sender as Control;
                if (control.Width - e.X < 20)
                {
                    return;
                }
            }

            Close();
        }

        ITextBoxWrapper FindWrapper(Control sender)
        {
            while (sender != null)
            {
                if (mListTargetControlWrapper.ContainsKey(sender))
                {
                    return mListTargetControlWrapper[sender];
                }

                sender = sender.Parent;
            }

            return null;
        }

        private void control_KeyDown(object sender, KeyEventArgs e)
        {
            TargetControlWrapper = FindWrapper(sender as Control);

            bool backspaceORdel = e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete;

            if (mHost.Visible)
            {
                if (ProcessKey((char)e.KeyCode, Control.ModifierKeys))
                {
                    e.SuppressKeyPress = true;
                }
                else
                {
                    if (!backspaceORdel)
                    {
                        ResetTimer(1);
                    }
                    else
                    {
                        ResetTimer();
                    }
                }
                return;
            }

            if (!mHost.Visible)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        {
                            if (ComboboxMode)
                            {
                                ShowAutocomplete(true, true);
                                e.SuppressKeyPress = true;
                                return;
                            }
                            else
                            {
                                timer.Stop();
                                return;
                            }
                        }
                        break;
                    case Keys.Up:
                    case Keys.PageUp:
                    case Keys.PageDown:
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.End:
                    case Keys.Home:
                    case Keys.ControlKey:
                    case Keys.Enter:
                    case Keys.Tab:
                        {
                            timer.Stop();
                            return;
                        }
                }

                if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Space)
                {
                    ShowAutocomplete(true, false);
                    e.SuppressKeyPress = true;
                    return;
                }
            }

            ResetTimer();
        }

        void ResetTimer()
        {
            ResetTimer(-1);
        }

        void ResetTimer(int interval)
        {
            if (interval <= 0)
            {
                timer.Interval = AppearInterval;
            }
            else
            {
                timer.Interval = interval;
            }
            timer.Stop();
            timer.Start();
        }

        private void control_Scroll(object sender, ScrollEventArgs e)
        {
            Close();
        }

        private void control_LostFocus(object sender, EventArgs e)
        {
            if (!mHost.Focused)
            {
                Close();
            }
        }

        public AutocompleteMenu GetAutocompleteMenu(Control control)
        {
            if (mListAutoCompleteMenu.ContainsKey(control))
            {
                return mListAutoCompleteMenu[control];
            }
            else
            {
                return null;
            }
        }

        bool forcedOpened = false;

        internal void ShowAutocomplete(bool forced, bool display_all)
        {
            if (forcedOpened && (ComboboxMode || ComboboxExMode))
            {
                Close();
                return;
            }

            if (forced)
            {
                forcedOpened = true;
            }

            if (TargetControlWrapper != null && TargetControlWrapper.Readonly)
            {
                Close();
                return;
            }

            if (!Enabled)
            {
                Close();
                return;
            }

            if (!forcedOpened && !AutoPopup)
            {
                Close();
                return;
            }

            //build list
            BuildAutocompleteList(forcedOpened, display_all);

            //show popup menu
            if (VisibleItems.Count > 0)
            {
                if (forced && VisibleItems.Count == 1 && mHost.ListView.SelectedItemIndex == 0)
                {
                    //do autocomplete if menu contains only one line and user press CTRL-SPACE
                    OnSelecting();
                    Close();
                }
                else
                {
                    ShowMenu();
                }
            }
            else
            {
                Close();
            }
        }

        private void ShowMenu()
        {
            if (!mHost.Visible)
            {
                var args = new CancelEventArgs();
                OnOpening(args);
                if (!args.Cancel)
                {
                    if (ComboboxMode)
                    {
                        //calc screen point for popup menu
                        Point point = new Point();
                        point.Offset(-3, TargetControlWrapper.TargetControl.Font.Height + 2);

                        //
                        mHost.Show(TargetControlWrapper.TargetControl, point);
                        mHost.IsOpen = true;
                        if (CaptureFocus)
                        {
                            (mHost.ListView as Control).Focus();
                            //ProcessKey((char) Keys.Down, Keys.None);
                        }
                    }
                    else
                    {
                        //calc screen point for popup menu
                        Point point = TargetControlWrapper.TargetControl.Location;
                        //point.Offset(2, TargetControlWrapper.TargetControl.Height + 2);
                        point = TargetControlWrapper.GetPositionFromCharIndex(Fragment.Start);
                        point.Offset(2, TargetControlWrapper.TargetControl.Font.Height + 2);
                        //
                        mHost.Show(TargetControlWrapper.TargetControl, point);
                        mHost.IsOpen = true;
                        if (CaptureFocus)
                        {
                            (mHost.ListView as Control).Focus();
                            //ProcessKey((char) Keys.Down, Keys.None);
                        }
                    }
                }
            }
            else
            {
                (mHost.ListView as Control).Invalidate();
            }
        }

        private void BuildAutocompleteList(bool forced, bool display_all)
        {
            var visibleItems = new List<AutocompleteItem>();

            bool foundSelected = false;
            int selectedIndex = -1;
            //get fragment around caret
            Range fragment = GetFragment(SearchPattern);
            string text = fragment.Text;
            if (ComboboxMode || (mTargetControlWrapper != null && mTargetControlWrapper.IsMultiData))
            {
                text = fragment.TargetWrapper.Text;
            }

            //
            if (mListData != null)
            {
                if (forced || (text.Length >= MinFragmentLength /* && tb.Selection.Start == tb.Selection.End*/))
                {
                    Fragment = fragment;
                    //build popup menu
                    foreach (AutocompleteItem item in mListData)
                    {
                        item.Parent = this;
                        CompareResult res = item.Compare(text);
                        if (display_all || res != CompareResult.Hidden)
                        {
                            visibleItems.Add(item);
                        }
                        if (res == CompareResult.VisibleAndSelected && !foundSelected)
                        {
                            foundSelected = true;
                            selectedIndex = visibleItems.Count - 1;
                        }
                    }
                }
            }

            VisibleItems = visibleItems;

            if (foundSelected)
            {
                SelectedItemIndex = selectedIndex;
            }
            else
            {
                SelectedItemIndex = 0;
            }

            mHost.ListView.HighlightedItemIndex = -1;
            mHost.CalcSize();
        }

        internal void OnOpening(CancelEventArgs args)
        {
            if (Opening != null)
            {
                Opening(this, args);
            }
        }

        private Range GetFragment(string searchPattern)
        {
            var tb = TargetControlWrapper;

            if (tb.SelectionLength > 0) return new Range(tb);

            string text = tb.Text;
            var regex = new Regex(searchPattern);
            var result = new Range(tb);

            int startPos = tb.SelectionStart;
            //go forward
            int i = startPos;
            while (i >= 0 && i < text.Length)
            {
                if (!regex.IsMatch(text[i].ToString()))
                    break;
                i++;
            }
            result.End = i;

            //go backward
            i = startPos;
            while (i > 0 && (i - 1) < text.Length)
            {
                if (!regex.IsMatch(text[i - 1].ToString()))
                    break;
                i--;
            }
            result.Start = i;

            return result;
        }

        public void Close()
        {
            mHost.IsOpen = false;
            mHost.Close();
            forcedOpened = false;
        }

        public void SetAutocompleteItems(IEnumerable<string> items)
        {
            var list = new List<AutocompleteItem>();
            if (items == null)
            {
                mListData = null;
                return;
            }
            foreach (string item in items)
            {
                list.Add(new SubstringAutocompleteItem(item, true));
            }
            SetAutocompleteItems(list);
        }

        public void SetAutocompleteItems(IList<AutoData> items)
        {
            var list = new List<AutocompleteItem>();
            if (items == null)
            {
                mListData = null;
                return;
            }
            foreach (AutoData item in items)
            {
                list.Add(new SubstringAutocompleteItem(item.Text, item.Key, item.Data, true));
            }
            SetAutocompleteItems(list);
        }

        public void SetAutocompleteItems(IEnumerable<AutocompleteItem> items)
        {
            mListData = items;
        }

        public void AddItem(string item)
        {
            AddItem(new AutocompleteItem(item));
        }

        public void AddItem(AutocompleteItem item)
        {
            if (mListData == null)
                mListData = new List<AutocompleteItem>();

            if (mListData is IList)
                (mListData as IList).Add(item);
            else
                throw new Exception("Current autocomplete items does not support adding");
        }

        /// <summary>
        /// Shows popup menu immediately
        /// </summary>
        /// <param name="forced">If True - MinFragmentLength will be ignored</param>
        public void Show(Control control, bool forced)
        {
            SetAutocompleteMenu(control);
            this.TargetControlWrapper = FindWrapper(control);
            ShowAutocomplete(forced, false);
        }

        public void Show(Control control, bool forced, bool display_all)
        {
            SetAutocompleteMenu(control);
            this.TargetControlWrapper = FindWrapper(control);
            ShowAutocomplete(forced, display_all);
        }

        internal virtual void OnSelecting()
        {
            if (SelectedItemIndex < 0 || SelectedItemIndex >= VisibleItems.Count)
                return;

            AutocompleteItem item = VisibleItems[SelectedItemIndex];
            var args = new SelectingEventArgs
                           {
                               Item = item,
                               SelectedIndex = SelectedItemIndex
                           };

            OnSelecting(args);

            if (args.Cancel)
            {
                SelectedItemIndex = args.SelectedIndex;
                (mHost.ListView as Control).Invalidate(true);
                return;
            }

            if (!args.Handled)
            {
                Range fragment = Fragment;
                ApplyAutocomplete(item, fragment);
            }

            Close();
            //
            var args2 = new SelectedEventArgs
                            {
                                Item = item,
                                Control = TargetControlWrapper.TargetControl
                            };
            item.OnSelected(args2);
            OnSelected(args2);
        }

        private void ApplyAutocomplete(AutocompleteItem item, Range fragment)
        {
            string newText = item.GetTextForReplace();
            //replace text of fragment
            if (ComboboxMode)
            {
                fragment.TargetWrapper.Text = newText;
            }
            else if ((mTargetControlWrapper != null && mTargetControlWrapper.IsMultiData))
            {
                String[] list_data = fragment.Text.Split(';');
                if (list_data.Length >= 1)
                {
                    list_data[list_data.Length - 1] = newText;
                    fragment.Text = String.Join(";", list_data);
                }
                else
                {
                    fragment.Text = newText;
                }
            }
            else
            {
                fragment.Text = newText;
            }
            fragment.TargetWrapper.TargetControl.Focus();
        }

        internal void OnSelecting(SelectingEventArgs args)
        {
            if (Selecting != null)
                Selecting(this, args);
        }

        public void OnSelected(SelectedEventArgs args)
        {
            if (Selected != null)
                Selected(this, args);
        }

        public void SelectNext(int shift)
        {
            SelectedItemIndex = Math.Max(0, Math.Min(SelectedItemIndex + shift, VisibleItems.Count - 1));
            //
            (mHost.ListView as Control).Invalidate();
        }

        public bool ProcessKey(char c, Keys keyModifiers)
        {
            var page = mHost.Height / (Font.Height + 4);
            if (keyModifiers == Keys.None)
                switch ((Keys)c)
                {
                    case Keys.Down:
                        SelectNext(+1);
                        return true;
                    case Keys.PageDown:
                        SelectNext(+page);
                        return true;
                    case Keys.Up:
                        SelectNext(-1);
                        return true;
                    case Keys.PageUp:
                        SelectNext(-page);
                        return true;
                    case Keys.Enter:
                    case Keys.Tab:
                        OnSelecting();                        
                        return true;
                    case Keys.Left:
                    case Keys.Right:
                        Close();
                        return false;
                    case Keys.Escape:
                        Close();
                        return true;
                }

            return false;
        }

        /// <summary>
        /// Menu is visible
        /// </summary>
        public bool Visible
        {
            get { return mHost != null && mHost.Visible; }
        }
    }
}