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

        internal class PokerGameMain
        {
            static void Main()
            {
                Pokergame Poker = new Pokergame();
                Poker.matching();
            }

        }// Main
    }
    internal class Pokergame
    {

        private int[] trumpCardSet;      // 내가 사용할 카드 세트뭉치
        private string[] trumpCardMark;     // 트럼프 카드의 마크
        private string[] trumpCardType;

        public void SetupTrumpCards()
        {
            trumpCardSet = new int[52];
            for (int i = 0; i < trumpCardSet.Length; i++)
            {
                trumpCardSet[i] = i + 1;

            }       // loop: 카드를 셋업하는 루프
            trumpCardMark = new string[4] { "♥", "♠", "◆", "♣" };

        }       // SetupTrumpCard()

        public int[] shuffleonce(int[] intArray)  // 굉장히 무거운 함수
        {
            Random random = new Random();
            int sourIndex = random.Next(0, intArray.Length);
            int destIndex = random.Next(0, intArray.Length);

            int tempvariable = intArray[sourIndex];
            intArray[sourIndex] = intArray[destIndex];
            intArray[destIndex] = tempvariable;

            return intArray;
        }       // 카드 한번 섞기
        public void shufflecards(int howManyLoop)
        {
            for (int i = 0; i < howManyLoop; i++)
            {
                trumpCardSet = shuffleonce(trumpCardSet);
            }
        }       // 카드 횟수만큼 섞기

        public void shufflecard()
        {
            shufflecards(100);
        }       // 카드 100번 섞기


        public List<int> ComputerCardSet()
        {
            List<int> ComputerCard = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                ComputerCard.Add(trumpCardSet[i]);
            }


            return ComputerCard;
        }       // 컴퓨터 카드 세팅


        public void ComputerCardshow(List<int> computercard)
        {

        }
        public int[] ComputerCardplus()
        {
            List<int> ComputerCard = ComputerCardSet();
            for (int i = 10; i < 12; i++)
            {
                ComputerCard.Add(trumpCardSet[i]);
            }
            ComputerCard.Sort();

            int count = 0;
            int[] ComputerCardArray = new int[7];
            foreach (int card in ComputerCard)
            {
                ComputerCardArray[count++] = card;
            }
            return ComputerCardArray;

        }       // 컴퓨터 카드에 2장 추가
        public int[] UserCardSet()
        {
            List<int> UserCard = new List<int>();

            for (int i = 5; i < 10; i++)
            {
                UserCard.Add(trumpCardSet[i]);
            }
            UserCard.Sort();
            int count = 0;
            int[] CardArray = new int[5];
            foreach (int card in UserCard)
            {
                CardArray[count++] = card;
            }
            return CardArray;
        }       // 유저 카드 세팅

        public void UserShow(int[] inputcards)
        {

        }
        public int[] UserChange(int[] inputcards, int i)
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
                        array[0] = trumpCardSet[12 + i];
                        enter = false;
                        break;
                    case "2":
                        array[1] = trumpCardSet[12 + i];
                        enter = false;
                        break;
                    case "3":
                        array[2] = trumpCardSet[12 + i];
                        enter = false;
                        break;
                    case "4":
                        array[3] = trumpCardSet[12 + i];
                        enter = false;
                        break;
                    case "5":
                        array[4] = trumpCardSet[12 + i];
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
            Console.WriteLine("유저의 카드:");
            for (int k = 0; k < array.Length; k++)
            {
                Console.Write(" -------  ");
            }
            Console.WriteLine();
            for (int j = 0; j < 5; j++)
            {
                switch ((int)(Math.Round(array[j] % 13.1)))
                {
                    case 1:
                        Console.Write("| A");
                        break;
                    case 10:
                        Console.Write("|10");
                        break;
                    case 11:
                        Console.Write("| J");
                        break;
                    case 12:
                        Console.Write("| Q");
                        break;
                    case 13:
                        Console.Write("| K");
                        break;
                    default:
                        Console.Write("|{0} ", (int)(Math.Round(array[j] % 13.1)));
                        break;
                }

                Console.Write("{0}   | ", trumpCardMark[(array[j] - 1) / 13]);
            }
            Console.WriteLine();
            for (int k = 0; k < array.Length; k++)
            {
                Console.Write("|       | ");
            }
            Console.WriteLine();
            for (int k = 0; k < array.Length; k++)
            {
                Console.Write("|       | ");
            }
            Console.WriteLine();
            for (int j = 0; j < 5; j++)
            {
                Console.Write("|   {0}", trumpCardMark[(array[j] - 1) / 13]);
                switch ((int)(Math.Round(array[j] % 13.1)))
                {
                    case 1:
                        Console.Write("A | ");
                        break;
                    case 10:
                        Console.Write("10| ");
                        break;
                    case 11:
                        Console.Write("J | ");
                        break;
                    case 12:
                        Console.Write("Q | ");
                        break;
                    case 13:
                        Console.Write("K | ");
                        break;
                    default:
                        Console.Write("{0} | ", (int)(Math.Round(array[j] % 13.1)));
                        break;
                }
            }
            Console.WriteLine();
            for (int k = 0; k < array.Length; k++)
            {
                Console.Write(" -------  ");
            }
            Console.WriteLine();           // 카드 모양 만들기 

            // 카드 바꾸기


            return array;
        }
        public int UserCardRoll(int[] inputcards)
        {
            int[] cardnumbers = new int[5];
            string[] cardmarks = new string[5];
            int Numcount = 0;
            for (int i = 0; i < 5; i++)
            {
                cardnumbers[i] = (int)Math.Round((double)inputcards[i] % 13.1);
                cardmarks[i] = trumpCardMark[(inputcards[i] - 1) / 13];
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
            int a = 0; int b = 0;
            foreach (int num in numbers)
            {
                cardnumbers[a++] = num;
            }
            foreach (string ch in marks)
            {
                cardmarks[b++] = ch;
            }

            //cardnumbers = new int[] {1,10,11,12,13};            //<-------------------------카드 테스트

            //cardmarks = new string[] {"♥","♥","♥","♥","♥"};
            //for(int i = 0; i < 5;i++)
            //{
            //    Console.WriteLine("[{0}{1}]",cardnumbers[i], cardmarks[i]);     //<--------카드 출력
            //}
            int TopCard = 0;
            for (int i = 0; i < 4; i++)
            {
                if (cardnumbers[i] == cardnumbers[i + 1]) // 같은 숫자
                {
                    Numcount++;         // Numcount가 1이면 원페어, 2면 투페어
                    if (cardnumbers[i] == 1)
                    {
                        TopCard = 14;
                    }
                    else
                    {
                        TopCard = cardnumbers[i + 1];
                    }
                    ++i;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (cardnumbers[i] == cardnumbers[i + 1] &&
                    cardnumbers[i + 1] == cardnumbers[i + 2])
                {
                    if (Numcount <= 2)
                    {
                        Numcount = 3;   // 그냥 트리플
                        if (cardnumbers[i] == 1)
                        {
                            TopCard = 14;
                        }
                        else
                        {
                            TopCard = cardnumbers[i + 2];
                        }
                    }
                    else
                    {
                        Numcount += 3;
                        if (cardnumbers[i] == 1)
                        {
                            TopCard = 14;
                        }
                        else
                        {
                            TopCard = cardnumbers[i + 2];
                        }
                    }     // 포카드 6 점수:8
                }
            }

            if (cardnumbers[0] + 1 == cardnumbers[1] &&
                cardnumbers[1] + 1 == cardnumbers[2] &&
                cardnumbers[2] + 1 == cardnumbers[3] &&
                cardnumbers[3] + 1 == cardnumbers[4])
            {
                if (cardnumbers[0] == 1)
                {
                    Numcount = 5;   // 백 스트레이트
                    TopCard = cardnumbers[4];
                }
                else if (cardmarks[0] == cardmarks[1] &&
                        cardmarks[1] == cardmarks[2] &&
                        cardmarks[2] == cardmarks[3] &&
                        cardmarks[3] == cardmarks[4])
                {
                    Numcount = 9;       //스티플
                    TopCard = cardnumbers[4];
                }
                else
                {
                    Numcount = 4;   // 스트레이트
                    TopCard = cardnumbers[4];
                }

            }
            if (cardnumbers[0] == 1 &&
               cardnumbers[1] == 10 &&
               cardnumbers[2] == 11 &&
               cardnumbers[3] == 12 &&
               cardnumbers[4] == 13)
            {
                if (cardmarks[0] == cardmarks[1] &&
                   cardmarks[1] == cardmarks[2] &&
                   cardmarks[2] == cardmarks[3] &&
                   cardmarks[3] == cardmarks[4])
                {
                    Numcount = 10;      //로티플
                    TopCard = cardnumbers[4];
                }
                else
                {
                    Numcount = 7; // 마운틴 점수: 6
                    TopCard = cardnumbers[4];
                }

            }
            if (cardmarks[0] == cardmarks[1] &&
                cardmarks[1] == cardmarks[2] &&
                cardmarks[2] == cardmarks[3] &&
                cardmarks[3] == cardmarks[4])
            {
                if (Numcount > 8)
                {

                }
                else
                {
                    Numcount = 8;      // 플러쉬  점수:7
                    TopCard = cardnumbers[4];
                }

            }


            switch (Numcount)
            {
                case 0:
                    Console.WriteLine("노페어");
                    break;
                case 1:
                    Console.WriteLine("원페어");
                    Numcount = 1 * 13 + TopCard;
                    break;
                case 2:
                    Console.WriteLine("투페어");
                    Numcount = 2 * 13 + TopCard;
                    break;
                case 3:
                    Console.WriteLine("트리플");
                    Numcount = 3 * 13 + TopCard;
                    break;
                case 4:
                    Console.WriteLine("스트레이트");
                    Numcount = 4 * 13 + TopCard;
                    break;
                case 5:
                    Console.WriteLine("백 스트레이트");
                    Numcount = 5 * 13;
                    break;
                case 6:
                    Console.WriteLine("포카드");
                    Numcount = 8 * 13 + TopCard;
                    break;
                case 7:
                    Console.WriteLine("마운틴");
                    Numcount = 6 * 13;
                    break;
                case 8:
                    Console.WriteLine("플러쉬");
                    Numcount = 7 * 13;
                    break;
                case 9:
                    Console.WriteLine("스티플");
                    Numcount = 9 * 13;
                    break;
                case 10:
                    Console.WriteLine("로티플");
                    Numcount = 10 * 13;
                    break;
                default:
                    break;
            }
            return Numcount;
        }       // 유저 패 맞추기

        public int ComputerRoll(int[] inputcards)
        {
            int[] cardnumber = new int[7];
            string[] cardmarks = new string[7];
            int Numcount = 0;
            for (int i = 0; i < 7; i++)
            {
                cardnumber[i] = (int)Math.Round((double)inputcards[i] % 13.1);
                cardmarks[i] = trumpCardMark[(inputcards[i] - 1) / 13];
            }
            Console.WriteLine("컴퓨터의 카드:");
            for (int k = 0; k < cardnumber.Length; k++)
            {
                Console.Write(" -------  ");
            }
            Console.WriteLine();
            for (int j = 0; j < 7; j++)
            {
                switch ((int)(Math.Round(cardnumber[j] % 13.1)))
                {
                    case 1:
                        Console.Write("| A");
                        break;
                    case 10:
                        Console.Write("|10");
                        break;
                    case 11:
                        Console.Write("| J");
                        break;
                    case 12:
                        Console.Write("| Q");
                        break;
                    case 13:
                        Console.Write("| K");
                        break;
                    default:
                        Console.Write("| {0}", (int)(Math.Round(cardnumber[j] % 13.1)));
                        break;
                }

                Console.Write("{0}   | ", cardmarks[j]);
            }
            Console.WriteLine();
            for (int k = 0; k < cardnumber.Length; k++)
            {
                Console.Write("|       | ");
            }
            Console.WriteLine();
            for (int k = 0; k < cardnumber.Length; k++)
            {
                Console.Write("|       | ");
            }
            Console.WriteLine();
            for (int j = 0; j < 7; j++)
            {
                Console.Write("|   {0}", cardmarks[j]);
                switch ((int)(Math.Round(cardnumber[j] % 13.1)))
                {
                    case 1:
                        Console.Write("A | ");
                        break;
                    case 10:
                        Console.Write("10| ");
                        break;
                    case 11:
                        Console.Write("J | ");
                        break;
                    case 12:
                        Console.Write("Q | ");
                        break;
                    case 13:
                        Console.Write("K | ");
                        break;
                    default:
                        Console.Write("{0} | ", (int)(Math.Round(cardnumber[j] % 13.1)));
                        break;
                }
            }
            Console.WriteLine();
            for (int k = 0; k < cardnumber.Length; k++)
            {
                Console.Write(" -------  ");
            }
            Console.WriteLine();           // 카드 모양 만들기 

            //List<int> numbers = new List<int>();
            //List<string> marks = new List<string>();
            //for (int i = 0; i < 7; i++)
            //{
            //    numbers.Add(cardnumber[i]);
            //    marks.Add(cardmarks[i]);
            //}
            //numbers.Sort();
            //marks.Sort();
            int temp = 0;
            string temp2 = null;
            for (int i = 0; i < cardnumber.Length - 1; i++)
            {
                for (int j = 0; j < cardnumber.Length - 1 - i; j++)
                {
                    if (cardnumber[j] > cardnumber[j + 1])
                    {
                        temp = cardnumber[j];
                        cardnumber[j] = cardnumber[j + 1];
                        cardnumber[j + 1] = temp;
                        temp2 = cardmarks[j];
                        cardmarks[j] = cardmarks[j + 1];
                        cardmarks[j + 1] = temp2;
                    }
                }
            }
            //cardnumber = new int[] {2,2,10,10,12,12,12};            //<-------------------------카드 체크 테스트
            //cardmarks = new string[] {"♥", "♥", "♥", "♥", "♠", "♠", "♠" };
            //for (int i = 0; i < 7; i++)
            //{
            //    Console.Write("[{0}", cardnumber[i]);
            //    Console.Write("{0}]", cardmarks[i]);            //<--------------------------정렬된 카드 출력
            //}
            //int a = 0; int b = 0;
            //foreach (int num in numbers)
            //{
            //    cardnumber[a++] = num;
            //}
            //foreach (string ch in marks)
            //{
            //    cardmarks[b++] = ch;
            //}
            int TopCard = 0;
            for (int i = 0; i < 6; i++)
            {
                if (cardnumber[i] == cardnumber[i + 1]) // 같은 숫자
                {
                    if (Numcount >= 2)
                    {

                    }
                    else
                    {
                        Numcount++;         // Numcount가 1이면 원페어, 2면 투페어
                        if (cardnumber[i] == 1)
                        {
                            TopCard = 14;
                        }
                        else
                        {
                            TopCard = cardnumber[i + 1];
                        }
                        ++i;
                    }

                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (cardnumber[i] == cardnumber[i + 1] &&
                    cardnumber[i + 1] == cardnumber[i + 2])
                {

                    if (cardnumber[i] == cardnumber[i + 3])
                    {
                        Numcount = 6;
                        if (cardnumber[i] == 1)
                        {
                            TopCard = 14;
                        }
                        else
                        {
                            TopCard = cardnumber[i + 3];
                        }

                        break;
                    }     // 6 포카드  점수: 8
                    else
                    {
                        Numcount = 3;
                        if (cardnumber[i] == 1)
                        {
                            TopCard = 14;
                        }
                        else
                        {
                            TopCard = cardnumber[i + 3];
                        }
                    }
                }
            }
            if (cardnumber[4] == cardnumber[5] &&
                    cardnumber[5] == cardnumber[6])
            {
                Numcount = 3;
                if (cardnumber[4] == 1)
                {
                    TopCard = 14;
                }
                else
                {
                    TopCard = cardnumber[6];
                }
            }
            //int[] distArray = cardnumber.Distinct().ToArray();
            List<int> card = new List<int>();
            List<string> marks = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                if (cardnumber[i] == cardnumber[i + 1])
                {

                }
                else
                {
                    card.Add(cardnumber[i]);
                    marks.Add(cardmarks[i]);
                }
            }
            if (cardnumber[5] != cardnumber[6])
            {

                card.Add(cardnumber[6]);
                marks.Add(cardmarks[6]);
            }
            else
            {
                card.Add(cardnumber[5]);
                marks.Add(cardmarks[5]);
            }
            Console.WriteLine();
            //for (int i = 0; i < card.Count; i++)
            //{     
            //    Console.Write("[{0}", card[i]);
            //    Console.Write("{0}]", marks[i]);        //<--------------------------정렬된 카드 출력
            //}
            List<string> marks2 = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                marks2.Add(cardmarks[i]);
            }
            marks2.Sort();
            int b = 0;
            foreach (string ch in marks2)
            {
                cardmarks[b++] = ch;
            }

            if (card.Count > 4)
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
                            Numcount = 5;       // 백 스트레이트
                            TopCard = card[i + 4];
                        }
                        if (marks[i] == marks[i + 1] &&
                                 marks[i + 1] == marks[i + 2] &&
                                 marks[i + 2] == marks[i + 3] &&
                                 marks[i + 3] == marks[i + 4])
                        {

                            Numcount = 9;       //스티플
                            TopCard = card[i + 4];
                            break;
                        }
                        else
                        {

                            Numcount = 4;       // 스트레이트  
                            TopCard = card[i + 4];
                            break;
                        }

                    }

                }
                if (card[0] == 1 &&
                    card[card.Count - 4] == 10 &&
                    card[card.Count - 3] == 11 &&
                    card[card.Count - 2] == 12 &&
                    card[card.Count - 1] == 13)
                {
                    if (marks[0] == marks[card.Count - 1] &&
                        marks[card.Count - 1] == marks[card.Count - 2] &&
                        marks[card.Count - 2] == marks[card.Count - 3] &&
                        marks[card.Count - 3] == marks[card.Count - 4])
                    {
                        Numcount = 10;      //로티플
                    }
                    else
                    {
                        Numcount = 7; // 마운틴 점수: 6

                    }

                }
                for (int i = 0; i < 3; i++)
                {
                    if (cardmarks[i] == cardmarks[i + 1] &&
                        cardmarks[i + 1] == cardmarks[i + 2] &&
                        cardmarks[i + 2] == cardmarks[i + 3] &&
                        cardmarks[i + 3] == cardmarks[i + 4])
                    {
                        if (Numcount > 8)
                        {

                        }
                        else
                        {
                            Numcount = 8;      // 플러쉬  점수:7
                        }
                    }
                }
            }
            else
            {

            }

            switch (Numcount)
            {
                case 0:
                    Console.WriteLine("노페어");
                    break;
                case 1:
                    Console.WriteLine("원페어");
                    Numcount = 1 * 15 + TopCard;
                    break;
                case 2:
                    Console.WriteLine("투페어");
                    Numcount = 2 * 15 + TopCard;
                    break;
                case 3:
                    Console.WriteLine("트리플");
                    Numcount = 3 * 15 + TopCard;
                    break;
                case 4:
                    Console.WriteLine("스트레이트");
                    Numcount = 4 * 15 + TopCard;
                    break;
                case 5:
                    Console.WriteLine("백 스트레이트");
                    Numcount = 5 * 15;
                    break;
                case 6:
                    Console.WriteLine("포카드");
                    Numcount = 8 * 15 + TopCard;
                    break;
                case 7:
                    Console.WriteLine("마운틴");
                    Numcount = 6 * 15;
                    break;
                case 8:
                    Console.WriteLine("플러쉬");
                    Numcount = 7 * 15;
                    break;
                case 9:
                    Console.WriteLine("스티플");
                    Numcount = 9 * 15;
                    break;
                case 10:
                    Console.WriteLine("로티플");
                    Numcount = 10 * 15;
                    break;
                default:
                    break;
            }
            return Numcount;
        }       // 컴퓨터 패 맞추기

        public void matching()
        {
            SetupTrumpCards();
            int playerPoint = 10000;
            bool GameOver = true;
            while (GameOver)
            {
                shufflecard();
                ComputerCardshow(ComputerCardSet());
                UserShow(UserCardSet());
                Console.Write("\n\n몇 포인트를 배팅 하시겠습니까? ");
                Console.Write("(플레이어의 포인트: {0}): ", playerPoint);
                int userbatting = int.Parse(Console.ReadLine());
                int computer = ComputerRoll(ComputerCardplus());
                if (computer % 13 == 1)
                {
                    computer += 13;
                }
                int user = 0;
                if (userbatting >= 0 && userbatting <= playerPoint)
                {



                    user = UserCardRoll(UserChange(UserChange(UserCardSet(), 1), 2));

                    if (user % 13 == 1)
                    {
                        user += 13;
                    }
                    if (user == computer)
                    {
                        Console.WriteLine("비겼습니다.\n");
                        Console.WriteLine("플레이어의 포인트: {0}", playerPoint);
                    }
                    else if (user > computer)
                    {
                        Console.WriteLine("이겼습니다.\n");
                        playerPoint += userbatting * 2;
                        Console.WriteLine("플레이어의 포인트: {0}", playerPoint);
                    }
                    else if (user < computer)
                    {
                        Console.WriteLine("졌습니다.\n");
                        playerPoint -= userbatting;
                        Console.WriteLine("플레이어의 포인트: {0}", playerPoint);
                    }

                }
                else
                {
                    Console.WriteLine("잘못 입력 하셨습니다.");
                }

                if (playerPoint <= 0)
                {
                    Console.WriteLine("\n당신은 모든 포인트를 잃으셨습니다.");
                    GameOver = false;
                }
                else if (playerPoint >= 100000)
                {
                    Console.WriteLine("\n축하 합니다 당신은 목표한 포인트를 얻으셨습니다.");
                    GameOver = false;
                }
                else if (playerPoint > 0 && playerPoint < 100000)
                {
                    Console.Write("5초 후 다음 게임으로 진행됩니다.....");

                    for (int i = 0; i < 5; i++)
                    {
                        Console.Write("{0}.", i);
                        Task.Delay(1000).Wait();
                    }
                    Console.Clear();
                }


            }

        }// 컴퓨터와 유저 패 비교하기 
    }
}

class Trump
{
    protected Dictionary<int, (string, int)> cardSet = new Dictionary<int, (string, int)>();
    private string[] cardMarks;
    protected List<int> drawCardNum = new List<int>();
    protected List<int> computerCardNum = new List<int>();
    protected List<int> playerCardNum = new List<int>();
    public List<int> PlayerCardNum
    {
        get
        { return playerCardNum; }
        private set { playerCardNum = value; }
    }
    public List<int> ComputerCardNum
    {
        get
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
            cardMark[i] = cardSet[playerCardNum[i]].Item1;
            cardNumber[i] = (cardSet[playerCardNum[i]].Item2).ToString();
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
            cardMark[i] = cardSet[computerCardNum[i]].Item1;
            cardNumber[i] = (cardSet[computerCardNum[i]].Item2).ToString();
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

        while (index < num)
        {
            cardNumber = ran.Next(1, 52 + 1);
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
            cardNumber = ran.Next(1, 52 + 1);

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