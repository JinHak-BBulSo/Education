using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using System.Threading.Tasks;

namespace coading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] dx = { 0, 0, -1, 1 };
            int[] dy = { -1, 1, 0, 0 };
            int dir = -1;
            int nowY = -1, nowX = -1;
            int size = 0;
            int coinCount = 0;
            int coinNumber = 0;

            while (size <= 2)
            {
                Console.Write("맵의 사이즈를 입력해주세요. : ");
                int.TryParse(Console.ReadLine(), out size);

                if (size <= 2)
                {
                    Console.WriteLine("잘못된 입력입니다. 재입력 해주세요.");
                }
            } // 맵 사이즈 입력

            string[,] map = new string[size, size];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (y == 0 || y == size - 1)
                        map[y, x] = "■";
                    else if (x == 0 || x == size - 1)
                        map[y, x] = "■";
                    else if (y == size / 2 && x == size / 2)
                    {
                        nowY = y;
                        nowX = x;
                        map[y, x] = "★";
                    }
                    else map[y, x] = ".";
                }
            } // 맵에 값 넣어주기

            PrintMap(map, size);

            while (true)
            {
                if(coinNumber == 0) coinNumber = CoinProduce(map, size);
                Console.WriteLine("종료는 q를 입력해주세요.");
                Console.WriteLine("w a s d를 통해 움직여주세요");

                ConsoleKeyInfo cki;
                cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.W:
                        dir = 0;
                        break;
                    case ConsoleKey.S:
                        dir = 1;
                        break;
                    case ConsoleKey.A:
                        dir = 2;
                        break;
                    case ConsoleKey.D:
                        dir = 3;
                        break;
                    case ConsoleKey.Q:
                        dir = 9999;
                        break;
                    default:
                        Console.WriteLine("잘못된 키입력");
                        dir = -1;
                        continue;
                }
                
                if (dir == 9999) break;
                if (nowY + dy[dir] <= 0 || nowY + dy[dir] >= size - 1 ||
                    nowX + dx[dir] <= 0 || nowX + dx[dir] >= size - 1)
                {
                    Console.Clear();
                    PrintMap(map, size);
                    Console.WriteLine("벽이라 움직일 수 없습니다.\n");
                    continue;
                }
                else if (map[nowY + dy[dir], nowX + dx[dir]] == "●")
                {
                    coinCount++;
                    coinNumber--;
                    map[nowY + dy[dir], nowX + dx[dir]] = ".";

                    string temp = "★";
                    map[nowY, nowX] = map[nowY + dy[dir], nowX + dx[dir]];
                    map[nowY + dy[dir], nowX + dx[dir]] = temp;

                    nowY = nowY + dy[dir];
                    nowX = nowX + dx[dir];
                }
                else
                {
                    string temp = "★";
                    map[nowY, nowX] = map[nowY + dy[dir], nowX + dx[dir]];
                    map[nowY + dy[dir], nowX + dx[dir]] = temp;

                    nowY = nowY + dy[dir];
                    nowX = nowX + dx[dir];
                }
                Console.Clear();
                PrintMap(map, size);
                Console.WriteLine("획득 코인의 수 : {0}", coinCount);
            } // 움직임 루프
        } // Main

        static void PrintMap(string[,] map, int size)
        {
            // 맵의 상황을 출력하는 메소드
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Console.Write("{0}\t", map[y, x]);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static int CoinProduce(string[,] map, int size)
        {
            string noticeStr =
                string.Format("맵 어딘가에 코인이 나타났다.");

            int index = 0;
            while (index < 3)
            {
                Task loopTask = default;

                int coinX, coinY;
                Random ran = new Random();

                coinX = ran.Next(1, size - 1);
                coinY = ran.Next(1, size - 1);

                if (map[coinY, coinX] == ".")
                {
                    foreach (char charactor_ in noticeStr)
                    {
                        Console.Write("{0}", charactor_);
                        Task.Delay(100).Wait();
                    }
                    map[coinY, coinX] = "●";
                    index++;
                    Console.Clear();
                    PrintMap(map, size);
                }
                else
                {
                    continue;
                }
            }
            return index;
        }
    } // Program
}