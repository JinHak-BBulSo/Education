using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using System.Threading.Tasks;

namespace coading
{
    internal class Program
    {
        /**
         * 트럼프 카드 게임
         * 컴퓨터가 5장 뽑고 플레이어 확인
         * 플레이어 5장 뽑고 베팅( 0 ~ 한도 )
         * 컴퓨터는 2장의 카드를 추가
         * 플레이어는 2장의 카드를 교체 가능
         * 모든 행동 후 결과 확인
         * 플레이어 승리시 2배
         * 플레이어 패배시 베팅금액 잃음
         * 10만포 or 0포시 게임셋
         * 시작 만포
         * 노페어, 원, 투, 트리플, 포카드, 플러쉬, 스트레이트 (기본형)
         * 마운틴, 백스트, 풀하우스, 스티플, 로티플(어려움)
         */
        
        static void Main(string[] args)
        {
            PokerGame poker = new PokerGame();
            poker.StartGame();
        } // Main
    }
    class PokerGame : Trump
    {
        private int playerMaxMark;
        private int playerMaxNum;
        private int playerMinMark;
        private int playerMinNum;
        private int playerScore;

        private int computerMaxMark;
        private int computerMaxNum;
        private int computerMinMark;
        private int computerMinNum;
        private int computerScore;

        private List<(int, int)> playerCheckNumberAndMark = new List<(int, int)>();
        private List<(int, int)> computerCheckNumberAndMark = new List<(int, int)>();

        static int POINT = 10000;
        public int Betting()
        {
            int betMoney = 0;

            while (true)
            {
                Console.Write("베팅할 금액을 입력해 주세요. : ");
                int.TryParse(Console.ReadLine(), out betMoney);

                if (betMoney < 0 || betMoney > POINT)
                {
                    Console.WriteLine("잘못된 금액입니다.\n" +
                        "다시 입력해 주세요.");
                }
                else
                    break;
            } // POINT 베팅

            return betMoney;
        }
        public int Betting(int money)
        {
            return money;
        }
        public void StartGame()
        {
            Trump trump = new Trump();
            int changeCardNum = -10;
            int changeCount = 0;

            trump.SetUpCards();
            trump.DrawNumber(5, "Computer");
            trump.DrawNumber(5, "Player");

            trump.RollComputerCard(5);
            trump.RollPlayerCard();

            while (changeCount < 2)
            {
                Console.WriteLine("변경을 원하지 않는다면 -1을 입력해 주세요.");
                Console.Write("변경할 카드를 선택해 주세요. : ");
                int.TryParse(Console.ReadLine(), out changeCardNum);
                if (changeCardNum == -1) break;
                else if(changeCardNum <= 0 || changeCardNum > 6)
                {
                    Console.WriteLine("잘못된 입력입니다. 재입력 바랍니다.");
                    continue;
                }
                else
                {
                    trump.ChangeCard(changeCardNum);
                    trump.RollPlayerCard();
                    changeCount++;
                }
            }

            Console.WriteLine("컴퓨터가 2장을 추가로 뽑습니다.");
            trump.DrawNumber(2, "Computer");
            trump.RollComputerCard(7);
            trump.RollPlayerCard();

            checkDeck(trump);
        }

        public void checkDeck(Trump trump)
        {
            List<int> playerCard = trump.PlayerCardNum.ToList();
            List<int> computerCard = trump.ComputerCardNum.ToList();

            foreach(int number in playerCard)
            {
                playerCheckNumberAndMark.Add(((number % 13) + 1, number / 13));
            }
            foreach(int number in computerCard)
            {
                computerCheckNumberAndMark.Add(((number % 13) + 1, number / 13));
            }
            playerCheckNumberAndMark.Sort();
            computerCheckNumberAndMark.Sort();

            isOnePair(playerCheckNumberAndMark, "Player");
            isOnePair(computerCheckNumberAndMark, "Computer");

            isTwoPair(playerCheckNumberAndMark, "Player");
            isTwoPair(computerCheckNumberAndMark, "Computer");

            isTripple(playerCheckNumberAndMark, "Player");
            isTripple(computerCheckNumberAndMark, "Computer");

            isFlush(playerCard, "Player");
            isFlush(computerCard, "Computer");

            isFourCard(playerCheckNumberAndMark, "Player");
            isFourCard(computerCheckNumberAndMark, "Computer");

            Console.WriteLine("플레이어의 점수 : {0}", playerScore);
            Console.WriteLine("컴퓨터의 점수 : {0}", computerScore);
        }
        
