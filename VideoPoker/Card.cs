namespace VideoPoker
{
    public class Card
    {
        public string Rank { get; private set; }
        public string Suit { get; private set; }

        public Card(string rank, string suit) 
        {
            Rank = rank;
            Suit = suit;
        }
        public string DisplayCard()
        {
            string symbol;
            switch (Suit)
            {
                case "Hearts":
                    symbol = "♥";
                    break;
                case "Diamonds":
                    symbol = "♦";
                    break;
                case "Clubs":
                    symbol = "♣";
                    break;
                case "Spades":
                    symbol = "♠";
                    break;
                default:
                    symbol = "";
                    break;
            }

            return $"{Rank}{symbol}";
        }

    }

}