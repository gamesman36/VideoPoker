namespace VideoPoker
{
    public enum HandType
    {
        LowPairOrHighCard,
        JacksOrBetter,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    public class HandEvaluator
    {
        private Dictionary<string, int> rankValues;

        public Dictionary<string, int> GetRankValues()
        {
            return rankValues;
        }

        public HandEvaluator()
        {
            rankValues = new Dictionary<string, int>()
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
            Dictionary<string, int> rankCounts = CountRanks(hand);
            Dictionary<string, int> suitCounts = CountSuits(hand);

            if (IsRoyalFlush(hand, suitCounts))
            {
                return HandType.RoyalFlush;
            }

            if (IsStraightFlush(hand, rankCounts, suitCounts))
            {
                return HandType.StraightFlush;
            }

            if (IsFourOfAKind(rankCounts))
            {
                return HandType.FourOfAKind;
            }

            if (IsFullHouse(rankCounts))
            {
                return HandType.FullHouse;
            }

            if (IsFlush(suitCounts))
            {
                return HandType.Flush;
            }

            if (IsStraight(rankCounts))
            {
                return HandType.Straight;
            }

            if (IsThreeOfAKind(rankCounts))
            {
                return HandType.ThreeOfAKind;
            }

            if (IsTwoPair(rankCounts))
            {
                return HandType.TwoPair;
            }

            if (IsJacksOrBetter(rankCounts))
            {
                return HandType.JacksOrBetter;
            }

            return HandType.LowPairOrHighCard;
        }

        public Dictionary<string, int> CountRanks(List<Card> hand)
        {
            Dictionary<string, int> rankCounts = new Dictionary<string, int>();

            foreach (Card card in hand)
            {
                if (rankCounts.ContainsKey(card.Rank))
                {
                    rankCounts[card.Rank]++;
                }
                else
                {
                    rankCounts[card.Rank] = 1;
                }
            }

            return rankCounts;
        }

        public Dictionary<string, int> CountSuits(List<Card> hand)
        {
            Dictionary<string, int> suitCounts = new Dictionary<string, int>();

            foreach (Card card in hand)
            {
                if (suitCounts.ContainsKey(card.Suit))
                {
                    suitCounts[card.Suit]++;
                }
                else
                {
                    suitCounts[card.Suit] = 1;
                }
            }

            return suitCounts;
        }

        private bool IsRoyalFlush(List<Card> hand, Dictionary<string, int> suitCounts)
        {
            return suitCounts.ContainsValue(5) && hand.All(card => rankValues.ContainsKey(card.Rank) && rankValues[card.Rank] >= 10);
        }

        private bool IsStraightFlush(List<Card> hand, Dictionary<string, int> rankCounts, Dictionary<string, int> suitCounts)
        {
            return suitCounts.ContainsValue(5) && IsStraight(rankCounts);
        }

        private bool IsFourOfAKind(Dictionary<string, int> rankCounts)
        {
            return rankCounts.ContainsValue(4);
        }

        private bool IsFullHouse(Dictionary<string, int> rankCounts)
        {
            return rankCounts.ContainsValue(3) && rankCounts.ContainsValue(2);
        }

        private bool IsFlush(Dictionary<string, int> suitCounts)
        {
            return suitCounts.ContainsValue(5);
        }

        private bool IsStraight(Dictionary<string, int> rankCounts)
        {
            List<int> sortedRanks = rankCounts.Keys.Select(rank => rankValues[rank]).OrderBy(rankValue => rankValue).ToList();

            if (sortedRanks.Count != 5)
            {
                return false;
            }

            for (int i = 1; i < sortedRanks.Count; i++)
            {
                if (sortedRanks[i] != sortedRanks[i - 1] + 1)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsThreeOfAKind(Dictionary<string, int> rankCounts)
        {
            return rankCounts.ContainsValue(3);
        }

        private bool IsTwoPair(Dictionary<string, int> rankCounts)
        {
            int pairCount = rankCounts.Values.Count(count => count == 2);
            return pairCount == 2;
        }

        private bool IsJacksOrBetter(Dictionary<string, int> rankCounts)
        {
            return rankCounts.ContainsKey("J") && rankCounts["J"] >= 2 ||
                   rankCounts.ContainsKey("Q") && rankCounts["Q"] >= 2 ||
                   rankCounts.ContainsKey("K") && rankCounts["K"] >= 2 ||
                   rankCounts.ContainsKey("A") && rankCounts["A"] >= 2;
        }
    }
}