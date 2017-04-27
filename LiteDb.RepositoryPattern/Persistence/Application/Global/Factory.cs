using System;
using LiteDb.RepositoryPattern.Core.Application.Global;

namespace LiteDb.RepositoryPattern.Persistence.Application.Global
{
    public class Factory<T> : IFactory<T>
    {
        private readonly Func<T> _factoryFunc;

        /// <summary>
        /// Simple implementation of the IFactory interface
        /// which takes a Func, that defines what gets returned
        /// </summary>
        /// <exception cref="ArgumentNullException">throws if the factory func is null</exception> 
        public Factory(Func<T> factoryFunc)
        {
            if (factoryFunc == null) throw new ArgumentNullException(nameof(factoryFunc));
            _factoryFunc = factoryFunc;
        }

        public T Get() => _factoryFunc();
    }
}