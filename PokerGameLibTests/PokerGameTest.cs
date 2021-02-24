using System;
using Xunit;
using System.Collections.Generic;

using IQ.Game.Poker.Models;
using IQ.Game.Poker.Utils;
using IQ.Game.Poker.Test.Utils;
using IQ.Game.Poker.Strategy.Score;

namespace IQ.Game.Poker.Test
{
    public class PokerGameTest
    {

        [Fact]
        public void Ranking_strategy_cannot_be_null()
        {
            //Arrange, Act and Assert
            var expectedMessage = "Value cannot be null. (Parameter 'rankingStrategy')";
            var ex1 = Assert.Throws<ArgumentNullException>(() => new PokerGame(null));
            Assert.Equal(expectedMessage, ex1.Message);

            //Arrange, Act and Assert
            PokerGame pg = new PokerGame();
            var ex2 = Assert.Throws<ArgumentNullException>(() => pg.setRankingStrategy(null));
            Assert.Equal(expectedMessage, ex2.Message);

            new HandCreator().NextHighCard();
        }

        [Fact]
        public void Player_cannot_be_null()
        {
            //Arrange
            PokerGame pg = new PokerGame();

            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Spades, CardRank.King),
                (CardSuit.Clubs, CardRank.Three),
                (CardSuit.Clubs, CardRank.Four),
                (CardSuit.Clubs, CardRank.Five),
                (CardSuit.Clubs, CardRank.Six)
            });

            var players = new List<Player> { player1, null };

            //Act and Assert
            var ex = Assert.Throws<ArgumentException>(() => pg.GetWinners(players));
            Assert.Equal("Player cannot be null!! (Parameter 'Player')", ex.Message);
        }

        [Fact]
        public void Player_must_have_a_name()
        {
            //Arrange, PlayerName is null
            PokerGame pg = new PokerGame();

            Player player1 = HandCreator.CreatePlayer(null, new (CardSuit, CardRank)[]
            {
                (CardSuit.Spades, CardRank.King),
                (CardSuit.Clubs, CardRank.Three),
                (CardSuit.Clubs, CardRank.Four),
                (CardSuit.Clubs, CardRank.Five),
                (CardSuit.Clubs, CardRank.Six)
            });

            var players = new List<Player> { player1 };

            //Act and Assert
            var expectedMessage = "Player must have a name!! (Parameter 'Player.PlayerName')";
            var ex1 = Assert.Throws<ArgumentException>(() => pg.GetWinners(players));
            Assert.Equal(expectedMessage, ex1.Message);

            //Arrange, PlayerName is empty
            Player player2 = HandCreator.CreatePlayer("", new (CardSuit, CardRank)[]
            {
                (CardSuit.Spades, CardRank.King),
                (CardSuit.Clubs, CardRank.Three),
                (CardSuit.Clubs, CardRank.Four),
                (CardSuit.Clubs, CardRank.Five),
                (CardSuit.Clubs, CardRank.Six)
            });

            players = new List<Player> { player2 };

            //Act and Assert
            var ex2 = Assert.Throws<ArgumentException>(() => pg.GetWinners(players));
            Assert.Equal(expectedMessage, ex2.Message);

            //Arrange, PlayerName is blank
            Player player3 = HandCreator.CreatePlayer("  ", new (CardSuit, CardRank)[]
            {
                (CardSuit.Spades, CardRank.King),
                (CardSuit.Clubs, CardRank.Three),
                (CardSuit.Clubs, CardRank.Four),
                (CardSuit.Clubs, CardRank.Five),
                (CardSuit.Clubs, CardRank.Six)
            });

            players = new List<Player> { player3 };

            //Act and Assert
            var ex3 = Assert.Throws<ArgumentException>(() => pg.GetWinners(players));
            Assert.Equal(expectedMessage, ex3.Message);
        }

        [Fact]
        public void Player_must_have_cards()
        {
            //Arrange
            PokerGame pg = new PokerGame();

            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[] { });

            var players = new List<Player> { player1 };

            //Act and Assert
            var ex = Assert.Throws<ArgumentException>(() => pg.GetWinners(players));
            Assert.Equal("Player player1 has no cards!! (Parameter 'Player.Cards')", ex.Message);
        }

        [Fact]
        public void Player_card_number_must_be_equal_to_five()
        {
            //Arrange
            PokerGame pg = new PokerGame();

            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Spades, CardRank.King),
                (CardSuit.Clubs, CardRank.Three),
                (CardSuit.Clubs, CardRank.Four),
                (CardSuit.Clubs, CardRank.Six)
            });

            var players = new List<Player> { player1 };

            //Act and Assert
            var ex = Assert.Throws<ArgumentException>(() => pg.GetWinners(players));
            Assert.Equal("Player player1 must have 5 cards!! (Parameter 'Player.Cards')", ex.Message);
        }

        [Fact]
        public void Player_cannot_have_invalid_card_suit()
        {
            //Arrange
            PokerGame pg = new PokerGame();

            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Spades, CardRank.King),
                (CardSuit.Clubs, CardRank.Three),
                (CardSuit.Clubs, CardRank.Four),
                (CardSuit.Clubs, CardRank.Five),
                ((CardSuit)4, CardRank.Six)
            });

            var players = new List<Player> { player1 };

            //Act and Assert
            var ex = Assert.Throws<ArgumentException>(() => pg.GetWinners(players));
            Assert.Equal("Invalid CardSuit: 4!! (Parameter 'Card.Suit')", ex.Message);
        }

        [Fact]
        public void Player_cannot_have_invalid_card_rank()
        {
            //Arrange
            PokerGame pg = new PokerGame();

            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Spades, CardRank.King),
                (CardSuit.Clubs, CardRank.Three),
                (CardSuit.Clubs, CardRank.Four),
                (CardSuit.Clubs, CardRank.Five),
                (CardSuit.Clubs, (CardRank)15)
            });

            var players = new List<Player> { player1 };

            //Act and Assert
            var ex = Assert.Throws<ArgumentException>(() => pg.GetWinners(players));
            Assert.Equal("Invalid CardRank: 15!! (Parameter 'Card.Rank')", ex.Message);
        }

        [Fact]
        public void Players_cannot_have_duplicated_cards()
        {
            //Arrange
            PokerGame pg = new PokerGame();

            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Nine),    //duplicated
                (CardSuit.Hearts, CardRank.Eight),
                (CardSuit.Spades, CardRank.Seven),
                (CardSuit.Diamonds, CardRank.Six)
            });

            Player player4 = HandCreator.CreatePlayer("player4", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Ace),
                (CardSuit.Clubs, CardRank.King),
                (CardSuit.Hearts, CardRank.Queen),
                (CardSuit.Spades, CardRank.Jack),
                (CardSuit.Clubs, CardRank.Nine)     //duplicated
            });

            var players = new List<Player> { player3, player4 };

            //Act and Assert
            var ex = Assert.Throws<ArgumentException>(() => pg.GetWinners(players));
            Assert.Equal("Duplicated cards: C9!! (Parameter 'Player.Cards')", ex.Message);
        }

        [Fact]
        public void FourOfAKind_not_implemented()
        {
            //Arrange
            PokerGame pg = new PokerGame();
            Player player2 = HandCreator.CreatePlayer("player2", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Seven),
                (CardSuit.Clubs, CardRank.Seven),
                (CardSuit.Hearts, CardRank.Seven),
                (CardSuit.Hearts, CardRank.Six),
                (CardSuit.Diamonds, CardRank.Ace)
            });
            //FourOfAKind
            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Nine),
                (CardSuit.Hearts, CardRank.Nine),
                (CardSuit.Spades, CardRank.Nine),
                (CardSuit.Diamonds, CardRank.King)
            });

            var players = new List<Player> { player2, player3 };

            //Act and Assert
            var ex = Assert.Throws<NotImplementedException>(() => pg.GetWinners(players));
            Assert.Equal("Four of a Kind not implemented!!", ex.Message);
        }

        [Fact]
        public void TwoPair_not_implemented()
        {
            //Arrange
            PokerGame pg = new PokerGame();
            //Two Pair
            Player player2 = HandCreator.CreatePlayer("player2", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Seven),
                (CardSuit.Clubs, CardRank.Seven),
                (CardSuit.Hearts, CardRank.Five),
                (CardSuit.Spades, CardRank.Five),
                (CardSuit.Diamonds, CardRank.Ace)
            });
            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Nine),
                (CardSuit.Hearts, CardRank.Jack),
                (CardSuit.Spades, CardRank.Ten),
                (CardSuit.Diamonds, CardRank.King)
            });

            var players = new List<Player> { player2, player3 };

            //Act and Assert
            var ex = Assert.Throws<NotImplementedException>(() => pg.GetWinners(players));
            Assert.Equal("Two Pair not implemented!!", ex.Message);
        }


        [Fact]
        public void No_players_returns_empty_collection()
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
        public void Only_one_player_returns_the_player()
        {
            //Arrange
            PokerGame pg = new PokerGame();

            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Spades, CardRank.King),
                (CardSuit.Clubs, CardRank.Three),
                (CardSuit.Clubs, CardRank.Four),
                (CardSuit.Clubs, CardRank.Five),
                (CardSuit.Clubs, CardRank.Six)
            });

            var players = new List<Player> { player1 };

            //Act
            var winners = pg.GetWinners(players);

            //Assert
            Assert.Single(winners);
            Assert.Equal("player1", winners[0].PlayerName);
            Assert.Equal(HandType.HighCard, winners[0].CardsType);
        }

        [Fact]
        public void Random_Flush_beats_ThreeOfAKind_OnePair_HighCard()
        {
            PokerGame pg = new PokerGame();

            for (int i = 0; i < 5; i++)
            {
                //Arrange
                HandCreator handCreator = new HandCreator();
                //Flush
                Player player1 = new Player("player1", handCreator.NextFlush());
                //ThreeOfAKind
                Player player2 = new Player("player2", handCreator.NextThreeOfAKind());
                //OnePair
                Player player3 = new Player("player3", handCreator.NextOnePair());
                //HighCard
                Player player4 = new Player("player4", handCreator.NextHighCard());

                var players = new List<Player> { player1, player2, player3, player4 };

                //Act
                var winners = pg.GetWinners(players);

                //Assert
                Assert.Single(winners);
                Assert.Equal("player1", winners[0].PlayerName);
                Assert.Equal(HandType.Flush, winners[0].CardsType);
            }
        }

        [Fact]
        public void Random_ThreeOfAKind_beats_OnePair_HighCard()
        {
            PokerGame pg = new PokerGame();

            for (int i = 0; i < 5; i++)
            {
                //Arrange
                HandCreator handCreator = new HandCreator();

                //ThreeOfAKind
                Player player2 = new Player("player2", handCreator.NextThreeOfAKind());
                //OnePair
                Player player3 = new Player("player3", handCreator.NextOnePair());
                //HighCard
                Player player4 = new Player("player4", handCreator.NextHighCard());

                var players = new List<Player> { player2, player3, player4 };

                //Act
                var winners = pg.GetWinners(players);

                //Assert
                Assert.Single(winners);
                Assert.Equal("player2", winners[0].PlayerName);
                Assert.Equal(HandType.ThreeOfAKind, winners[0].CardsType);
            }
        }

        [Fact]
        public void Random_OnePair_beats_HighCard()
        {
            PokerGame pg = new PokerGame();

            for (int i = 0; i < 5; i++)
            {
                //Arrange
                HandCreator handCreator = new HandCreator();

                //OnePair
                Player player3 = new Player("player3", handCreator.NextOnePair());
                //HighCard
                Player player4 = new Player("player4", handCreator.NextHighCard());

                var players = new List<Player> { player3, player4 };

                //Act
                var winners = pg.GetWinners(players);

                //Assert
                Assert.Single(winners);
                Assert.Equal("player3", winners[0].PlayerName);
                Assert.Equal(HandType.OnePair, winners[0].CardsType);
            }
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
            //Arrange
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
            //Arrange
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

        [Fact]
        public void ThreeOfAKind_three_players()
        {
            //Arrange
            PokerGame pg = new PokerGame();
            Player player2 = HandCreator.CreatePlayer("player2", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Seven),
                (CardSuit.Clubs, CardRank.Seven),
                (CardSuit.Hearts, CardRank.Seven),
                (CardSuit.Hearts, CardRank.Six),
                (CardSuit.Diamonds, CardRank.Ace)
            });
            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Nine),
                (CardSuit.Hearts, CardRank.Nine),
                (CardSuit.Spades, CardRank.Seven),
                (CardSuit.Diamonds, CardRank.King)
            });
            Player player4 = HandCreator.CreatePlayer("player4", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Jack),
                (CardSuit.Clubs, CardRank.Jack),
                (CardSuit.Hearts, CardRank.Jack),
                (CardSuit.Spades, CardRank.Three),
                (CardSuit.Hearts, CardRank.Two)
            });

            var players = new List<Player> { player2, player3, player4 };

            //Act
            var winners = pg.GetWinners(players);

            //Assert
            Assert.Single(winners);
            Assert.Equal("player4", winners[0].PlayerName);
            Assert.Equal(HandType.ThreeOfAKind, winners[0].CardsType);
        }

        [Fact]
        public void ThreeOfAKind_four_players()
        {
            //Arrange
            PokerGame pg = new PokerGame();
            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.King),
                (CardSuit.Clubs, CardRank.King),
                (CardSuit.Hearts, CardRank.King),
                (CardSuit.Spades, CardRank.Ace),
                (CardSuit.Hearts, CardRank.Queen)
            });
            Player player2 = HandCreator.CreatePlayer("player2", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Ace),
                (CardSuit.Clubs, CardRank.Ace),
                (CardSuit.Hearts, CardRank.Ace),
                (CardSuit.Hearts, CardRank.Two),
                (CardSuit.Diamonds, CardRank.Three)
            });
            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Nine),
                (CardSuit.Hearts, CardRank.Nine),
                (CardSuit.Spades, CardRank.Seven),
                (CardSuit.Spades, CardRank.King)
            });
            Player player4 = HandCreator.CreatePlayer("player4", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Jack),
                (CardSuit.Clubs, CardRank.Jack),
                (CardSuit.Hearts, CardRank.Jack),
                (CardSuit.Spades, CardRank.Ten),
                (CardSuit.Hearts, CardRank.Eight)
            });

            var players = new List<Player> { player1, player2, player3, player4 };

            //Act
            var winners = pg.GetWinners(players);

            //Assert
            Assert.Single(winners);
            Assert.Equal("player2", winners[0].PlayerName);
            Assert.Equal(HandType.ThreeOfAKind, winners[0].CardsType);
        }

        [Fact]
        public void OnePair_one_winner()
        {
            //Arrange
            PokerGame pg = new PokerGame();
            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Nine),
                (CardSuit.Hearts, CardRank.Ten),
                (CardSuit.Spades, CardRank.Seven),
                (CardSuit.Spades, CardRank.King)
            });
            Player player2 = HandCreator.CreatePlayer("player2", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Ace),
                (CardSuit.Clubs, CardRank.Ace),
                (CardSuit.Hearts, CardRank.Four),
                (CardSuit.Hearts, CardRank.Two),
                (CardSuit.Diamonds, CardRank.Three)
            });
            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.King),
                (CardSuit.Clubs, CardRank.King),
                (CardSuit.Spades, CardRank.Jack),
                (CardSuit.Spades, CardRank.Ace),
                (CardSuit.Hearts, CardRank.Queen)
            });
            Player player4 = HandCreator.CreatePlayer("player4", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Jack),
                (CardSuit.Clubs, CardRank.Queen),
                (CardSuit.Hearts, CardRank.Jack),
                (CardSuit.Spades, CardRank.Ten),
                (CardSuit.Hearts, CardRank.Ace)
            });

            var players = new List<Player> { player1, player2, player3, player4 };

            //Act
            var winners = pg.GetWinners(players);

            //Assert
            Assert.Single(winners);
            Assert.Equal("player2", winners[0].PlayerName);
            Assert.Equal(HandType.OnePair, winners[0].CardsType);
        }

        [Fact]
        public void OnePair_two_winners()
        {
            //Arrange
            PokerGame pg = new PokerGame();
            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Nine),
                (CardSuit.Hearts, CardRank.Queen),
                (CardSuit.Spades, CardRank.Jack),
                (CardSuit.Spades, CardRank.King)
            });
            Player player2 = HandCreator.CreatePlayer("player2", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Ten),
                (CardSuit.Clubs, CardRank.Ten),
                (CardSuit.Hearts, CardRank.Four),
                (CardSuit.Hearts, CardRank.Seven),
                (CardSuit.Diamonds, CardRank.Eight)
            });
            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
            {
                (CardSuit.Hearts, CardRank.Ten),
                (CardSuit.Spades, CardRank.Ten),
                (CardSuit.Spades, CardRank.Seven),
                (CardSuit.Spades, CardRank.Four),
                (CardSuit.Hearts, CardRank.Eight)
            });
            Player player4 = HandCreator.CreatePlayer("player4", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.King),
                (CardSuit.Clubs, CardRank.Ace),
                (CardSuit.Hearts, CardRank.Five),
                (CardSuit.Spades, CardRank.Queen),
                (CardSuit.Diamonds, CardRank.Five)
            });

            var players = new List<Player> { player1, player2, player3, player4 };

            //Act
            var winners = pg.GetWinners(players);

            //Assert
            Assert.Collection(winners,
                item =>
                {
                    Assert.Equal("player2", item.PlayerName);
                    Assert.Equal(HandType.OnePair, item.CardsType);
                },
                item =>
                {
                    Assert.Equal("player3", item.PlayerName);
                    Assert.Equal(HandType.OnePair, item.CardsType);
                });
        }

        [Fact]
        public void HighCard_one_winner()
        {
            //Arrange
            PokerGame pg = new PokerGame();

            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Spades, CardRank.King),
                (CardSuit.Clubs, CardRank.Three),
                (CardSuit.Clubs, CardRank.Four),
                (CardSuit.Clubs, CardRank.Five),
                (CardSuit.Clubs, CardRank.Six)
            });

            Player player2 = HandCreator.CreatePlayer("player2", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Two),
                (CardSuit.Hearts, CardRank.Three),
                (CardSuit.Diamonds, CardRank.Four),
                (CardSuit.Diamonds, CardRank.Queen),
                (CardSuit.Diamonds, CardRank.King)
            });

            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
           {
                (CardSuit.Hearts, CardRank.Nine),
                (CardSuit.Hearts, CardRank.Ten),
                (CardSuit.Clubs, CardRank.Jack),
                (CardSuit.Hearts, CardRank.Queen),
                (CardSuit.Hearts, CardRank.King)
           });

            Player player4 = HandCreator.CreatePlayer("player4", new (CardSuit, CardRank)[]
            {
                (CardSuit.Spades, CardRank.Two),
                (CardSuit.Spades, CardRank.Three),
                (CardSuit.Spades, CardRank.Four),
                (CardSuit.Hearts, CardRank.Five),
                (CardSuit.Spades, CardRank.Ace)
            });

            var players = new List<Player> { player1, player2, player3, player4 };

            //Act
            var winners = pg.GetWinners(players);

            //Assert
            Assert.Single(winners);
            Assert.Equal("player4", winners[0].PlayerName);
            Assert.Equal(HandType.HighCard, winners[0].CardsType);
        }

        [Fact]
        public void HighCard_two_winners()
        {
            //Arrange
            PokerGame pg = new PokerGame();

            Player player1 = HandCreator.CreatePlayer("player1", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Eight),
                (CardSuit.Clubs, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Jack),
                (CardSuit.Clubs, CardRank.Queen),
                (CardSuit.Clubs, CardRank.King)
            });

            Player player2 = HandCreator.CreatePlayer("player2", new (CardSuit, CardRank)[]
            {
                (CardSuit.Hearts, CardRank.Eight),
                (CardSuit.Diamonds, CardRank.Ten),
                (CardSuit.Diamonds, CardRank.Jack),
                (CardSuit.Diamonds, CardRank.Queen),
                (CardSuit.Diamonds, CardRank.King)
            });

            Player player3 = HandCreator.CreatePlayer("player3", new (CardSuit, CardRank)[]
           {
                (CardSuit.Hearts, CardRank.Nine),
                (CardSuit.Clubs, CardRank.Ten),
                (CardSuit.Hearts, CardRank.Jack),
                (CardSuit.Hearts, CardRank.Queen),
                (CardSuit.Hearts, CardRank.King)
           });

            Player player4 = HandCreator.CreatePlayer("player4", new (CardSuit, CardRank)[]
            {
                (CardSuit.Diamonds, CardRank.Nine),
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
                    Assert.Equal(HandType.HighCard, item.CardsType);
                },
                item =>
                {
                    Assert.Equal("player4", item.PlayerName);
                    Assert.Equal(HandType.HighCard, item.CardsType);
                });
        }
    }
}
