using System.Collections.Generic;
using IQ.Game.Poker.Models;

namespace IQ.Game.Poker.Strategy
{
    public interface IRankingStrategy
    {
        IList<Player> GetWinners(ICollection<Player> players);
    }
}