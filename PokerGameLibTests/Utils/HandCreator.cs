using System;
using System.Collections.Generic;

using IQ.Game.Poker.Models;

namespace IQ.Game.Poker.Test.Utils
{
    public class HandCreator
    {
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
    }
}