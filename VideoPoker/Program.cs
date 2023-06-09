using System.Text;

namespace VideoPoker
{
    class Program
    {
        static Deck deck;

        static Player NewPlayer()
        {
            Console.Write("Your name: ");
            string name = Console.ReadLine();
            Console.Write("Starting bankroll: ");
            int bankroll = int.Parse(Console.ReadLine());
            return new Player(name, bankroll);
        }

        static Machine InsertCoins(Player player)
        {
            Console.WriteLine();
            Console.WriteLine($"{player.Name}'s bankroll is {player.Bankroll}");
            Console.Write("How many coins to play? 1-5: ");
            int coins = int.Parse(Console.ReadLine());
            if (coins == 0) Environment.Exit(0);
            Console.WriteLine();
            return new Machine(coins);
        }

        static List<Card> DealBeforeDraw()
        {
            deck = new Deck();
            List<Card> hand = deck.DealHandList(5);

            foreach (Card card in hand)
            {
                Console.Write(card.DisplayCard() + " ");
            }

            Console.WriteLine();
            return hand;
        }

        static List<int> HoldCards()
        {
            Console.WriteLine();
            Console.WriteLine("Which cards to hold? 1-5: ");
            string holdInput = Console.ReadLine();
            List<int> holdPositions = new List<int>();

            foreach (char c in holdInput)
            {
                if (char.IsDigit(c))
                {
                    int holdPosition = int.Parse(c.ToString());
                    holdPositions.Add(holdPosition);
                }
            }

            return holdPositions;
        }

        static List<Card> UpdateHand(List<Card> hand, List<int> holdPositions)
        {
            List<Card> updatedHand = new List<Card>();

            for (int i = 0; i < hand.Count; i++)
            {
                Card card = hand[i];
                if (holdPositions.Contains(i + 1))
                {
                    updatedHand.Add(card);
                }
            }

            for (int i = 0; i < hand.Count; i++)
            {
                Card card = hand[i];
                if (!holdPositions.Contains(i + 1))
                {
                    Card newCard = deck.DealCard();
                    updatedHand.Insert(i, newCard);
                }
            }

            return updatedHand;
        }

        static string NameHand(HandType handType)
        {
            if (handType == HandType.RoyalFlush) return "Royal Flush";
            if (handType == HandType.StraightFlush) return "Straight Flush";
            if (handType == HandType.FourOfAKind) return "Four of a Kind";
            if (handType == HandType.FullHouse) return "Full House";
            if (handType == HandType.Flush) return "Flush";
            if (handType == HandType.Straight) return "Straight";
            if (handType == HandType.ThreeOfAKind) return "Three of a Kind";
            if (handType == HandType.TwoPair) return "Two Pair";
            if (handType == HandType.JacksOrBetter) return "Jacks or Better";
            if (handType == HandType.LowPairOrHighCard) return "No Payout";
            return "Unknown";
        }

        static void DisplayHand(List<Card> hand)
        {
            foreach (Card card in hand)
            {
                Console.Write(card.DisplayCard() + " ");
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var player = NewPlayer();

            while (true)
            {
                var machine = InsertCoins(player);
                player.UpdateBankroll(-machine.Coins);

                List<Card> hand = DealBeforeDraw();
                List<int> holdPositions = HoldCards();
                List<Card> updatedHand = UpdateHand(hand, holdPositions);

                DisplayHand(updatedHand);

                var handEvaluator = new HandEvaluator();
                var rankValues = handEvaluator.GetRankValues();
                var rankCounts = new Dictionary<string, int>();
                var suitCounts = new Dictionary<string, int>();
                var handType = handEvaluator.EvaluateHand(updatedHand);

                Console.WriteLine($"Your hand is {NameHand(handType)}");
                int typeIndex = (int)handType;
                int payout = machine.Payouts[typeIndex] * machine.Coins;
                player.UpdateBankroll(payout);
            }
        }
    }
}