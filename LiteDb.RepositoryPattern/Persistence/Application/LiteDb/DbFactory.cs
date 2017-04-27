using LiteDb.RepositoryPattern.Core.Application.Global;
using LiteDb.RepositoryPattern.Core.Application.LiteDb;
using LiteDB;

namespace LiteDb.RepositoryPattern.Persistence.Application.LiteDb
{
    /// <summary>
    /// Creates LiteDbObjects depending on the Factory setting
    /// </summary>
    public class DbFactory<T> : IFactory<IDb<T>>
    {
        /// <summary>
        /// LiteDbConfiguration
        /// set on Test => for using <see cref="TestDb{T}"/> objects, this creates in memory databases, which can be tested easyly
        /// set on Production => create a real LiteDb objects which runs on a real lite.db in you configured litedb location 
        /// <see cref="ConnectionString"/>
        /// </summary>
        public enum Configuration
        {
            Test,
            Production
        }

        private readonly Configuration _factoryConfiguration;

        public DbFactory(Configuration factoryConfiguration)
        {
            _factoryConfiguration = factoryConfiguration;
        }

        /// <summary>
        /// Caches testdb in memory for lifetime of factory
        /// </summary>
        private TestDb<T> TestDbContainer { get; set; }

        /// <summary>
        /// Generate a new Instance of IDb
        /// </summary>
        public IDb<T> Get()
        {
            switch (_factoryConfiguration)
            {
                case Configuration.Test:
                    // set factory on test mode
                    return TestDbContainer ?? (TestDbContainer = new TestDb<T>());
                case Configuration.Production:
                    // set factory on production mode
                    return new Db<T>();
                default:
                    return null;
            }
        }
    }
}
