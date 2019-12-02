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
            //Day2();
        }
    
        public static void Day1()
        {
            int[] masses = ReadIntLines("day1-input.txt");
            AoC19.Day1.Part1(masses);
            AoC19.Day1.Part2(masses);
        }

        public static void Day2()
        {
            //int[] input = ReadIntCommaSeparated("day2-input.txt");
            int[] input = new int[] {1,9,10,3,2,3,11,0,99,30,40,50}
            AoC19.Day2.Part1(input);
            AoC19.Day2.Part2(input);
        }

        public static int[] ReadIntLines(string filename)
        {
            return File.ReadLines(filename).Select(l => int.Parse(l)).ToArray();
        }

        public static int[] ReadIntCommaSeparated(string filename)
        {

            return File.ReadLines(filename).SelectMany(l => l.Split(',')).Select(i => int.Parse(i)).ToArray();
        }
    }
}
