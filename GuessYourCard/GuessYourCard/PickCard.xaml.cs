using System.Collections.Generic;
using System.Windows;

namespace GuessYourCard
{
    /// <summary>
    /// Interaction logic for PickCard.xaml
    /// </summary>
    public partial class PickCard : Window
    {
        public List<Card> deckOfCards = new List<Card>()
        {
            new Card ("Ace", "Hearts"),
            new Card ("5", "Hearts"),
            new Card ("2", "Hearts"),
            new Card ("3", "Hearts"),
            new Card ("10", "Spades"),
            new Card ("7", "Clubs"),
            new Card ("6", "Clubs"),
            new Card ("3", "Diamonds"),
            new Card ("King", "Clubs"),
            new Card ("Queen", "Clubs"),
            new Card ("6", "Diamonds"),
            new Card ("10", "Diamonds"),
            new Card ("8", "Spades"),
            new Card ("3", "Spades"),
            new Card ("4", "Clubs"),
            new Card ("9", "Clubs"),
            new Card ("6", "Spades"),
            new Card ("2", "Diamonds")
        };

        public PickCard()
        {
            InitializeComponent();

            // Add all items in list to list box
            foreach (var deck in deckOfCards)
            {
                string card = "[" + deck.CardNumber + "] of " + deck.Suit;
                cardBox.Items.Add(card.ToString());
            }
        }

        private void btnShuffle_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            string message = "Please remember your card!\nPress OK to continue or CANCEL to take another look.";
            string caption = "*REMINDER*";
            var result = MessageBox.Show(message, caption, buttons);
            
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
            else if (result == MessageBoxResult.Cancel)
            {
                // Do nothing
            }
        }


    }
}
