using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Vitei.ATFExtensions.Controls.PropertyEditing
{
    /// <summary>
    /// TypeConverter for use with ColorEditor; converts color stored in DOM as an ARGB int to/from color or string types</summary>
    public class FloatArrayColorConverter : TypeConverter
    {
        /// <summary>
        /// Determines whether this instance can convert from the specified context</summary>
        /// <param name="context">An System.ComponentModel.ITypeDescriptorContext that provides a format context</param>
        /// <param name="sourceType">A System.Type that represents the type you want to convert from</param>
        /// <returns>True iff this instance can convert from the specified context</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string) ||
                    sourceType == typeof(Color));
        }

        /// <summary>
        /// Determines whether this instance can convert to the specified context</summary>
        /// <param name="context">An System.ComponentModel.ITypeDescriptorContext that provides a format context</param>
        /// <param name="destType">Type of the destination</param>
        /// <returns>True iff this instance can convert to the specified context</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
        {
            return (destType == typeof(string) ||
                    destType == typeof(Color));
        }

        /// <summary>
        /// Converts the given object to the type of this converter, using the specified context and culture information</summary>
        /// <param name="context">An System.ComponentModel.ITypeDescriptorContext that provides a format context</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo"></see> to use as the current culture.</param>
        /// <param name="value">The object to convert</param>
        /// <returns>An object that represents the converted value</returns>
        /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed</exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return null;

            if (value is string)
            {
                // string -> RGBA float[]
                TypeConverter colorConverter = TypeDescriptor.GetConverter(typeof(Color));
                Color color = (Color)colorConverter.ConvertFrom(context, culture, value);
                return ToFloat(color);
            }
            else if (value is Color)
            {
                // Color -> RGBA float[]
                return ToFloat((Color)value);
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>Converts the given value object to the specified type, using the specified
        /// context and culture information</summary>
        /// <param name="context">An System.ComponentModel.ITypeDescriptorContext that provides a format context</param>
        /// <param name="culture">A System.Globalization.CultureInfo. If null is passed, the current culture is assumed</param>
        /// <param name="value">The System.Object to convert</param>
        /// <param name="destType">The System.Type to convert the value parameter to</param>
        /// <returns>An object that represents the converted value</returns>
        public override object ConvertTo(ITypeDescriptorContext context,
                CultureInfo culture,
                object value,
                Type destType)
        {
            if (value == null)
                return null;

            if (value is Color)
                value = ToFloat((Color)value);

            if ((value is float[]) && destType == typeof(string))
            {
                // RGBA float[] -> string
                Color color = ToColor((float[])value);
                TypeConverter colorConverter = TypeDescriptor.GetConverter(typeof(Color));
                return colorConverter.ConvertTo(context, culture, color, destType);
            }
            else if ((value is float[]) && destType == typeof(Color))
            {
                // RGBA float[] -> Color
                return ToColor((float[])value);
            }

            return base.ConvertTo(context, culture, value, destType);
        }

        private Color ToColor(float[] value)
        {
            return Color.FromArgb(
                (int)(value[3] * 255.0f),
                (int)(value[0] * 255.0f),
                (int)(value[1] * 255.0f),
                (int)(value[2] * 255.0f)
                );
        }

        private float[] ToFloat(Color value)
        {
            float inv255 = 1.0f / 255.0f;

            return new float[4] {
                value.R * inv255,
                value.G * inv255,
                value.B * inv255,
                value.A * inv255,
            };
        }
    }
}
