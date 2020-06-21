using Onebrb.Core.Interfaces.Repos;
using Onebrb.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Onebrb.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Messages = new MessageRepository(_dbContext);
        }

        public IMessageRepository Messages { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
