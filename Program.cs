using System;

namespace AoC19
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("# ADVENT OF CODE 2019 #");
            Console.WriteLine();

            Day1();
            Day2();
        }
    
        public static void Day1()
        {
            Console.WriteLine("##### DAY 1 #####");
            int[] masses = Input.ReadIntLines("day1-input.txt");
            AoC19.Day1.Part1(masses);
            AoC19.Day1.Part2(masses);
            Console.WriteLine();
        }

        public static void Day2()
        {
            Console.WriteLine("##### DAY 2 #####");
            int[] input = Input.ReadIntSeparated("day2-input.txt", ',');
            AoC19.Day2.Part1(input);
            AoC19.Day2.Part2(input);
            Console.WriteLine();
        }

        public static void Day3()
        {
            Console.WriteLine("##### DAY 3 #####");
            int[] input = Input.ReadIntSeparated("day3-input.txt", ',');
            AoC19.Day3.Part1(input);
            AoC19.Day3.Part2(input);
            Console.WriteLine();
        }
    }
}
