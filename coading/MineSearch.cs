using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coading
{
    public class MineSearch
    {
        public static void MineSearchStart()
        {
            Random randomMine = new Random();
            const int MINE_PERCETAGE = 30;
            const int BOARD_SIZE_X = 10;
            const int BOARD_SIZE_Y = 10;

            bool isDebugMode = false;
            bool isGameover = false;
            bool isPlayerWin = false;
            int playerTurnCnt = 0;

            /**
             * 차있는 상태 : ■
             * -1 : 지뢰가 있음
             * 0 : 지뢰가 없음
             **/
            int[,] gameBoard = new int[BOARD_SIZE_Y, BOARD_SIZE_X];
            int[,] playBoard = new int[BOARD_SIZE_Y, BOARD_SIZE_X];
            int[,] mineCntMap = new int[BOARD_SIZE_Y, BOARD_SIZE_X];

            for (int y = 0; y < BOARD_SIZE_Y; y++)
            {
                for (int x = 0; x < BOARD_SIZE_X; x++)
                {
                    gameBoard[y, x] = randomMine.Next(1, 100 + 1);
                    playBoard[y, x] = -1;

                    if (gameBoard[y, x] < MINE_PERCETAGE)
                        mineCntMap[y, x] = -1;
                    else
                        mineCntMap[y, x] = 0;
                }
            }

            while (!isGameover)
            {
                // 현재 보드 상태 출력
                for (int y = 0; y < BOARD_SIZE_Y; y++)
                {
                    for (int x = 0; x < BOARD_SIZE_X; x++)
                    {
                        switch (playBoard[y, x])
                        {
                            case -2:
                                Console.Write("X".PadRight(3, ' '));
                                break;
                            case -1:
                                Console.Write("□".PadRight(2, ' '));
                                break;
                            case 0:
                                Console.Write("■".PadRight(2, ' '));
                                break;
                            default:
                                Console.Write("{0}".PadRight(5, ' '), playBoard[y, x]);
                                break;
                        }// switch
                    } // x loop
                    Console.WriteLine();
                } // y loop
                Console.WriteLine();

                int playerX = -1;
                int playerY = -1;
                bool isLocationValid = false;

                while (!isLocationValid)
                {
                    Console.Write("[플레이어] x 좌표 입력 : ");
                    int.TryParse(Console.ReadLine(), out playerX);
                    Console.Write("[플레이어] y 좌표 입력 : ");
                    int.TryParse(Console.ReadLine(), out playerY);

                    isLocationValid =
                        (0 <= playerX && playerX < BOARD_SIZE_X) &&
                        (0 <= playerY && playerY < BOARD_SIZE_X);
                    if (!isLocationValid)
                    {
                        Console.WriteLine("{0} {1}", "[System] 해당 좌표는 유효하지 않습니다.",
                            "다른 좌표를 입력하세요.\n");
                        continue;
                    }

                    isLocationValid = isLocationValid && playBoard[playerY, playerX].Equals(-1);
                    if (!isLocationValid)
                    {
                        Console.WriteLine("{0} {1}", "[System] 해당 좌표는 이미 오픈되었습니다.",
                            "다른 좌표를 입력하세요.\n");
                        continue;
                    }
                    playerTurnCnt++;

                    if (playerTurnCnt.Equals(1))
                    {
                        gameBoard[playerY, playerX] = MINE_PERCETAGE + 1;
                        mineCntMap[playerY, playerX] = 0;
                        playBoard[playerY, playerX] = -1;

                        for (int y = 0; y < BOARD_SIZE_Y; y++)
                        {
                            for (int x = 0; x < BOARD_SIZE_X; x++)
                            {
                                if (!mineCntMap[y, x].Equals(-1)) continue;

                                bool isSearchTileValid = false;
                                for (int searchY = y - 1; searchY < y + 1; searchY++)
                                {
                                    for (int searchX = x - 1; searchX < x + 1; searchX++)
                                    {
                                        isSearchTileValid =
                                            (0 <= searchX && searchX < BOARD_SIZE_X) &&
                                            (0 <= searchY && searchY < BOARD_SIZE_Y);
                                        if (!isSearchTileValid) continue;

                                        if (mineCntMap[searchY, searchX].Equals(-1)) continue;

                                        mineCntMap[searchY, searchX]++;
                                    }
                                }
                            } // loop : 지뢰 주변 타일 순회
                        }
                    }
                }

                if (gameBoard[playerY, playerX] < MINE_PERCETAGE)
                {
                    isGameover = true;
                    isPlayerWin = false;
                    playBoard[playerY, playerX] = -2;
                }
                else
                {
                    bool isSearchTileValid = false;
                    for (int searchY = playerY - 1; searchY < playerY + 1; searchY++)
                    {
                        for (int searchX = playerX - 1; searchX < playerX + 1; searchX++)
                        {
                            isSearchTileValid =
                                (0 <= searchX && searchX < BOARD_SIZE_X) &&
                                (0 <= searchY && searchY < BOARD_SIZE_Y);

                            if (mineCntMap[searchY, searchX].Equals(-1)) playBoard[searchY, searchX] = -2; // 지뢰인 경우
                            else playBoard[searchY, searchX] = mineCntMap[searchY, searchX]; // 지뢰가 아닌 경우
                        }
                    }
                }

                int unknownTileCnt = 0;
                for (int y = 0; y < BOARD_SIZE_Y; y++)
                {
                    if (0 < unknownTileCnt) break;

                    for (int x = 0; x < BOARD_SIZE_X; x++)
                    {
                        if (playBoard[y, x].Equals(-1) &&
                            !mineCntMap[y, x].Equals(-1))
                        {
                            unknownTileCnt++;
                        }
                    }
                }

                if (unknownTileCnt.Equals(0))
                {
                    isGameover = true;
                    isPlayerWin = true;
                }

                if (isGameover) break;

                if (isDebugMode)
                {
                    Console.WriteLine();
                    for (int y = 0; y < BOARD_SIZE_Y; y++)
                    {
                        for (int x = 0; x < BOARD_SIZE_X; x++)
                            Console.Write("{0} ", mineCntMap[y, x]);
                        Console.WriteLine();
                    }

                    for (int y = 0; y < BOARD_SIZE_Y; y++)
                    {
                        for (int x = 0; x < BOARD_SIZE_X; x++)
                        {
                            if (gameBoard[y, x] < MINE_PERCETAGE)
                                Console.Write("# ");
                            else
                                Console.Write(", ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();


            }// loop 게임 루프

            Console.WriteLine();
            for (int y = 0; y < BOARD_SIZE_Y; y++)
            {
                for (int x = 0; x < BOARD_SIZE_X; x++)
                {
                    switch (playBoard[y, x])
                    {
                        case -2:
                            Console.Write("X".PadRight(3, ' '));
                            break;
                        case -1:
                            Console.Write("□".PadRight(2, ' '));
                            break;
                        case 0:
                            Console.Write("■".PadRight(2, ' '));
                            break;
                        default:
                            Console.Write("{0}".PadRight(5, ' '), playBoard[y, x]);
                            break;
                    } // switch
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            if (isPlayerWin) Console.WriteLine("[플레이어] 지뢰를 모두 찾고 승리했습니다.");
            else Console.WriteLine("[플레이어] 지뢰를 밟고 패배했습니다.");
        }// loop 지뢰 찾기
    }
}
