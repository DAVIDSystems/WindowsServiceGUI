using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace System.Windows.Forms
{
    public class RTFScrolledBottom : RichTextBox
    {
        [DllImport("user32.dll")]
        static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);

        [Serializable, StructLayout(LayoutKind.Sequential)]
        struct SCROLLINFO
        {
            public int cbSize; // (uint) int is because of Marshal.SizeOf
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }
        public enum ScrollInfoMask : uint
        {
            SIF_RANGE = 0x1,
            SIF_PAGE = 0x2,
            SIF_POS = 0x4,
            SIF_DISABLENOSCROLL = 0x8,
            SIF_TRACKPOS = 0x10,
            SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS),
        }

        public event EventHandler ViewWasScrolled;
        public delegate void ScrollPosition(int pos, int max);
        public event ScrollPosition _scrollEvent;
        private int _lastScrollPosition;
        private SCROLLINFO _si;

        private const int WM_VSCROLL = 0x115;
        private const int WM_MOUSEWHEEL = 0x20A;
        private const int WM_USER = 0x400;
        private const int SB_VERT = 1;
        private const int EM_SETSCROLLPOS = WM_USER + 222;
        private const int EM_GETSCROLLPOS = WM_USER + 221;

        public RTFScrolledBottom() : base()
        {
            _si = new SCROLLINFO();
            _si.cbSize = Marshal.SizeOf(_si);
            _si.fMask = (int)ScrollInfoMask.SIF_ALL;
        }

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
                ushort command = (ushort) (m.WParam.ToInt32() & 0xFFFF);
                if (command == 8)
                {
                    GetScrollInfo(m.HWnd, 1, ref _si);
                    int max = _si.nMax - (int) _si.nPage;
                    _scrollEvent?.Invoke(_lastScrollPosition, max);
                }
                else
                {
                    OnScrolledView(EventArgs.Empty);
                }
                if (command == 4)
                {
                    _lastScrollPosition = (m.WParam).ToInt32() >> 16;
                }
            }

            base.WndProc(ref m);
        }

    }
}