using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;

namespace GuessYourCard
{
    /// <summary>
    /// Interaction logic for GuessCard.xaml
    /// </summary>
    public partial class GuessCard : Window
    {
        // Declaring arrays to be shuffled and displayed
        static Card[] bottomPile = new Card[6];
        static Card[] tempBottomPile = new Card[6];
        static Card[] middlePile = new Card[6];
        static Card[] tempMiddlePile = new Card[6];
        static Card[] topPile = new Card[6];
        static Card[] tempTopPile = new Card[6];

        static short numberOfShuffles = 0;
        
        static Random r = new Random();

        public GuessCard()
        {
            InitializeComponent();
            
            string message = "*The goal of the game is to pick a card and let the computer know" +
                            " which pile the card lies in. Do this for a total of 3 shuffles and" +
                            " watch the computer reveal your card!*\n" +
                            "\n1. Pick a card. Keep it in mind. Shuffle.\n\n" +
                            "2. The computer will then shuffle into 3 separate \"piles\"." +
                            " Select which pile your card is in by clicking the corresponding button.\n\n" +
                            "3. Repeat step 2 for a total of 3 shuffles until the computer shows you your card.\n" +
                            "\nEnjoy!";
            
            MessageBox.Show(message, "Game RUles", MessageBoxButton.OK);
            
            if (btnPickYourCard.IsPressed.Equals(false))
            {
                pickCardLabel.Visibility = Visibility.Visible;
            }
            
            btnLeft.IsEnabled = false;
            btnMiddle.IsEnabled = false;
            btnRight.IsEnabled = false;
        }
        
        private void btnPickYourCard_Click(object sender, RoutedEventArgs e)
        {
            numberOfShuffles = 0;
            pickCardLabel.Visibility = Visibility.Collapsed;
            btnPickYourCard.IsEnabled = false;
            
            PickCard pickCard = new PickCard();
            pickCard.ShowDialog();

            // Shuffle cards after player picks cards
            InitialShuffle(pickCard.deckOfCards);

            btnLeft.IsEnabled = true;
            btnMiddle.IsEnabled = true;
            btnRight.IsEnabled = true;
        }

        /// <summary>
        /// Using Fisher-Yates Shuffle based on Java code
        /// to shuffle between 3 decks
        /// http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
        /// </summary>
        private void InitialShuffle(List<Card> deck)
        {
            int j = 0; // variable to use for shuffling between 3 different piles
            int index = 0; // variable to use for assigning all indexes in deck

            for (int i = deck.Count - 1; i >= 0; --i)
            {
                if (j < 6)
                {
                    int k = r.Next(i + 1);
                    bottomPile[index] = deck[k];
                    deck.RemoveAt(k);
                    j++;
                    index++;
                }
                else if ((j >= 6) && (j < 12))
                {
                    if (index == 6) { index = 0; }

                    int k = r.Next(i + 1);
                    middlePile[index] = deck[k];
                    deck.RemoveAt(k);
                    j++;
                    index++;
                }
                else if (j >= 12)
                {
                    if (index == 6) { index = 0; }

                    int k = r.Next(i + 1);
                    topPile[index] = deck[k];
                    deck.RemoveAt(k);
                    index++;
                }
            }

            // Display cards into list boxes
            DisplayCards();
        }

