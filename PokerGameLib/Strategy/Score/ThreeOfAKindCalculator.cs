using System;
using System.Collections.Generic;
using System.Linq;

using IQ.Game.Poker.Models;
using IQ.Game.Poker.Utils;

namespace IQ.Game.Poker.Strategy.Score
{
    public class ThreeOfAKindCalculator : ScoreCalculator
    {
        private static readonly ThreeOfAKindCalculator _calculator = new ThreeOfAKindCalculator();

        private ThreeOfAKindCalculator()
        {
            CardsType = HandType.ThreeOfAKind;
        }

        public static ThreeOfAKindCalculator Instance()
        {
            return _calculator;
        }
        public override uint Calculate(List<Card> cards)
        {
            base.Validate(cards);

            var topCountByRank = cards.GroupBy(card => card.Rank, (rank, cds) => new
            {
                Rank = rank,
                Count = cds.Count()
            }).OrderByDescending(elem => elem.Count)
            .ThenByDescending(elem => elem.Rank).First();

            return (uint)topCountByRank.Rank;
        }
    }
}