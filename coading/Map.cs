using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coading
{
    public class Map
    {
        static public void MapSetting(int size, ref int nowY, ref int nowX, int mapNumber, string[,] map)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (y == 0 || y == size - 1)
                        map[y, x] = "■";
                    else if (x == 0 || x == size - 1)
                        map[y, x] = "■";
                    else if (y == size / 2 && x == size / 2 && nowY == -1 && nowX == -1)
                    {
                        nowY = y;
                        nowX = x;
                        map[y, x] = "★";
                    }
                    else map[y, x] = ".";
                }
            } // 맵에 값 넣어주기

            switch (mapNumber)
            {
                case 0:
                    map[0, size / 2] = "□";
                    map[size / 2, 0] = "□";
                    map[size / 2, size - 1] = "□";
                    map[size - 1, size / 2] = "□";
                    break;
                case 1:
                    map[size - 1, size / 2] = "□";
                    break;
                case 2:
                    map[size / 2, 0] = "□";
                    break;
                case 3:
                    map[0, size / 2] = "□";
                    break;
                case 4:
                    map[size / 2, size - 1] = "□";
                    break;
            }
            map[nowY, nowX] = "★";
        }

        static public void PrintMap(string[,] map, int size)
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
            //Console.WriteLine();
        }

        static private int CheckPortalPosition(ref int nowY, ref int nowX, int size)
        {
            int portalPosition = -1;
            if (nowY == 0 && nowX == size / 2)
            {
                portalPosition = 1;
                nowY = size - 2; nowX = size / 2;
            }
            else if (nowY == size / 2 && nowX == size - 1)
            {
                portalPosition = 2;
                nowY = size / 2; nowX = 1;
            }
            else if (nowY == size - 1 && nowX == size / 2)
            {
                portalPosition = 3;
                nowY = 1; nowX = size / 2;
            }
            else
            {
                portalPosition = 4;
                nowY = size / 2; nowX = size - 2;
            }

            return portalPosition;
        }

        static public void ChangeMap(string[,] map, int size, ref int mapNumber, ref int nowY, ref int nowX)
        {
            int portalPosition = CheckPortalPosition(ref nowY, ref nowX, size);
            
            if (mapNumber == 0)
            {
                switch(portalPosition)
                {
                    case 1:
                        mapNumber = 1;
                        break;
                    case 2:
                        mapNumber = 2;
                        break;
                    case 3:
                        mapNumber = 3;
                        break;
                    case 4:
                        mapNumber = 4;
                        break;
                }
                MapSetting(size, ref nowY, ref nowX, mapNumber, map);
            }
            else
            {                
                mapNumber = 0;
                MapSetting(size, ref nowY, ref nowX, mapNumber, map);
            }
        }
    }
}
