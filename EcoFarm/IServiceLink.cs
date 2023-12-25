using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm
{
    internal interface IServiceLink
    {
        string localHostClient { get; }
        string mainClient { get; }

        internal Task GetClient();
    }
}
