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
using System.Collections;

namespace PokerVideoGame
{
    public partial class Form1 : Form
    {
        // GENEROWANIE NOWEJ TALII
        private DeckOfCards deck = new DeckOfCards();

        // List of files to show 
        private List<string> Files;

        // Sciezka do folderu z obrazami kart
        public const string imagePath =
        @"C:\Users\Mikolaj\source\repos\PokerVideoGame\PokerVideoGame\PNG-cards-1.3\";


        public List<Card> cardsOnTable = new List<Card>();
        public Card[] cardsTable = new Card[5]; // tablica na karty, ktore obecnie sa na stole

        // Money
        private int cash = 100; // 100$ na start
        public int coinValue = 10; // wielkosc zakladu (wcisniecie "ADD COIN" zwieksza o
        // krotnosc w zakresie od 1 do 5 - czyli np jak krotnsc 2 to 2*10=20 wielkosc 
        // zakladu
        public int coinLeverage = 1; // mnoznik kwoty zakladu

        public int wage = 0; // stawka zakladu

        // 1 oznacza, ze karta ma zostac zmieniona w nastepnym rozdaniu
        // na skutek klikniecia "HOLD" 1 zostaje zamieniona na 0
        // 0 oznacza, ze karta ma pozostac w danym miejscu bez zmian
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

        // counter for DEAL button
        private int button7_Was_Clicked_Counter = 0;

        public Form1()
        {
            InitializeComponent();
            // Z CODING HOMEWORK
            //pictures = new PictureBox[52];
        }

        private void ResetButtons()
        {
            // RESET KART KTORE MAJA ZOSTAC
            for (int i = 0; i < cards_to_change.Length; i++)
                cards_to_change[i] = 1;

            // RESTART THE BUTTONS
            button7.Enabled = true;
            //Buttons clicked event
            button1_Was_Clicked = false;
            button2_Was_Clicked = false;
            button3_Was_Clicked = false;
            button4_Was_Clicked = false;
            button5_Was_Clicked = false;
            // counters for buttons (change color if clicked based on modulo)
            button1_Was_Clicked_Counter = 0;
            button2_Was_Clicked_Counter = 0;
            button3_Was_Clicked_Counter = 0;
            button4_Was_Clicked_Counter = 0;
            button5_Was_Clicked_Counter = 0;

            button7_Was_Clicked_Counter = 0;

            // reset the buttons back ground color
            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;
            button5.BackColor = Color.White;
        } // Reset wszystkich przyciskow

        // ******* OBSLUGA KART *******

