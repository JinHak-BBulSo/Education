using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coading
{
    public class Coin
    {
        static public int CoinProduce(string[,] map, int size, ref int mapCoinCount)
        {
            string noticeStr =
                string.Format("맵 어딘가에 코인이 나타났다.");

            int index = 0;
            while (index < 3)
            {
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
                    mapCoinCount++;
                    Console.Clear();
                    Map.PrintMap(map, size);
                }
                else
                {
                    continue;
                }
            }
            return index;
        }
    }
}
