using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace TaroMongerCore
{
    public class RateResults
    {
        public List<RateResult> Results { get; set; }
        public override bool Equals(object obj)
        {
            var board = obj as RateResults;

            if (board == null)
                return false;

            if (board.Results.Count != Results.Count)
                return false;

            return !board.Results.Where((t, i) => !t.Equals(Results[i])).Any();
        }
        public override int GetHashCode()
        {
            // determine what's appropriate to return here - a unique board id may be appropriate if available
            return 1;
        }
    }

    public class RateResult
    {
        public string Description { get; set; }
        public string Value { get; set; }
        public double Limit { get; set; }
        public double Nhr { get; set; }
        public double Hur { get; set; }
        public override bool Equals(object obj)
        {
            var board = obj as RateResult;

            if (board == null)
                return false;

            return Description.Equals(board.Description) &&
                   Value.Equals(board.Value) &&
                   Limit.Equals(board.Limit) &&
                   Nhr.Equals(board.Nhr) &&
                   Hur.Equals(board.Hur);
        }
        public override int GetHashCode()
        {
            // determine what's appropriate to return here - a unique board id may be appropriate if available
            return 1;
        }
    }
}
