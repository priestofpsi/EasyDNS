using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;

namespace theDiary.EasyDNS.Windows
{
    public interface IModalValue
    {
        dynamic Value { get; set; }
    }

    public class BorderlessForm
        : Form
    {
        protected BorderlessForm()
        {
            this.Movable = true;
            this.BorderColor = Color.Black;
            this.BorderSize = 2;
            this.FormBorderStyle = FormBorderStyle.None; // no borders
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, BorderPadding); } }
        private Rectangle Left { get { return new Rectangle(0, 0, BorderPadding, this.ClientSize.Height); } }
        private Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - BorderPadding, this.ClientSize.Width, BorderPadding); } }
        private Rectangle Right { get { return new Rectangle(this.ClientSize.Width - BorderPadding, 0, BorderPadding, this.ClientSize.Height); } }

        private Rectangle TopLeft { get { return new Rectangle(0, 0, BorderPadding, BorderPadding); } }
        private Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - BorderPadding, 0, BorderPadding, BorderPadding); } }
        private Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - BorderPadding, BorderPadding, BorderPadding); } }
        private Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - BorderPadding, this.ClientSize.Height - BorderPadding, BorderPadding, BorderPadding); } }

        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int HT_CAPTION = 0x2;
        private const int HTLEFT = 10, HTRIGHT = 11, HTTOP = 12, HTTOPLEFT = 13, HTTOPRIGHT = 14, HTBOTTOM = 15, HTBOTTOMLEFT = 16, HTBOTTOMRIGHT = 17;
        private const int BorderPadding = 10; // you can rename this variable if you like

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern bool ReleaseCapture();

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left || !this.Movable)
                return;
            Rectangle rct = DisplayRectangle;
            if (!rct.Contains(e.Location))
                return;
            this.Cursor = Cursors.Cross;
            BorderlessForm.ReleaseCapture();
            BorderlessForm.SendMessage(this.Handle, BorderlessForm.WM_NCLBUTTONDOWN, BorderlessForm.HT_CAPTION, 0);
            this.Cursor = Cursors.Default;
        }

        protected override void OnPaint(PaintEventArgs e) // you can safely omit this method if you want
        {
            var fill = new SolidBrush(this.BackColor);
            e.Graphics.FillRectangle(fill, this.Top);
            e.Graphics.FillRectangle(fill, this.Left);
            e.Graphics.FillRectangle(fill, this.Right);
            e.Graphics.FillRectangle(fill, this.Bottom);
            if (this.BorderSize <= 0)
                return;

            var modifer = (this.BorderSize % 2 == 0) ? 0 : 1;
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(this.BorderColor), this.BorderSize), 0, 0, this.Width- modifer, this.Height- modifer);
        }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg != 0x84)
                return;

            var cursor = this.PointToClient(Cursor.Position);

            if (this.TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
            else if (this.TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
            else if (this.BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
            else if (this.BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

            else if (this.Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
            else if (this.Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
            else if (this.Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
            else if (this.Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;

        }

        [Category("Appearance")]
        [DefaultValue(typeof(System.Drawing.Color), "Black")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color BorderColor { get; set; }

        [Category("Appearance")]
        [DefaultValue(2)]
        public int BorderSize { get; set; }

        [Category("Behaviour")]
        [DefaultValue(true)]
        [Description("Specifies whether the form can be moved or not.")]
        public bool Movable { get; set; }
    }
}
