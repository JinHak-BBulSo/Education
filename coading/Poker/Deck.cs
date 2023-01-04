using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrumpCardGame
{
    internal class Deck
    {
        private List<Card> mCards;

        public Deck()
        {
            Init();
        }

        public void Init()
        {
            CardSetup();
            CardShuffle();
        }

        public void CardSetup()
        {
            try
            {
                if (mCards != null)
                {
                    mCards.Clear();
                }
                mCards = new List<Card>();
                string[] Marks = new string[4] { "♣", "♥", "◆", "♠" };

                for (int i = 1; i <= 52; i++)
                {
                    Card card = new Card();
                    switch (i % 13)
                    {
                        case 0:
                            card.Name = "K";
                            card.Number = 13;
                            break;
                        case 1:
                            card.Name = "A";
                            card.Number = 14;
                            break;
                        case 11:
                            card.Name = "J";
                            card.Number = 11;
                            break;
                        case 12:
                            card.Name = "Q";
                            card.Number = 12;
                            break;
                        default:
                            card.Name = Convert.ToString(i % 13);
                            card.Number = i % 13;
                            break;
                    }
                    card.Mark = Marks[(i - 1) / 13];
                    card.MarkNum = (i - 1) / 13;
                    mCards.Add(card);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void CardShuffle()
        {
            try
            {
                Random random = new Random();
                for (int i = 0; i < 100; i++)
                {
                    int sourIdx = random.Next(0, 52);
                    int destIdx = random.Next(0, 52);
                    Card temp = cards[sourIdx];
                    cards[sourIdx] = cards[destIdx];
                    cards[destIdx] = temp;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Card> cards
        {
            get { return mCards; }
            set { mCards = value; }
        }
    }
}
