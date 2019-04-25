using System;
using System.Collections.Generic;

namespace TaroMongerCore
{
    public class RateBook
    {
        public List<RatePage> Pages { get; set; }
        public DateTime EffectiveDateTime { get; set; }
        public string PlanId { get; set; }
        public Guid Id { get; set; }
        public string State { get; set; }
    }
}
