using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using LiteDb.RepositoryPattern.Core.Repositorys;
using LiteDb.RepositoryPattern.Model.Classes;
using LiteDb.RepositoryPattern.Persistence.Application.LiteDb;

namespace LiteDb.RepositoryPattern.Persistence.Repositorys.Tests
{
    [TestClass()]
    public class FooRepositoryTests
    {
        private static DbFactory<Foo> MockDbFactory => new DbFactory<Foo>(DbFactory<Foo>.Configuration.Test);


        [TestMethod()]
        public void GetByNameTest_ReturnsallItemsWithGivenName()
        {
            IFooRepository repository = new FooRepository(MockDbFactory);

            var result = repository.GetByName("special name").ToList();

            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreNotNull(result);
            Assert.IsTrue(result.Any());
            result.ForEach(i => { Assert.AreEqual(i.Name, "special name"); });
        }

        [TestMethod()]
        public void CreateTest_ShouldCreateNewGuidWhichIsNotMinusOne()
        {
            IFooRepository repository = new FooRepository(MockDbFactory);

            var foo = new Foo
            {
                Id = 123456,
                Name = "random"
            };

            var result = repository.Create(foo);
            Assert.AreNotEqual(123456, result);
            Assert.AreNotEqual(-1, result);
        }

        [TestMethod()]
        public void CreateTest_ShouldReturnMinusOneIfWrongName()
        {
            IFooRepository repository = new FooRepository(MockDbFactory);

            var foo = new Foo
            {
                Id = 123456,
                Name = "wrong name",
            };

            var result = repository.Create(foo);
            Assert.AreEqual(-1, result);
        }

        [TestMethod()]
        public void CreateTest_ShouldReturnMinusOneIfNameNull()
        {
            IFooRepository repository = new FooRepository(MockDbFactory);

            var Foo = new Foo
            {
                Id = 123456,
                Name = null,
            };

            var result = repository.Create(Foo);
            Assert.AreEqual(-1, result);
        }
    }
}