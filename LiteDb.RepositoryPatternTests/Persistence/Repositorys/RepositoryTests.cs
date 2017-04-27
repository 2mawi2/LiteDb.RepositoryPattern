using System.Linq;
using LiteDb.RepositoryPattern.Core.Application.Global;
using LiteDb.RepositoryPattern.Core.Application.LiteDb;
using LiteDb.RepositoryPattern.Model.Classes;
using LiteDb.RepositoryPattern.Persistence.Application.LiteDb;
using LiteDb.RepositoryPattern.Persistence.Repositorys;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteDb.RepositoryPatternTests.Persistence.Repositorys
{
    [TestClass()]
    public class RepositoryTests
    {
        private class RepositoryImpl : Repository<Foo>
        {
            public RepositoryImpl(IFactory<IDb<Foo>> db, string collectionName = null) : base(db, collectionName)
            {
            }
        }

        private DbFactory<Foo> _factory;
        private RepositoryImpl _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _factory = new DbFactory<Foo>(DbFactory<Foo>.Configuration.Test);
            _repository = new RepositoryImpl(_factory);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _factory = null;
        }


        [TestMethod()]
        public void GenericRepositoryTest()
        {
            Assert.IsNotNull(_repository.Db);
            Assert.AreEqual(_factory.Get(), _repository.Db.Get());
        }


        [TestMethod()]
        public void CreateTest_ShouldReturnMinus1IfNull()
        {
            var result = _repository.Create(null);
            Assert.IsTrue(result == -1);
        }


        [TestMethod()]
        public void CreateTest()
        {
            var foo = new Foo
            {
                Id = 4352345,
                Name = "Test Foo"
            };
            var result = _repository.Create(foo);
            Assert.IsTrue(result != -1);
            Assert.AreEqual(result, _repository.GetById(foo.Id).Id);
        }


        [TestMethod()]
        public void GetAllTest()
        {
            var result = _repository.GetAll();
            CollectionAssert.AreEqual(result.ToList(), TestDb<Foo>.FooFakeCollection.ToList());
        }

        [TestMethod()]
        public void GetByIdTest_ShouldHandleNullValues()
        {
            var result = _repository.GetById(null);
            Assert.AreEqual(result, default(Foo));
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var result = _repository.GetById(1);
            Assert.AreEqual(result, TestDb<Foo>.FooFakeCollection.FirstOrDefault(i => i.Id == 1));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var current = TestDb<Foo>.FooFakeCollection.First();
            current.Name = "Updated Author";
            var id = current.Id;
            var result = _repository.Update(current);
            Assert.IsTrue(result);
            Assert.AreEqual("Updated Author", TestDb<Foo>.FooFakeCollection.FirstOrDefault(i => i.Id == id)?.Name);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var idToDelete = TestDb<Foo>.FooFakeCollection.First().Id;
            var result = _repository.Delete(idToDelete);
            Assert.IsTrue(result);
            var shouldNotExist = _repository.GetById(idToDelete);
            Assert.IsNull(shouldNotExist);
        }
    }
}