using System;
using System.Collections.Generic;
using System.Linq;

using IQ.Game.Poker.Models;
using IQ.Game.Poker.Utils;

namespace IQ.Game.Poker.Strategy.Score
{
    public class FlushCalculator : ScoreCalculator
    {
        private const uint Weight = 12;

        public FlushCalculator()
        {
            CardsType = HandType.Flush;
        }

        public override uint Calculate(List<Card> cards)
        {
            if (HandUtils.GetHandType(cards) != HandType.Flush)
            {
                throw new Exception($"Invalid HandType!!");
            }

            uint score = 0;
            List<Card> sortedCards = cards.OrderByDescending(card => card.Rank).ToList();
            for (int i = 0; i < sortedCards.Count; i++)
            {
                score += (uint)((uint)(sortedCards[i].Rank) * Math.Pow(Weight, sortedCards.Count - i - 1));

            }

            return score;
        }
    }
}