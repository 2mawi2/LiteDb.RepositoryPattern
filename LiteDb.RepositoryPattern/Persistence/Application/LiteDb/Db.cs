using LiteDb.RepositoryPattern.Core.Application.LiteDb;
using LiteDB;

namespace LiteDb.RepositoryPattern.Persistence.Application.LiteDb
{
    public class Db<T> : LiteDatabase, IDb<T>
    {
        public Db(BsonMapper mapper = null)
            : base(Core.Application.Global.Global.ConnectionString, mapper)
        {
        }

        public LiteCollection<T> Collection(string collectionName = null)
        {
            return collectionName == null ? GetCollection<T>() : GetCollection<T>(collectionName);
        }
    }
}