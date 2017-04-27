using System.Linq;
using LiteDb.RepositoryPattern.Model.Classes;
using LiteDb.RepositoryPattern.Persistence.Application.LiteDb;
using LiteDb.RepositoryPattern.Persistence.Repositorys;
using static System.Console;

namespace LiteDb.RepositoryPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            UseLiteDb();
        }

        public static void UseLiteDb()
        {
            var dbFactory = new DbFactory<Foo>(DbFactory<Foo>.Configuration.Production);
            var repository = new FooRepository(dbFactory);

            var createdId = repository.Create(new Foo {Name = "correct name"});
            WriteLine(repository.GetByName("correct name").First().Name);
            ReadLine();
        }
    }
}