using System;

namespace IQ.Game.Poker.Models
{
    public class Card : IEquatable<Card>
    {
        public CardSuit Suit { get; private set; }
        public CardRank Rank { get; private set; }

        public Card(CardSuit suit, CardRank rank)
        {
            this.Suit = suit;
            this.Rank = rank;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Card);
        }

        public bool Equals(Card c)
        {
            if (Object.ReferenceEquals(c, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, c))
            {
                return true;
            }

            if (this.GetType() != c.GetType())
            {
                return false;
            }

            return (Suit == c.Suit) && (Rank == c.Rank);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Suit, Rank).GetHashCode();
        }

        public static bool operator ==(Card lc, Card rc)
        {
            // Check for null on left side.
            if (Object.ReferenceEquals(lc, null))
            {
                if (Object.ReferenceEquals(rc, null))
                {
                    // null == null = true.
                    return true;
                }
                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lc.Equals(rc);
        }

        public static bool operator !=(Card lc, Card rc)
        {
            return !(lc == rc);
        }

        public override string ToString()
        {
            return this.Suit.ToString()[0] + "" + (uint)this.Rank;
        }

    }

    public enum CardSuit
    {
        Spades,
        Hearts,
        Diamonds,
        Clubs
    }

    public enum CardRank : uint
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    }

}