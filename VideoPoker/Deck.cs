namespace VideoPoker
{
    public class Deck
    {
        private List<Card> cards;
        private Random random;

        public Deck()
        {
            cards = new List<Card>();
            random = new Random();
            InitializeDeck();
            ShuffleDeck();
        }

        private void InitializeDeck()
        {
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };

            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    cards.Add(new Card(rank, suit));
                }
            }
        }

        private void ShuffleDeck()
        {
            for (int i = cards.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                Card temp = cards[j];
                cards[j] = cards[i];
                cards[i] = temp;
            }
        }

        public Card DealCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        public List<Card> DealHandList(int numCards)
        {
            List<Card> hand = new List<Card>();
            for (int i = 0; i < numCards; i++)
            {
                hand.Add(DealCard());
            }
            return hand;
        }
    }
}