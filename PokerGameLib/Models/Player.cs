using System.Collections.Generic;

namespace IQ.Game.Poker.Models
{
    public class Player
    {
        public string PlayerName { get; private set; }

        public IList<Card> Cards { get; private set; }

        public HandType CardsType { get; set; }

        public uint Score { get; set; }

        public Player(string playerName, IList<Card> cards)
        {
            this.PlayerName = playerName;
            this.Cards = cards;
        }

        public override string ToString()
        {
            return $"Name: {PlayerName}; Cards: {string.Join(", ", Cards)}; Hand Type: {CardsType}; Score: {Score}.";
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