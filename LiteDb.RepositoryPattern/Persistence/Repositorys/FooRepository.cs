using System;
using System.Collections.Generic;
using System.Linq;
using LiteDb.RepositoryPattern.Core.Application.Global;
using LiteDb.RepositoryPattern.Core.Application.LiteDb;
using LiteDb.RepositoryPattern.Core.Repositorys;
using LiteDb.RepositoryPattern.Model.Classes;

namespace LiteDb.RepositoryPattern.Persistence.Repositorys
{
    public class FooRepository : Repository<Foo>, IFooRepository
    {
        public FooRepository(IFactory<IDb<Foo>> db) : base(db)
        {
        }

        public override int Create(Foo model)
        {
            if (model.Name == null || model.Name.Equals("wrong name")) return -1;
            OverrideGuidOfFoo(model);
            return base.Create(model);
        }

        private static void OverrideGuidOfFoo(Foo model) => model.Id = Guid.NewGuid().GetHashCode();

        public IEnumerable<Foo> GetByName(string name) => GetAll()?.Where(i => i.Name == name);
    }
}