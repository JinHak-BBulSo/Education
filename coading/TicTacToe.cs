using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coading
{
    public class TicTacToe
    {
        enum TicTacToePlayerType
        {
            NONE = 0, PLAYER, COMPUTER
        }
        public static void TicTacToeStart()
        {
            int[,] gameBoard = new int[3, 3];
            int playerX, playerY = 0;
            bool isValidLocation = false;
            bool isPlayerTurn = false;
            bool isGameOver = false;

            string playerIcon = string.Empty;
            string playerType = string.Empty;

            while (isGameOver == false)
            {
                isPlayerTurn = true;
                playerType = "[플레이어]";

                playerX = 0;
                playerY = 0;
                isValidLocation = false;


                while (true)
                {
                    if (isValidLocation) break;

                    Console.Write("[플레이어] (x) 좌표: ");
                    int.TryParse(Console.ReadLine(), out playerX);
                    Console.Write("[플레이어] (y) 좌표: ");
                    int.TryParse(Console.ReadLine(), out playerY);

                    if (gameBoard[playerY, playerX].Equals((int)TicTacToePlayerType.NONE))
                    {
                        gameBoard[playerY, playerX] = (int)(TicTacToePlayerType.PLAYER);
                        isValidLocation = true;
                    }
                    else
                    {
                        Console.WriteLine("[System] 해당 좌표는 비어있지 않습니다. / 다른 좌표릴 입력하세요.");
                        isValidLocation = false;
                    }
                }
                for (int y = 0; y <= gameBoard.GetUpperBound(0); y++)
                {
                    Console.WriteLine("---|---|---");
                    for (int x = 0; x <= gameBoard.GetUpperBound(1); x++)
                    {
                        switch (gameBoard[y, x])
                        {
                            case (int)TicTacToePlayerType.PLAYER:
                                playerIcon = "O";
                                break;
                            case (int)TicTacToePlayerType.COMPUTER:
                                playerIcon = "X";
                                break;
                            default:
                                playerIcon = " ";
                                break;
                        }
                        Console.Write(" {0} ", playerIcon);

                        if (x == gameBoard.GetUpperBound(1)) { /* Do nothing */ }
                        else { Console.Write("|"); }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("---|---|---");

                Console.WriteLine();
                isGameOver = false;
                for (int y = 0; y <= gameBoard.GetUpperBound(0); y++)
                {
                    if (gameBoard[y, 0].Equals((int)TicTacToePlayerType.PLAYER) &&
                        gameBoard[y, 1].Equals((int)TicTacToePlayerType.PLAYER) &&
                        gameBoard[y, 2].Equals((int)TicTacToePlayerType.PLAYER))
                    {
                        isGameOver = true;
                    }
                    else { continue; }
                }

                for (int x = 0; x <= gameBoard.GetUpperBound(1); x++)
                {
                    if (gameBoard[0, x].Equals((int)TicTacToePlayerType.PLAYER) &&
                        gameBoard[1, x].Equals((int)TicTacToePlayerType.PLAYER) &&
                        gameBoard[2, x].Equals((int)TicTacToePlayerType.PLAYER))
                    {
                        isGameOver = true;
                    }
                    else { continue; }
                }

                if (gameBoard[0, 0].Equals((int)TicTacToePlayerType.PLAYER) &&
                    gameBoard[1, 1].Equals((int)TicTacToePlayerType.PLAYER) &&
                    gameBoard[2, 2].Equals((int)TicTacToePlayerType.PLAYER))
                {
                    isGameOver = true;
                }

                if (gameBoard[0, 2].Equals((int)TicTacToePlayerType.PLAYER) &&
                    gameBoard[1, 1].Equals((int)TicTacToePlayerType.PLAYER) &&
                    gameBoard[2, 0].Equals((int)TicTacToePlayerType.PLAYER))
                {
                    isGameOver = true;
                }
                if (isGameOver) break;

                isPlayerTurn = false;
                playerType = "[컴퓨터]";
                bool isComputerDoing = false;

                Console.WriteLine("{0}의 턴", playerType);

                if (!isComputerDoing)
                {
                    if (gameBoard[1, 1].Equals((int)TicTacToePlayerType.NONE))
                    {
                        gameBoard[1, 1] = (int)TicTacToePlayerType.COMPUTER;
                        isComputerDoing = true;
                    }
                    else { /* Do nothing */ };
                }

                if (!isComputerDoing)
                {
                    for (int y = 0; y <= gameBoard.GetUpperBound(0); y++)
                    {
                        for (int x = 0; x <= gameBoard.GetUpperBound(1); x++)
                        {
                            if (!isComputerDoing && gameBoard[y, x].Equals((int)TicTacToePlayerType.NONE))
                            {
                                gameBoard[y, x] = (int)TicTacToePlayerType.COMPUTER;
                                isComputerDoing = true;
                                break;
                            }
                            else { continue; }
                        }
                    }
                }
                else {  /* Do nothing */}


                for (int y = 0; y <= gameBoard.GetUpperBound(0); y++)
                {
                    Console.WriteLine("---|---|---");
                    for (int x = 0; x <= gameBoard.GetUpperBound(1); x++)
                    {
                        switch (gameBoard[y, x])
                        {
                            case (int)TicTacToePlayerType.PLAYER:
                                playerIcon = "O";
                                break;
                            case (int)TicTacToePlayerType.COMPUTER:
                                playerIcon = "X";
                                break;
                            default:
                                playerIcon = " ";
                                break;
                        }
                        Console.Write(" {0} ", playerIcon);

                        if (x == gameBoard.GetUpperBound(1)) { /* Do nothing */}
                        else { Console.Write("|"); }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("---|---|---");

                Console.WriteLine();
                isGameOver = false;

                for (int y = 0; y <= gameBoard.GetUpperBound(0); y++)
                {
                    if (gameBoard[y, 0].Equals((int)TicTacToePlayerType.COMPUTER) &&
                        gameBoard[y, 1].Equals((int)TicTacToePlayerType.COMPUTER) &&
                        gameBoard[y, 2].Equals((int)TicTacToePlayerType.COMPUTER))
                    {
                        isGameOver = true;
                    }
                    else { continue; }
                }

                for (int x = 0; x <= gameBoard.GetUpperBound(1); x++)
                {
                    if (gameBoard[0, x].Equals((int)TicTacToePlayerType.COMPUTER) &&
                        gameBoard[1, x].Equals((int)TicTacToePlayerType.COMPUTER) &&
                        gameBoard[2, x].Equals((int)TicTacToePlayerType.COMPUTER))
                    {
                        isGameOver = true;
                    }
                    else { continue; }
                }

                if (gameBoard[0, 0].Equals((int)TicTacToePlayerType.COMPUTER) &&
                    gameBoard[1, 1].Equals((int)TicTacToePlayerType.COMPUTER) &&
                    gameBoard[2, 2].Equals((int)TicTacToePlayerType.COMPUTER))
                {
                    isGameOver = true;
                }

                if (gameBoard[0, 2].Equals((int)TicTacToePlayerType.COMPUTER) &&
                    gameBoard[1, 1].Equals((int)TicTacToePlayerType.COMPUTER) &&
                    gameBoard[2, 0].Equals((int)TicTacToePlayerType.COMPUTER))
                {
                    isGameOver = true;
                }
                if (isGameOver) break;
            } // loop 게임루프
            Console.WriteLine("{0} 의 승리", playerType);
        } // TicTacToeStart
    }
}
