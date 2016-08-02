using ApprendaRectangles.ColorPallet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApprendaRectangles
{
    public partial class RectangleAnalyzer : Form
    {
        public List<PictureBox> Rectangles;
        public Color LastColor;
        public bool MouseDragging;
        private Size DragOffset;

        public RectangleAnalyzer()
        {
            InitializeComponent();
            LastColor = Color.Red;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Rectangles = new List<PictureBox>();
            pnlGrid.Controls.Clear();

            Rectangles.Add(GenerateRectangle());
            Rectangles.Add(GenerateRectangle());

            //Hook up the Mouse up/down events to the new rectangles so they can be moved
            foreach(PictureBox rectangle in Rectangles)
            {
                rectangle.MouseDown += Rectangle_MouseDown;
                rectangle.MouseUp += Rectangle_MouseUp;
                rectangle.MouseEnter += Rectangle_MouseEnter;
            }

            pnlGrid.Controls.AddRange(Rectangles.ToArray());

        }

        private void Rectangle_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Control c = sender as Control;
            //    c.Top
            txtStats.Clear();
            Point p = c.PointToClient(Cursor.Position);
            txtStats.AppendText($"Rectangle Coordinates (x,y): ({p.X}, {p.Y})");
          //  Cursor.Position = pnlGrid.PointToClient(Cursor.Position);
        }

        private void Rectangle_MouseUp(object sender, MouseEventArgs e)
        {

            if (MouseDragging == true)
            {
                Control c = sender as Control;

                Point newLocationOffset = e.Location - DragOffset;

                //int X = e.X + c.Left - dragOffsetX;
                //int Y = e.Y + c.Top - dragOffsetY;
                ////Make sure user isn't dragging rectangle off the grid, if so, set it as far as they can go (legally)
                //if (X + c.Width > 500)
                //    X = (500 - c.Width);
                //else if (X < 0)
                //    X = 0;
                //if (Y + c.Height > 500)
                //    Y = (500 - c.Height);
                //else if (Y < 0)
                //    Y = 0;

                c.Left += newLocationOffset.X;
                c.Top += newLocationOffset.Y;

                //Snap to the Grid (Every 10)
                c.Left = SnapToGridOfTen(c.Left);
                c.Top = SnapToGridOfTen(c.Top);

                MouseDragging = false;
                c.Show();
                txtStats.Clear();
                txtStats.AppendText($"Rectangle Coordinates (x,y): ({c.Left}, {c.Top})");
            }
        }


        private void Rectangle_MouseDown(object sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            c.BringToFront();
            MouseDragging = true;
            DragOffset = new Size(e.Location);

            Bitmap bmp = new Bitmap(c.Width, c.Height);
            c.BackColor = Color.White;
            c.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
            c.BackColor = Color.Transparent;
            c.Hide();
            Cursor cur = new Cursor(bmp.GetHicon());
            Cursor.Current = cur;

        }

        private PictureBox GenerateRectangle()
        {
            //Build new rectangle (as PictureBox)
            PictureBox NewRectangle = new PictureBox();

            //Generate Random Sizes for the two rectangles (not too big or too small though)
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int height = SnapToGridOfTen(rnd.Next(50, 400));
            int width = SnapToGridOfTen(rnd.Next(50, 400));

            //cannot position new rectangle in such a way as it falls off grid (compensate for borders of 2)
            int maxXCoordinate = SnapToGridOfTen(500 - width);
            int maxYCoordinate = SnapToGridOfTen(500 - height);
            NewRectangle.Location = new Point(rnd.Next(1, maxXCoordinate), rnd.Next(1, maxYCoordinate));

            NewRectangle.BackColor = Color.Transparent;
            NewRectangle.Size = new Size(height, width);

            //So we make each rectangle a different color.
            Color newColor = LastColor == AppColor.RectangleOne.Color ? AppColor.RectangleTwo.Color : AppColor.RectangleOne.Color;
            LastColor = newColor;
            Bitmap bmpPic = new Bitmap(NewRectangle.Width, NewRectangle.Height);
            using (Graphics gfxPic = Graphics.FromImage(bmpPic))
            {
                ColorMatrix cmxPic = new ColorMatrix();
                using (ImageAttributes iaPic = new ImageAttributes())
                {
                    int borderWidth = 3;
                    Pen pen = new Pen(newColor,borderWidth);
                    gfxPic.DrawLine(pen,0,1, NewRectangle.Width,1); //top horizontal
                    gfxPic.DrawLine(pen,0, NewRectangle.Height+1-borderWidth, NewRectangle.Width, NewRectangle.Height+1-borderWidth); //bottom horizontal
                    gfxPic.DrawLine(pen, 1, 1, 1, NewRectangle.Height - borderWidth); //left vertical
                    gfxPic.DrawLine(pen, NewRectangle.Width+1 - borderWidth, 1, NewRectangle.Width+1 - borderWidth, NewRectangle.Height - borderWidth); //right vertical
                }
            }
            NewRectangle.BackColor = Color.Transparent;
            NewRectangle.Image = bmpPic;

          
         //   NewRectangle.Refresh();
            return NewRectangle;
        }

        private int SnapToGridOfTen(int value)
        {
            if (value % 10 != 0)
            {
                int adjustment = 0;
                if (value % 10 >= 5)
                    adjustment = 10 - (value % 10);
                else
                    adjustment = (value % 10) * -1;
                value += adjustment;
            }
            return value;
        }

        private void DrawGridOnPanel(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.FromArgb(50, 0, 0, 0), 1);

            for(int x = 0; x <= 500; x+=10)
            {
                e.Graphics.DrawLine(p, x, 0, x, 500);
            }
            for (int y = 0; y <= 500; y+=10)
            {
                e.Graphics.DrawLine(p, 0, y, 500, y);
            }

        }
    }
}
