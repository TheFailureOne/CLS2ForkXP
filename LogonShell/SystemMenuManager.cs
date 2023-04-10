using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LogonShell
{
    /// <summary>
    /// Removes and disables the Move and Close items in a form's system menu.
    /// </summary>
    public sealed class SystemMenuManager
    {
        /// <summary>
        /// The possible states of a menu item.
        /// </summary>
        public enum MenuItemState
        {
            /// <summary>
            /// Appears normal and responds to clicks.
            /// </summary>
            Enabled = MF_ENABLED,
            /// <summary>
            /// Appears greyed out and does not respond to clicks.
            /// </summary>
            Greyed = MF_GRAYED,
            /// <summary>
            /// Appears normal but does not respond to clicks.
            /// </summary>
            Disabled = MF_DISABLED,
            /// <summary>
            /// Is not present.
            /// </summary>
            Removed
        }

        /// <summary>
        /// Represents the Move menu item.
        /// </summary>
        private const int SC_MOVE = 0xF010;
        /// <summary>
        /// Represents the Close menu item.
        /// </summary>
        private const int SC_CLOSE = 0xF060;

        /// <summary>
        /// Indicates that a menu item is identified by command ID.
        /// </summary>
        private const int MF_BYCOMMAND = 0x0;

        /// <summary>
        /// Indicates that a menu item is enabled.
        /// </summary>
        private const int MF_ENABLED = 0x0;
        /// <summary>
        /// Indicates that a menu item is greyed out.
        /// </summary>
        private const int MF_GRAYED = 0x1;
        /// <summary>
        /// Indicates that a menu item is disabled.
        /// </summary>
        private const int MF_DISABLED = 0x2;

        /// <summary>
        /// The form whose menu is being managed.
        /// </summary>
        private Form target;
        /// <summary>
        /// The state of the form's Close menu item.
        /// </summary>
        private MenuItemState closeState;
        /// <summary>
        /// The handle of the menu being managed.
        /// </summary>
        private IntPtr menuHandle;

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern int EnableMenuItem(IntPtr hMenu, int wIDEnableItem, int wEnable);

        [DllImport("user32.dll")]
        private static extern bool DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        public SystemMenuManager(Form target, bool movePresent)
            : this(target, movePresent, MenuItemState.Enabled)
        {
        }

        public SystemMenuManager(Form target, MenuItemState closeState)
            : this(target, true, closeState)
        {
        }

        public SystemMenuManager(Form target, bool movePresent, MenuItemState closeState)
        {
            this.target = target;
            this.closeState = closeState;

            this.target.Load += new EventHandler(target_Load);
            this.target.Resize += new EventHandler(target_Resize);
            this.target.KeyDown += new KeyEventHandler(target_KeyDown);

            this.menuHandle = GetSystemMenu(target.Handle, false);

            if (!movePresent)
            {
                // Remove the Move menu item.
                DeleteMenu(this.menuHandle, SC_MOVE, MF_BYCOMMAND);
            }

            if (closeState == MenuItemState.Removed)
            {
                // Remove the Close menu item.
                DeleteMenu(this.menuHandle, SC_CLOSE, MF_BYCOMMAND);
            }
            else
            {
                this.RefreshCloseItem();
            }

            if (closeState != MenuItemState.Enabled)
            {
                // Set the Keypreview to True so that the Alt+F4 key combination can be detected.
                target.KeyPreview = true;
            }
        }

        /// <summary>
        /// Sets the state of the Close menu item if present but not enabled.
        /// </summary>
        private void RefreshCloseItem()
        {
            if (this.closeState == MenuItemState.Disabled || this.closeState == MenuItemState.Greyed)
            {
                EnableMenuItem(this.menuHandle,
                               SC_CLOSE,
                               MF_BYCOMMAND | (int)this.closeState);
            }
        }

        private void target_Load(object sender, EventArgs e)
        {
            // Refresh the initial state of the Close menu item.
            RefreshCloseItem();
        }

        private void target_Resize(object sender, EventArgs e)
        {
            // If present, the Close item will be re-enabled after a resize operation so refresh its state.
            RefreshCloseItem();
        }

        private void target_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 &&
                e.Alt &&
                this.closeState != MenuItemState.Enabled)
            {
                // Suppress the Alt+F4 key combination.
                e.Handled = true;
            }
        }
    }
}
