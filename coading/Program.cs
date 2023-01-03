using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Poker poker = new Poker();
            poker.StartGame();
        }
    }
    internal class Poker
    {

        private int[] cardSet;
        private string[] cardMark;
        private string[] trumpCardType;

        public void SetupCard()
        {
            cardSet = new int[52];
            for (int i = 0; i < cardSet.Length; i++)
            {
                cardSet[i] = i + 1;

            }
            cardMark = new string[4] { "♥", "♠", "◆", "♣" };
        } // SetupTrumpCard()

        public int[] shuffleonce(int[] intArray)
        {
            Random random = new Random();
            int sourIndex = random.Next(0, intArray.Length);
            int destIndex = random.Next(0, intArray.Length);

            int tempvariable = intArray[sourIndex];
            intArray[sourIndex] = intArray[destIndex];
            intArray[destIndex] = tempvariable;

            return intArray;
        } 
        public void shufflecards(int howManyLoop)
        {
            for (int i = 0; i < howManyLoop; i++)
            {
                cardSet = shuffleonce(cardSet);
            }
        } 

        public void shufflecard()
        {
            shufflecards(100);
        } 

        public void PlayerRollCard(int[] playerCard)
        {
            int[] cardnumbers = new int[5];
            string[] cardmarks = new string[5];

            for (int i = 0; i < 5; i++)
            {
                cardnumbers[i] = (int)(playerCard[i] % 13.1);
                cardmarks[i] = cardMark[(playerCard[i] - 1) / 13];
            }

            Console.Write("플레이어의 카드 : ");
            for (int i = 0; i < playerCard.Length; i++)
            {
                Console.Write("{0}{1} ", cardmarks[i], cardnumbers[i]);
            }
            Console.WriteLine();
        }

        public void ComputerRollCard(int[] computerCard, int n)
        {
            int[] cardnumbers = new int[n];
            string[] cardmarks = new string[n];

            for (int i = 0; i < n; i++)
            {
                cardnumbers[i] = (int)(computerCard[i] % 13.1);
                cardmarks[i] = cardMark[(computerCard[i] - 1) / 13];
            }

            Console.Write("컴퓨터의 카드 : ");
            for (int i = 0; i < computerCard.Length; i++)
            {
                Console.Write("{0}{1} ", cardmarks[i], cardnumbers[i]);
            }
            Console.WriteLine();
        }
        public List<int> ComputerCard()
        {
            List<int> computerCard = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                computerCard.Add(cardSet[i]);
            }
            return computerCard;
        }

        public int[] ComputerDrawTwoCard()
        {
            List<int> computerCard = ComputerCard();
            for (int i = 10; i < 12; i++)
            {
                computerCard.Add(cardSet[i]);
            }
            computerCard.Sort();

            int count = 0;
            int[] ComputerCardArray = new int[7];
            foreach (int card in computerCard)
            {
                ComputerCardArray[count++] = card;
            }
            return ComputerCardArray;

        }
        public int[] PlayerCard()
        {
            List<int> playerCard = new List<int>();

            for (int i = 5; i < 10; i++)
            {
                playerCard.Add(cardSet[i]);
            }
            playerCard.Sort();
            int count = 0;
            int[] CardArray = new int[5];
            foreach (var item in playerCard)
            {
                CardArray[count++] = item;
            }
            return CardArray;
        }

        public int[] PlayerCardChange(int[] inputcards, int i)
        {
            int[] array = inputcards;
            bool enter = true;
            string number = null;
            Console.WriteLine("\n카드를 바꾸시겠습니까?(1,2,3,4,5중 선택(없으면 0))");

            while (enter)
            {
                number = Console.ReadLine();
                switch (number)
                {
                    case "1":
                        array[0] = cardSet[12 + i];
                        enter = false;
                        break;
                    case "2":
                        array[1] = cardSet[12 + i];
                        enter = false;
                        break;
                    case "3":
                        array[2] = cardSet[12 + i];
                        enter = false;
                        break;
                    case "4":
                        array[3] = cardSet[12 + i];
                        enter = false;
                        break;
                    case "5":
                        array[4] = cardSet[12 + i];
                        enter = false;
                        break;
                    case "0":
                        enter = false;
                        break;
                    default:
                        break;
                }
                number = null;
            }

            return array;
        }

        public void OneTwoPair(int[] cardnumbers, int cardNumMax, ref int pairCount, ref int scorePoint)
        {
            for (int i = 0; i < 4; i++)
            {
                // 좌우 같은지 확인
                if (cardnumbers[i] == cardnumbers[i + 1])
                {
                    pairCount++;
                    scorePoint++;
                    if (cardnumbers[i] == 1)
                    {
                        cardNumMax = 14;
                    }
                    else if (cardNumMax > cardnumbers[i + 1])
                    {
                        /* Do nothing */
                    }
                    else
                    {
                        cardNumMax = cardnumbers[i + 1];
                    }
                    i++;
                }
            }
        }

        public void TrippleFullhouse(int[] cardnumbers, int cardNumMax, int pairCount, ref int scorePoint)
        {
            for (int i = 0; i < 3; i++)
            {
                if (cardnumbers[i] == cardnumbers[i + 1] &&
                    cardnumbers[i + 1] == cardnumbers[i + 2])
                {
                    if (scorePoint < 2)
                    {
                        //페어는 1개면서 트리플인 경우
                        scorePoint = 3;
                        if (cardnumbers[i] == 1)
                        {
                            cardNumMax = 14;
                        }
                        else
                        {
                            cardNumMax = cardnumbers[i + 2];
                        }
                    }
                    else
                    {
                        //투페어 이상인데 트리플이 만족하는 경우 -> 풀하우스
                        if (pairCount >= 2)
                        {
                            scorePoint = 6;
                        }
                    }
                }
            }
        }

        public void FourCard(int[] cardnumbers, int cardNumMax, ref int scorePoint)
        {
            for (int i = 0; i < 2; i++)
            {
                if (cardnumbers[i] == cardnumbers[i + 1] &&
                    cardnumbers[i + 1] == cardnumbers[i + 2] &&
                    cardnumbers[i + 2] == cardnumbers[i + 3])
                {
                    scorePoint = 7;
                    if (cardnumbers[i] == 1)
                    {
                        cardNumMax = 14;
                    }
                    else
                    {
                        cardNumMax = cardnumbers[i + 2];
                    }
                }
            }
        }
        public void Straight(int[] cardnumbers, int cardNumMax, ref int scorePoint)
        {
            if (cardnumbers[0] + 1 == cardnumbers[1] &&
                cardnumbers[1] + 1 == cardnumbers[2] &&
                cardnumbers[2] + 1 == cardnumbers[3] &&
                cardnumbers[3] + 1 == cardnumbers[4])
            {
                scorePoint = 4;
                if (cardnumbers[0] == 1)
                {
                    cardNumMax = 14;
                }
                else
                {
                    cardNumMax = cardnumbers[4];
                }
            }
        }
        public void Flush(int[] cardnumbers, int cardNumMax, ref int scorePoint, string[] cardmarks)
        {
            if (cardmarks[0] == cardmarks[1] &&
                cardmarks[1] == cardmarks[2] &&
                cardmarks[2] == cardmarks[3] &&
                cardmarks[3] == cardmarks[4])
            {
                scorePoint = 5;
                cardNumMax = cardnumbers[4];
            }
        }
        public int PlayerCardCheck(int[] playerCard)
        {
            int[] cardnumbers = new int[5];
            string[] cardmarks = new string[5];
            int scorePoint = 0; // 점수 계산용

            for (int i = 0; i < 5; i++)
            {
                cardnumbers[i] = (int)(playerCard[i] % 13.1);
                cardmarks[i] = cardMark[(playerCard[i] - 1) / 13];
            }

            List<int> numbers = new List<int>();
            List<string> marks = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                numbers.Add(cardnumbers[i]);
                marks.Add(cardmarks[i]);
            }

            numbers.Sort();
            marks.Sort();
            int a = 0, b = 0;

            foreach (var item in numbers)
            {
                cardnumbers[a++] = item;
            }
            foreach (var item in marks)
            {
                cardmarks[b++] = item;
            }

            int cardNumMax = 0;
            int pairCount = 0; // 페어의 수
            OneTwoPair(cardnumbers, cardNumMax, ref pairCount, ref scorePoint);
            TrippleFullhouse(cardnumbers, cardNumMax, pairCount, ref scorePoint);
            FourCard(cardnumbers, cardNumMax, ref scorePoint);
            Straight(cardnumbers, cardNumMax, ref scorePoint);
            Flush(cardnumbers, cardNumMax, ref scorePoint, cardmarks);

            switch (scorePoint)
            {
                case 0:
                    Console.WriteLine("노페어");
                    break;
                case 1:
                    Console.WriteLine("원페어");
                    scorePoint += cardNumMax;
                    break;
                case 2:
                    Console.WriteLine("투페어");
                    scorePoint += cardNumMax;
                    break;
                case 3:
                    Console.WriteLine("트리플");
                    scorePoint += cardNumMax;
                    break;
                case 4:
                    Console.WriteLine("스트레이트");
                    scorePoint += cardNumMax;
                    break;
                case 5:
                    Console.WriteLine("플러쉬");
                    scorePoint += cardNumMax;
                    break;
                case 6:
                    Console.WriteLine("풀하우스");
                    scorePoint += cardNumMax;
                    break;
                case 7:
                    Console.WriteLine("포카드");
                    scorePoint += cardNumMax;
                    break;
                default:
                    Console.WriteLine("에러");
                    break;
            }
            return scorePoint;
        } // PlayerCardCheck

        public void ComputerOneTwoPair(int[] cardnumbers, int cardNumMax, ref int pairCount, ref int scorePoint)
        {
            for (int i = 0; i < 6; i++)
            {
                if (cardnumbers[i] == cardnumbers[i + 1])
                {
                    if (scorePoint >= 2)
                    {
                        // 투페어는 2까지임
                        scorePoint = 2;
                        continue;
                    }
                    else
                    {
                        pairCount++;
                        scorePoint++;
                        if (cardnumbers[i] == 1)
                        {
                            cardNumMax = 14;
                        }
                        else if (cardNumMax > cardnumbers[i + 1])
                        {
                            /* Do nothing */
                        }
                        else
                        {
                            cardNumMax = cardnumbers[i + 1];
                        }
                        i++;
                    }

                }
            }
        }

        public void ComputerTriFourFull(int[] cardnumbers, int cardNumMax, int pairCount, ref int scorePoint)
        {
            for (int i = 0; i < 4; i++)
            {
                if (cardnumbers[i] == cardnumbers[i + 1] &&
                    cardnumbers[i + 1] == cardnumbers[i + 2])
                {
                    if (cardnumbers[i] == cardnumbers[i + 3])
                    {
                        // 4장이 전부 숫자가 같은 경우 포카드~
                        scorePoint = 7;
                        if (cardnumbers[i] == 1)
                        {
                            cardNumMax = 14;
                        }
                        else
                        {
                            cardNumMax = cardnumbers[i + 3];
                        }
                        break;
                    }
                    else
                    {
                        //트리플이면서 투페어도 만족
                        if (pairCount >= 2)
                        {
                            scorePoint = 6;
                        }
                        else
                        {
                            //페어가 1개 즉 트리플
                            scorePoint = 3;
                            if (cardnumbers[i] == 1)
                            {
                                cardNumMax = 14;
                            }
                            else
                            {
                                cardNumMax = cardnumbers[i + 3];
                            }
                        }
                    }
                }
            }
            // index가 4인경우 포카드 검사가 안되기때문에 따로 뺌
            if (cardnumbers[4] == cardnumbers[5] &&
                    cardnumbers[5] == cardnumbers[6])
            {
                scorePoint = 3;
                if (cardnumbers[4] == 1)
                {
                    cardNumMax = 14;
                }
                else
                {
                    cardNumMax = cardnumbers[6];
                }
            }
        }
        public void ComputerStraight(List<int> card, List<string> marks, string[] cardmarks, int cardNumMax, ref int scorePoint)
        {
            for (int i = 0; i < card.Count - 4; i++)
            {

                if (card[i] + 1 == card[i + 1] &&
                    card[i + 1] + 1 == card[i + 2] &&
                    card[i + 2] + 1 == card[i + 3] &&
                    card[i + 3] + 1 == card[i + 4])
                {
                    if (card[i] == 1)
                    {
                        scorePoint = 4;
                        cardNumMax = card[i + 4];
                    }
                    else
                    {

                        scorePoint = 4;
                        cardNumMax = card[i + 4];
                        break;
                    }
                }
            }
        }
        public void ComputerFlush(string[] cardmarks, ref int scorePoint)
        {
            for (int i = 0; i < 3; i++)
            {
                if (cardmarks[i] == cardmarks[i + 1] &&
                    cardmarks[i + 1] == cardmarks[i + 2] &&
                    cardmarks[i + 2] == cardmarks[i + 3] &&
                    cardmarks[i + 3] == cardmarks[i + 4])
                {
                    if (scorePoint > 8)
                    {

                    }
                    else
                    {
                        scorePoint = 5;
                    }
                }
            }
        }
        public int ComputerCardCheck(int[] inputcards)
        {
            int[] cardnumbers = new int[7];
            string[] cardmarks = new string[7];
            int scorePoint = 0;
            for (int i = 0; i < 7; i++)
            {
                cardnumbers[i] = (int)(inputcards[i] % 13.1);
                cardmarks[i] = cardMark[(inputcards[i] - 1) / 13];
            }

            int temp = 0;
            string temp2 = null;
            for (int i = 0; i < cardnumbers.Length - 1; i++)
            {
                for (int j = 0; j < cardnumbers.Length - 1 - i; j++)
                {
                    if (cardnumbers[j] > cardnumbers[j + 1])
                    {
                        temp = cardnumbers[j];
                        cardnumbers[j] = cardnumbers[j + 1];
                        cardnumbers[j + 1] = temp;
                        temp2 = cardmarks[j];
                        cardmarks[j] = cardmarks[j + 1];
                        cardmarks[j + 1] = temp2;
                    }
                }
            }

            int pairCount = 0;
            int cardNumMax = 0;
            ComputerOneTwoPair(cardnumbers, cardNumMax, ref pairCount, ref scorePoint);
            ComputerTriFourFull(cardnumbers, cardNumMax, pairCount, ref scorePoint);

            // 스트레이트 세팅
            List<int> card = new List<int>();
            List<string> marks = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                if (cardnumbers[i] == cardnumbers[i + 1])
                {
                    /* Do nothing */
                }
                else
                {
                    // 스트레이트 검사용 중복제거
                    card.Add(cardnumbers[i]);
                    marks.Add(cardmarks[i]);
                }
            }
            if (cardnumbers[5] != cardnumbers[6])
            {

                card.Add(cardnumbers[6]);
                marks.Add(cardmarks[6]);
            }
            else
            {
                card.Add(cardnumbers[5]);
                marks.Add(cardmarks[5]);
            }
            // 스트레이트 세팅 완료
            Console.WriteLine();

            List<string> marks2 = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                marks2.Add(cardmarks[i]);
            }
            marks2.Sort();
            int b = 0;
            foreach (var item in marks2)
            {
                cardmarks[b++] = item;
            }

            // 4이하라면 스트레이트는 존재하지 않음
            if (card.Count > 4)
            {
                ComputerStraight(card, marks, cardmarks, cardNumMax, ref scorePoint);
                ComputerFlush(cardmarks, ref scorePoint);
            }

            switch (scorePoint)
            {
                case 0:
                    Console.WriteLine("노페어");
                    break;
                case 1:
                    Console.WriteLine("원페어");
                    scorePoint += cardNumMax;
                    break;
                case 2:
                    Console.WriteLine("투페어");
                    scorePoint += cardNumMax;
                    break;
                case 3:
                    Console.WriteLine("트리플");
                    scorePoint += cardNumMax;
                    break;
                case 4:
                    Console.WriteLine("스트레이트");
                    scorePoint += cardNumMax;
                    break;
                case 5:
                    Console.WriteLine("플러쉬");
                    scorePoint += cardNumMax;
                    break;
                case 6:
                    Console.WriteLine("풀하우스");
                    scorePoint += cardNumMax;
                    break;
                case 7:
                    Console.WriteLine("포카드");
                    scorePoint += cardNumMax;
                    break;
                default:
                    Console.WriteLine("에러");
                    break;
            }
            return scorePoint;
        } // ComputerCardCheck

        public void StartGame()
        {
            SetupCard();
            int money = 10000;

            while (true)
            {
                shufflecard();
                int[] playerCard = PlayerCard();
                List<int> list = ComputerCard();
                int[] computerCard = list.ToArray();
                PlayerRollCard(playerCard);
                ComputerRollCard(computerCard, 5);
                int betting = 0;
                while (true)
                {
                    Console.Write("포인트를 베팅해 주세요. : ");
                    int.TryParse(Console.ReadLine(), out betting);
                    if (betting < 0 || betting > money)
                    {
                        Console.WriteLine("잘못된 입력입니다. 재입력 바랍니다.");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("현재 포인트: {0}): ", money);
                        break;
                    }
                }
                computerCard = ComputerDrawTwoCard();
                playerCard = PlayerCardChange(PlayerCardChange(PlayerCard(), 1), 2);
                PlayerRollCard(playerCard);
                ComputerRollCard(computerCard, 7);
                int player = 0;
                int computer = ComputerCardCheck(computerCard);
                player = PlayerCardCheck(playerCard);

                if (computer % 13 == 1)
                {
                    computer += 13;
                }

                if (player % 13 == 1)
                {
                    player += 13;
                }
                if (player == computer)
                {
                    Console.WriteLine("리매치 합니다.\n");
                }
                else if (player > computer)
                {
                    Console.WriteLine("You Win\n");
                    money += betting * 2;
                    Console.WriteLine("획득 포인트 : {0}, 현재 포인트 : {1}", betting * 2, money);
                }
                else if (player < computer)
                {
                    Console.WriteLine("You Lose\n");
                    money -= betting;
                    Console.WriteLine("잃은 포인트 : {0}, 현재 포인트 : {1}", betting, money);
                }

                if (money <= 0)
                {
                    Console.WriteLine("파산");
                    break;
                }
                else if (money >= 100000)
                {
                    Console.WriteLine("승리");
                    break;
                }
                else
                {
                    Console.Write("다음 경기를 시작합니다");
                }
            }
        }
    }
}