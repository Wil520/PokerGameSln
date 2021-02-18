using System;
using System.Collections.Generic;
using System.Linq;

using IQ.Game.Poker.Models;
using IQ.Game.Poker.Utils;


namespace IQ.Game.Poker.Strategy.Score
{
    public class HighCardCalculator : ScoreCalculator
    {
        private const uint Weight = 12;
        private static readonly HighCardCalculator _calculator = new HighCardCalculator();
        private HighCardCalculator()
        {
            CardsType = HandType.HighCard;
        }
        public static HighCardCalculator Instance()
        {
            return _calculator;
        }
        public override uint Calculate(IList<Card> cards)
        {
            base.Validate(cards);

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