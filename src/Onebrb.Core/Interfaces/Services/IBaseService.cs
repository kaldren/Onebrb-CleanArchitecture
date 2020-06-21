using Onebrb.Core.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Core.Interfaces.Services
{
    public interface IBaseService : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
