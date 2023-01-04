using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrumpCardGame
{
    internal class Dealer
    {
        private List<Card> mCards;
        public Dealer()
        {
            mCards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            mCards.Add(card);
        }

        public List<Card> Cards
        {
            get
            {
                return mCards;
            }
            set
            {
                Cards = value;
            }
        }
    }
}
