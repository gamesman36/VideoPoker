using System;
using System.Collections.Generic;

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

        static Machine InsertCoins()
        {
            Console.Write("How many coins to play? 1-5: ");
            int coins = int.Parse(Console.ReadLine());
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
            var player = NewPlayer();
            var machine = InsertCoins();
            List<Card> hand = DealBeforeDraw();
            List<int> holdPositions = HoldCards();
            List<Card> updatedHand = UpdateHand(hand, holdPositions);

            DisplayHand(updatedHand);

            var handEvaluator = new HandEvaluator();
            var rankValues = handEvaluator.GetRankValues();
            var rankCounts = new Dictionary<string, int>();
            var suitCounts = new Dictionary<string, int>();
            var handType = handEvaluator.EvaluateHand(updatedHand);
        }
    }
}