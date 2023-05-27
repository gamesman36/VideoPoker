using System.Dynamic;

namespace VideoPoker
{
    public class Machine
    {
        public int Coins { get; private set; } 
        public int[] Payouts { get; private set; } = new int[] { 0, 1, 2, 3, 4, 6, 9, 25, 50, 250 };
        private HandEvaluator HandEvaluator;

        public Machine(int coins)
        {
            Coins = coins;
            HandEvaluator = new HandEvaluator();
        }

        public void EvaluatePlayerHand(Player player)
        {
            List<Card> playerHand = player.Hand;
            HandType handType = HandEvaluator.EvaluateHand(playerHand);
        }
    }
}
