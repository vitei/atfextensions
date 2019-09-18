using Sce.Atf.Dom;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Vitei.ATFExtensions
{
    public static class ClassBuilder
    {
        public readonly static string DynamicAssemblyName = "Vitei.Dynamic";

        private static AssemblyName s_AssemblyName;
        private static AssemblyBuilder s_AssemblyBuilder;
        private static ModuleBuilder s_ModuleBuilder;

        static ClassBuilder()
        {
            s_AssemblyName = new AssemblyName(DynamicAssemblyName);
            s_AssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                s_AssemblyName,
                AssemblyBuilderAccess.Run
            );
            s_ModuleBuilder = s_AssemblyBuilder.DefineDynamicModule("MainModule");
        }

        public static Type BuildType(string typeSignature, IEnumerable<AttributeInfo> attributes, Type parentType = null)
        {
            TypeBuilder tb = GetTypeBuilder(typeSignature, parentType);

            // @todo setup attributes here
            foreach (var attr in attributes)
            {
                CreateProperty(tb, attr.Name, attr.Type.ClrType, parentType);
            }

            Type t = tb.CreateType();

            return t;
        }

        public static TypeBuilder GetTypeBuilder(string typeSignature, Type parentType = null)
        {
            // @implementme
            TypeBuilder tb = s_ModuleBuilder.DefineType(
                typeSignature,
                TypeAttributes.Public |
                TypeAttributes.Class |
                TypeAttributes.AutoClass |
                TypeAttributes.AnsiClass |
                TypeAttributes.BeforeFieldInit |
                TypeAttributes.AutoLayout,
                parentType
            );

            return tb;
        }

        private static void CreateProperty(TypeBuilder type_b, string propertyName, Type propertyType, Type parentType = null)
        {            
            PropertyBuilder prop_b = type_b.DefineProperty(
                propertyName,
                PropertyAttributes.None,
                propertyType,
                null
            );

            // getter
            // get { return GetAttribute<string>(DomTypes.soundFileDefType.filenameAttribute); }
            MethodBuilder getPropMthdBldr = type_b.DefineMethod(

                // name
                "get_" + propertyName,

                // method attributes
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.HideBySig,

                // return types
                propertyType,

                // parameter types
                Type.EmptyTypes

            );
            MethodInfo mi_GetAttribute = parentType.GetMethod("GetAttribute").MakeGenericMethod(
                propertyType
            );

            ILGenerator getIl = getPropMthdBldr.GetILGenerator();
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Call, mi_GetAttribute);
            getIl.Emit(OpCodes.Ret);

            // setter
            // set { SetAttribute(<type>.<field>, value); }
            MethodBuilder setPropMthdBldr = type_b.DefineMethod(
                "set_" + propertyName,
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.HideBySig,
                null,
                new[] {
                    propertyType
                }
            );
            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();
            setIl.Emit(OpCodes.Ldarg_0);
            //setIl.Emit(OpCodes.Stfld, field_b);
            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            prop_b.SetGetMethod(getPropMthdBldr);
            prop_b.SetSetMethod(setPropMthdBldr);

        }
    }
}
