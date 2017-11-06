using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpmon
{
    public static class Functions
    {
        public static int Choose(int x1, int x2)
        {
            int[] selectionArray = new int[] { x1, x2 };
            return selectionArray[GameInstance.Rng.Next(0, 2)];
        }
        public static int Choose(int x1, int x2, int x3)
        {
            int[] selectionArray = new int[] { x1, x2, x3 };
            return selectionArray[GameInstance.Rng.Next(0, 3)];
        }
        public static int Choose(int x1, int x2, int x3, int x4)
        {
            int[] selectionArray = new int[] { x1, x2, x3, x4 };
            return selectionArray[GameInstance.Rng.Next(0, 4)];
        }
        
    }
}
