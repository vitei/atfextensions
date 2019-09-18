//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.ComponentModel;

using Sce.Atf.Controls.PropertyEditing;

namespace Vitei.ATFExtensions.Controls.PropertyEditing
{
    /// <summary>
    /// TypeConverter for use with enum editors where enum is stored as int or Usagi enum.
    /// It converts int/Usagi enum to string and string to int/Usagi enum.</summary>
    public class UsagiEnumTypeConverter : IntEnumTypeConverter
    {
        /// <summary>
        /// Default construct, required for IAnnotatedParams</summary>
        public UsagiEnumTypeConverter()
        { }

        /// <summary>
        /// Construct using an enum type</summary>
        /// <param name="enumType">Enum type</param>
        public UsagiEnumTypeConverter(Type enumType)
            : base(enumType)
        {
            m_enumType = enumType;
        }

        #region base override

        /// <summary>
        /// Convert value to a type in a context and culture</summary>
        /// <param name="context">Context in which conversion occurs</param>
        /// <param name="culture">Culture that indicates conversion format</param>
        /// <param name="value">Value to convert</param>
        /// <param name="destinationType">Type to convert to</param>
        /// <returns>Converted value</returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if ((value is int || value.GetType() == m_enumType) && destinationType == typeof(string))
            {
                return base.ConvertTo(context, culture, (int)value, destinationType);
            }
            throw new ArgumentException(string.Format("value must be an int or {0} and destinationType must be a string", m_enumType));
        }

        #endregion

        private Type m_enumType;
    }
}