using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PokerVideoGame
{

    public partial class Form1 : Form
    {
        // GENEROWANIE NOWEJ TALII
        private DeckOfCards deck = new DeckOfCards();
     
        // List of files to show 
        private List<string> Files;

        // tablica z PictureBoxami - z CODINGHOMEWORK 
        private PictureBox[] pictures; 
        public const string imagePath = // sciezka do folderu z obrazami
        @"C:\Users\Mikolaj\source\repos\PokerVideoGame\PokerVideoGame\PNG-cards-1.3\";

        // tablica na 5 wylosowanych kart
        public int[] cardsOnTable = {0,0,0,0,0};

        public Form1()
        {
            InitializeComponent();
            // Z CODING HOMEWORK
            pictures = new PictureBox[52];
        }

        // wyciagniecie z nazwy numeru zdjecia i wpisanie go do tablicy
        string[] PathToCardNumber() // wartosc z konkretnego indeksu jest rowna karcie z talii
        {
            string[] cardsNumber = new string[Files.Count];
            const string v = "char";
            for (int i = 0; i < Files.Count; i++)
            {
                cardsNumber[i] = Files[i].Substring(74, Files[i].Length - 74 - 4);
            }

            return cardsNumber;
        }

        // z CODING HOMEWORK
        private void ShuffleCards()
        {
            deck.SetUpDeck();
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int firstCard = random.Next(0, 52); // 0 do 52 bo 52 sie nie wlicza do tego zakresu
                // numer karty, ktory chce potasowac i indeks karty Z ktora chce potasowac
                int secondCard = random.Next(0, 52);
                if (firstCard != secondCard) // zeby nie tasowac tej samej karty ze soba
                {
                    // tasowanie kart
                    //var tempCard = deck.deck[firstCard];
                    //deck.deck[firstCard] = deck.deck[secondCard];
                    //deck.deck[secondCard] = tempCard;
                    // tasowanie dla obrazow
                    var temp = Files[firstCard];
                    Files[firstCard] = Files[secondCard];
                    Files[secondCard] = temp;
                    firstCard = secondCard;
                    /* 
                    var temp = pictures[firstCard];
                    pictures[firstCard] = pictures[secondCard];
                    pictures[secondCard] = temp;
                    firstCard = secondCard;
                    */
                }
            }
        }

        public int[] readCards(int card1, int card2, int card3, int card4, int card5)
        {
            int[] cardsOnTable = { card1, card2, card3, card4, card5 };
            for(int i = 0; i < 5; i++)
            Console.WriteLine(cardsOnTable[i]);
            return cardsOnTable;
        }
        // StartUp
        private void Form1_Load(object sender, EventArgs args)
        {
            // basic settings
            var ext = new List<string> { ".jpg", ".gif", ".png" };
            // we use same directory where program is 
            string targetDirectory = imagePath;
            // Here we create our list of files
            // new list
            // user GetFiles to getfilenames
            // Filter unwanted stuff away (like our program)
            Files = new List<string>
                (Directory.GetFiles(targetDirectory, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => ext.Any(e => s.EndsWith(e))));
            // Potasuj karty przed rozpczeciem gry
            ShuffleCards();
            // Create timer to call timer1_Tick every 3 seconds 
            /* timer1 = new System.Windows.Forms.Timer();
             timer1.Tick += new EventHandler(timer1_Tick);
             timer1.Interval = 3000; // 3 seconds
             timer1.Start();
            */
            ChangePicture();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ChangePicture();
            timer1.Stop();
        }
        
        private void ChangePicture()
        {
            // Do we have pictures in list?
            string[] cardsNumber = PathToCardNumber(); // tablica przechowujaca numery obrazow
            // z kartami
            if (Files.Count > 2)
            {
                // OK lets grab the first one
                string File = Files.First();
                // Load it
                // 1st card
                pictureBox1.Load(File);
                int cardNumber1 = int.Parse(cardsNumber[0]); // parsuje nr karty do inta
                deck.printCardName(cardNumber1); // kolor i figura karty
                Console.WriteLine(cardsNumber[0]); // numer obrazu karty
                Files.RemoveAt(0);
                // 2nd card
                File = Files.First();
                pictureBox2.Load(File);
                int cardNumber2 = int.Parse(cardsNumber[1]);
                deck.printCardName(cardNumber2); // kolor i figura karty
                Console.WriteLine(cardsNumber[1]); // numer obrazu karty
                Files.RemoveAt(0);
                // 3rd card
                File = Files.First();
                pictureBox3.Load(File);
                int cardNumber3 = int.Parse(cardsNumber[2]);
                deck.printCardName(cardNumber3); // kolor i figura karty
                Console.WriteLine(cardsNumber[2]); // numer obrazu karty
                Files.RemoveAt(0);
                // 4th card
                File = Files.First();
                pictureBox4.Load(File);
                int cardNumber4 = int.Parse(cardsNumber[3]);
                deck.printCardName(cardNumber4); // kolor i figura karty
                Console.WriteLine(cardsNumber[3]); // numer obrazu karty
                Files.RemoveAt(0);
                // 5th card
                File = Files.First();
                pictureBox5.Load(File);
                int cardNumber5 = int.Parse(cardsNumber[4]);
                deck.printCardName(cardNumber5); // kolor i figura karty
                Console.WriteLine(cardsNumber[4]); // numer obrazu karty
                Files.RemoveAt(0);
                // zaladuj wszystkie wylosowane 5 kart do tablicy
                int[] cardsOnTheTable = { cardNumber1, cardNumber2, cardNumber3, cardNumber4,
                cardNumber5};

                readCards(cardNumber1, cardNumber2, cardNumber3, cardNumber4, cardNumber5);
            }
            else
            {
                // Out of pictures, stopping timer
                // and wait God to do something
                timer1.Stop();
            } 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // timer1.Start();
            ChangePicture();
        }
    }
}
