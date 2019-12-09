using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AoC19
{
    internal class Day9
    {
        public static void Part1(long[] program)
        {
            Console.WriteLine("PART 1");
            var sw = new Stopwatch();
            sw.Start();

            var programCopy = new long[program.Length];
            Array.Copy(program, programCopy, program.Length);

            var computer = new IntCodeComputer();
            computer.Run(programCopy, 1);

            sw.Stop();

            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2(long[] program)
        {
            Console.WriteLine("PART 2");
            var sw = new Stopwatch();
            sw.Start();

            var programCopy = new long[program.Length];
            Array.Copy(program, programCopy, program.Length);

            var computer = new IntCodeComputer();
            computer.Run(programCopy, 2);

            sw.Stop();
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }
    }
}