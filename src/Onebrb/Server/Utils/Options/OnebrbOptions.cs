using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Utils.Options
{
    public class OnebrbOptions
    {
        public const string Options = "OnebrbOptions";

        public string ConnectionString { get; set; } = string.Empty;
    }
}
