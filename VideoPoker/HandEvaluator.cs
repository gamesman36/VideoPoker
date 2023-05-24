namespace VideoPoker
{
    public enum HandType
    {
        Unknown,
        RoyalFlush,
        StraightFlush,
        FourOfAKind,
        FullHouse,
        Flush,
        Straight,
        ThreeOfAKind,
        TwoPair,
        JacksOrBetter,
        HighCard
    }
    public class HandEvaluator
    {
        private Dictionary<string, int> RankValues;

        public Dictionary<string, int> GetRankValues()
        {
            return RankValues;
        }

        public HandEvaluator()
        {
            RankValues = new Dictionary<string, int>()
            {
                { "2", 2 },
                { "3", 3 },
                { "4", 4 },
                { "5", 5 },
                { "6", 6 },
                { "7", 7 },
                { "8", 8 },
                { "9", 9 },
                { "10", 10 },
                { "J", 11 },
                { "Q", 12 },
                { "K", 13 },
                { "A", 14 }
            };
        }

        public HandType EvaluateHand(List<Card> hand)
        {
            Dictionary<string, int> RankCounts = CountRanks(hand);
            Dictionary<string, int> SuitCounts = CountSuits(hand);

            if (IsRoyalFlush(hand, SuitCounts))
            {
                return HandType.RoyalFlush;
            }

            if (IsStraightFlush(hand, RankCounts, SuitCounts))
            {
                return HandType.StraightFlush;
            }

            if (IsFourOfAKind(RankCounts))
            {
                return HandType.FourOfAKind;
            }

            if (IsFullHouse(RankCounts))
            {
                return HandType.FullHouse;
            }

            if (IsFlush(SuitCounts))
            {
                return HandType.Flush;
            }

            if (IsStraight(RankCounts))
            {
                return HandType.Straight;
            }

            if (IsThreeOfAKind(RankCounts))
            {
                return HandType.ThreeOfAKind;
            }

            if (IsTwoPair(RankCounts))
            {
                return HandType.TwoPair;
            }

            if (IsJacksOrBetter(RankCounts))
            {
                return HandType.JacksOrBetter;
            }

            return HandType.HighCard;
        }

        public Dictionary<string, int> CountRanks(List<Card> hand)
        {
            Dictionary<string, int> RankCounts = new Dictionary<string, int>();

            foreach (Card card in hand)
            {
                if (RankCounts.ContainsKey(card.Rank))
                {
                    RankCounts[card.Rank]++;
                }
                else
                {
                    RankCounts[card.Rank] = 1;
                }
            }

            return RankCounts;
        }

        public Dictionary<string, int> CountSuits(List<Card> hand)
        {
            Dictionary<string, int> SuitCounts = new Dictionary<string, int>();

            foreach (Card card in hand)
            {
                if (SuitCounts.ContainsKey(card.Suit))
                {
                    SuitCounts[card.Suit]++;
                }
                else
                {
                    SuitCounts[card.Suit] = 1;
                }
            }

            return SuitCounts;
        }

        private bool IsRoyalFlush(List<Card> hand, Dictionary<string, int> SuitCounts)
        {
            return SuitCounts.ContainsValue(5) && hand.All(card => RankValues.ContainsKey(card.Rank) && RankValues[card.Rank] >= 10);
        }

        private bool IsStraightFlush(List<Card> hand, Dictionary<string, int> RankCounts, Dictionary<string, int> SuitCounts)
        {
            return SuitCounts.ContainsValue(5) && IsStraight(RankCounts);
        }

        private bool IsFourOfAKind(Dictionary<string, int> RankCounts)
        {
            return RankCounts.ContainsValue(4);
        }

        private bool IsFullHouse(Dictionary<string, int> RankCounts)
        {
            return RankCounts.ContainsValue(3) && RankCounts.ContainsValue(2);
        }

        private bool IsFlush(Dictionary<string, int> SuitCounts)
        {
            return SuitCounts.ContainsValue(5);
        }

        private bool IsStraight(Dictionary<string, int> RankCounts)
        {
            List<int> SortedRanks = RankCounts.Keys.Select(rank => RankValues[rank]).OrderBy(rankValue => rankValue).ToList();

            if (SortedRanks.Count != 5)
            {
                return false;
            }

            for (int i = 1; i < SortedRanks.Count; i++)
            {
                if (SortedRanks[i] != SortedRanks[i - 1] + 1)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsThreeOfAKind(Dictionary<string, int> RankCounts)
        {
            return RankCounts.ContainsValue(3);
        }

        private bool IsTwoPair(Dictionary<string, int> RankCounts)
        {
            int PairCount = RankCounts.Values.Count(count => count == 2);
            return PairCount == 2;
        }

        private bool IsJacksOrBetter(Dictionary<string, int> RankCounts)
        {
            return RankCounts.ContainsKey("J") && RankCounts["J"] >= 2 ||
                   RankCounts.ContainsKey("Q") && RankCounts["Q"] >= 2 ||
                   RankCounts.ContainsKey("K") && RankCounts["K"] >= 2 ||
                   RankCounts.ContainsKey("A") && RankCounts["A"] >= 2;
        }
    }
}