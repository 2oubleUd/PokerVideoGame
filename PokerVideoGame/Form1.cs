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
        public DeckOfCards deck = new DeckOfCards();
        

        // List of files to show 
        private List<string> Files;

        // tablica z PictureBoxami - z CODINGHOMEWORK 
        private PictureBox[] pictures;
        public const string imagePath =
            @"C:\Users\bilin\OneDrive\Pulpit\Nikolai\PokerVideoGame2\PokerVideoGame\PNG-cards-1.3";
        

        public Form1()
        {
            InitializeComponent();
            // Z CODING HOMEWORK
            pictures = new PictureBox[52];
        }
        string[] PathToCardNumber()
        {
            string[] cardsNumber=new string[Files.Count];
            const string v = "char";
            for (int i = 0; i < Files.Count; i++)
            {

                cardsNumber[i] = Files[i].Substring(84, Files[i].Length-84 - 4);

            }
            return cardsNumber;

        }
       
        // z CODING HOMEWORK
        private void ShuffleCards()
        {
            
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int firstCard = random.Next(0, 52); // 0 do 52 bo 52 sie nie wlicza do tego zakresu
                                                    // numer karty, ktory chce potasowac i indeks karty Z ktora chce potasowac
                int secondCard = random.Next(0, 52);
                if (firstCard != secondCard) // zeby nie tasowac tej samej karty ze soba
                {   
                    // tasowanie kart
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
        // StartUp
        private void Form1_Load(object sender, EventArgs args)
        {
            // basic settings
            var ext = new List<string> { ".jpg", ".gif", ".png" };
            // we use same directory where program is 
            string targetDirectory = /*Directory.GetCurrentDirectory()*/ imagePath;
            // Here we create our list of files
            // new list
            // user GetFiles to getfilenames
            // Filter unwanted stuff away (like our program)
            Files = new List<string>
                (Directory.GetFiles(targetDirectory, "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => ext.Any(e => s.EndsWith(e))));

            // Potasuj karty przed rozpczeciem gry
            ShuffleCards();
            
            ChangePicture();
          

        }


        private void ChangePicture()
        {
            // trzeba napisac warunek na to ze jak zaczyna sie gre to losuje sie 5 kart
            // po czym jak sie dobierze nowe karty to gra sie zaczyna od nowa (przetasowanie kart i od nowa)
            // pytanie jak przeniesc obiektowa mechanike pokera do formsa? Mozna podpatrzec z tego projektu ze
            // snake'iem
            // Do we have pictures in list?
            if(Files.Count > 2)
            {
                
                // OK lets grab the first one
                string File = Files.First();

                Console.WriteLine("XDDDDDDDDDDDDDDDDDDDDDDDDDDDD");
                string[] cardsNumber=PathToCardNumber();
                Console.WriteLine(File);
                // Load it
                // 1st card
                pictureBox1.Load(File);
                Files.RemoveAt(0);
                // 2nd card
                File = Files.First();
                pictureBox2.Load(File);
                Files.RemoveAt(0);
                // 3rd card
                File = Files.First();
                pictureBox3.Load(File);
                Files.RemoveAt(0);
                // 4th card
                File = Files.First();
                pictureBox4.Load(File);
                Files.RemoveAt(0);
                // 5th card
                File = Files.First();
                pictureBox5.Load(File);
                Files.RemoveAt(0);
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
            ChangePicture();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