        private void isOnePair(List<(int, int)> numberAndMark, string whoIs)
        {
            for(int i = 0; i < numberAndMark.Count() - 2; i++)
            {
                if (numberAndMark[i].Item1 == numberAndMark[i + 1].Item1)
                {
                    if (whoIs == "Player")
                    {
                        playerScore = 1;
                        playerMaxMark = numberAndMark[i].Item2;
                        playerMaxNum = numberAndMark[i].Item1;
                        if (playerMaxNum == 1) break;
                    }
                    else
                    {
                        computerScore = 1;
                        computerMaxMark = numberAndMark[i].Item2;
                        computerMaxNum = numberAndMark[i].Item1;
                        if(computerMaxNum == 1) break;
                    }
                }
            }
        }

        private void isTwoPair(List<(int, int)> numberAndMark, string whoIs)
        {
            for (int i = 0; i < numberAndMark.Count() - 3; i++)
            {
                if (numberAndMark[i].Item1 == numberAndMark[i + 1].Item1)
                {
                    for(int j = i + 2; j < numberAndMark.Count() - 1; j++)
                    {
                        if (numberAndMark[j].Item1 == numberAndMark[j + 1].Item1)
                        {
                            if (whoIs == "Player")
                            {
                                playerScore = 2;
                                playerMinMark = numberAndMark[i].Item2;
                                playerMinNum = numberAndMark[i].Item1;
                                playerMaxMark = numberAndMark[j].Item2;
                                playerMaxNum= numberAndMark[j].Item1;
                                if(playerMaxMark > playerMinMark) playerMaxNum = playerMinNum;
                            }
                            else
                            {
                                computerScore = 2;
                                computerMinMark = numberAndMark[i].Item2;
                                computerMinNum = numberAndMark[i].Item1;
                                computerMaxMark = numberAndMark[j].Item2;
                                computerMaxNum= numberAndMark[j].Item1;
                                if(computerMaxMark > computerMinMark) computerMaxNum = computerMinNum;
                            }
                        } 
                    }
                    if (playerMinNum == 1 && whoIs == "Player") break;
                    else if (computerMinNum == 1) break;
                }
            }
        }

        private void isTripple(List<(int, int)> numberAndMark, string whoIs)
        {
            for (int i = 0; i < numberAndMark.Count() - 2; i++)
            {
                if (numberAndMark[i].Item1 == numberAndMark[i + 1].Item1 && numberAndMark[i + 1].Item1 == numberAndMark[i + 2].Item1)
                {
                    if (whoIs == "Player")
                    {
                        playerScore = 3;
                        playerMaxMark = numberAndMark[i].Item2;
                        if (numberAndMark[i].Item1 == 1) playerMaxNum = numberAndMark[i].Item1;
                        else
                            playerMaxNum = numberAndMark[i + 2].Item1;
                    }
                    else
                    {
                        computerScore = 3;
                        computerMaxMark = numberAndMark[i].Item2;
                        if(numberAndMark[i].Item1 == 1) computerMaxNum = numberAndMark[i].Item1;
                        else
                            computerMaxNum = numberAndMark[i + 2].Item1;
                    }
                }
            }
        }
        private void isStraight(List<(int, int)> numberAndMark, string whoIs)
        {
            if(whoIs == "Player")
            {
                if (numberAndMark[0].Item1 == 1 && numberAndMark[1].Item1 == 10)
                {
                    if(
                    (numberAndMark[0].Item1 - numberAndMark[1].Item1 == -9) &&
                    (numberAndMark[1].Item1 - numberAndMark[2].Item1 == -1) &&
                    (numberAndMark[2].Item1 - numberAndMark[3].Item1 == -1) &&
                    (numberAndMark[3].Item1 - numberAndMark[4].Item1 == -1)
                    )
                    {

                    }
                }
                else if (
                    (numberAndMark[0].Item1 - numberAndMark[1].Item1 == -1) &&
                    (numberAndMark[1].Item1 - numberAndMark[2].Item1 == -1) &&
                    (numberAndMark[2].Item1 - numberAndMark[3].Item1 == -1) &&
                    (numberAndMark[3].Item1 - numberAndMark[4].Item1 == -1)
                    )
                {

                }
            }
            else
            {

            }
        }
        private void isFlush(List<int> card, string whoIs)
        {
            if(whoIs == "Player")
            {
                bool isFlush = true;
                int maxNum = (card[0] % 13);
                int n = card[0] / 13;
                foreach(int mark in card)
                {
                    if (mark % 13 == 1) maxNum = 1;
                    if((mark % 13)> maxNum && maxNum != 1) maxNum = mark % 13;
                    if (n != (mark / 13))
                    {
                        isFlush = false;
                        break;
                    }
                }
                if (isFlush)
                {
                    playerMaxMark = n;
                    playerMaxNum = maxNum;
                    playerScore = 5;
                }
            }
            else
            {
                bool isFlush = true;
                int maxNum = (card[0] % 13);
                int n = card[0] / 13;
                for (int i = 0; i < 3; i++)
                {
                    int mark = card[i];
                    if (mark % 13 == 1) maxNum = 1;
                    if ((mark % 13) > maxNum && maxNum != 1) maxNum = mark % 13;
                    if (n != (mark / 13))
                    {
                        isFlush = false;
                        break;
                    }
                }
                if (isFlush)
                {
                    computerMaxMark = n;
                    playerMaxNum = maxNum;
                    playerScore = 5;
                }
            }
            
        }
        private void isFourCard(List<(int, int)> numberAndMark, string whoIs)
        {
            for (int i = 0; i < numberAndMark.Count() - 3; i++)
            {
                if (numberAndMark[i].Item1 == numberAndMark[i + 1].Item1 &&
                    numberAndMark[i + 1].Item1 == numberAndMark[i + 2].Item1 &&
                    numberAndMark[i + 2].Item1 == numberAndMark[i + 3].Item1)
                {
                    if (whoIs == "Player")
                    {
                        playerScore = 6;
                        playerMaxMark = numberAndMark[i].Item2;
                        if (numberAndMark[i].Item1 == 1) playerMaxNum = numberAndMark[i].Item1;
                        else
                            playerMaxNum = numberAndMark[i + 3].Item1;
                    }
                    else
                    {
                        computerScore = 6;
                        computerMaxMark = numberAndMark[i].Item2;
                        if (numberAndMark[i].Item1 == 1) computerMaxNum = numberAndMark[i].Item1;
                        else
                            computerMaxNum = numberAndMark[i + 3].Item1;
                    }
                }
            }
        }
    }