        // wyciagniecie z nazwy numeru zdjecia i wpisanie go do tablicy
        List<string> PathToCardNumber()
        {
            //string[] cardsNumber = new string[Files.Count];
            List<string> cardsNumber = new List<string>();
            for (int i = 0; i < Files.Count; i++)
            {
                cardsNumber.Add(Files[i].Substring(74, Files[i].Length - 74 - 4));
            }
            return cardsNumber;
        } // wartosc z konkretnego indeksu jest rowna
          // karcie z talii

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
                    // tasowaniu podlega tylko obraz karty 
                    // sama karta jest przypisywana z talii na podstawie indeksu obrazu
                    var temp = Files[firstCard];
                    Files[firstCard] = Files[secondCard];
                    Files[secondCard] = temp;
                    firstCard = secondCard;
                
                }
            }
        } // tasowanie talii obrazow

        public List<string> InitListOfPictures()
        {
            // basic settings
            var ext = new List<string> { ".jpg", ".gif", ".png" };
            // we use same directory where program is 
            string targetDirectory = imagePath;
            // Here we create our list of files new list user GetFiles to getfilenames
            // Filter unwanted stuff away (like our program)
            Files = new List<string>
                (Directory.GetFiles(targetDirectory, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => ext.Any(e => s.EndsWith(e))));
            return Files;
        } // generowanie listy obrazow kart

        public Card[] FirstHand()
        {
            List<string> cardsNumber = PathToCardNumber(); // tablica przechowujaca numery obrazow
            // z kartami
            // Do we have pictures in list?
            if (Files.Count > 2)
            {
                // 1st card
                string File = Files.First();
                pictureBox1.Load(File);
                int cardNumber1 = int.Parse(cardsNumber[0]);
                cardsTable[0] = DrawCard(pictureBox1, File, cardNumber1);

                deck.printCardName(cardNumber1); // kolor i figura karty
                Console.WriteLine(cardsNumber[0]); // numer obrazu karty
                Files.RemoveAt(0);

                // 2nd card
                File = Files.First(); // pierwszy z gory nr obrazu karty
                pictureBox2.Load(File); // zaladuj obraz o tym numerze
                int cardNumber2 = int.Parse(cardsNumber[1]);
                cardsTable[1] = DrawCard(pictureBox2, File, cardNumber2);
                Files.RemoveAt(0);

                // 3rd card
                File = Files.First();
                pictureBox3.Load(File);
                int cardNumber3 = int.Parse(cardsNumber[2]);
                cardsTable[2] = DrawCard(pictureBox3, File, cardNumber3);
                Files.RemoveAt(0);
                // 4th card
                File = Files.First();
                pictureBox4.Load(File);
                int cardNumber4 = int.Parse(cardsNumber[3]);
                cardsTable[3] = DrawCard(pictureBox4, File, cardNumber4);
                Files.RemoveAt(0);
                // 5th card
                File = Files.First();
                pictureBox5.Load(File);
                int cardNumber5 = int.Parse(cardsNumber[4]);
                cardsTable[4] = DrawCard(pictureBox5, File, cardNumber5);
                Files.RemoveAt(0);
                return cardsTable;
            }
            else
            {
                // Out of pictures, stopping timer
                // and wait God to do something
                timer1.Stop();
                return cardsTable;
            }
        } // poczatek rozdania

        // lista z numerami kart i numer konkretnej karty z talii
        public Card DrawCard(PictureBox picture, string File, int cardNumber) 
        {
            //File = Files.First();
            picture.Load(File);
            // parsuje nr karty do inta
            deck.printCardName(cardNumber); // kolor i figura karty
            Card card = deck.cardValue(cardNumber);
            Console.WriteLine(cardNumber); // numer obrazu karty
            //Files.RemoveAt(0);
            return card;
        }

        private void ChangePicture()
        {
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

                    // Add this new card to the table
                    cardsTable[0] = DrawCard(pictureBox1, File, cardNumber1);

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

                    // 
                    cardsTable[1] = DrawCard(pictureBox2, File, cardNumber2);

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

                    cardsTable[2] = DrawCard(pictureBox3, File, cardNumber3);

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

                    cardsTable[3] = DrawCard(pictureBox4, File, cardNumber4);

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

                    cardsTable[4] = DrawCard(pictureBox5, File, cardNumber5);

                    Console.WriteLine(cardsNumber.First()); // numer obrazu karty
                    cardsNumber.RemoveAt(0);
                    Files.RemoveAt(0);
                }
                button7.Enabled = false; // wylacz z uzycia 'DEAL' po dobraniu nowych kart  
                // zaladuj wszystkie wylosowane 5 kart do tablicy
            }

            else
            {
                // Out of pictures, stopping timer
                // and wait God to do something
                timer1.Stop();
            }
        } // wymiana wybranych kart

        #region PokerRanking
        // ******** SPRAWDZANIE UKLADOW POKEROWYCH ********

        public int Ranking(Card[] table, int wage)
        {
            // tu wywolania poszczegolnych funkcji sprawdzajacych uklady
            // ...

            if(Pair(table))
            {
                cash = cash + wage;
                Console.WriteLine($"Cash: " + cash);
            }

            if (TwoPairs(table))
            {
                cash = cash + (2 * wage);
                Console.WriteLine($"Cash: " + cash);
            }
            
            if(ThreeOfKind(table))
            {
                cash = cash + (3 * wage);
                Console.WriteLine($"Cash: " + cash);
            }
           
            if(FourOfKind(table))
            {
                cash = cash + (25 * wage);
                Console.WriteLine($"Cash: " + cash);
            }
           
            if(Flush(table))
            {
                cash = cash + (6 * wage);
                Console.WriteLine($"Cash: " + cash);
            }
           
            if(FullHouse(table))
            {
                cash = cash + (9 * wage);
                Console.WriteLine($"Cash: " + cash);
            }
         
            if(Straight(table))
            {
                cash = cash + (4 * wage);
                Console.WriteLine($"Cash: " + cash);
            }
         
            if(StraightFlush(table))
            {
                cash = cash + (50 * wage);
                Console.WriteLine($"Cash: " + cash);
            }

            return 0;
        }

        public bool Pair(Card[] table)
        {
            // see if exacly 2 cards on the table have the same rank
            if(table.GroupBy(c => c.MyValue).Count(g => g.Count() == 2) == 1)
            {
                Console.WriteLine("It's a pair !!!");
                return table.GroupBy(c => c.MyValue).Count(g => g.Count() == 2) == 1;
            }
            else
            {
                return false;
            }
        }

        public bool TwoPairs(Card[] table)
        {
            // see if there are 2 lots of exacly 2 cards with the same rank
            if(table.GroupBy(c => c.MyValue).Count(g => g.Count() == 2) == 2)
            {
                Console.WriteLine("Two pairs !!!");
                return table.GroupBy(c => c.MyValue).Count(g => g.Count() == 2) == 2;
            }
            else
            {
                return false;
            }
            
        }

        public bool ThreeOfKind(Card[] table)
        {
            if (table.GroupBy(c => c.MyValue).Any(g => g.Count() == 3))
            {
                Console.WriteLine("Three of kind !!!");
                return table.GroupBy(c => c.MyValue).Any(g => g.Count() == 3);
            }
            else
            {
                return false;
            }
            
        }

        public bool FourOfKind(Card[] table)
        {
            if (table.GroupBy(c => c.MyValue).Any(g => g.Count() == 4))
            {
                Console.WriteLine("Four of kind !!!");
                return table.GroupBy(c => c.MyValue).Any(g => g.Count() == 4);
            }
            else
            {
                return false;
            }
        }

        public bool Flush(Card[] table)
        {
            if(table.GroupBy(c => c.MySuit).Count(g => g.Count() >= 5) == 1)
            {
                Console.WriteLine("It's a Flush !!!");
                return table.GroupBy(c => c.MySuit).Count(g => g.Count() >= 5) == 1;
            }
            else
            {
                return false;
            }
        }

        public bool FullHouse(Card[] table)
        {
            if (table.GroupBy(c => c.MyValue).Count(g => g.Count() == 2) == 2
                && table.GroupBy(c => c.MyValue).Any(g => g.Count() == 3))
            {
                Console.WriteLine("Full House !!!");
                return TwoPairs(table) && ThreeOfKind(table);
            }
            else
            {
                return false;
            }
            
        }

        // Straight
        public bool Straight(Card[] table)
        {
            var ordered = table.OrderByDescending(a => a.MyValue).ToList();
            for(int i = 0; i < ordered.Count-5; i++)
            {
                var skipped = ordered.Skip(i);
                var possibleStraight = skipped.Take(5);
                if(IsStraight(possibleStraight))
                {
                    Console.WriteLine("It's STRAIGHT");
                    return true;
                }
            }
            return false;
        }

        public bool IsStraight(IEnumerable <Card> table)
        {
            if(table.GroupBy(c => c.MyValue).Count() == table.Count()
                && table.Max(c => (int)c.MyValue) - table.Min(c => (int)c.MyValue) == 4)
            {
                Console.WriteLine("It's a STRAIGHT");
                return table.GroupBy(c => c.MyValue).Count() == table.Count()
                && table.Max(c => (int)c.MyValue) - table.Min(c => (int)c.MyValue) == 4;
            }
            else
            {
                return false;
            }
        }

        // Stright Flash    
        public bool StraightFlush(Card[] table)
        {
            if (Straight(table) && Flush(table))
            {
                Console.WriteLine("It's Straight Flush !!!");
                return Straight(table) && Flush(table);
            }
            else
            {
                return false;
            }

        }

        #endregion


        // *********************** MONEY ***********************

        public int SetWage(int coinValue, int leverage) // stawka zakladu =
        // wartosc monety i jej mnoznik
        {
            Console.WriteLine($"WAGER = " + (coinValue * leverage));
            return coinValue * leverage;
        }

        // *********************** StartUp ***********************
        private void Form1_Load(object sender, EventArgs args)
        {
            // Na poczatku gry w miejscu kart powinny byc wstawione obrazy przedstawiajace
            // ich tyly. Dopiero po wcisnieciu klawisza 'DEAL' karty zostana rozlosowane
            //ResetButtons();
            //Files = InitListOfPictures(); // wczytaj zdjecia kart do listy

            //// Wczytanie tylow kart XD
            //string backside =
            //    @"C:\Users\Mikolaj\source\repos\PokerVideoGame\PokerVideoGame\PNG-backside\53.png";
            //pictureBox1.Load(backside);
            //pictureBox2.Load(backside);
            //pictureBox3.Load(backside);
            //pictureBox4.Load(backside);
            //pictureBox5.Load(backside);
            PlayFurther(); 
            // JEST TAKI PROBLEM, ZE DOPIERO PO WCISNIECIU PRZYCISKU 'DEAL' POWINNY BYC
            // LOSOWANE KARTY 
            //ShuffleCards(); // potasuj je
            //cardsTable = FirstHand(); // z potasowanej talii wyciagnij 5 kart
            //Ranking(cardsTable); // odczytaj jaki uklad przedstawiaja
            //SetWage(coinValue, coinLeverage); // ustal kwote zakladu
        }
        
        private void PlayFurther()
        {
            ResetButtons();
            Files = InitListOfPictures();
            ShuffleCards();
            wage = SetWage(coinValue, coinLeverage);
            cash = cash - wage;
            cardsTable = FirstHand();
            //Ranking(cardsTable, wage);
            Console.WriteLine($"CASH: " + cash);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ChangePicture();
            timer1.Stop();
        }

        // ******************* OBSLUGA PRZYCISKOW *******************
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

        // Obsluga przycisku ADD COIN
        private void button6_Click(object sender, EventArgs e)
        {

            if(coinLeverage < 5)
            {
                coinLeverage++;
                Console.WriteLine(coinLeverage);
                SetWage(coinValue, coinLeverage);
            }
            else
            {
                coinLeverage = 1;
                Console.WriteLine(coinLeverage);
                SetWage(coinValue, coinLeverage);
            }          
        }

        private async void button7_Click(object sender, EventArgs e) // Button DEAL
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
             Ranking(cardsTable, wage);
             await Task.Delay(5000);
             // NIE WIEM JAK ZROBIC ZEBY PO ZAKONCZENIU ROZDANIA WROCIC NA POCZATEK PROGRAMU
             // ROZLOSOWAC NA NOWO KARTY ITD. ZROBILEM TA FUNCKJE PlayFurther, ktora jest 
             // kopia Form1_Load ale to jest bez sensu troche xD bo wtedy nie dziala zostawianie
             // zaznaczonych kart
             
             PlayFurther();
        }
    }
}