        private void ShuffleLeft()
        {
            
            if (numberOfShuffles == 0)
            {
                // Initialize j to index the piles we are shuffling
                // Initialize k to index the temporary piles we are shuffling
                int j = 0, k = 0;
                for (int i = 0; i < 6; i++) // Run for-loop for a total of 6 times for the pile shuffle
                {
                    // When i is 2 or greater, first pile has been shuffled
                    if (i < 2)
                    {
                        
                        string card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = topPile[j]; // Taking current index of the topPile and storing it into a temporary array
                        j++; // Increment the index of topPile to continue shuffling properly

                        card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        tempMiddlePile[k] = topPile[j];
                        middleBox.Items.Add(card.ToString());
                        j++;

                        card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        tempBottomPile[k] = topPile[j];
                        rightBox.Items.Add(card.ToString());
                        j++;

                        k++; // Increment index variable of the temporary arrays that we are shuffling to
                    }
                    // When i is greater than 4, second pile has been shuffled
                    else if ((i >= 2) && (i < 4))
                    {
                        if (j == 6) { j = 0; } // If j is 6 from the last if statement, reset it to 0
                                               // This allows the next pile we are shuffling to be shuffled
                                               //properly starting from index 0

                        string card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = middlePile[j];
                        j++; // Increment the index of middlePile to continue shuffling properly

                        card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = middlePile[j];
                        j++;

                        card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = middlePile[j];
                        j++;

                        k++; // Increment index variable of the temporary arrays that we are shuffling to
                    }
                    else if (i >= 4)
                    {
                        if (j == 6) { j = 0; }

                        string card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = bottomPile[j];
                        j++; // Increment the index of bottomPile to continue shuffling properly

                        card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = bottomPile[j];
                        j++;

                        card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = bottomPile[j];
                        j++;

                        k++; // Increment index variable of the temporary arrays that we are shuffling to
                    }
                }

                /*****************************************************************
                 * Because this is a pile shuffle, we created 3 separate stacks
                 * which in code we treated as temporary arrays. That way when the
                 * pile shuffle is complete, we OVERWRITE the top, middle, and 
                 * bottom piles with those temporary arrays. We then REVERSE
                 * those arrays due to the nature of the pile shuffle.
                 *****************************************************************/

                topPile = tempTopPile.ToArray();
                topPile.Reverse();
                middlePile = tempMiddlePile.ToArray();
                middlePile.Reverse();
                bottomPile = tempBottomPile.ToArray();
                bottomPile.Reverse();
            }

            else if (numberOfShuffles == 1)
            {
                // Because the user chose the topPile (1) on the
                // second shuffle, we must swap the "arrays" (piles) so
                // that when we pile shuffle we are shuffling the proper
                // way of this trick. *topPile MUST BE middlePile*

                Card[] tempArray = middlePile.ToArray(); // Save middle pile to not lose
                middlePile = topPile.ToArray();          // Overwrite middle pile contents with top pile
                topPile = tempArray.ToArray();

                int j = 0, k = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (i < 2)
                    {
                        string card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = topPile[j];
                        j++;

                        card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = topPile[j];
                        j++;

                        card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = topPile[j];
                        j++;

                        k++;
                    }
                    else if ((i >= 2) && (i < 4))
                    {
                        if (j == 6) { j = 0; }
                        string card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = middlePile[j];
                        j++;

                        card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = middlePile[j];
                        j++;

                        card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = middlePile[j];
                        j++;

                        k++;
                    }
                    else if (i >= 4)
                    {
                        if (j == 6) { j = 0; }
                        string card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = bottomPile[j];
                        j++;

                        card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = bottomPile[j];
                        j++;

                        card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = bottomPile[j];
                        j++;

                        k++;
                    }
                }

                topPile = tempTopPile.ToArray();
                topPile.Reverse();
                middlePile = tempMiddlePile.ToArray();
                middlePile.Reverse();
                bottomPile = tempBottomPile.ToArray();
                bottomPile.Reverse();
            }

            else if (numberOfShuffles == 2)
            {
                string card = "[" + topPile[2].CardNumber + "] of " + topPile[2].Suit;

                leftBox.Items.Clear();
                middleBox.Items.Clear();
                rightBox.Items.Clear();

                MessageBox.Show("Is " + card + " your card?", "Card Reveal", MessageBoxButton.YesNo);

                btnLeft.IsEnabled = false;
                btnMiddle.IsEnabled = false;
                btnRight.IsEnabled = false;

            }

        }

        private void ShuffleMiddle()
        {
            if (numberOfShuffles == 0)
            {
                // Because the user chose the middlePile (2) on the
                // first shuffle, we must swap the "arrays" (piles) so
                // that when we pile shuffle we are shuffling the proper
                // way of this trick. *middlePile MUST BE topPile*

                Card[] tempArray = topPile.ToArray();
                topPile = middlePile.ToArray();
                middlePile = tempArray.ToArray();

                int j = 0, k = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (i < 2)
                    {
                        string card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = topPile[j];
                        j++;

                        card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = topPile[j];
                        j++;

                        card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = topPile[j];
                        j++;

                        k++;
                    }
                    else if ((i >= 2) && (i < 4))
                    {
                        if (j == 6) { j = 0; }
                        string card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = middlePile[j];
                        j++;

                        card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = middlePile[j];
                        j++;

                        card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = middlePile[j];
                        j++;

                        k++;
                    }
                    else if (i >= 4)
                    {
                        if (j == 6) { j = 0; }
                        string card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = bottomPile[j];
                        j++;

                        card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = bottomPile[j];
                        j++;

                        card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = bottomPile[j];
                        j++;

                        k++;
                    }
                }


                /*****************************************************************
                * Because this is a pile shuffle, we created 3 separate stacks
                * which in code we treated as temporary arrays. That way when the
                * pile shuffle is complete, we OVERWRITE the top, middle, and 
                * bottom piles with those temporary arrays. We then REVERSE
                * those arrays due to the nature of the pile shuffle.
                *****************************************************************/

                topPile = tempTopPile.ToArray();
                topPile.Reverse();
                middlePile = tempMiddlePile.ToArray();
                middlePile.Reverse();
                bottomPile = tempBottomPile.ToArray();
                bottomPile.Reverse();
            }
            else if (numberOfShuffles == 1)
            {
                int j = 0, k = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (i < 2)
                    {
                        string card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = topPile[j];
                        j++;

                        card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = topPile[j];
                        j++;

                        card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = topPile[j];
                        j++;

                        k++;
                    }
                    else if ((i >= 2) && (i < 4))
                    {
                        if (j == 6) { j = 0; }

                        string card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = middlePile[j];
                        j++;

                        card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = middlePile[j];
                        j++;

                        card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = middlePile[j];
                        j++;

                        k++;
                    }
                    else if (i >= 4)
                    {
                        if (j == 6) { j = 0; }
                        string card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = bottomPile[j];
                        j++;

                        card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = bottomPile[j];
                        j++;

                        card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = bottomPile[j];
                        j++;

                        k++;
                    }
                }

