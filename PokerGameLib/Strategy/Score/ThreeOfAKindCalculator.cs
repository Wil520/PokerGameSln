using System;
using System.Collections.Generic;
using System.Linq;

using IQ.Game.Poker.Models;
using IQ.Game.Poker.Utils;

namespace IQ.Game.Poker.Strategy.Score
{
    public class ThreeOfAKindCalculator : ScoreCalculator
    {
        public ThreeOfAKindCalculator()
        {
            CardsType = HandType.ThreeOfAKind;
        }

        public override uint Calculate(List<Card> cards)
        {

            if (HandUtils.GetHandType(cards) != HandType.ThreeOfAKind)
            {
                throw new Exception($"Invalid HandType!!");
            }

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