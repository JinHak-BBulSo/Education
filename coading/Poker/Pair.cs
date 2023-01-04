using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrumpCardGame
{
    public class Pair<T1, T2>
    {
        private T1 mLeftValue;
        private T2 mRightValue;
        public Pair(ref T1 left, ref T2 right)
        {
            mLeftValue = left;
            mRightValue = right;
        }

        public Pair()
        {
            mLeftValue = default(T1);
            mRightValue = default(T2);
        }

        public void Make_Pair(ref T1 left, ref T2 right)
        {
            mLeftValue = left;
            mRightValue = right;
        }

        public override string ToString()
        {
            return mLeftValue.ToString() + mRightValue.ToString();
        }

        public T1 LeftValue
        {
            get
            {
                if (mLeftValue != null)
                {
                    return mLeftValue;
                }
                else
                {
                    return default(T1);
                }
            }
            set { mLeftValue = value; }
        }
        public T2 RightValue
        {
            get
            {
                if (mRightValue != null)
                {
                    return mRightValue;
                }
                else
                {
                    return default(T2);
                }
            }
            set { mRightValue = value; }
        }


    }
}
