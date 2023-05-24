namespace VideoPoker
{
    public class Machine
    {
        public int Coins { get; private set; }

        private static int[] Payouts = { 1, 2, 3, 4, 6, 9, 25, 50, 250 };

        private HandEvaluator HandEvaluator;

        public Machine(int coins)
        {
            Coins = coins;
            HandEvaluator = new HandEvaluator();
        }
        public void MultiplyPayouts(int multiplier)
        {
            Payouts = Payouts.Select(x => x * multiplier).ToArray();
        }

        public void EvaluatePlayerHand(Player player)
        {
            List<Card> playerHand = player.GetHand();
            HandType handType = HandEvaluator.EvaluateHand(playerHand);
        }
    }
}
