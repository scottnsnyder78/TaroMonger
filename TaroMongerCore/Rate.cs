using System;
using System.Collections.Generic;
using System.Linq;

namespace TaroMongerCore
{
    public static class Rate
    {
        public static RateBook RatePolicy(RateBook book)
        {
            var coverages = book.Pages.SelectMany(x => x.Sections.SelectMany(y => y.Items)).ToList();
            const int maxLoops = 100;
            var loopCount = 0;
            while (coverages.Any(c => c.Rated.Equals(false)) && loopCount <= maxLoops)
            {
                RateLimits(coverages);
                RateHur(coverages);
                RateNhr(coverages);
                loopCount++;
            }
            return book;
        }

        private static void RateNhr(List<RateItem> coverages)
        {
            var ratedNhr = coverages.Where(o => o.NhrRated).Select(o => o.Id).ToList();
            var rateableNhrs = coverages.Where(c => !c.Rated).SelectMany(o => o.Options).Where(o =>
                o.Selected && (!o.NhrBasedOff.Any() || AllBasedOffsRated(o.NhrBasedOff, ratedNhr)));
            foreach (var rateableNhr in rateableNhrs)
            {
                var coverage = coverages.FirstOrDefault(c => c.Options.Any<RateOption>(o => o.Id.Equals(rateableNhr.Id)));
                var basedOffs = coverages.Where(c => rateableNhr.NhrBasedOff.Contains(c.Id));
                coverage.Nhr = rateableNhr.NhrFlatAmount +
                               (rateableNhr.NhrFactor * basedOffs.Select(b => b.Nhr).Sum());
            }
        }

        private static void RateHur(List<RateItem> coverages)
        {
            var ratedHur = coverages.Where(o => o.HurRated).Select(o => o.Id).ToList();
            var rateableHurs = coverages.Where(c => !c.Rated).SelectMany(o => o.Options).Where(o =>
                o.Selected && (!o.HurBasedOff.Any() || AllBasedOffsRated(o.HurBasedOff, ratedHur)));
            foreach (var rateableHur in rateableHurs)
            {
                var coverage = coverages.FirstOrDefault(c => c.Options.Any<RateOption>(o => o.Id.Equals(rateableHur.Id)));
                var basedOffs = coverages.Where(c => rateableHur.HurBasedOff.Contains(c.Id));
                coverage.Hur = rateableHur.HurFlatAmount +
                                 (rateableHur.HurFactor * basedOffs.Select(b => b.Hur).Sum());
            }
        }

        private static void RateLimits(List<RateItem> coverages)
        {
            var ratedLimits = coverages.Where(o => o.LimitRated).Select(o => o.Id).ToList();
            var rateableLimits = coverages.Where(c => !c.Rated).SelectMany(o => o.Options).Where(o =>
                o.Selected && (!o.LimitBasedOff.Any() || AllBasedOffsRated(o.LimitBasedOff, ratedLimits)));
            foreach (var rateableLimit in rateableLimits)
            {
                var coverage = coverages.FirstOrDefault(c => c.Options.Any<RateOption>(o => o.Id.Equals(rateableLimit.Id)));
                var basedOffs = coverages.Where(c => rateableLimit.LimitBasedOff.Contains(c.Id));
                coverage.Limit = rateableLimit.LimitFlatAmount +
                                 (rateableLimit.LimitFactor * basedOffs.Select(b => b.Limit).Sum());
            }
        }

        private static bool AllBasedOffsRated(List<Guid> basedOffs, List<Guid> rated)
        {
            return basedOffs.Intersect(rated).Count() == basedOffs.Count();
        }
    }
}
