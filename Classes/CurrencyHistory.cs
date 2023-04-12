using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Classes
{
    internal class CurrencyHistory
    {
        public decimal priceUsd { get; set; }
        public long time { get; set; }
        public decimal circulatingSupply { get; set; }
        public DateTime date { get; set; }
    }
}
