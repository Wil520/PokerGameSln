using System.Collections.Generic;
using IQ.Game.Poker.Models;

namespace IQ.Game.Poker.Strategy
{
    public interface IRankingStrategy
    {

        List<Player> GetWinners(List<Player> players);
    }
}