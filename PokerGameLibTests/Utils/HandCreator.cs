using System;
using System.Collections.Generic;
using System.Linq;

using IQ.Game.Poker.Models;
using IQ.Game.Poker.Utils;

namespace IQ.Game.Poker.Test.Utils
{
    public class HandCreator
    {
        private IList<Card> _availableCards;
        public HandCreator()
        {
            InitCards();
        }

        private void InitCards()
        {
            //All cards
            var cardSuits = Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>();
            var cardRanks = Enum.GetValues(typeof(CardRank)).Cast<CardRank>();
            _availableCards = cardSuits.Join(cardRanks, suit => 1, rank => 1, (suit, rank) => new Card(suit, rank)).ToList();

            //Shuffle the cards
            var rand = new Random();
            _availableCards = _availableCards.OrderBy(card => rand.Next()).ToList();

            // LogAvailableCards();
        }

        public IList<Card> NextFlush()
        {
            if (_availableCards.Count() < HandUtils.CardNumber) return new List<Card>();

            var cardsBySuit = _availableCards.GroupBy(card => card.Suit).Where(g => g.Count() >= HandUtils.CardNumber);

            //No five cards of the same suit available.
            if (cardsBySuit.Count() == 0) return new List<Card>();

            CardSuit suit = cardsBySuit.First().Key;
            var flushCards = _availableCards.Where(card => card.Suit == suit).Take(HandUtils.CardNumber).ToList();
            _availableCards = _availableCards.Except(flushCards).ToList();

            // LogAvailableCards();

            return flushCards;
        }

        public IList<Card> NextThreeOfAKind()
        {
            if (_availableCards.Count() < HandUtils.CardNumber) return new List<Card>();

            var cardsByRank = _availableCards.GroupBy(card => card.Rank).Where(g => g.Count() >= 3);

            //No three cards of the same rank available
            if (cardsByRank.Count() == 0) return new List<Card>();

            CardRank rank = cardsByRank.First().Key;
            //The three cards of the same rank
            var threeOfAKindCards = _availableCards.Where(card => card.Rank == rank).Take(3).ToList();
            //The other two cards
            var twoCards = _availableCards.Where(card => card.Rank != rank).Take(2);

            if (twoCards.Count() != 2) return new List<Card>();

            threeOfAKindCards = threeOfAKindCards.Concat(twoCards).ToList();
            _availableCards = _availableCards.Except(threeOfAKindCards).ToList();

            // LogAvailableCards();

            return threeOfAKindCards;
        }
        public IList<Card> NextOnePair()
        {
            if (_availableCards.Count() < HandUtils.CardNumber) return new List<Card>();

            var cardsByRank = _availableCards.GroupBy(card => card.Rank);

            //One Pair requires 4 distinct ranks
            if (cardsByRank.Count() < 4) return new List<Card>();

            cardsByRank = cardsByRank.Where(g => g.Count() >= 2);
            //No two cards of the same rank available
            if (cardsByRank.Count() == 0) return new List<Card>();

            //The rank used to make the One Pair
            CardRank rank = cardsByRank.First().Key;
            //The two cards of the same rank
            var onePairCards = _availableCards.Where(card => card.Rank == rank).Take(2).ToList();
            Card card3 = _availableCards.Where(card => card.Rank != rank).First();
            Card card4 = _availableCards.Where(card => card.Rank != rank && card.Rank != card3.Rank).First();
            Card card5 = _availableCards.Where(card => card.Rank != rank && card.Rank != card3.Rank && card.Rank != card4.Rank).First();

            onePairCards = onePairCards.Concat(new Card[] { card3, card4, card5 }).ToList();
            _availableCards = _availableCards.Except(onePairCards).ToList();

            // LogAvailableCards();

            return onePairCards;
        }
        public IList<Card> NextHighCard()
        {
            if (_availableCards.Count() < HandUtils.CardNumber) return new List<Card>();

            var cardsByRank = _availableCards.GroupBy(card => card.Rank);
            //High Card requires 5 distinct ranks
            if (cardsByRank.Count() < 5) return new List<Card>();

            var cardsBySuit = _availableCards.GroupBy(card => card.Suit);
            //High Card requires at least 2 distinct suits
            if (cardsBySuit.Count() < 2) return new List<Card>();

            IList<Card> highCardCards = new List<Card>();
            foreach (var card in _availableCards)
            {
                if (!highCardCards.Any(c => c.Rank == card.Rank))
                {
                    highCardCards.Add(card);
                    if (highCardCards.Count == HandUtils.CardNumber) break;
                }
            }
            //If all cards have the same suit, need to replace one, otherwise, it would be a Flush
            if (highCardCards.GroupBy(card => card.Suit).Count() == 1)
            {
                CardSuit suit = highCardCards[0].Suit;
                Card cardWithDiffSuit = _availableCards.Where(card => card.Suit != suit).First();
                var query = highCardCards.Where(card => card.Rank == cardWithDiffSuit.Rank);
                //High Card list contains an element that has the same rank as the card that has a different suit
                if (query.Count() > 0)
                {
                    highCardCards.Remove(query.First());
                    highCardCards.Add(cardWithDiffSuit);
                }
                else
                {
                    highCardCards[0] = cardWithDiffSuit;
                }
            }

            _availableCards = _availableCards.Except(highCardCards).ToList();

            // LogAvailableCards();

            return highCardCards;
        }
        public static IList<Card> Create((CardSuit suit, CardRank rank)[] tuples)
        {
            var cards = new List<Card>();
            foreach (var item in tuples)
            {
                cards.Add(new Card(item.suit, item.rank));
            }
            return cards;
        }
        public static Player CreatePlayer(string playerName, (CardSuit suit, CardRank rank)[] tuples)
        {
            return new Player(playerName, Create(tuples));
        }
        private void LogAvailableCards()
        {
            Console.WriteLine($"availableCards: {string.Join(",", _availableCards)}");
        }
    }
}