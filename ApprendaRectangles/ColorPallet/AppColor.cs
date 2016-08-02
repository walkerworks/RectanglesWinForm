using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprendaRectangles.ColorPallet
{
    [TypeConverter(typeof(AppColorTypeConverter))]
    public class AppColor
    {
        public static AppColor Action = new AppColor { Color = Color.FromArgb(252, 163, 17) };
        public static AppColor Background = new AppColor { Color = Color.FromArgb(229, 229, 229) };
        public static AppColor Text = new AppColor { Color = Color.FromArgb(20, 33, 61) };
        public static AppColor RectangleOne = new AppColor { Color = Color.FromArgb(104, 142, 38) };
        public static AppColor RectangleTwo = new AppColor { Color = Color.FromArgb(161, 7, 2) };

        public Color Color { get; private set; }

        internal static AppColor Find(Color color)
        {
            if (color == AppColor.Action.Color)
                return AppColor.Action;
            else if (color == AppColor.Background.Color)
                return AppColor.Background;
            else if (color == AppColor.Text.Color)
                return AppColor.Text;

            return new AppColor { Color = Color.Transparent };
        }
    }
}
