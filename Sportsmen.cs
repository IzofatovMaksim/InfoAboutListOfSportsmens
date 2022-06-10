using System;

namespace ConsoleApp1
{
    public struct Sportsmen : IComparable<Sportsmen>
    {
        public string Name { get; private set; }
        public int YearOfBirth { get; private set; }
        public string KindOfSport { get; private set; }
        public int Rank { get; private set; }

        public Sportsmen(string name, int yearOfBirth, string kindOfSport, int rank)
        {
            Name = name;
            YearOfBirth = yearOfBirth;
            KindOfSport = kindOfSport;
            Rank = rank;
        }

        public int CompareTo(Sportsmen other) => YearOfBirth.CompareTo(other.YearOfBirth);
    }
}