    class Trump
    {
        private Dictionary<int, (string, int)> cardSet = new Dictionary<int, (string, int)>();
        private string[] cardMarks;
        private List<int> drawCardNum = new List<int>();
        private List<int> computerCardNum = new List<int>();
        private List<int> playerCardNum = new List<int>();
        public List<int> PlayerCardNum { get
            { return playerCardNum; }
            private set { playerCardNum = value; }
                       }
        public List<int> ComputerCardNum { get 
            { return computerCardNum; }
            private set { computerCardNum = value; }
        }

        public void SetUpCards()
        {
            cardMarks = new string[4] { "♠", "◆", "♥", "♣" };
            int index = 0;
            for (int i = 0; i < 52; i++)
            {
                cardSet.Add(i + 1, (cardMarks[index], (i % 13) + 1));
                if (i % 13 == 12) index++;
            }
        }

        #region 카드 뽑기
        //플레이어 카드 뽑기
        public void RollPlayerCard()
        {
            string[] cardMark = new string[5];
            string[] cardNumber = new string[5];

            for (int i = 0; i < 5; i++) 
            {
                cardMark[i] = cardSet[playerCardNum[i] + 1].Item1;
                cardNumber[i] = (cardSet[playerCardNum[i] + 1].Item2).ToString();
                switch (cardNumber[i])
                {
                    case "11":
                        cardNumber[i] = "J";
                        break;
                    case "12":
                        cardNumber[i] = "Q";
                        break;
                    case "13":
                        cardNumber[i] = "K";
                        break;
                    case "1":
                        cardNumber[i] = "A";
                        break;
                }
            }

            Console.WriteLine("내가 뽑은 카드 목록입니다");
            Console.WriteLine(" -----\t  -----\t   ----- ");
            Console.WriteLine("|{0} {1}|  |{2} {3}|  |{4} {5}|", 
                cardMark[0], cardNumber[0].PadRight(2), 
                cardMark[1], cardNumber[1].PadRight(2), 
                cardMark[2], cardNumber[2].PadRight(2));
            Console.WriteLine("|     |  |     |  |     |");
            Console.WriteLine("|{0} {1}|  |{2} {3}|  |{4} {5}|", 
                cardNumber[0].PadRight(2), cardMark[0],
                cardNumber[1].PadRight(2), cardMark[1],
                cardNumber[2].PadRight(2), cardMark[2]);
            Console.WriteLine(" -----\t  -----\t   ----- \n");

            Console.WriteLine(" -----\t  -----\t");
            Console.WriteLine("|{0} {1}|  |{2} {3}|", 
                cardMark[3], cardNumber[3].PadRight(2),
                cardMark[4], cardNumber[4].PadRight(2));
            Console.WriteLine("|     |  |     |");
            Console.WriteLine("|{0} {1}|  |{2} {3}|", 
                cardNumber[3].PadRight(2), cardMark[3],
                cardNumber[4].PadRight(2), cardMark[4]);
            Console.WriteLine(" -----\t  -----\t");
        }

