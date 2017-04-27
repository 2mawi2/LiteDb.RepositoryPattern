using System;
using LiteDB;

namespace LiteDb.RepositoryPattern.Core.Application.LiteDb
{
    public interface IDb<T> : IDisposable
    {
        LiteCollection<T> Collection(string collectionName = null);
    }
}