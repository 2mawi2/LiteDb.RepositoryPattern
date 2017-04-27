using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LiteDb.RepositoryPattern.Model.Classes;

namespace LiteDb.RepositoryPattern.Persistence.Application.Global.Tests
{
    [TestClass()]
    public class FactoryTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FactoryTest_ConstructorThrowsOnNull()
        {
            var factory = new Factory<Foo>(null);
        }

        [TestMethod()]
        public void GetTest_ReturnsTheCorrectFuncResult()
        {
            var testObject = new Foo { Name = "factory result object" };
            Func<Foo> factoryFunc = () => testObject;
            var factory = new Factory<Foo>(factoryFunc);
            Assert.AreEqual(testObject, factory.Get());
        }
    }
}