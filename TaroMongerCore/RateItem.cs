using System;
using System.Collections.Generic;

namespace TaroMongerCore
{
    public class RateItem
    {
        private double _limit;
        private double _nhr;
        private double _hur;
        public Guid Id { get; set; }
        public string Name { get; set; }

        public double Limit
        {
            get => _limit;
            set
            {
                _limit = value;
                LimitRated = true;
            }
        }

        public double Nhr
        {
            get => _nhr;
            set
            {
                _nhr = value;
                NhrRated = true;
            }
        }

        public double Hur
        {
            get => _hur;
            set
            {
                _hur = value;
                HurRated = true;
            }
        }

        public List<RateOption> Options { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool Rated => LimitRated && HurRated && NhrRated;
        public bool LimitRated { get; set; }
        public bool NhrRated { get; set; }
        public bool HurRated { get; set; }
    }
}
