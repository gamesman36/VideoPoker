namespace VideoPoker
{
    public class Player
    {
        public string Name { get; private set; }
        public int Bankroll { get; private set; }
        public List<Card> Hand { get; private set; }
        public Player(string name, int bankroll)
        {
            Name = name;
            Bankroll = bankroll;
            Hand = new List<Card>();
        }

        public void UpdateBankroll(int amount)
        {
            Bankroll += amount;
        }
    }
}