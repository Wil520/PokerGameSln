using System;
using System.Collections.Generic;
using System.Linq;

using IQ.Game.Poker.Models;
using IQ.Game.Poker.Utils;

namespace IQ.Game.Poker.Strategy.Score
{
    public class OnePairCalculator : ScoreCalculator
    {
        private const uint Weight = 14;
        private static readonly OnePairCalculator _calculator = new OnePairCalculator();
        private OnePairCalculator()
        {
            CardsType = HandType.OnePair;
        }
        public static OnePairCalculator Instance()
        {
            return _calculator;
        }
        public override uint Calculate(ISet<Card> cards)
        {
            base.Validate(cards);

            var countByRankList = cards.GroupBy(card => card.Rank, (rank, cds) => new
            {
                Rank = rank,
                Count = cds.Count()
            }).OrderByDescending(elem => elem.Count)
            .ThenByDescending(elem => elem.Rank).ToList();

            uint score = 0;
            for (int i = 0; i < countByRankList.Count; i++)
            {
                score += (uint)((uint)(countByRankList[i].Rank) * Math.Pow(Weight, countByRankList.Count - i - 1));

            }

            return score;
        }
    }
}