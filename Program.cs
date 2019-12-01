using System;
using System.IO;
using System.Linq;

namespace AoC19
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2019");

            Day1();
            Day2();
        }
    
        public static void Day1()
        {
            int[] masses = ReadInputFile("day1-input.txt");
            AoC19.Day1.Question1(masses);
            AoC19.Day1.Question2(masses);
        }

        public static void Day2()
        {
            int[] input = ReadInputFile("day2-input.txt");
            AoC19.Day2.Question1(input);
            AoC19.Day2.Question2(input);
        }

        public static int[] ReadInputFile(string filename)
        {
            return File.ReadLines(filename).Select(l => int.Parse(l)).ToArray();
        }
    }
}
