using ApprendaRectangles.ColorPallet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;

namespace ApprendaRectangles
{
    public partial class RectangleAnalyzer : Form
    {
        public List<PictureBox> Rectangles;
        public AppColor LastColor;
        public bool MouseDragging;
        private Cursor RectangleCursor  = new Cursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("ApprendaRectangles.Images.cursor.cur"));

        public RectangleAnalyzer()
        {
            InitializeComponent();
            GenerateNewRectangles();
        }

        #region Rectangle Generator
        
        /// <summary>
        /// Generates two new Rectangles that can be repositioned and analyzed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateNewRectangles();
        }

        /// <summary>
        /// Method to reset or initialize the grid with new rectangles 
        /// </summary>
        private void GenerateNewRectangles()
        {
            //Clear any existing rectangles from the grid
            pnlGrid.Controls.Clear();

            //Generate two new randomly sized & positioned Rectangles
            Rectangles = new List<PictureBox>();
            Rectangles.Add(GenerateRectangle());
            Rectangles.Add(GenerateRectangle());

            //Hook up the Mouse up/down events to the new rectangles so they can be moved
            foreach (PictureBox rectangle in Rectangles)
            {
                rectangle.MouseDown += Rectangle_MouseDown;
                rectangle.MouseUp += Rectangle_MouseUp;
            }

            //Add the rectangles to the Grid
            pnlGrid.Controls.AddRange(Rectangles.ToArray());
        }

        /// <summary>
        /// Creates a PictureBox that represents one of the rectangles on the grid
        /// </summary>
        /// <returns>A randomly sized PictureBox</returns>
        private PictureBox GenerateRectangle()
        {
            //Build new rectangle (as PictureBox)
            PictureBox NewRectangle = new PictureBox();

            //Generate Random Sizes for the two rectangles (not too big or too small though)
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int height = SnapToGridOfTen(rnd.Next(50, 250));
            int width = SnapToGridOfTen(rnd.Next(50, 250));
            NewRectangle.Size = new Size(width, height);

            //cannot position new rectangle in such a way as it falls off grid (compensate for borders of 3)
            int maxXCoordinate = SnapToGridOfTen(500 - width);
            int maxYCoordinate = SnapToGridOfTen(500 - height);
            NewRectangle.Location = new Point(SnapToGridOfTen(rnd.Next(1, maxXCoordinate)), SnapToGridOfTen(rnd.Next(1, maxYCoordinate)));

            //So we make each rectangle a different color - toggle between our two colors
            AppColor newColor = LastColor == AppColor.RectangleOne ? AppColor.RectangleTwo : AppColor.RectangleOne;
            LastColor = newColor;
            NewRectangle.Name = $"{newColor.ColorName} Rectangle";

            //Draw a new Rectangle with no color except borders
            Bitmap bmpPic = new Bitmap(NewRectangle.Width, NewRectangle.Height);
            using (Graphics gfxPic = Graphics.FromImage(bmpPic))
            {
                ColorMatrix cmxPic = new ColorMatrix();
                using (ImageAttributes iaPic = new ImageAttributes())
                {
                    int borderWidth = 3;
                    Pen pen = new Pen(newColor.Color, borderWidth);
                    gfxPic.DrawLine(pen, 0, 1, NewRectangle.Width, 1); //top horizontal
                    gfxPic.DrawLine(pen, 0, NewRectangle.Height + 1 - borderWidth, NewRectangle.Width, NewRectangle.Height + 1 - borderWidth); //bottom horizontal
                    gfxPic.DrawLine(pen, 1, 1, 1, NewRectangle.Height - borderWidth); //left vertical
                    gfxPic.DrawLine(pen, NewRectangle.Width + 1 - borderWidth, 1, NewRectangle.Width + 1 - borderWidth, NewRectangle.Height - borderWidth); //right vertical
                }
            }
            //Set the PictureBox's Image to the Rectangle we just drew
            NewRectangle.Image = bmpPic;
            //Set the PictureBox's background to transparent
            NewRectangle.BackColor = Color.Transparent;

            return NewRectangle;
        }

        #endregion

        #region Rectangle Mouse Events

        /// <summary>
        /// Handles the mouseup event when user is dragging a rectangle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rectangle_MouseUp(object sender, MouseEventArgs e)
        {

            if (MouseDragging == true)
            {
                Control c = sender as Control;

                //Get the current mouse position relative to the parent grid offset
                var ptc = this.PointToClient(Cursor.Position);
                int X = ptc.X - pnlGrid.Left;
                int Y = ptc.Y - pnlGrid.Top;

                //Make sure user isn't dragging rectangle off the grid, if so, set it as far as they can go (legally)
                if (X + c.Width > 500)
                    X = (500 - c.Width);
                else if (X < 0)
                    X = 0;
                if (Y + c.Height > 500)
                    Y = (500 - c.Height);
                else if (Y < 0)
                    Y = 0;

                //Snap to the Grid (Every 10)
                c.Left = SnapToGridOfTen(X);
                c.Top = SnapToGridOfTen(Y);

                MouseDragging = false;

                c.Show();
            }
        }

        /// <summary>
        /// Handles the mousedown event when user is hovering over a rectangle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rectangle_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDragging = true;

            Control c = sender as Control;

            //bring the selected rectangle to the front
            c.BringToFront();

            //Hide the *real* rectangle so only the cursor image is visible
            c.Hide();

            Cursor.Current = RectangleCursor;

        }

        #endregion

        #region Helpers
         
        /// <summary>
        /// Takes any value and rounds it up or down to the the nearest 10 to 
        /// ensure our points are always on the drawn grid lines
        /// </summary>
        /// <param name="value">The value to adjust (if necessary)</param>
        /// <returns>The adjusted value</returns>
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

        /// <summary>
        /// Handles the pnlGrid OnPaint Event to draw graph lines at intervals of 10 
        /// on the background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #endregion

    }
}