        //컴퓨터 카드 뽑기
        public void RollComputerCard(int k)
        {
            string[] cardMark = new string[k];
            string[] cardNumber = new string[k];

            for (int i = 0; i < k; i++)
            {
                cardMark[i] = cardSet[computerCardNum[i] + 1].Item1;
                cardNumber[i] = (cardSet[computerCardNum[i] + 1].Item2).ToString();
                switch (cardNumber[i])
                {
                    case "11":
                        cardNumber[i] = "J";
                        break;
                    case "12":
                        cardNumber[i] = "Q";
                        break;
                    case "13":
                        cardNumber[i] = "K";
                        break;
                }
            }

            Console.WriteLine("컴퓨터가 뽑은 카드 목록입니다");
            Console.WriteLine(" -----\t  -----\t   ----- ");
            Console.WriteLine("|{0} {1}|  |{2} {3}|  |{4} {5}|",
                cardMark[0], cardNumber[0].PadRight(2),
                cardMark[1], cardNumber[1].PadRight(2),
                cardMark[2], cardNumber[2].PadRight(2));
            Console.WriteLine("|     |  |     |  |     |");
            Console.WriteLine("|{0} {1}|  |{2} {3}|  |{4} {5}|",
                cardNumber[0].PadRight(2), cardMark[0],
                cardNumber[1].PadRight(2), cardMark[1],
                cardNumber[2].PadRight(2), cardMark[2]);
            Console.WriteLine(" -----\t  -----\t   ----- \n");

            Console.WriteLine(" -----\t  -----\t");
            Console.WriteLine("|{0} {1}|  |{2} {3}|",
                cardMark[3], cardNumber[3].PadRight(2),
                cardMark[4], cardNumber[4].PadRight(2));
            Console.WriteLine("|     |  |     |");
            Console.WriteLine("|{0} {1}|  |{2} {3}|",
                cardNumber[3].PadRight(2), cardMark[3],
                cardNumber[4].PadRight(2), cardMark[4]);
            Console.WriteLine(" -----\t  -----\t\n");

            if (k > 5)
            {
                Console.WriteLine(" -----\t  -----\t");
                Console.WriteLine("|{0} {1}|  |{2} {3}|",
                    cardMark[5], cardNumber[5].PadRight(2),
                    cardMark[6], cardNumber[6].PadRight(2));
                Console.WriteLine("|     |  |     |");
                Console.WriteLine("|{0} {1}|  |{2} {3}|",
                    cardNumber[5].PadRight(2), cardMark[3],
                    cardNumber[6].PadRight(2), cardMark[4]);
                Console.WriteLine(" -----\t  -----\t");
            }
        }
        #endregion 카드뽑기 끝

        public void DrawNumber(int num, string whoIsDraw)
        {
            int index = 0;
            int cardNumber = -1;
            int[] getCardNumber = new int[num];
            Random ran = new Random();

            while(index < num)
            {
                cardNumber = ran.Next(0, 51 + 1);
                if (index == 0)
                {
                    drawCardNum.Add(cardNumber);
                    index++;
                }
                else
                {
                    if (drawCardNum.Contains(cardNumber)) continue;
                    else
                    {
                        drawCardNum.Add(cardNumber);
                        index++;
                    }
                }
                if (whoIsDraw == "Player") playerCardNum.Add(cardNumber);
                else computerCardNum.Add(cardNumber);
            }
        }

        public void ChangeCard(int n)
        {
            int cardNumber = -1;
            Random ran = new Random();

            while (true)
            {
                cardNumber = ran.Next(0, 51 + 1);

                if (drawCardNum.Contains(cardNumber)) continue;
                else
                {
                    playerCardNum[n - 1] = cardNumber;
                    break;
                }
            }
        }
    }// Program
}