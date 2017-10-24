namespace GuessYourCard
{
    public class Card
    {
        public string CardNumber { get; set; }
        public string Suit { get; set; }

        public Card(string num, string suit)
        {
            CardNumber = num;
            Suit = suit;
        }
    }
}
