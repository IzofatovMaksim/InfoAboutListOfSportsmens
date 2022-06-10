using System.Collections;

namespace ConsoleApp1
{
    class SortByKindOfSport : IComparer
    {
        public int Compare(object x, object y)
        {
            if(x is Sportsmen sp1 && y is Sportsmen sp2)
            {
                return sp1.KindOfSport.CompareTo(sp2.KindOfSport);
            }
            return -1;
        }
    }
}
