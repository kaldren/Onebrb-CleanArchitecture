using Onebrb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Core.Entities
{
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
