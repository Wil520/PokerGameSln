using System;
using Xunit;
using System.Collections.Generic;

using IQ.Game.Poker.Models;
using IQ.Game.Poker.Utils;
using IQ.Game.Poker.Test.Utils;

namespace IQ.Game.Poker.Test
{
    public class PokerGameTest
    {
        [Fact]
        public void NoPlayers_returns_empty_collection()
        {
            //Arrange
            PokerGame pg = new PokerGame();
            //Act
            var winners = pg.GetWinners(null);
            //Assert
            Assert.Empty(winners);

            //Act
            winners = pg.GetWinners(new List<Player>());
            //Assert
            Assert.Empty(winners);
        }

        [Fact]
        public void Flush_beats_ThreeOfAKind_OnePair_HighCard()
        {
            //Arrange
            PokerGame pg = new PokerGame();
            //Flush
            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Clubs, CardRank.Two),
                (CardSuit.Clubs, CardRank.Three),
                (CardSuit.Clubs, CardRank.Four),
                (CardSuit.Clubs, CardRank.Five),
                (CardSuit.Clubs, CardRank.Six)
            });
            //ThreeOfAKind
            Player player2 = HandCreator.CreatePlayer("player2", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Seven),
                (CardSuit.Clubs, CardRank.Seven),
                (CardSuit.Hearts, CardRank.Seven),
                (CardSuit.Hearts, CardRank.Six),
                (CardSuit.Diamonds, CardRank.Five)
            });
            //OnePair
            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Nine),
                (CardSuit.Hearts, CardRank.Eight),
                (CardSuit.Spades, CardRank.Seven),
                (CardSuit.Diamonds, CardRank.Six)
            });
            //HighCard
            Player player4 = HandCreator.CreatePlayer("player4", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Ace),
                (CardSuit.Clubs, CardRank.King),
                (CardSuit.Hearts, CardRank.Queen),
                (CardSuit.Spades, CardRank.Jack),
                (CardSuit.Hearts, CardRank.Ten)
            });

            var players = new List<Player> { player1, player2, player3, player4 };

            //Act
            var winners = pg.GetWinners(players);

            //Assert
            Assert.Single(winners);
            Assert.Equal("player1", winners[0].PlayerName);
            Assert.Equal(HandType.Flush, winners[0].CardsType);

        }

        [Fact]
        public void ThreeOfAKind_beats_OnePair_HighCard()
        {
            //Arrange
            PokerGame pg = new PokerGame();
            //ThreeOfAKind
            Player player2 = HandCreator.CreatePlayer("player2", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Seven),
                (CardSuit.Clubs, CardRank.Seven),
                (CardSuit.Hearts, CardRank.Seven),
                (CardSuit.Hearts, CardRank.Six),
                (CardSuit.Diamonds, CardRank.Five)
            });
            //OnePair
            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Nine),
                (CardSuit.Hearts, CardRank.Eight),
                (CardSuit.Spades, CardRank.Seven),
                (CardSuit.Diamonds, CardRank.Six)
            });
            //HighCard
            Player player4 = HandCreator.CreatePlayer("player4", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Ace),
                (CardSuit.Clubs, CardRank.King),
                (CardSuit.Hearts, CardRank.Queen),
                (CardSuit.Spades, CardRank.Jack),
                (CardSuit.Hearts, CardRank.Ten)
            });

            var players = new List<Player> { player2, player3, player4 };

            //Act
            var winners = pg.GetWinners(players);

            //Assert
            Assert.Single(winners);
            Assert.Equal("player2", winners[0].PlayerName);
            Assert.Equal(HandType.ThreeOfAKind, winners[0].CardsType);

        }

        [Fact]
        public void OnePair_beats_HighCard()
        {
            //Arrange
            PokerGame pg = new PokerGame();
            //OnePair
            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Nine),
                (CardSuit.Hearts, CardRank.Eight),
                (CardSuit.Spades, CardRank.Seven),
                (CardSuit.Diamonds, CardRank.Six)
            });
            //HighCard
            Player player4 = HandCreator.CreatePlayer("player4", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Ace),
                (CardSuit.Clubs, CardRank.King),
                (CardSuit.Hearts, CardRank.Queen),
                (CardSuit.Spades, CardRank.Jack),
                (CardSuit.Hearts, CardRank.Ten)
            });

            var players = new List<Player> { player3, player4 };

            //Act
            var winners = pg.GetWinners(players);

            //Assert
            Assert.Single(winners);
            Assert.Equal("player3", winners[0].PlayerName);
            Assert.Equal(HandType.OnePair, winners[0].CardsType);
        }

        [Fact]
        public void Flush_one_winner()
        {
            PokerGame pg = new PokerGame();

            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Clubs, CardRank.Two),
                (CardSuit.Clubs, CardRank.Three),
                (CardSuit.Clubs, CardRank.Four),
                (CardSuit.Clubs, CardRank.Five),
                (CardSuit.Clubs, CardRank.Six)
            });

            Player player2 = HandCreator.CreatePlayer("player2", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Two),
                (CardSuit.Diamonds, CardRank.Three),
                (CardSuit.Diamonds, CardRank.Four),
                (CardSuit.Diamonds, CardRank.Queen),
                (CardSuit.Diamonds, CardRank.King)
            });

            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
           {
                (CardSuit.Hearts, CardRank.Nine),
                (CardSuit.Hearts, CardRank.Ten),
                (CardSuit.Hearts, CardRank.Jack),
                (CardSuit.Hearts, CardRank.Queen),
                (CardSuit.Hearts, CardRank.King)
           });

            Player player4 = HandCreator.CreatePlayer("player4", new (CardSuit, CardRank)[]
            {
                (CardSuit.Spades, CardRank.Two),
                (CardSuit.Spades, CardRank.Three),
                (CardSuit.Spades, CardRank.Four),
                (CardSuit.Spades, CardRank.Five),
                (CardSuit.Spades, CardRank.Ace)
            });

            var players = new List<Player> { player1, player2, player3, player4 };

            //Act
            var winners = pg.GetWinners(players);

            //Assert
            Assert.Single(winners);
            Assert.Equal("player4", winners[0].PlayerName);
            Assert.Equal(HandType.Flush, winners[0].CardsType);
        }

        [Fact]
        public void Flush_two_winners()
        {
            PokerGame pg = new PokerGame();

            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Clubs, CardRank.Eight),
                (CardSuit.Clubs, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Jack),
                (CardSuit.Clubs, CardRank.Queen),
                (CardSuit.Clubs, CardRank.King)
            });

            Player player2 = HandCreator.CreatePlayer("player2", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Eight),
                (CardSuit.Diamonds, CardRank.Ten),
                (CardSuit.Diamonds, CardRank.Jack),
                (CardSuit.Diamonds, CardRank.Queen),
                (CardSuit.Diamonds, CardRank.King)
            });

            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
           {
                (CardSuit.Hearts, CardRank.Nine),
                (CardSuit.Hearts, CardRank.Ten),
                (CardSuit.Hearts, CardRank.Jack),
                (CardSuit.Hearts, CardRank.Queen),
                (CardSuit.Hearts, CardRank.King)
           });

            Player player4 = HandCreator.CreatePlayer("player4", new (CardSuit, CardRank)[]
            {
                (CardSuit.Spades, CardRank.Nine),
                (CardSuit.Spades, CardRank.Ten),
                (CardSuit.Spades, CardRank.Jack),
                (CardSuit.Spades, CardRank.Queen),
                (CardSuit.Spades, CardRank.King)
            });

            var players = new List<Player> { player1, player2, player3, player4 };

            //Act
            var winners = pg.GetWinners(players);

            //Assert
            Assert.Collection(winners,
                item =>
                {
                    Assert.Equal("player3", item.PlayerName);
                    Assert.Equal(HandType.Flush, item.CardsType);
                },
                item =>
                {
                    Assert.Equal("player4", item.PlayerName);
                    Assert.Equal(HandType.Flush, item.CardsType);
                });
        }
    }
}
