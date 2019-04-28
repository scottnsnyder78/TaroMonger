using System;
using System.Collections.Generic;

namespace TaroMongerCore
{
    public class RateOption
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public double LimitFlatAmount { get; set; }
        public double LimitFactor { get; set; }
        public double NhrFlatAmount { get; set; }
        public double NhrFactor { get; set; }
        public double HurFlatAmount { get; set; }
        public double HurFactor { get; set; }
        public List<Guid> LimitBasedOff { get; set; }
        public List<Guid> NhrBasedOff { get; set; }
        public List<Guid> HurBasedOff { get; set; }
        public Boolean Selected { get; set; }
    }
}
