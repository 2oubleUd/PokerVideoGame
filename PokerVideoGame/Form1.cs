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
        public const string imagePath =
            @"C:\Users\Mikolaj\source\repos\PokerVideoGame\PokerVideoGame\PNG-cards-1.3\";

       
        public Card[] cardsTable = new Card[5]; // tablica na karty, ktore obecnie sa na stole
        private int[] cards_to_change = { 1, 1, 1, 1, 1 };
        //Buttons clicked event
        private bool button1_Was_Clicked = false;
        private bool button2_Was_Clicked = false;
        private bool button3_Was_Clicked = false;
        private bool button4_Was_Clicked = false;
        private bool button5_Was_Clicked = false;
        // counters for buttons (change color if clicked based on modulo)
        private int button1_Was_Clicked_Counter = 0;
        private int button2_Was_Clicked_Counter = 0;
        private int button3_Was_Clicked_Counter = 0;
        private int button4_Was_Clicked_Counter = 0;
        private int button5_Was_Clicked_Counter = 0;

        // Mechanika pokera 
        public void Ranking()
        {

        }
        public Form1()
        {
            InitializeComponent();
            // Z CODING HOMEWORK
            pictures = new PictureBox[52];
        }

        // wyciagniecie z nazwy numeru zdjecia i wpisanie go do tablicy
        /*string[]*/ List<string> PathToCardNumber() // wartosc z konkretnego indeksu jest rowna karcie z talii
        {
            //string[] cardsNumber = new string[Files.Count];
            List<string> cardsNumber = new List<string>();
            const string v = "char";
            for (int i = 0; i < Files.Count; i++)
            {
                cardsNumber.Add(Files[i].Substring(74, Files[i].Length - 74 - 4));
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

                    var temp = Files[firstCard];
                    Files[firstCard] = Files[secondCard];
                    Files[secondCard] = temp;
                    firstCard = secondCard;

                }
            }
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
            FirstHand();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ChangePicture();
            timer1.Stop();
        }
        private void FirstHand()
        {
            /*string[]*/ List<string> cardsNumber = PathToCardNumber(); // tablica przechowujaca numery obrazow
            // z kartami
            // Do we have pictures in list?
            if (Files.Count > 2)
            {
                // OK lets grab the first one
                string File = Files.First();
                // Load it
                // 1st card
                //int cardNumber1 = int.Parse(cardsNumber[0]);
                int cardNumber1 = int.Parse(cardsNumber.First());
                pictureBox1.Load(File);
                // parsuje nr karty do inta
                cards_to_change[0] = cardNumber1;
                cardsTable[0] = deck.cardValue(cardNumber1);
                //Ranking(cardsTable[0].MySuit, cardsTable[0].MyValue);
                deck.printCardName(cardNumber1); // kolor i figura karty
                Console.WriteLine(cardsNumber[0]); // numer obrazu karty
                Files.RemoveAt(0);

                // 2nd card
                File = Files.First();
                int cardNumber2 = int.Parse(cardsNumber[1]);
                pictureBox2.Load(File);
                //int cardNumber2 = int.Parse(cardsNumber[1]);
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
            }
            else
            {
                // Out of pictures, stopping timer
                // and wait God to do something
                timer1.Stop();
            }
        }
        private void ChangePicture()
        {
            //string[] cardsNumber = PathToCardNumber(); // tablica przechowujaca numery obrazow
            List<string> cardsNumber = PathToCardNumber();
            // z kartami
            // Do we have pictures in list?
            if (Files.Count > 2)
            {
                // OK lets grab the first one
                string File;  
                // Load it
                // 1st card
                
                if (cards_to_change[0] != 0)
                {
                    int cardNumber1 = int.Parse(cardsNumber.First());
                    File = Files.First();
                    pictureBox1.Load(File);
                    // parsuje nr karty do inta
                    cards_to_change[0] = cardNumber1;
                    deck.printCardName(cardNumber1); // kolor i figura karty
                    //Console.WriteLine(cardsNumber[0]); // numer obrazu karty
                    Console.WriteLine(cardsNumber.First());
                    cardsNumber.RemoveAt(0);
                    Files.RemoveAt(0);
                }
                // 2nd card
                if (cards_to_change[1] != 0)
                {
                    int cardNumber2 = int.Parse(cardsNumber.First());
                    File = Files.First();
                    pictureBox2.Load(File);
                    cards_to_change[1] = cardNumber2;
                    deck.printCardName(cardNumber2); // kolor i figura karty
                    Console.WriteLine(cardsNumber.First()); // numer obrazu karty
                    cardsNumber.RemoveAt(0);
                    Files.RemoveAt(0);
                }
                // 3rd card
                if (cards_to_change[2] != 0)
                {
                    int cardNumber3 = int.Parse(cardsNumber.First());
                    File = Files.First();
                    pictureBox3.Load(File);
                    cards_to_change[2] = cardNumber3;
                    deck.printCardName(cardNumber3); // kolor i figura karty
                    Console.WriteLine(cardsNumber.First());
                    // numer obrazu karty
                    cardsNumber.RemoveAt(0);
                    Files.RemoveAt(0);
                }
                // 4th card
                if (cards_to_change[3] != 0)
                {
                    int cardNumber4 = int.Parse(cardsNumber.First());
                    File = Files.First();
                    pictureBox4.Load(File);
                    cards_to_change[3] = cardNumber4;
                    deck.printCardName(cardNumber4); // kolor i figura karty
                    Console.WriteLine(cardsNumber.First()); // numer obrazu karty
                    cardsNumber.RemoveAt(0);
                    Files.RemoveAt(0);
                }
                // 5th card
                if (cards_to_change[4] != 0)
                {
                    int cardNumber5 = int.Parse(cardsNumber.First());
                    File = Files.First();
                    pictureBox5.Load(File);
                    cards_to_change[4] = cardNumber5;
                    deck.printCardName(cardNumber5); // kolor i figura karty
                    Console.WriteLine(cardsNumber.First()); // numer obrazu karty
                    cardsNumber.RemoveAt(0);
                    Files.RemoveAt(0);
                }
                button7.Enabled = false; // wylacz z uzycia 'DEAL' po dobraniu nowych kart  
                // zaladuj wszystkie wylosowane 5 kart do tablicy
                //int[] cardsOnTheTable = { cardNumber1, cardNumber2, cardNumber3, cardNumber4,
                //cardNumber5};
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
            button1_Was_Clicked = true;
            button1_Was_Clicked_Counter++;
            if (button1_Was_Clicked_Counter % 2 == 1)
            {
                button1.BackColor = Color.Green;
            }
            else
            {
                button1.BackColor = Color.White;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2_Was_Clicked = true;
            button2_Was_Clicked_Counter++;
            if (button2_Was_Clicked_Counter % 2 == 1)
            {
                button2.BackColor = Color.Green;
            }
            else
            {
                button2.BackColor = Color.White;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3_Was_Clicked = true;
            button3_Was_Clicked_Counter++;
            if (button3_Was_Clicked_Counter % 2 == 1)
            {
                button3.BackColor = Color.Green;
            }
            else
            {
                button3.BackColor = Color.White;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4_Was_Clicked = true;
            button4_Was_Clicked_Counter++;
            if (button4_Was_Clicked_Counter % 2 == 1)
            {
                button4.BackColor = Color.Green;
            }
            else
            {
                button4.BackColor = Color.White;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5_Was_Clicked = true;
            button5_Was_Clicked_Counter++;
            if (button5_Was_Clicked_Counter % 2 == 1)
            {
                button5.BackColor = Color.Green;
            }
            else
            {
                button5.BackColor = Color.White;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int btn1mod = button1_Was_Clicked_Counter % 2;
            if (button1_Was_Clicked == true && btn1mod == 1)
            {
                cards_to_change[0] = 0;
            }
            int btn2mod = button2_Was_Clicked_Counter % 2;
            if (button2_Was_Clicked == true && btn2mod == 1)
            {
                cards_to_change[1] = 0;
            }
            int btn3mod = button3_Was_Clicked_Counter % 2;
            if (button3_Was_Clicked == true && btn3mod == 1)
            {
                cards_to_change[2] = 0;
            }
            int btn4mod = button4_Was_Clicked_Counter % 2;
            if (button4_Was_Clicked == true && btn4mod == 1)
            {
                cards_to_change[3] = 0;
            }
            int btn5mod = button5_Was_Clicked_Counter % 2;
            if (button5_Was_Clicked == true && btn5mod == 1)
            {
                cards_to_change[4] = 0;
            }
            ChangePicture();
        }
    }
}