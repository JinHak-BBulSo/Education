using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TrumpCardGame
{
    internal class PokerCardGame : CardDraw, CardGamePlay
    {
        /**
         * - 컴퓨터가 5장의 카드를 뽑는다. (플레이어가 볼 수 있음)
         * - 플레이어도 5장의 카드를 뽑는다.
         * - 플레이어는 베팅을 한다. (0 ~ 알아서)
         * - 컴퓨터는 2장의 카드를 추가로 받는다. -> 총 7장의 카드 보유
         * - 플레이어는 2장의 카드를 교체할 수 있따.(카드 1장 교체 가능한 횟수가 최대 2회)
         * - 모든 액션이 끝난 후에는 결과를 체크한다.
         * - 플레이어가 이기면 2배로 받고
         * - 플레이어가 지면 베팅금액을 잃는다.
         * - 100,000 포인트 이상을 얻거나, 0포인트 이하가 되면 게임 종료
         * - 시작 포인트는 10,000 포인트
         * - 탑, 원페어, 투페어, 트리플, 포카드, 플러쉬, 스트레이트
         *      - 마운틴, 백스트레이트, 풀하우스, 스티플, 로티플은 HARD모드
         */
        public enum PokerHandRangkings
        {
            TOP, OWN_PAIR, TWO_PAIR, TRIPLE, STRAIGHT, BACK_STRAIGHT, MOUNTAIN, FLUSH, FULL_HOUSE, FOUR_CARD, STRAIGHT_FLUSH, BACK_STRAIGHT_FLUSH, ROYAL_STARIGHT_FLUSH
        }
        Deck mDeck;
        Player mPlayer;
        Dealer mDealer;
        bool gameEnd;

        public PokerCardGame()
        {
            Init();
            Update();
        }

        public void Init()
        {
            mDeck = new Deck();
            mPlayer = new Player(10000);
            mDealer = new Dealer();
            gameEnd = false;
        }

        public void Update()
        {
            while (!gameEnd)
            {
                if (mPlayer.Money <= 0 || 100000 <= mPlayer.Money)
                {
                    gameEnd = true;
                    break;
                }
                CardGamePlay();
                Console.Clear();
                mPlayer.Cards.Clear();
                mDealer.Cards.Clear();
                mDeck.Init();
            }
            if (mPlayer.Money <= 0)
            {
                Console.WriteLine("당신은 파산했슴");
            }
            if (100000 <= mPlayer.Money)
            {
                Console.WriteLine("당신은 너무 많이 뜯어가 쫓겨났음");
            }
        }


        public Card CardDraw()
        {
            Card drawCard = mDeck.cards[0];
            mDeck.cards.Remove(drawCard);
            return drawCard;
        }

        public void CardGamePlay()
        {
            string GameBoard = "     ===========================\n" +
                    "      Welcome to Kyungil Casino\n" +
                    "     ===========================";
            Console.WriteLine(GameBoard);

            mDealer.AddCard(CardDraw());
            mDealer.AddCard(CardDraw());
            mDealer.AddCard(CardDraw());
            mDealer.AddCard(CardDraw());
            mDealer.AddCard(CardDraw());

            List<Card> sortedList = mDealer.Cards.OrderBy(x => x.MarkNum).ToList();
            sortedList = sortedList.OrderBy(x => x.Number).ToList();

            string dealerCards = string.Empty;

            mDealer.Cards.Clear();

            foreach (var i in sortedList)
            {
                mDealer.Cards.Add(i);
            }
            foreach (var i in mDealer.Cards)
            {
                dealerCards += i.Name + i.Mark + " ";
            }
            sortedList.Clear();
            //for(int i = 0; i < mDealer.Cards.Count; i++)
            //{
            //    dealerCards += mDealer.Cards[i].Name + mDealer.Cards[i].Mark + " ";
            //}
            Console.WriteLine("컴퓨터의 카드 : {0}", dealerCards);
            Console.WriteLine();
            Console.WriteLine();
            mPlayer.AddCard(CardDraw());
            mPlayer.AddCard(CardDraw());
            mPlayer.AddCard(CardDraw());
            mPlayer.AddCard(CardDraw());
            mPlayer.AddCard(CardDraw());

            string playerCards = string.Empty;

            sortedList = mPlayer.Cards.OrderBy(x => x.MarkNum).ToList();
            sortedList = sortedList.OrderBy(x => x.Number).ToList();

            mPlayer.Cards.Clear();

            foreach (var i in sortedList)
            {
                mPlayer.Cards.Add(i);
            }
            foreach (var i in mPlayer.Cards)
            {
                playerCards += i.Name + i.Mark + " ";
            }
            Console.WriteLine("플레이어의 카드 : {0}", playerCards);
            Console.WriteLine("남은 잔액 : {0}", mPlayer.Money);
            Console.Write("베팅 금액을 입력 : ");
            int bettingMoney = mPlayer.Betting();

            mDealer.AddCard(CardDraw());
            mDealer.AddCard(CardDraw());
            sortedList = mDealer.Cards.OrderBy(x => x.MarkNum).ToList();
            sortedList = sortedList.OrderBy(x => x.Number).ToList();
            mDealer.Cards.Clear();
            dealerCards = string.Empty;
            foreach (var i in sortedList)
            {
                mDealer.Cards.Add(i);
            }
            foreach (var i in mDealer.Cards)
            {
                dealerCards += i.Name + i.Mark + " ";
            }
            sortedList.Clear();
            Console.Clear();
            Console.WriteLine(GameBoard);
            Console.WriteLine("컴퓨터의 카드 : {0}", dealerCards);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("플레이어의 카드 : {0}", playerCards);
            Console.WriteLine("남은 잔액 : {0}", mPlayer.Money);
            for (int i = 0; i < 2; i++)
            {
                int userInput = 0;
                Console.Write("교체할 카드 선택  (0) = 선택 안함 :");
                int.TryParse(Console.ReadLine(), out userInput);
                if (userInput == 0)
                {
                    continue;
                }
                else if (5 < userInput || userInput < 0)
                {
                    continue;
                }
                else
                {
                    mPlayer.Cards.RemoveAt(userInput - 1);
                    mPlayer.AddCard(CardDraw());
                }
                Console.Clear();
                playerCards = string.Empty;

                sortedList = mPlayer.Cards.OrderBy(x => x.MarkNum).ToList();
                sortedList = sortedList.OrderBy(x => x.Number).ToList();

                mPlayer.Cards.Clear();

                foreach (var j in sortedList)
                {
                    mPlayer.Cards.Add(j);
                }
                foreach (var j in mPlayer.Cards)
                {
                    playerCards += j.Name + j.Mark + " ";
                }
                Console.WriteLine(GameBoard);
                Console.WriteLine("컴퓨터의 카드 : {0}", dealerCards);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("플레이어의 카드 : {0}", playerCards);
                Console.WriteLine("남은 잔액 : {0}", mPlayer.Money);
            }
            Console.Clear();
            Console.WriteLine(GameBoard);
            Console.WriteLine("컴퓨터의 카드 : {0}", dealerCards);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("플레이어의 카드 : {0}", playerCards);
            Console.WriteLine("남은 잔액 : {0}", mPlayer.Money);

            Pair<PokerHandRangkings, List<Card>> dealerDeck = new Pair<PokerHandRangkings, List<Card>>();
            dealerDeck = DeckCheck(mDealer.Cards);
            dealerDeck.RightValue = dealerDeck.RightValue.OrderBy(x => x.MarkNum).ToList();
            dealerDeck.RightValue = dealerDeck.RightValue.OrderBy(x => x.Number).ToList();
            Pair<PokerHandRangkings, List<Card>> playerDeck = new Pair<PokerHandRangkings, List<Card>>();
            playerDeck = DeckCheck(mPlayer.Cards);
            playerDeck.RightValue = playerDeck.RightValue.OrderBy(x => x.Number).ToList();
            playerDeck.RightValue = playerDeck.RightValue.OrderBy(x => x.Number).ToList();
            if (WinnerCheck(dealerDeck, playerDeck))
            {
                Console.Write("컴퓨터 덱 : ");
                foreach (var i in dealerDeck.RightValue)
                {
                    Console.Write("{0}{1} ", i.Name, i.Mark);
                }
                Console.WriteLine("{0}", dealerDeck.LeftValue);
                Console.Write("플레이어 덱 : ");
                foreach (var i in playerDeck.RightValue)
                {
                    Console.Write("{0}{1} ", i.Name, i.Mark);
                }
                Console.WriteLine("{0}", playerDeck.LeftValue);
                Console.WriteLine("플레이어 이김");
                mPlayer.AddMoney(bettingMoney * 2);
            }
            else
            {
                Console.Write("컴퓨터 덱 : ");
                foreach (var i in dealerDeck.RightValue)
                {
                    Console.Write("{0}{1} ", i.Name, i.Mark);
                }
                Console.WriteLine("{0}", dealerDeck.LeftValue);
                Console.Write("플레이어 덱 : ");
                foreach (var i in playerDeck.RightValue)
                {
                    Console.Write("{0}{1} ", i.Name, i.Mark);
                }
                Console.WriteLine("{0}", playerDeck.LeftValue);
                Console.WriteLine("컴퓨터 이김");
            }
            int input = 0;
            Console.WriteLine("넘어가려면 Enter");
            int.TryParse(Console.ReadLine(), out input);

        }

        public Pair<PokerHandRangkings, List<Card>> DeckCheck(List<Card> cards)
        {
            Pair<PokerHandRangkings, List<Card>> result = new Pair<PokerHandRangkings, List<Card>>();

            List<Card> resultCards = new List<Card>();

            bool Is_Triple = false;
            bool Is_Full_House = false;
            bool Is_Four_Card = false;


            // 숫자별 파악   
            Dictionary<int, List<Card>> numbersDictionary = new Dictionary<int, List<Card>>();

            for (int i = 0; i < 14; i++)
            {
                numbersDictionary[i] = new List<Card>();
            }
            for (int i = 0; i < cards.Count; i++)
            {
                numbersDictionary[cards[i].Number - 1].Add(cards[i]);
            }

            int pair = 0;
            // 풀하우스, 포카드 체크
            for (int i = 0; i < numbersDictionary.Count; i++)
            {
                if (3 < numbersDictionary[i].Count)
                {
                    Is_Four_Card = true;
                }
                else if (2 < numbersDictionary[i].Count)
                {
                    Is_Triple = true;
                }
                else if (1 < numbersDictionary[i].Count)
                {
                    pair++;
                }
            }

            if (1 <= pair && Is_Triple)
            {
                Is_Full_House = true;
            }

            Dictionary<string, List<Card>> marksDictionary = new Dictionary<string, List<Card>>();
            marksDictionary.Add("♣", new List<Card>());
            marksDictionary.Add("♥", new List<Card>());
            marksDictionary.Add("◆", new List<Card>());
            marksDictionary.Add("♠", new List<Card>());

            string[] marks = { "♣", "♥", "◆", "♠" };
            int StraightCnt = 0;
            // 마크별 카드 저장
            for (int i = 0; i < cards.Count; i++)
            {
                marksDictionary[cards[i].Mark].Add(cards[i]);
            }

            for (int i = 0; i < marks.Length; i++)
            {
                // 플러시 체크
                if (5 <= marksDictionary[marks[i]].Count)
                {
                    //로티플 체크
                    for (int j = 0; j < marksDictionary[marks[i]].Count; j++)
                    {
                        if (marksDictionary[marks[i]][j].Number == 14 ||
                            marksDictionary[marks[i]][j].Number == 10 ||
                            marksDictionary[marks[i]][j].Number == 11 ||
                            marksDictionary[marks[i]][j].Number == 12 ||
                            marksDictionary[marks[i]][j].Number == 13)
                        {
                            resultCards.Add(marksDictionary[marks[i]][j]);
                            StraightCnt++;
                        }
                    }   // loop : 로티플 체크
                    if (5 <= StraightCnt)
                    {
                        result.RightValue = resultCards;
                        result.LeftValue = PokerHandRangkings.ROYAL_STARIGHT_FLUSH;
                        return result;
                    }
                    else
                    {
                        StraightCnt = 0;
                        resultCards.Clear();
                    }

                    // 백티플 체크
                    for (int j = 0; j < marksDictionary[marks[i]].Count; j++)
                    {
                        if (marksDictionary[marks[i]][j].Number == 14 ||
                            marksDictionary[marks[i]][j].Number == 2 ||
                            marksDictionary[marks[i]][j].Number == 3 ||
                            marksDictionary[marks[i]][j].Number == 4 ||
                            marksDictionary[marks[i]][j].Number == 5)
                        {
                            resultCards.Add(marksDictionary[marks[i]][j]);
                            StraightCnt++;
                        }
                    }   // loop : 백티플 체크
                    if (5 <= StraightCnt)
                    {
                        result.RightValue = resultCards;
                        result.LeftValue = PokerHandRangkings.BACK_STRAIGHT_FLUSH;
                        return result;
                    }
                    else
                    {
                        StraightCnt = 0;
                        resultCards.Clear();
                    }
                    // 스트레이트 체크
                    for (int j = 0; j < marksDictionary[marks[i]].Count; j++)
                    {
                        for (int k = j; k < marksDictionary[marks[i]].Count; k++)
                        {
                            if (k == j) continue;
                            if (marksDictionary[marks[i]][j].Number + 1 == marksDictionary[marks[i]][k].Number)
                            {
                                StraightCnt++;
                                resultCards.Add(marksDictionary[marks[i]][j]);
                                break;
                                //continue;
                            }
                            else
                            {
                                StraightCnt = 0;
                                resultCards.Clear();
                                break;
                            }
                        }
                    }
                    if (4 == StraightCnt)
                    {
                        resultCards.Add(marksDictionary[marks[i]][marksDictionary[marks[i]].Count - 1]);
                        result.RightValue = resultCards;
                        result.LeftValue = PokerHandRangkings.STRAIGHT_FLUSH;
                        return result;
                    }
                    // 스티플 큰수부터 대입
                    else if (4 < StraightCnt)
                    {
                        int minCnt = StraightCnt - 4;
                        resultCards.Clear();
                        for (int j = minCnt; j < marksDictionary[marks[i]].Count; j++)
                        {
                            resultCards.Add(marksDictionary[marks[i]][j]);
                        }
                        result.RightValue = resultCards;
                        result.LeftValue = PokerHandRangkings.STRAIGHT_FLUSH;
                        return result;
                    }
                    else
                    {
                        StraightCnt = 0;
                        resultCards.Clear();
                    }

                    // 위에 조건식에서 return되지 않으면 + 족보가 풀하우스, 포카드가 아니면
                    if (Is_Full_House || Is_Four_Card)
                    {
                        /* Do Nothing */
                    }
                    else
                    {
                        if (5 < marksDictionary[marks[i]].Count)
                        {
                            int minCnt = marksDictionary[marks[i]].Count - 5;
                            for (int j = minCnt; j < marksDictionary[marks[i]].Count; j++)
                            {
                                resultCards.Add(marksDictionary[marks[i]][j]);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < marksDictionary[marks[i]].Count; j++)
                            {
                                resultCards.Add(marksDictionary[marks[i]][j]);
                            }
                        }

                        result.RightValue = resultCards;
                        result.LeftValue = PokerHandRangkings.FLUSH;
                        return result;
                    }
                }
            }       // 플러시 체크

            StraightCnt = 0;
            resultCards.Clear();

            // 포카드 체크
            for (int i = numbersDictionary.Count - 1; 0 < i; i--)
            {
                if (Is_Four_Card)
                {
                    // 포카드 체크
                    if (3 < numbersDictionary[i].Count)
                    {
                        for (int j = 0; j < numbersDictionary[i].Count; j++)
                        {
                            resultCards.Add(numbersDictionary[i][j]);
                        }
                        result.LeftValue = PokerHandRangkings.FOUR_CARD;
                        result.RightValue = resultCards;
                        return result;
                    }
                }
            }
            resultCards.Clear();

            // 풀 하우스 체크             문제 : 트리플과 투페어가 나오면 투 페어중 높은 값을 대입해 줘야함
            if (Is_Full_House)
            {
                for (int i = 0; i < numbersDictionary.Count; i++)
                {
                    if (2 < numbersDictionary[i].Count)
                    {
                        for (int j = 0; j < numbersDictionary[i].Count; j++)
                        {
                            resultCards.Add(numbersDictionary[i][j]);
                        }
                    }
                }
                for (int i = numbersDictionary.Count - 1; 0 < i; i--)
                {
                    if (2 == numbersDictionary[i].Count)
                    {
                        for (int j = 0; j < numbersDictionary[i].Count; j++)
                        {
                            resultCards.Add(numbersDictionary[i][j]);
                        }
                        if (resultCards.Count == 5)
                        {
                            result.LeftValue = PokerHandRangkings.FULL_HOUSE;
                            result.RightValue = resultCards;
                            return result;
                        }
                    }
                }
            }
            resultCards.Clear();

            // 마운틴 체크
            for (int i = 0; i < numbersDictionary.Count; i++)
            {
                int maxIdx = 0;
                int maxMarkNum = -1;
                string maxMark = string.Empty;
                for (int j = 0; j < numbersDictionary[i].Count; j++)
                {
                    if (numbersDictionary[i][j].Number == 14 ||
                        numbersDictionary[i][j].Number == 10 ||
                        numbersDictionary[i][j].Number == 11 ||
                        numbersDictionary[i][j].Number == 12 ||
                        numbersDictionary[i][j].Number == 13)
                    {
                        if (maxMarkNum < numbersDictionary[i][j].MarkNum)
                        {
                            maxIdx = j;
                            maxMarkNum = numbersDictionary[i][j].MarkNum;
                            maxMark = numbersDictionary[i][j].Mark;
                        }
                    }
                }
                if (maxMark != string.Empty)
                {
                    resultCards.Add(numbersDictionary[i][maxIdx]);
                }
            }
            if (resultCards.Count == 5)
            {
                result.LeftValue = PokerHandRangkings.MOUNTAIN;
                result.RightValue = resultCards;
                return result;
            }

            resultCards.Clear();
            // 백스트레이트 체크
            for (int i = 0; i < numbersDictionary.Count; i++)
            {
                int maxIdx = 0;
                int maxMarkNum = -1;
                string maxMark = string.Empty;
                for (int j = 0; j < numbersDictionary[i].Count; j++)
                {
                    if (numbersDictionary[i][j].Number == 14 ||
                        numbersDictionary[i][j].Number == 2 ||
                        numbersDictionary[i][j].Number == 3 ||
                        numbersDictionary[i][j].Number == 4 ||
                        numbersDictionary[i][j].Number == 5)
                    {
                        if (maxMarkNum < numbersDictionary[i][j].MarkNum)
                        {
                            maxIdx = j;
                            maxMarkNum = numbersDictionary[i][j].MarkNum;
                            maxMark = numbersDictionary[i][j].Mark;
                        }
                    }
                }
                if (maxMark != string.Empty)
                {
                    resultCards.Add(numbersDictionary[i][maxIdx]);
                }
            }
            if (resultCards.Count == 5)
            {
                result.LeftValue = PokerHandRangkings.BACK_STRAIGHT;
                result.RightValue = resultCards;
                return result;
            }

            resultCards.Clear();

            // 스트레이트 체크
            for (int i = numbersDictionary.Count - 1; 5 <= i; i--)
            {
                int maxIdx = 0;
                int maxMarkNum = -1;
                string maxMark = string.Empty;
                for (int j = i; i - 5 < j; j--)
                {
                    if (1 < numbersDictionary[j].Count)
                    {
                        for (int k = 0; k < numbersDictionary[j].Count; k++)
                        {
                            if (maxMarkNum < numbersDictionary[j][k].MarkNum)
                            {
                                maxMarkNum = numbersDictionary[j][k].MarkNum;
                                maxMark = numbersDictionary[j][k].Mark;
                                maxIdx = k;
                            }
                        }
                        resultCards.Add(numbersDictionary[j][maxIdx]);
                    }
                    else if (0 < numbersDictionary[j].Count)
                    {
                        resultCards.Add(numbersDictionary[j][0]);
                    }
                }
                if (resultCards.Count == 5)
                {
                    result.LeftValue = PokerHandRangkings.STRAIGHT;
                    result.RightValue = resultCards.OrderBy(x => x.Number).ToList();
                    return result;
                }
                else
                {
                    resultCards.Clear();
                }
            }

            // 트리플 체크
            if (Is_Triple)
            {
                for (int i = numbersDictionary.Count - 1; 0 <= i; i--)
                {
                    if (2 < numbersDictionary[i].Count)
                    {
                        for (int j = 0; j < numbersDictionary[i].Count; j++)
                        {
                            resultCards.Add(numbersDictionary[i][j]);
                        }
                        if (resultCards.Count == 3)
                        {
                            result.LeftValue = PokerHandRangkings.TRIPLE;
                            result.RightValue = resultCards.OrderBy(x => x.MarkNum).ToList();
                            return result;
                        }
                        else
                        {
                            resultCards.Clear();
                        }
                    }
                }
            }
            pair = 0;
            // 페어 체크
            for (int i = numbersDictionary.Count - 1; 0 <= i; i--)
            {
                if (1 < numbersDictionary[i].Count)
                {
                    pair++;
                    for (int j = 0; j < numbersDictionary[i].Count; j++)
                    {
                        resultCards.Add(numbersDictionary[i][j]);
                        if (3 < resultCards.Count)
                        {
                            break;
                        }
                    }
                }
            }

            if (1 < pair)
            {
                result.LeftValue = PokerHandRangkings.TWO_PAIR;
                result.RightValue = resultCards;
                return result;
            }
            else if (pair == 1)
            {
                result.LeftValue = PokerHandRangkings.OWN_PAIR;
                result.RightValue = resultCards;
                return result;
            }
            resultCards.Clear();

            resultCards.Add(cards[cards.Count - 1]);
            result.LeftValue = PokerHandRangkings.TOP;
            result.RightValue = resultCards;
            return result;
        }

        public bool WinnerCheck(Pair<PokerHandRangkings, List<Card>> dealerDeck, Pair<PokerHandRangkings, List<Card>> playerDeck)
        {
            if (dealerDeck.LeftValue < playerDeck.LeftValue)
            {
                return true;
            }
            else if (dealerDeck.LeftValue == playerDeck.LeftValue)
            {
                if (dealerDeck.RightValue[dealerDeck.RightValue.Count - 1].Number < playerDeck.RightValue[playerDeck.RightValue.Count - 1].Number)
                {
                    return true;
                }
                else if (dealerDeck.RightValue[dealerDeck.RightValue.Count - 1].Number == playerDeck.RightValue[playerDeck.RightValue.Count - 1].Number)
                {
                    if (dealerDeck.RightValue[dealerDeck.RightValue.Count - 1].MarkNum < playerDeck.RightValue[playerDeck.RightValue.Count - 1].MarkNum)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
            return false;
        }

    }
}
