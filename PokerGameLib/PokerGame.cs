using System;
using System.Collections.Generic;
using IQ.Game.Poker.Models;
using IQ.Game.Poker.Strategy;
using IQ.Game.Poker.Strategy.Score;

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

        public List<Player> GetWinners(List<Player> players)
        {
            if (players == null || players.Count == 0)
            {
                return new List<Player>();

            }

            if (this._rankingStrategy == null)
            {
                throw new Exception($"Ranking strategy cannot be null!!");
            }

            return _rankingStrategy.GetWinners(players);
        }

    }
}
