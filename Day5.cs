using System;
using System.Diagnostics;

namespace AoC19
{
    internal class Day5
    {
        public static void Part1(int[] program)
        {
            Console.WriteLine("PART 1");
            var sw = new Stopwatch();
            sw.Start();

            var programCopy = new int[program.Length];
            Array.Copy(program, programCopy, program.Length);

            var computer = new IntCodeComputer();
            var (output,_) = computer.Run(programCopy, 1);

            sw.Stop();

            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2(int[] program, int input)
        {
            Console.WriteLine("PART 2");
            var sw = new Stopwatch();
            sw.Start();

            var programCopy = new int[program.Length];
            Array.Copy(program, programCopy, program.Length);

            var computer = new IntCodeComputer();
            var (output,_) = computer.Run(programCopy, input);

            sw.Stop();
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }
    }
}