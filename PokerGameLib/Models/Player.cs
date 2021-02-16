using System.Collections.Generic;

namespace IQ.Game.Poker.Models
{
    public class Player
    {
        public string PlayerName { get; private set; }

        public List<Card> Cards { get; private set; }

        public HandType CardsType { get; set; }

        public uint Score { get; set; }

        public Player(string playerName, List<Card> cards) {
            this.PlayerName = playerName;
            this.Cards = cards;
        }

    }

    public enum HandType
    {
        Flush,
        ThreeOfAKind,
        OnePair,
        HighCard
    }
}