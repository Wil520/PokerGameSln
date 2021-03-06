using System;
using System.Collections.Generic;
using System.Linq;

using IQ.Game.Poker.Models;
using IQ.Game.Poker.Utils;

namespace IQ.Game.Poker.Strategy.Score
{
    public class BasicScoreRankingStrategy : IRankingStrategy
    {
        public IList<Player> GetWinners(ICollection<Player> players)
        {

            if (players == null || players.Count == 0)
            {
                return new List<Player>();
            }

            HandUtils.CheckPlayers(players);

            foreach (var player in players)
            {
                ScoreCalculator calculator = ScoreCalculator.GetCalculator(player.Cards);
                player.CardsType = calculator.CardsType;
                player.Score = calculator.Calculate(player.Cards);
            }
            
            IOrderedEnumerable<Player> sortedPlayers = players
                .OrderBy(player => player.CardsType)
                .ThenByDescending(player => player.Score);

            Player topPlayer = sortedPlayers.First();

            return sortedPlayers.TakeWhile(player => player.CardsType == topPlayer.CardsType && player.Score == topPlayer.Score).ToList();
        }
    }
}