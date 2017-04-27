using System.Collections.Generic;
using LiteDb.RepositoryPattern.Model.Classes;

namespace LiteDb.RepositoryPattern.Core.Repositorys
{
    public interface IFooRepository : IRepository<Foo>
    {
        IEnumerable<Foo> GetByName(string name);
    }
}