                topPile = tempTopPile.ToArray();
                topPile.Reverse();
                middlePile = tempMiddlePile.ToArray();
                middlePile.Reverse();
                bottomPile = tempBottomPile.ToArray();
                bottomPile.Reverse();
            }
            else if (numberOfShuffles == 2)
            {
                string card = "[" + middlePile[2].CardNumber + "] of " + middlePile[2].Suit;

                leftBox.Items.Clear();
                middleBox.Items.Clear();
                rightBox.Items.Clear();

                MessageBox.Show("Is " + card + " your card?", "Card Reveal", MessageBoxButton.YesNo);

                btnLeft.IsEnabled = false;
                btnMiddle.IsEnabled = false;
                btnRight.IsEnabled = false;
            }
        }

        private void ShuffleRight()
        {
            if (numberOfShuffles == 0)
            {
                // Because the user chose the bottomPile (3) on the
                // first shuffle, we must swap the "arrays" (piles) so
                // that when we pile shuffle we are shuffling the proper
                // way of this trick. *bottomPile MUST BE topPile*

                Card[] tempArray = topPile.ToArray();
                topPile = bottomPile.ToArray();
                bottomPile = tempArray.ToArray();

                int j = 0, k = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (i < 2)
                    {
                        string card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = topPile[j];
                        j++;

                        card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = topPile[j];
                        j++;

                        card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = topPile[j];
                        j++;

                        k++;
                    }
                    else if ((i >= 2) && (i < 4))
                    {
                        if (j == 6) { j = 0; }
                        string card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = middlePile[j];
                        j++;

                        card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = middlePile[j];
                        j++;

                        card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = middlePile[j];
                        j++;

                        k++;
                    }
                    else if (i >= 4)
                    {
                        if (j == 6) { j = 0; }
                        string card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = bottomPile[j];
                        j++;

                        card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = bottomPile[j];
                        j++;

                        card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = bottomPile[j];
                        j++;

                        k++;
                    }
                }


                /*****************************************************************
                * Because this is a pile shuffle, we created 3 separate stacks
                * which in code we treated as temporary arrays. That way when the
                * pile shuffle is complete, we OVERWRITE the top, middle, and 
                * bottom piles with those temporary arrays. We then REVERSE
                * those arrays due to the nature of the pile shuffle.
                *****************************************************************/

                topPile = tempTopPile.ToArray();
                topPile.Reverse();
                middlePile = tempMiddlePile.ToArray();
                middlePile.Reverse();
                bottomPile = tempBottomPile.ToArray();
                bottomPile.Reverse();
            }
            else if (numberOfShuffles == 1)
            {
                // Because the user chose the bottomPile (3) on the
                // second shuffle, we must swap the "arrays" (piles) so
                // that when we pile shuffle we are shuffling the proper
                // way of this trick. *bottomPile MUST BE middlePile*

                Card[] tempArray = middlePile.ToArray();
                middlePile = bottomPile.ToArray();
                bottomPile = tempArray.ToArray();

                int j = 0, k = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (i < 2)
                    {
                        string card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = topPile[j];
                        j++;

                        card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = topPile[j];
                        j++;

                        card = "[" + topPile[j].CardNumber + "] of " + topPile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = topPile[j];
                        j++;

                        k++;
                    }
                    else if ((i >= 2) && (i < 4))
                    {
                        if (j == 6) { j = 0; }

                        string card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = middlePile[j];
                        j++;

                        card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = middlePile[j];
                        j++;

                        card = "[" + middlePile[j].CardNumber + "] of " + middlePile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = middlePile[j];
                        j++;

                        k++;
                    }
                    else if (i >= 4)
                    {
                        if (j == 6) { j = 0; }
                        string card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        leftBox.Items.Add(card.ToString());
                        tempTopPile[k] = bottomPile[j];
                        j++;

                        card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        middleBox.Items.Add(card.ToString());
                        tempMiddlePile[k] = bottomPile[j];
                        j++;

                        card = "[" + bottomPile[j].CardNumber + "] of " + bottomPile[j].Suit;
                        rightBox.Items.Add(card.ToString());
                        tempBottomPile[k] = bottomPile[j];
                        j++;

                        k++;
                    }
                }

                topPile = tempTopPile.ToArray();
                topPile.Reverse();
                middlePile = tempMiddlePile.ToArray();
                middlePile.Reverse();
                bottomPile = tempBottomPile.ToArray();
                bottomPile.Reverse();
            }
            else if (numberOfShuffles == 2)
            {
                string card = "[" + bottomPile[2].CardNumber + "] of " + bottomPile[2].Suit;

                leftBox.Items.Clear();
                middleBox.Items.Clear();
                rightBox.Items.Clear();

                MessageBox.Show("Is " + card + " your card?", "Card Reveal", MessageBoxButton.YesNo);

                btnLeft.IsEnabled = false;
                btnMiddle.IsEnabled = false;
                btnRight.IsEnabled = false;
            }
        }
        
        private void DisplayCards()
        {
            int totalCards = topPile.Count() + middlePile.Count() + bottomPile.Count();
            int j = 0; // variable to switch between the decks
            int index = 0; // variable to assign all indexes in each deck

            // Add decks to list boxes
            for (int i = 0; i <= totalCards - 1; i++)
            {
                if (j < 6)
                {
                    string card = "[" + topPile[index].CardNumber + "] of " + topPile[index].Suit;
                    leftBox.Items.Add(card);
                    j++;
                    index++;
                }
                else if ((j >= 6) && (j < 12))
                {
                    if (index == 6) { index = 0; }
                    string card = "[" + middlePile[index].CardNumber + "] of " + middlePile[index].Suit;
                    middleBox.Items.Add(card);
                    j++;
                    index++;
                }
                else if (j >= 12)
                {
                    if (index == 6) { index = 0; }
                    string card = "[" + bottomPile[index].CardNumber + "] of " + bottomPile[index].Suit;
                    rightBox.Items.Add(card);
                    index++;
                }
            }
        }
        
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            // Reset Buttons and Label
            btnPickYourCard.IsEnabled = true;
            btnLeft.IsEnabled = false;
            btnMiddle.IsEnabled = false;
            btnRight.IsEnabled = false;
            pickCardLabel.Visibility = Visibility.Visible;


            // Clear cards from all list boxes
            leftBox.Items.Clear();
            middleBox.Items.Clear();
            rightBox.Items.Clear();
        }
        
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void DisplaySteps_Click(object sender, RoutedEventArgs e)
        {
            // Create message string for MessageBox.Show()
            string message = "*The goal of the game is to pick a card and let the computer know" +
                            " which pile the card lies in. Do this for a total of 3 shuffles and" +
                            " watch the computer reveal your card!*\n" +
                            "\n1. Pick a card. Keep it in mind. Shuffle.\n\n" +
                            "2. The computer will then shuffle into 3 separate \"piles\"." +
                            " Select which pile your card is in by clicking the corresponding button.\n\n" +
                            "3. Repeat step 2 for a total of 3 shuffles until the computer shows you your card.\n" +
                            "\nEnjoy!";
            
            MessageBox.Show(message, "Game Rules", MessageBoxButton.OK);
        }
        
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Concept by Steve Muthomi.\n\nCode & Design by Cristian Canedo.", "About");
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            leftBox.Items.Clear();
            middleBox.Items.Clear();
            rightBox.Items.Clear();

            ShuffleLeft();
            numberOfShuffles++;
        }

        private void btnMiddle_Click(object sender, RoutedEventArgs e)
        {
            leftBox.Items.Clear();
            middleBox.Items.Clear();
            rightBox.Items.Clear();

            ShuffleMiddle();
            numberOfShuffles++;
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            leftBox.Items.Clear();
            middleBox.Items.Clear();
            rightBox.Items.Clear();

            ShuffleRight();
            numberOfShuffles++;
        }
    }
}
