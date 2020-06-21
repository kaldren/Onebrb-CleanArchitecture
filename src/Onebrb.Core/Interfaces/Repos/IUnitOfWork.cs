using Onebrb.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Onebrb.Core.Interfaces.Repos
{
    public interface IUnitOfWork : IDisposable
    {
        IMessageRepository Messages { get; }
        Task<int> SaveChangesAsync();
        Task DisposeAsync();
    }
}
