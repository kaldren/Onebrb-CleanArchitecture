namespace Onebrb.Core.Interfaces.Services
{
    public interface IService<TEntity> : IBaseService where TEntity : class
    {
    }
}
