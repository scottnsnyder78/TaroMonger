using System;
using System.Collections.Generic;

namespace TaroMongerCore
{
    public class RateItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal FlatAmount { get; set; }
        public decimal Factor { get; set; }
        public List<RateOption> Options { get; set; }
        public List<Guid> BasedOff { get; set; }
        public string Value { get; set; }
    }
}
