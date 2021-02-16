using System;
using System.Collections.Generic;
using System.Linq;

using IQ.Game.Poker.Models;

namespace IQ.Game.Poker.Utils
{
    public static class HandUtils
    {
        public static HandType GetHandType(List<Card> cards)
        {

            if (cards == null || cards.Count == 0)
            {
                throw new Exception($"No cards!!");
            }

            if (cards.Count != 5)
            {
                throw new Exception($"Card number must be 5!!");
            }

            Card firstCard = cards.First();
            bool isFlush = cards.All(card => card.Suit == firstCard.Suit);
            if (isFlush)
            {
                return HandType.Flush;
            }

            var countByRank = cards.GroupBy(card => card.Rank, (rank, cds) => new
            {
                Rank = rank,
                Count = cds.Count()
            }).OrderByDescending(elem => elem.Count)
            .ThenByDescending(elem => elem.Rank);

            var topCountByRank = countByRank.First();        

            if (topCountByRank.Count == 4)
            {
                throw new NotImplementedException($"Four of a Kind not implemented!!");
            }
            
            bool isThreeOfAKind = topCountByRank.Count == 3;
            if (isThreeOfAKind)
            {
                return HandType.ThreeOfAKind;
            }

            if (topCountByRank.Count == 2)
            {
                if (countByRank.Count() == 3)
                {
                    throw new NotImplementedException($"Two Pair not implemented!!");
                }
                return HandType.OnePair;
            }

            return HandType.HighCard;
        }
    }
}