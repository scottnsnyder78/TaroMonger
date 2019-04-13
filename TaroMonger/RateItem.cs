using System;
using System.Collections.Generic;

namespace TaroMonger
{
    public class RateItem
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public decimal FlatAmount { get; set; }
        public decimal Factor { get; set; }
        public List<RateOption> Option { get; set; }
        public List<Guid> BasedOff { get; set; }
    }
}
