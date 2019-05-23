using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace System
{
    /*  FlexibleMessageBox – A flexible replacement for the .NET MessageBox
     * 
     *  Author:         Jörg Reichert (public@jreichert.de)
     *  Contributors:   Thanks to: David Hall, Roink
     *  Version:        1.3
     *  Published at:   http://www.codeproject.com/Articles/601900/FlexibleMessageBox
     *  
     ************************************************************************************************************
     * Features:
     *  - It can be simply used instead of MessageBox since all important static "Show"-Functions are supported
     *  - It is small, only one source file, which could be added easily to each solution 
     *  - It can be resized and the content is correctly word-wrapped
     *  - It tries to auto-size the width to show the longest text row
     *  - It never exceeds the current desktop working area
     *  - It displays a vertical scrollbar when needed
     *  - It does support hyperlinks in text
     * 
     *  Because the interface is identical to MessageBox, you can add this single source file to your project 
     *  and use the FlexibleMessageBox almost everywhere you use a standard MessageBox. 
     *  The goal was NOT to produce as many features as possible but to provide a simple replacement to fit my 
     *  own needs. Feel free to add additional features on your own, but please left my credits in this class.
     * 
     ************************************************************************************************************
     * Usage examples:
     * 
     *  FlexibleMessageBox.Show("Just a text");
     * 
     *  FlexibleMessageBox.Show("A text", 
     *                          "A caption"); 
     *  
     *  FlexibleMessageBox.Show("Some text with a link: www.google.com", 
     *                          "Some caption",
     *                          MessageBoxButtons.AbortRetryIgnore, 
     *                          MessageBoxIcon.Information,
     *                          MessageBoxDefaultButton.Button2);
     *  
     *  var dialogResult = FlexibleMessageBox.Show("Do you know the answer to life the universe and everything?", 
     *                                             "One short question",
     *                                             MessageBoxButtons.YesNo);     
     * 
     ************************************************************************************************************
     *  THE SOFTWARE IS PROVIDED BY THE AUTHOR "AS IS", WITHOUT WARRANTY
     *  OF ANY KIND, EXPRESS OR IMPLIED. IN NO EVENT SHALL THE AUTHOR BE
     *  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY ARISING FROM,
     *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OF THIS
     *  SOFTWARE.
     *  
     ************************************************************************************************************
     * History:
     *  Version 1.3 - 19.Dezember 2014
     *  - Added refactoring function GetButtonText()
     *  - Used CurrentUICulture instead of InstalledUICulture
     *  - Added more button localizations. Supported languages are now: ENGLISH, GERMAN, SPANISH, ITALIAN
     *  - Added standard MessageBox handling for "copy to clipboard" with <Ctrl> + <C> and <Ctrl> + <Insert>
     *  - Tab handling is now corrected (only tabbing over the visible buttons)
     *  - Added standard MessageBox handling for ALT-Keyboard shortcuts
     *  - SetDialogSizes: Refactored completely: Corrected sizing and added caption driven sizing
     * 
     *  Version 1.2 - 10.August 2013
     *   - Do not ShowInTaskbar anymore (original MessageBox is also hidden in taskbar)
     *   - Added handling for Escape-Button
     *   - Adapted top right close button (red X) to behave like MessageBox (but hidden instead of deactivated)
     * 
     *  Version 1.1 - 14.June 2013
     *   - Some Refactoring
     *   - Added internal form class
     *   - Added missing code comments, etc.
     *  
     *  Version 1.0 - 15.April 2013
     *   - Initial Version
    */
    public class SanitaMessageBox
    {
        #region Public statics

        /// <summary>
        /// Defines the maximum width for all FlexibleMessageBox instances in percent of the working area.
        /// 
        /// Allowed values are 0.2 - 1.0 where: 
        /// 0.2 means:  The FlexibleMessageBox can be at most half as wide as the working area.
        /// 1.0 means:  The FlexibleMessageBox can be as wide as the working area.
        /// 
        /// Default is: 70% of the working area width.
        /// </summary>
        public static double MAX_WIDTH_FACTOR = 0.7;

        /// <summary>
        /// Defines the maximum height for all FlexibleMessageBox instances in percent of the working area.
        /// 
        /// Allowed values are 0.2 - 1.0 where: 
        /// 0.2 means:  The FlexibleMessageBox can be at most half as high as the working area.
        /// 1.0 means:  The FlexibleMessageBox can be as high as the working area.
        /// 
        /// Default is: 90% of the working area height.
        /// </summary>
        public static double MAX_HEIGHT_FACTOR = 0.9;

        /// <summary>
        /// Defines the font for all FlexibleMessageBox instances.
        /// 
        /// Default is: SystemFonts.MessageBoxFont
        /// </summary>
        //public static Font FONT = SystemFonts.MessageBoxFont;
        public static Font FONT = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        #endregion

        #region Public show functions

        /// <summary>
        /// Shows the specified message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResult Show(string text)
        {
            return FlexibleMessageBoxForm.Show(null, text, "Message", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Shows the specified message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResult Show(string text, string caption)
        {
            return FlexibleMessageBoxForm.Show(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Shows the specified message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return FlexibleMessageBoxForm.Show(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Shows the specified message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return FlexibleMessageBoxForm.Show(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
        }

        ///// <summary>
        ///// Shows the specified message box.
        ///// </summary>
        ///// <param name="text">The text.</param>
        ///// <param name="caption">The caption.</param>
        ///// <param name="buttons">The buttons.</param>
        ///// <param name="icon">The icon.</param>
        ///// <param name="defaultButton">The default button.</param>
        ///// <returns>The dialog result.</returns>
        //public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        //{
        //    return FlexibleMessageBoxForm.Show(null, text, caption, buttons, icon, defaultButton);
        //}

        #endregion

        #region Internal form class

        /// <summary>
        /// The form to show the customized message box.
        /// It is defined as an internal class to keep the public interface of the FlexibleMessageBox clean.
        /// </summary>
        

        #endregion
    }
}
