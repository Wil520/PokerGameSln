using System;
using System.Collections.Generic;

using IQ.Game.Poker.Models;
using IQ.Game.Poker.Utils;

namespace IQ.Game.Poker.Strategy.Score
{
    public abstract class ScoreCalculator
    {
        private static readonly Dictionary<HandType, ScoreCalculator> _calculatorDict
               = new Dictionary<HandType, ScoreCalculator>();

        public HandType CardsType { get; protected set; }

        static ScoreCalculator()
        {
            _calculatorDict.Add(HandType.Flush, FlushCalculator.Instance());
            _calculatorDict.Add(HandType.ThreeOfAKind, ThreeOfAKindCalculator.Instance());
            _calculatorDict.Add(HandType.OnePair, OnePairCalculator.Instance());
            _calculatorDict.Add(HandType.HighCard, HighCardCalculator.Instance());
        }

        public static ScoreCalculator GetCalculator(List<Card> cards)
        {
            return _calculatorDict[HandUtils.GetHandType(cards)];
        }

        protected void Validate(List<Card> cards) {
            if (HandUtils.GetHandType(cards) != CardsType)
            {
                throw new ArgumentException($"Hand type must be {CardsType}!!", "cards");
            }
        }

        public abstract uint Calculate(List<Card> cards);
    }
}