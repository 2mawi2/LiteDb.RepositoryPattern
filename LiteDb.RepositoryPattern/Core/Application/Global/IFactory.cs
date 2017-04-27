namespace LiteDb.RepositoryPattern.Core.Application.Global
{
    public interface IFactory<T>
    {
        T Get();
    }
}
