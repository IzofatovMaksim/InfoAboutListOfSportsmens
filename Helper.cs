using System;

namespace ConsoleApp1
{
    public static class Helper
    {
        public static int GetAge(this Sportsmen sportsmen) => DateTime.Now.Year - sportsmen.YearOfBirth;
    }
}
