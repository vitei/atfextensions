using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using Sce.Atf;
using Sce.Atf.Dom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vitei.ATFExtensions.Test
{
    [TestClass]
    public class ClassBuilderTest
    {
        [TestMethod]
        public void EmptyDomNodeAdapterCreationTest()
        {
            Type foonodeType = ClassBuilder.BuildType(
                "FooNode",
                null,
                typeof(DomNodeAdapter)
            );
            var foo = Activator.CreateInstance(foonodeType);
            Assert.IsInstanceOfType(foo, foonodeType);
            Assert.IsInstanceOfType(foo, typeof(DomNodeAdapter));
        }
        [TestMethod]
        public void DomNodeAdapterWithAttributesTest()
        {
            var attributes = new List<AttributeInfo>()
            {
                new AttributeInfo(
                    "Vee",
                    new AttributeType(
                        AttributeTypes.Single.ToString(),
                        typeof(float)
                    )
                ),
                new AttributeInfo(
                    "Tay",
                    new AttributeType(
                        AttributeTypes.Double.ToString(),
                        typeof(float)
                    )
                )
            };
            Type foonodeType = ClassBuilder.BuildType(
                "FooNode",
                attributes,
                typeof(DomNodeAdapter)
            );
            var foo = Activator.CreateInstance(foonodeType);
            Assert.IsInstanceOfType(foo, foonodeType);
            Assert.IsInstanceOfType(foo, typeof(DomNodeAdapter));

        }
    }
}
