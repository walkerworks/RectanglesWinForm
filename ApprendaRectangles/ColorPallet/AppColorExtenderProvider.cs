using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// WinForms out of the box is inherently awful looking.  These static classes & component extensions create a color pallet we
/// can reference easily to at least give the app an acceptable look
///</summary>
///<remarks>
/// *Borrowed* from http://stackoverflow.com/questions/8852197/c-custom-colors-class-accessible-to-all-projects-in-solution
/// </remarks>
namespace ApprendaRectangles.ColorPallet
{
    [ProvideProperty("CustomForeColor", typeof(Control))]
    [ProvideProperty("CustomBackColor", typeof(Control))]
    public class AppColorExtenderProvider : Component, IExtenderProvider
    {
        public AppColor GetCustomForeColor(Control control)
        {
            return AppColor.Find(control.ForeColor);
        }

        public AppColor GetCustomBackColor(Control control)
        {
            return AppColor.Find(control.BackColor);
        }

        public void SetCustomBackColor(Control control, AppColor value)
        {
            control.BackColor = value.Color;
        }

        public void SetCustomForeColor(Control control, AppColor value)
        {
            control.ForeColor = value.Color;
        }

        public bool ShouldSerializeCustomForeColor(Control control)
        {
            return false;
        }

        public bool ShouldSerializeCustomBackColor(Control control)
        {
            return false;
        }

        #region IExtenderProvider Members

        public bool CanExtend(object extendee)
        {
            return (extendee is Control);
        }

        #endregion
    }
}
