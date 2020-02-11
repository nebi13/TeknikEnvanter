using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikEnvanter2
{
    public class Event
    {
        public int Priority { get; set; }

    }

    public enum PriorityEnum
    {
        Low = 1,
        Normal = 2,
        high = 3
    }
}
