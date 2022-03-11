using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;
namespace CardDeck
{
    public partial class Form1 : Form
    {
        // deck of cards
        List<string> deck = new List<string>();
        List<string> dealercards = new List<string>();
        List<string> playercards = new List<string>();

        public Form1()
        {
            InitializeComponent();

            //fill deck with standard 52 cards
            //H - Hearts, D - Diamonds, C - Clubs, S - Spades
            
            deck.AddRange(new string[] { "2H", "3H", "4H", "5H", "6H", "7H", "8H", "9H", "10H", "JH", "QH", "KH", "AH" });
            deck.AddRange(new string[] { "2D", "3D", "4D", "5D", "6D", "7D", "8D", "9D", "10D", "JD", "QD", "KD", "AD" });
            deck.AddRange(new string[] { "2C", "3C", "4C", "5C", "6C", "7C", "8C", "9C", "10C", "JC", "QC", "KC", "AC" });
            deck.AddRange(new string[] { "2S", "3S", "4S", "5S", "6S", "7S", "8S", "9S", "10S", "JS", "QS", "KS", "AS" });

            ShowDeck();
            dealButton.Enabled = false;
            collectButton.Enabled = false;
            dealButton.BackColor = Color.Gray;
            collectButton.BackColor = Color.Gray;
        }

        public void ShowDeck()
        {
            outputLabel.Text += $"";
            for (int i = 0; i < deck.Count; i++)
            {
              
               
   outputLabel.Text += $" {  deck[i]} ";

            }

        }

        private void shuffleButton_Click(object sender, EventArgs e)
        {
            List<string> deckTemp = new List<string>();
            Random randGen = new Random();

            while (deck.Count > 0)
            {
                int index = randGen.Next(0, deck.Count);                
                deckTemp.Add(deck[index]);
                deck.RemoveAt(index);
            }
            SoundPlayer player = new SoundPlayer(Properties.Resources.shuffle);
            player.Play();
            deck = deckTemp;

            ShowDeck();
            shuffleButton.Enabled = false;
            shuffleButton.BackColor = Color.Gray;
            dealButton.Enabled=true;
            dealButton.BackColor= Color.GreenYellow;
        }

        private void dealButton_Click(object sender, EventArgs e)
        {
            dealButton.Enabled = false;
            dealButton.BackColor = Color.Gray;
            collectButton.Enabled= true;
          
           for (int i = 0; i < 5; i++)
            {
                playercards.Add(deck[0]);
                deck.RemoveAt(0);
                dealercards.Add(deck[0]);
                deck.RemoveAt(0);
                
            }

            playerCardsLabel.Text = $" ";
            dealerCardsLabel.Text = $" ";
            ShowDeck();


            for (int i = 0; i < playercards.Count; i++)
            {

                Thread.Sleep(1000);
                Refresh();
                SoundPlayer player = new SoundPlayer(Properties.Resources.deal);
                player.Play();
                playerCardsLabel.Text += $" {  playercards[i]} ";

            }


            for (int i = 0; i < dealercards.Count; i++)
            {
                Thread.Sleep(1000);
                Refresh();
                SoundPlayer player = new SoundPlayer(Properties.Resources.deal);
                player.Play();
                dealerCardsLabel.Text += $" {  dealercards[i]} ";

            }

            collectButton.BackColor = Color.GreenYellow;
        

    }

        private void collectButton_Click(object sender, EventArgs e)
        {
            collectButton.Enabled = false;
            collectButton.BackColor = Color.Gray;
            shuffleButton.Enabled = true;
            shuffleButton.BackColor = Color.GreenYellow;

            for (int i = 0; i < 5; i++)
            {
                deck.Add(playercards[i]);

                deck.Add(dealercards[i]);
               
            }
            dealercards.Clear();
            playercards.Clear();
            playerCardsLabel.Text = $"";
            dealerCardsLabel.Text = $"";
            SoundPlayer player = new SoundPlayer(Properties.Resources.collect);
        }
    }
}
