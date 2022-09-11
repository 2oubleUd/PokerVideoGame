using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace PokerVideoGame
{
    public class DeckOfCards : Card
    {
        const int NUM_OF_CARDS = 52; // number of all cards
        public Card[] deck; // array of all playing cards (zmienilem na public)

        public DeckOfCards() // konstruktor
        {
            deck = new Card[NUM_OF_CARDS];
        }
        // chcemy otrzymac obecny deck 
        public Card[] getDeck { get { return deck; } } // ta funkcja tylko wyswietla obecny deck, wiec nie 
        // potrzebuje niczego ustawiac - dlatego nie ma set'a

        // create deck of 52 cards: 13 values each with 4 suits
        public void SetUpDeck()
        {
            int i = 0;
            foreach (VALUE v in Enum.GetValues(typeof(VALUE))) // zwraca figure karty
            {
                foreach (SUIT s in Enum.GetValues(typeof(SUIT)))
                {
                    deck[i] = new Card { MySuit = s, MyValue = v }; // pobrany kolor i figura sa wpisywane jako karta
                    // do tablicy przechowujacej talie
                    i++;
                }
            }
        }

        public string printCardName(int i)
        {
            Card card = deck[i - 1];
            Console.WriteLine(card.MyValue + " of " + card.MySuit);
            return card.MyValue + " of " + card.MySuit;
        }
        public Card cardValue(int i)
        {
            Card card = deck[i - 1];
            return card;
        }

    }
}