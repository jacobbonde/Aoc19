﻿using System;

namespace AoC19
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("# ADVENT OF CODE 2019 #");
            Console.WriteLine();

            // Day1();
            // Day2();
            // Day3();
            // Day3C();
            // Day4();
            Day5();
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
            string[] input = Input.ReadStringLines("day3-input.txt");
            string[] input1 = input[0].Split(',');
            string[] input2= input[1].Split(',');

            //  string[] input1 = new[] {"R8","U5","L5","D3"};
            //  string[] input2 = new[] {"U7","R6","D4","L4"};

            // string[] input1 = new[] {"R75","D30","R83","U83","L12","D49","R71","U7","L72"};
            // string[] input2 = new[] {"U62","R66","U55","R34","D71","R55","D58","R83"};

            //  string[] input1 = new[] {"R98","U47","R26","D63","R33","U87","L62","D20","R33","U53","R51"};
            //  string[] input2 = new[] {"U98","R91","D20","R16","D67","R40","U7","R15","U6","R7"};

            AoC19.Day3.Part1(input1, input2);
            AoC19.Day3.Part2(input1, input2);
            Console.WriteLine();
        }

        public static void Day3C()
        {
            Console.WriteLine("##### DAY 3 #####");
            string[] input = Input.ReadStringLines("day3-input.txt");
            string[] input1 = input[0].Split(',');
            string[] input2= input[1].Split(',');

            //  string[] input1 = new[] {"R8","U5","L5","D3"};
            //  string[] input2 = new[] {"U7","R6","D4","L4"};

            // string[] input1 = new[] {"R75","D30","R83","U83","L12","D49","R71","U7","L72"};
            // string[] input2 = new[] {"U62","R66","U55","R34","D71","R55","D58","R83"};

            //  string[] input1 = new[] {"R98","U47","R26","D63","R33","U87","L62","D20","R33","U53","R51"};
            //  string[] input2 = new[] {"U98","R91","D20","R16","D67","R40","U7","R15","U6","R7"};

            var day3c = new Day3C();
            AoC19.Day3C.Part1(input1, input2);
            AoC19.Day3C.Part2(input1, input2);
            Console.WriteLine();
        }

        public static void Day4()
        {
            Console.WriteLine("##### DAY 4 #####");
            //string[] input = Input.ReadStringLines("day4-input.txt");
            // string[] input1 = input[0].Split(',');
            // string[] input2= input[1].Split(',');

            //  string[] input1 = new[] {"R8","U5","L5","D3"};
            //  string[] input2 = new[] {"U7","R6","D4","L4"};

            // string[] input1 = new[] {"R75","D30","R83","U83","L12","D49","R71","U7","L72"};
            // string[] input2 = new[] {"U62","R66","U55","R34","D71","R55","D58","R83"};

            //  string[] input1 = new[] {"R98","U47","R26","D63","R33","U87","L62","D20","R33","U53","R51"};
            //  string[] input2 = new[] {"U98","R91","D20","R16","D67","R40","U7","R15","U6","R7"};

            AoC19.Day4.Part1();
            AoC19.Day4.Part2();
            Console.WriteLine();
        }

        public static void Day5()
        {
            Console.WriteLine("##### DAY 5 #####");
            //string[] input = Input.ReadStringLines("day5-input.txt");
            // string[] input1 = input[0].Split(',');
            // string[] input2= input[1].Split(',');

            //  string[] input1 = new[] {"R8","U5","L5","D3"};
            //  string[] input2 = new[] {"U7","R6","D4","L4"};

            // string[] input1 = new[] {"R75","D30","R83","U83","L12","D49","R71","U7","L72"};
            // string[] input2 = new[] {"U62","R66","U55","R34","D71","R55","D58","R83"};

            //  string[] input1 = new[] {"R98","U47","R26","D63","R33","U87","L62","D20","R33","U53","R51"};
            //  string[] input2 = new[] {"U98","R91","D20","R16","D67","R40","U7","R15","U6","R7"};

            AoC19.Day5.Part1();
            AoC19.Day5.Part2();
            Console.WriteLine();
        }
    }
}
