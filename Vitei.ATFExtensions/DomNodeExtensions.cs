using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sce.Atf;
using Sce.Atf.Dom;

namespace Vitei.ATFExtensions
{
    public static class DomNodeExtensions
    {
        /// <summary>
        /// Set a named attribute on a DOM node. This should only be used with simple types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="p_name"></param>
        /// <param name="p_val"></param>
        /// <returns></returns>
        public static bool SetNamedAttribute<T>(this DomNode node, string p_name, T p_val)
        {
            AttributeInfo attributeInfo = node.Type.GetAttributeInfo(p_name);
            bool bCanWrite = attributeInfo.Type.ClrType == typeof(T);
            if (!bCanWrite)
            {
                throw new ArgumentException(String.Format(
                    "Specified data type does not match attribute data type (expected {0}, got {1}).".Localize(),
                    attributeInfo.Type.ClrType.ToString(),
                    typeof(T).ToString()
                ));
            }
            else
            {
                // all good!
                node.SetAttribute(attributeInfo, p_val);
            }

            return bCanWrite;
        }
    }
}
