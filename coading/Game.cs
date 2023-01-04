using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TrumpCardGame;

namespace coading
{
    public class Game
    {
        private int eatCoinCount = 0;
        private int mapCoinCount = 0;
        private int mapNumber = 0;
        private bool coinGameClear = false;
        private bool pokerGameStart = false;
        private bool mineSearchStart = false;
        private bool tictactoeStart = false;

        public void StartGame()
        {
            int[] dx = { 0, 0, -1, 1 };
            int[] dy = { -1, 1, 0, 0 };
            int dir = -1;
            int nowY = -1, nowX = -1;
            int size = 0;
            
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

            Map.MapSetting(size, ref nowY, ref nowX, 0, map);
            Map.PrintMap(map, size);

            while (true)
            {
                Move(map, size, ref nowY, ref nowX, ref dir, ref mapNumber);
                if (dir == 9999) break;
                if(mapNumber == 1 && eatCoinCount < 15 && mapCoinCount == 0 && !coinGameClear)
                {
                    Coin.CoinProduce(map, size, ref mapCoinCount);
                }
                else if (mapNumber == 2 && !pokerGameStart)
                {
                    pokerGameStart = true;
                    PokerCardGame poker = new PokerCardGame();
                }
                else if(mapNumber == 3 && !mineSearchStart)
                {
                    mineSearchStart = true;
                    MineSearch.MineSearchStart();
                }
                else if(mapNumber == 4 && !tictactoeStart)
                {
                    tictactoeStart = true;
                    TicTacToe.TicTacToeStart();
                }
                if (mapCoinCount >= 15) coinGameClear = true;
            } // 움직임 루프
        } // Main

        public void Move(string[,] map, int size, ref int nowY, ref int nowX, ref int dir, ref int mapNumber)
        {
            int[] dx = { 0, 0, -1, 1 };
            int[] dy = { -1, 1, 0, 0 };

            if (mapNumber == 1)
            {
                Console.WriteLine("현재 먹은 코인의 갯수 : {0}", eatCoinCount);
            }
            
            Console.WriteLine("종료는 q를 입력해주세요.");
            Console.WriteLine("w a s d를 통해 움직여주세요 : ");
            
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
                    break;
            }
            Console.WriteLine();
            Console.WriteLine();

            if (dir == -1) return;
            if (dir == 9999) return ;

            if (map[nowY + dy[dir], nowX + dx[dir]] == "■")
            {
                Map.PrintMap(map, size);
                Console.WriteLine("벽이라 움직일 수 없습니다.\n");
            }
            else if (map[nowY + dy[dir], nowX + dx[dir]] == "●")
            {
                eatCoinCount++;
                mapCoinCount--;
                map[nowY + dy[dir], nowX + dx[dir]] = ".";

                string temp = "★";
                map[nowY, nowX] = map[nowY + dy[dir], nowX + dx[dir]];
                map[nowY + dy[dir], nowX + dx[dir]] = temp;

                nowY = nowY + dy[dir];
                nowX = nowX + dx[dir];
            }
            else if(map[nowY + dy[dir], nowX + dx[dir]] == "□")
            {
                map[nowY, nowX] = ".";
                nowY = nowY + dy[dir];
                nowX = nowX + dx[dir];
                if (mapNumber == 0)
                    Map.ChangeMap(map, size, ref mapNumber, ref nowY, ref nowX);
                else
                {
                    mapCoinCount = 0;
                    Map.ChangeMap(map, size, ref mapNumber, ref nowY, ref nowX);
                }
            }
            else
            {
                string temp = "★";
                map[nowY, nowX] = map[nowY + dy[dir], nowX + dx[dir]];
                map[nowY + dy[dir], nowX + dx[dir]] = temp;

                nowY = nowY + dy[dir];
                nowX = nowX + dx[dir];
            }
            Map.PrintMap(map, size);
            clearConsole();
        }

        static void clearConsole()
        {
            for (int i = 0; i < 6; i++)
                Console.WriteLine();
        }

    }
}
