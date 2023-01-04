using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrumpCardGame
{
    internal class Player
    {
        private int mMoney;
        private List<Card> mCards;

        public Player(int money)
        {
            mMoney = money;
            mCards = new List<Card>();
        }

        public int Betting()
        {
            int userBettingMoney;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out userBettingMoney))
                {
                    if (mMoney < userBettingMoney)
                    {
                        Console.WriteLine("소지한 금액만큼만 배팅하실 수 있습니다. 호갱님");
                        Console.Write("베팅 금액을 입력 : ");
                        continue;
                    }
                    else
                    {
                        mMoney -= userBettingMoney;
                        return userBettingMoney;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public void AddCard(Card card)
        {
            mCards.Add(card);
        }

        public void AddMoney(int money)
        {
            mMoney += money;
        }

        public int Money
        {
            get { return mMoney; }
            set { mMoney = value; }
        }

        public List<Card> Cards
        {
            get { return mCards; }
            set { mCards = value; }
        }
    }
}
