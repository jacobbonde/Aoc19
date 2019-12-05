using System;
using System.Diagnostics;

namespace AoC19.Day5
{
    internal class Day5A
    {
        public static void Part1(int[] program)
        {
            var sw = new Stopwatch();
            sw.Start();

            var programCopy = new int[program.Length];
            Array.Copy(program, programCopy, program.Length);

            var (output,_) = IntCodeComputerDay5.Run(programCopy, 1);

            Console.WriteLine("PART 1");
            Console.WriteLine($"\t Output: {output}");
            
            sw.Stop();

            Console.WriteLine("PART 1");
            Console.WriteLine($"\t Answer: ");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2()
        {
            var sw = new Stopwatch();
            sw.Start();

            sw.Stop();

            Console.WriteLine("PART 2");
            Console.WriteLine($"\t Answer: ");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }
    }
}