using System;
using System.Collections.Generic;
using System.Linq;

namespace TaroMongerCore
{
    public class RateBook
    {
        public List<RatePage> Pages { get; set; }
        public DateTime EffectiveDateTime { get; set; }
        public string PlanId { get; set; }
        public Guid Id { get; set; }
        public string State { get; set; }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                var thisCovA = Pages[0].Sections[0].Items.FirstOrDefault(c => c.Name.Equals("Coverage A"));
                var thisCovB = Pages[0].Sections[0].Items.FirstOrDefault(c => c.Name.Equals("Coverage B"));
                var thatCovA = ((RateBook) obj).Pages[0].Sections[0].Items.FirstOrDefault(c => c.Name.Equals("Coverage A"));
                var thatCovB = ((RateBook)obj).Pages[0].Sections[0].Items.FirstOrDefault(c => c.Name.Equals("Coverage B"));
                return thisCovA.Hur.Equals(thatCovA.Hur) && thisCovA.Limit.Equals(thatCovA.Limit) &&
                       thisCovA.Nhr.Equals(thatCovA.Nhr) &&
                       thisCovB.Hur.Equals(thatCovB.Hur) && thisCovB.Limit.Equals(thatCovB.Limit) &&
                       thisCovB.Nhr.Equals(thatCovB.Nhr);
            }
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }


}
