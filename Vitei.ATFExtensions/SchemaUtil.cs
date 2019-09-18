using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sce.Atf.Dom;

namespace Vitei.ATFExtensions
{
    public class SchemaUtil
    {
        static public Dictionary<AttributeTypes, string> XSDMappings = new Dictionary<AttributeTypes, string>()
        {
            { AttributeTypes.String, "xs:string" },
            { AttributeTypes.Single, "xs:float" },
            { AttributeTypes.Double, "xs:double" }
        };

        static public Dictionary<Type, string> TypeXSDMappings = new Dictionary<Type, string>()
        {
            { typeof(System.Single), "xs:float" },
            { typeof(System.Double), "xs:double" },
            { typeof(System.Decimal), "xs:decimal" },
            { typeof(System.DateTime), "xs:dateTime" },
            { typeof(System.Boolean), "xs:boolean" },
            { typeof(System.Int32), "xs:int" },
            { typeof(System.Int64), "xs:long" },
            { typeof(System.UInt32), "xs:unsignedInt" },
            { typeof(System.UInt64), "xs:unsignedLong" },
            { typeof(System.String), "xs:string" },

            // @todo fix this fucking crime, see blueprint.proto
            { typeof(System.Byte[]), "xs:unsignedByte" },
        };

        static public Dictionary<Type, string> NativeTypeXSDMappings = new Dictionary<Type, string>()
        {
            { typeof(System.Single), "float" },
            { typeof(System.Double), "double" },
            { typeof(System.DateTime), "unsigned int" },
            { typeof(System.Boolean), "bool" },
            { typeof(System.Int32), "int" },
            { typeof(System.Int64), "long" },
            { typeof(System.UInt32), "unsigned int" },
            { typeof(System.UInt64), "unsigned long" },
            { typeof(System.String), "wchar_t*" },

            // @todo fix this fucking crime, see blueprint.proto
            { typeof(System.Byte[]), "byte" },
        };
    }
}
