using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Sce.Atf.Dom;

namespace Vitei.ATFExtensions
{
    public class Util
    {
        public static AttributeInfo GetAttributeInfo(Type pType, string pAttributeInfoName)
        {
            AttributeInfo rval = null;

            var ti = pType;
            var members = ti
                .GetMembers()
                .Where(m => m.MemberType == MemberTypes.Field && m.Name == pAttributeInfoName && ((FieldInfo)m).FieldType == typeof(AttributeInfo))
                .Select(m => ((FieldInfo)m).GetValue(null));
            
            if (members.Count() > 0)
            {
                rval = members.First() as AttributeInfo;
            }

            return rval;
        }

        public static string PluralizeTypeName (string p_str)
        {
            return p_str.Substring(0, p_str.Length - 4) + "s";
        }

        public static readonly Dictionary<Type, AttributeTypes> AttributeTypeMappings
            = new Dictionary<Type, AttributeTypes>()
            {
                { typeof(bool), AttributeTypes.Boolean },
                { typeof(bool[]), AttributeTypes.BooleanArray },

                { typeof(sbyte), AttributeTypes.Int8 },
                { typeof(sbyte[]), AttributeTypes.Int8Array },

                { typeof(byte), AttributeTypes.UInt8 },
                { typeof(byte[]), AttributeTypes.UInt8Array },

                { typeof(short), AttributeTypes.Int16 },
                { typeof(short[]), AttributeTypes.Int16Array },

                { typeof(ushort), AttributeTypes.UInt16 },
                { typeof(ushort[]), AttributeTypes.UInt16Array },

                { typeof(int), AttributeTypes.Int32 },
                { typeof(int[]), AttributeTypes.Int32Array },

                { typeof(uint), AttributeTypes.UInt32 },
                { typeof(uint[]), AttributeTypes.UInt32Array },

                { typeof(long), AttributeTypes.Int64 },
                { typeof(long[]), AttributeTypes.Int64Array },

                { typeof(ulong), AttributeTypes.UInt64 },
                { typeof(ulong[]), AttributeTypes.UInt64Array },

                { typeof(float), AttributeTypes.Single },
                { typeof(float[]), AttributeTypes.SingleArray },

                { typeof(double), AttributeTypes.Double },
                { typeof(double[]), AttributeTypes.DoubleArray },

                { typeof(decimal), AttributeTypes.Decimal },
                { typeof(decimal[]), AttributeTypes.DecimalArray },

                { typeof(string), AttributeTypes.String },
                { typeof(string[]), AttributeTypes.StringArray },

                { typeof(DateTime), AttributeTypes.DateTime },

                { typeof(Uri), AttributeTypes.Uri }
            };
    }
}
