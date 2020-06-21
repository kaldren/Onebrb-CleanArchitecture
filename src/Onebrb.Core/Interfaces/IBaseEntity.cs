using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Core.Interfaces
{
    public interface IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}
