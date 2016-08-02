using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApprendaRectangles.ColorPallet
{
    public class AppColorTypeConverter : StringConverter
    {
        static Dictionary<AppColor, string> _nameIndex = InitializeNameIndex();
        static Dictionary<string, AppColor> _colorIndex = InitializeColorIndex();

        private static Dictionary<string, AppColor> InitializeColorIndex()
        {
            return typeof(AppColor)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .ToDictionary(f => f.Name, f => (AppColor)f.GetValue(null));
        }

        private static Dictionary<AppColor, string> InitializeNameIndex()
        {
            return typeof(AppColor)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .ToDictionary(f => (AppColor)f.GetValue(null), f => f.Name);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new System.ComponentModel.TypeConverter.StandardValuesCollection(_nameIndex.Values.ToList());
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(AppColor))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                AppColor result;
                if (_colorIndex.TryGetValue((string)value, out result))
                    return result;
                else
                    return new AppColor();
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is AppColor)
            {
                string result;
                if (_nameIndex.TryGetValue((AppColor)value, out result))
                    return result;
                else
                    return String.Empty;
            }
            else
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
