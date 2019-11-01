using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace System.Windows.Forms
{
    public class RTFScrolledBottom : RichTextBox
    {
        public event EventHandler ViewWasScrolled;

        private const int WM_VSCROLL = 0x115;
        private const int WM_MOUSEWHEEL = 0x20A;
        private const int WM_USER = 0x400;
        private const int SB_VERT = 1;
        private const int EM_SETSCROLLPOS = WM_USER + 222;
        private const int EM_GETSCROLLPOS = WM_USER + 221;

        protected virtual void OnScrolledView(EventArgs e)
        {
            if (ViewWasScrolled != null)
                ViewWasScrolled(this, e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            OnScrolledView(EventArgs.Empty);

            base.OnKeyUp(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL)
            {
                OnScrolledView(EventArgs.Empty);
            }

            base.WndProc(ref m);
        }

    }
}