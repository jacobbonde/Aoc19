using System;
using System.Diagnostics;

namespace AoC19
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("# ADVENT OF CODE 2019 #");
            Console.WriteLine();

            var sw = new Stopwatch();
            sw.Start();

            // Day1();
            // Day2();
            // Day3();
            // Day3C();
            // Day4();
            // Day5();
            // Day6();
            //Day7();
            //Day8();
            //Day9();
            //Day10();
            //Day11();
            Day12();

            sw.Stop();
            Console.WriteLine($"Total Time: {sw.ElapsedMilliseconds}");
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

            AoC19.Day4.Part1();
            AoC19.Day4.Part2();
            Console.WriteLine();
        }

        public static void Day5()
        {
            Console.WriteLine("##### DAY 5 #####");
            int[] program = Input.ReadIntSeparated("day5-input.txt", ',');

            //int[] part1Test = new[] {1002,4,3,4,33};

            //int[] eqPosTest = new[] {3,9,8,9,10,9,4,9,99,-1,8};
            //int[] ltPosTest = new[] {3,9,7,9,10,9,4,9,99,-1,8};
            //int[] eqImmTest = new[] {3,3,1108,-1,8,3,4,3,99};
            //int[] ltImmTest = new[] {3,3,1107,-1,8,3,4,3,99};

            // int[] jumpPosTest = new[] {3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9};
            // int[] jumpImmTest = new[] {3,3,1105,-1,9,1101,0,0,12,4,12,99,1};

            AoC19.Day5.Part1(program);
            AoC19.Day5.Part2(program, 5);
            Console.WriteLine();
        }

        public static void Day6()
        {
            Console.WriteLine("##### DAY 6 #####");
            string[] input = Input.ReadStringLines("day6-input.txt");

            //int[] part1Test = new[] {1002,4,3,4,33};

            //int[] eqPosTest = new[] {3,9,8,9,10,9,4,9,99,-1,8};
            //int[] ltPosTest = new[] {3,9,7,9,10,9,4,9,99,-1,8};
            //int[] eqImmTest = new[] {3,3,1108,-1,8,3,4,3,99};
            //int[] ltImmTest = new[] {3,3,1107,-1,8,3,4,3,99};

            // int[] jumpPosTest = new[] {3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9};
            // int[] jumpImmTest = new[] {3,3,1105,-1,9,1101,0,0,12,4,12,99,1};

            AoC19.Day6.Part1And2(input);
            //AoC19.Day6.Part2(input);
            Console.WriteLine();
        }

        public static void Day7()
        {
            Console.WriteLine("##### DAY 7 #####");
            int[] program = Input.ReadIntSeparated("day7-input.txt", ',');

//             int[] part1Test1 = new[] {3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0};

//             int[] part1Test2 = new[] {3,23,3,24,1002,24,10,24,1002,23,-1,23,
// 101,5,23,23,1,24,23,23,4,23,99,0,0};
//             int[] part1Test3 = new[] {3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,
// 1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0};
            //int[] eqImmTest = new[] {3,3,1108,-1,8,3,4,3,99};
            //int[] ltImmTest = new[] {3,3,1107,-1,8,3,4,3,99};

            // int[] jumpPosTest = new[] {3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9};
            // int[] jumpImmTest = new[] {3,3,1105,-1,9,1101,0,0,12,4,12,99,1};

            //AoC19.Day7.Part1(program);
            //AoC19.Day7.Part2(program);
            Console.WriteLine();
        }

        public static void Day8()
        {
            Console.WriteLine("##### DAY 8 #####");
            string imageData = Input.ReadStringLines("day8-input.txt")[0];

//             int[] part1Test1 = new[] {3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0};

//             int[] part1Test2 = new[] {3,23,3,24,1002,24,10,24,1002,23,-1,23,
// 101,5,23,23,1,24,23,23,4,23,99,0,0};
//             int[] part1Test3 = new[] {3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,
// 1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0};
            //int[] eqImmTest = new[] {3,3,1108,-1,8,3,4,3,99};
            //int[] ltImmTest = new[] {3,3,1107,-1,8,3,4,3,99};

            // int[] jumpPosTest = new[] {3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9};
            // int[] jumpImmTest = new[] {3,3,1105,-1,9,1101,0,0,12,4,12,99,1};

            AoC19.Day8.Part1(imageData);
            //AoC19.Day7.Part2(imageData);
            Console.WriteLine();
        }

        public static void Day9()
        {
            Console.WriteLine("##### DAY 9 #####");
            long[] program = Input.ReadLongSeparated("day9-input.txt", ',');

             //int[] part1Test1 = new[] {109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99};

             //int[] part1Test2 = new[] {1102,34915192,34915192,7,4,7,99,0};
             //long[] part1Test3 = new[] {104,1125899906842624,99};
            //int[] eqImmTest = new[] {3,3,1108,-1,8,3,4,3,99};
            //int[] ltImmTest = new[] {3,3,1107,-1,8,3,4,3,99};

            // int[] jumpPosTest = new[] {3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9};
            // int[] jumpImmTest = new[] {3,3,1105,-1,9,1101,0,0,12,4,12,99,1};

            //AoC19.Day9.Part1(program);
            AoC19.Day9.Part2(program);
            Console.WriteLine();
        }

        public static void Day10()
        {
            Console.WriteLine("##### DAY 910 #####");
            //string[] input = Input.ReadStringLines("day10-input.txt");
            string[] input = Input.ReadStringLines("day10-test1.txt");

            AoC19.Day10.Part1(input);
            //AoC19.Day9.Part2(program);
            Console.WriteLine();
        }

        public static void Day11()
        {
            Console.WriteLine("##### DAY 11 #####");
            long[] program = Input.ReadLongSeparated("day11-input.txt", ',');

             //int[] part1Test1 = new[] {109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99};

             //int[] part1Test2 = new[] {1102,34915192,34915192,7,4,7,99,0};
             //long[] part1Test3 = new[] {104,1125899906842624,99};
            //int[] eqImmTest = new[] {3,3,1108,-1,8,3,4,3,99};
            //int[] ltImmTest = new[] {3,3,1107,-1,8,3,4,3,99};

            // int[] jumpPosTest = new[] {3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9};
            // int[] jumpImmTest = new[] {3,3,1105,-1,9,1101,0,0,12,4,12,99,1};

            AoC19.Day11.Part1(program);
            //AoC19.Day11.Part2(program);
            Console.WriteLine();
        }

        public static void Day12()
        {
            Console.WriteLine("##### DAY 12 #####");
            string[] input = Input.ReadStringLines("day12-input.txt");

             //int[] part1Test1 = new[] {109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99};

             //int[] part1Test2 = new[] {1102,34915192,34915192,7,4,7,99,0};
             //long[] part1Test3 = new[] {104,1125899906842624,99};
            //int[] eqImmTest = new[] {3,3,1108,-1,8,3,4,3,99};
            //int[] ltImmTest = new[] {3,3,1107,-1,8,3,4,3,99};

            // int[] jumpPosTest = new[] {3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9};
            // int[] jumpImmTest = new[] {3,3,1105,-1,9,1101,0,0,12,4,12,99,1};

            AoC19.Day12.Part1(input);
            //AoC19.Day12.Part2(program);
            Console.WriteLine();
        }
    }
}
