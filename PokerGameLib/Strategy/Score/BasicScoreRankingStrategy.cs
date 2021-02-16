using System;
using System.Collections.Generic;
using System.Linq;

using IQ.Game.Poker.Models;

namespace IQ.Game.Poker.Strategy.Score
{
    public class BasicScoreRankingStrategy : IRankingStrategy
    {
        public List<Player> GetWinners(List<Player> players)
        {

            if (players == null || players.Count == 0)
            {
                return new List<Player>();

            }

            foreach (var player in players)
            {
                ScoreCalculator calculator = ScoreCalculator.GetCalculator(player.Cards);
                player.CardsType = calculator.CardsType;
                player.Score = calculator.Calculate(player.Cards);

                Console.WriteLine($"Player name: {player.PlayerName}, player hand type: {player.CardsType}, player score: {player.Score}.");
            }

            IOrderedEnumerable<Player> sortedPlayers = players
                .OrderBy(player => player.CardsType)
                .ThenByDescending(player => player.Score);

            Player topPlayer = sortedPlayers.First();

            return sortedPlayers.TakeWhile(player => player.CardsType == topPlayer.CardsType && player.Score == topPlayer.Score).ToList();
        }
    }
}