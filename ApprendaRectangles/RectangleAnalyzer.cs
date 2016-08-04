using ApprendaRectangles.ColorPallet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;

namespace ApprendaRectangles
{
    public partial class RectangleAnalyzer : Form
    {
        public List<TransparencyEnabledPicBox> Rectangles;
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
            Rectangles = new List<TransparencyEnabledPicBox>();
            Rectangles.Add(GenerateRectangle());
            Rectangles.Add(GenerateRectangle());

            //Hook up the Mouse up/down events to the new rectangles so they can be moved
            foreach (TransparencyEnabledPicBox rectangle in Rectangles)
            {
                rectangle.MouseDown += Rectangle_MouseDown;
                rectangle.MouseUp += Rectangle_MouseUp;
            }

            //Add the rectangles to the Grid
            pnlGrid.Controls.AddRange(Rectangles.ToArray());

            AnalyzeGrid();
        }

        /// <summary>
        /// Creates a TransparencyEnabledPicBox that represents one of the rectangles on the grid
        /// </summary>
        /// <returns>A randomly sized TransparencyEnabledPicBox</returns>
        private TransparencyEnabledPicBox GenerateRectangle()
        {
            //Build new rectangle (as TransparencyEnabledPicBox)
            TransparencyEnabledPicBox NewRectangle = new TransparencyEnabledPicBox();

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
            //Set the TransparencyEnabledPicBox's Image to the Rectangle we just drew
            NewRectangle.Image = bmpPic;
            //Set the TransparencyEnabledPicBox's background to transparent
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
                AnalyzeGrid();
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

        #region Analyzation Methods

        private void AnalyzeGrid()
        {
            txtStats.Clear();
            if (Rectangles.Count != 2)
            {
                txtStats.AppendText("Cannot find two rectangles on grid to analyze. Generate some new ones and try again.");
                return;
            }
                
            var rectangleOne = RectangleView.GatherPoints(Rectangles[0]);
            var rectangleTwo = RectangleView.GatherPoints(Rectangles[1]);

            var results = rectangleOne.GetRelationship(rectangleTwo);
            switch(results.Item1)
            {
                case RectangleStatus.Adjacent:
                    txtStats.AppendText($"Adjacent{Environment.NewLine}Rectangles are adjacent on the following coordinates (X,Y): {string.Join(",",results.Item3.Select(p => $"({p.X},{p.Y})"))}");
                    break;
                case RectangleStatus.Intersecting:
                    txtStats.AppendText($"Intersecting{Environment.NewLine}Rectangles are intersected on the following coordinates (X,Y): {string.Join(",", results.Item3.Select(p => $"({p.X},{p.Y})"))}");
                    break;
                case RectangleStatus.Contained:
                    txtStats.AppendText($"Containment{Environment.NewLine}{results.Item2}");
                    break;
                case RectangleStatus.NoRelationship:
                default:
                    txtStats.AppendText($"No Relationship{Environment.NewLine}There is no relationship between these shapes.");
                    break;
            }
            txtStats.SelectionStart = 0;
            txtStats.ScrollToCaret();
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

        protected override void OnHelpButtonClicked(CancelEventArgs e)
        {
            base.OnHelpButtonClicked(e);
            Help form = new Help();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
            e.Cancel = true;
        }

        /// <summary>
        /// Class for deconstructing a <see cref="TransparencyEnabledPicBox"/> into it's constituent <see cref="Point"/>s along it's four lines
        /// </summary>
        private class RectangleView
        {
            private TransparencyEnabledPicBox _basePicBox;
            private List<Point> _left;
            private List<Point> _right;
            private List<Point> _top;
            private List<Point> _bottom;
            public string Name { get { return _basePicBox.Name; } }
            public List<Point> Left { get { return _left; } }
            public List<Point> Right { get { return _right; } }
            public List<Point> Top { get { return _top; } }
            public List<Point> Bottom { get { return _bottom; } }

            /// <summary>
            /// Ctor - private to prevent misconfigured <see cref="RectangleView"/> objects.
            /// Use Static factory method
            /// </summary>
            /// <param name="basePicBox">The <see cref="TransparencyEnabledPicBox"/> that we're deconstructing</param>
            /// <param name="leftVertical">The Left vertical line on a given rectangle</param>
            /// <param name="rightVertical">The Right vertical line on a given rectangle</param>
            /// <param name="topHorizontal">The Top horizontal line on a given rectangle</param>
            /// <param name="bottomHorizontal">The Bottom horizontal line on a given rectangle</param>
            private RectangleView (TransparencyEnabledPicBox basePicBox, List<Point> leftVertical, List<Point> rightVertical, List<Point> topHorizontal, List<Point> bottomHorizontal)
            {
                _left = leftVertical;
                _right = rightVertical;
                _top = topHorizontal;
                _bottom = bottomHorizontal;
                _basePicBox = basePicBox;
            }

            /// <summary>
            /// Builds a <see cref="RectangleView"/> object for the given <see cref="TransparencyEnabledPicBox"/> and it's coordinates/size
            /// </summary>
            /// <param name="rectangle">A <see cref="TransparencyEnabledPicBox"/> to decontruct into it's constituent <see cref="Point"/>s</param>
            /// <returns>A <see cref="RectangleView"/> object representing all the <see cref="Point"/>s in the <see cref="TransparencyEnabledPicBox"/></returns>
            public static RectangleView GatherPoints(TransparencyEnabledPicBox rectangle)
            {
                var left = new List<Point>();
                for(int i = rectangle.Top; i <= rectangle.Top+rectangle.Height;i++)
                    left.Add(new Point(rectangle.Left, i));
                var right = new List<Point>();
                for (int i = rectangle.Top; i <= rectangle.Top + rectangle.Height; i++)
                    right.Add(new Point(rectangle.Left+rectangle.Width, i));
                var top = new List<Point>();
                for (int i = rectangle.Left; i <= rectangle.Left + rectangle.Width; i++)
                    top.Add(new Point(i, rectangle.Top));
                var bottom = new List<Point>();
                for (int i = rectangle.Left; i <= rectangle.Left + rectangle.Width; i++)
                    bottom.Add(new Point(i, rectangle.Top+rectangle.Height));
                return new RectangleView(rectangle, left, right, top, bottom);
            }

            /// <summary>
            /// Returns every <see cref="Point"/> in this <see cref="RectangleView"/>
            /// </summary>
            public IEnumerable<Point> AllPoints
            {
                get
                {
                    foreach (Point p in _left)
                        yield return p;
                    foreach (Point p in _right)
                        yield return p;
                    foreach (Point p in _top)
                        yield return p;
                    foreach (Point p in _bottom)
                        yield return p;
                }
            }

            /// <summary>
            /// Determines if this <see cref="RectangleView"/> is adjacent to the given 
            /// <see cref="RectangleView"/> on any side
            /// </summary>
            /// <param name="otherRectangle">The <see cref="RectangleView"/> to compare to the current</param>
            /// <returns>
            /// A <see cref="Tuple{RectangleStatus,string,List{Point}}"/> object representing the relationship between the rectangles, any additional info
            /// and any shared <see cref="Point"/>s
            /// </returns>
            /// <remarks>
            /// Adjacency: is defined as Points are shared between the rectangles, 
            /// but there is no containment of any points within either rectangle.
            /// 
            /// Intersection: is defined as Points are shared between the rectangles, 
            /// and there is some containment of any points within either rectangle.
            /// 
            /// Containment: is defined as NO Points are shared between the rectangles, 
            /// and there is some containment of any points within either rectangle.
            /// </remarks>
            public Tuple<RectangleStatus, string, List<Point>> GetRelationship(RectangleView otherRectangle)
            {
                RectangleStatus status = RectangleStatus.NoRelationship;
                string message = string.Empty;
                var intersectingPoints = AllPoints.Where(p => otherRectangle.AllPoints.Contains(p));
                var containedPoints = AllPoints.Where(p => otherRectangle.IsPointWithin(p));
                if (intersectingPoints.Any())
                {

                    if (containedPoints.Any())
                        status = RectangleStatus.Intersecting;
                    else
                        status = RectangleStatus.Adjacent;
                    return new Tuple<RectangleStatus, string, List<Point>>(status, message, intersectingPoints.ToList());
                }

                var inverseContainedPoints = otherRectangle.AllPoints.Where(p => IsPointWithin(p));
                if (containedPoints.Any())
                {
                    status = RectangleStatus.Contained;
                    message = $"{Name} is Contained within {otherRectangle.Name}";

                }
                if (inverseContainedPoints.Any())
                {
                    status = RectangleStatus.Contained;
                    message = $"{otherRectangle.Name} is Contained within {Name}";
                }
                return new Tuple<RectangleStatus, string, List<Point>>(status, message, intersectingPoints.ToList());
            }


            /// <summary>
            /// Determines if the given point lies within our <see cref="RectangleView"/>
            /// </summary>
            /// <param name="point">The <see cref="Point"/> to analyze</param>
            /// <returns>True or False</returns>
            private bool IsPointWithin(Point point)
            {
                //A shared point is not considered *within* for the purposes of this application
                if (AllPoints.Contains(point))
                    return false;
                Rectangle current = new Rectangle(_basePicBox.Location, _basePicBox.Size);
                return current.Contains(point);
            }
        }

        #endregion

    }
}
