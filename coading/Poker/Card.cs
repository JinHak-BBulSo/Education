using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrumpCardGame
{
    public class Card
    {
        private string mName;
        private int mNumber;
        private string mMark;
        private int mMarkNum;

        public Card()
        {
            mName = string.Empty;
            mMark = string.Empty;
            mNumber = 0;
            mMarkNum = 0;
        }
        public Card(string name, string mark, int number, int markNum)
        {
            mName = name;
            mMark = mark;
            mNumber = number;
            mMarkNum = markNum;
        }


        public int Number
        {
            get { return mNumber; }
            set { mNumber = value; }
        }
        public string Mark
        {
            get { return mMark; }
            set { mMark = value; }
        }
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public int MarkNum
        {
            get { return mMarkNum; }
            set { mMarkNum = value; }
        }
    }
}
