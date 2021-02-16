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
            _calculatorDict.Add(HandType.Flush, new FlushCalculator());
            _calculatorDict.Add(HandType.ThreeOfAKind, new ThreeOfAKindCalculator());
            _calculatorDict.Add(HandType.OnePair, new OnePairCalculator());
            _calculatorDict.Add(HandType.HighCard, new HighCardCalculator());
        }

        public static ScoreCalculator GetCalculator(List<Card> cards)
        {
            return _calculatorDict[HandUtils.GetHandType(cards)];
        }

        public abstract uint Calculate(List<Card> cards);
    }
}