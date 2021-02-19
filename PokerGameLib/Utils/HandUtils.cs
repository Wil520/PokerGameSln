using System;
using System.Collections.Generic;
using System.Linq;

using IQ.Game.Poker.Models;

namespace IQ.Game.Poker.Utils
{
    public static class HandUtils
    {
        public const int CardNumber = 5;
        public static HandType GetHandType(IList<Card> cards)
        {

            if (cards == null || cards.Count == 0)
            {
                throw new ArgumentException($"No cards!!", "cards");
            }

            if (cards.Count != CardNumber)
            {
                throw new ArgumentException($"Card number must be {CardNumber}!!", "cards");
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

        public static void CheckPlayers(ICollection<Player> players)
        {
            if (players == null || players.Count == 0) return;

            var cardList = new List<Card>();
            foreach (var player in players)
            {
                CheckPlayer(player);
                cardList.AddRange(player.Cards);
            }

            IEnumerable<Card> duplicatedCards = cardList.GroupBy(card => card, (card, cards) => new
            {
                Card = card,
                Count = cards.Count()
            }).Where(cc => cc.Count > 1)
              .Select(cc => cc.Card);

            if (duplicatedCards.Count() > 0)
            {
                throw new ArgumentException($"Duplicated cards: {string.Join(", ", duplicatedCards)}!!", "Cards");
            }
        }

        private static void CheckPlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentException($"Player cannot be null!!", "Player");
            }
            if (string.IsNullOrWhiteSpace(player.PlayerName))
            {
                throw new ArgumentException($"Player must have a name!!", "PlayerName");
            }
            if (player.Cards == null || player.Cards.Count == 0)
            {
                throw new ArgumentException($"Player {player.PlayerName} has no cards!!", "Cards");
            }
            if (player.Cards.Count != CardNumber)
            {
                throw new ArgumentException($"Player {player.PlayerName} must have {CardNumber} cards!!", "Cards");
            }
        }
    }
}