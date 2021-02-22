using System;
using System.Collections.Generic;

using IQ.Game.Poker.Models;
using IQ.Game.Poker.Strategy;
using IQ.Game.Poker.Strategy.Score;
using IQ.Game.Poker.Utils;

namespace IQ.Game.Poker
{
    public class PokerGame
    {
        private IRankingStrategy _rankingStrategy;

        public PokerGame()
        {
            _rankingStrategy = new BasicScoreRankingStrategy();
        }

        public PokerGame(IRankingStrategy rankingStrategy)
        {
            _rankingStrategy = rankingStrategy;
        }

        public void setRankingStrategy(IRankingStrategy rankingStrategy)
        {
            this._rankingStrategy = rankingStrategy;
        }

        public IList<Player> GetWinners(ICollection<Player> players)
        {
            if (players == null || players.Count == 0)
            {
                return new List<Player>();
            }

            if (this._rankingStrategy == null)
            {
                throw new InvalidOperationException($"A ranking strategy is required to get winners!!");
            }

            HandUtils.CheckPlayers(players);

            var winners = _rankingStrategy.GetWinners(players);

            Log(players, winners);

            return winners;
        }

        private static void Log(ICollection<Player> players, IList<Player> winners)
        {
            Console.WriteLine("Players:");
            Console.WriteLine("-----------------------------");
            foreach (var player in players)
            {
                Console.WriteLine(player);
            }
            Console.WriteLine("-----------------------------");

            Console.WriteLine("Winners:");
            Console.WriteLine("-----------------------------");
            foreach (var winner in winners)
            {
                Console.WriteLine(winner);
            }
            Console.WriteLine("-----------------------------");
        }
    }
}
