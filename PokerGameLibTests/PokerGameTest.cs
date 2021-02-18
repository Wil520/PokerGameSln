using System;
using Xunit;
using System.Collections.Generic;

using IQ.Game.Poker.Models;
using IQ.Game.Poker.Utils;

namespace IQ.Game.Poker.Test
{
    public class PokerGameTest
    {
        [Fact]
        public void TestNoPlayers()
        {
            //Arrange
            PokerGame pg = new PokerGame();

            //Act
            IList<Player> winners = pg.GetWinners(null);

            //Assert
            Assert.Empty(winners);
        }

        [Fact]
        public void Test1()
        {
            //Arrange
            PokerGame pg = new PokerGame();
            //Flush
            ISet<Card> player1Cards = new HashSet<Card> { new Card(CardSuit.Clubs, CardRank.Two), new Card(CardSuit.Clubs, CardRank.Three), new Card(CardSuit.Clubs, CardRank.Four), new Card(CardSuit.Clubs, CardRank.Five), new Card(CardSuit.Clubs, CardRank.Six) };
            Player player1 = new Player("player1", player1Cards);
            //ThreeOfAKind
            ISet<Card> player2Cards = new HashSet<Card> { new Card(CardSuit.Diamonds, CardRank.Seven), new Card(CardSuit.Clubs, CardRank.Seven), new Card(CardSuit.Hearts, CardRank.Seven), new Card(CardSuit.Clubs, CardRank.Six), new Card(CardSuit.Diamonds, CardRank.Six) };
            Player player2 = new Player("player2", player2Cards);
            //OnePair
            ISet<Card> player3Cards = new HashSet<Card> { new Card(CardSuit.Diamonds, CardRank.Eight), new Card(CardSuit.Clubs, CardRank.Eight), new Card(CardSuit.Hearts, CardRank.Seven), new Card(CardSuit.Spades, CardRank.Six), new Card(CardSuit.Diamonds, CardRank.Five) };
            Player player3 = new Player("player3", player3Cards);
            //HighCard
            ISet<Card> player4Cards = new HashSet<Card> { new Card(CardSuit.Diamonds, CardRank.Ace), new Card(CardSuit.Clubs, CardRank.King), new Card(CardSuit.Hearts, CardRank.Queen), new Card(CardSuit.Spades, CardRank.Jack), new Card(CardSuit.Hearts, CardRank.Ten) };
            Player player4 = new Player("player4", player4Cards);

            IList<Player> players = new List<Player> { player1, player2, player3, player4 };

            //Act
            IList<Player> winners = pg.GetWinners(players);

            //Assert
            Assert.Single(winners);
            Assert.Equal("player1", winners[0].PlayerName);
            Assert.Equal(HandType.Flush, winners[0].CardsType);
        }
    }
}
