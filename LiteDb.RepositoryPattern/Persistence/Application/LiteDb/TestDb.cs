using System.Collections.Generic;
using System.IO;
using LiteDb.RepositoryPattern.Core.Application.LiteDb;
using LiteDb.RepositoryPattern.Model.Classes;
using LiteDB;

namespace LiteDb.RepositoryPattern.Persistence.Application.LiteDb
{
    public class TestDb<T> : LiteDatabase, IDb<T>
    {
        /// <summary>
        /// Fake Db context running on memory stream for all unit tests
        /// </summary>
        public TestDb(BsonMapper mapper = null) : base(new MemoryStream(), mapper)
        {
            GetCollection<Foo>().Insert(FooFakeCollection);
        }


        public LiteCollection<T> Collection(string collectionName = null)
        {
            return collectionName == null ? GetCollection<T>() : GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Add here required Fake collection
        /// and add them in the constructor to the Memorysteam LiteDb database
        /// </summary>
        public static readonly IEnumerable<Foo> FooFakeCollection = new List<Foo>
        {
            new Foo
            {
                Id = 1,
                Name = "wrong name"
            },
            new Foo
            {
                Id = 2,
                Name = "correct name"
            },
            new Foo
            {
                Id = 3,
                Name = "special name"
            },
            new Foo
            {
                Id = 4,
                Name = "special name"
            },
        };
    }
}