using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AoC19
{
    internal class Day7
    {
        public static void Part1(int[] program)
        {
            Console.WriteLine("PART 1");
            var sw = new Stopwatch();
            sw.Start();

            var programCopy = new int[program.Length];
            Array.Copy(program, programCopy, program.Length);

            var  phaseSettingSequences = CreatePhaseSettingSequences(new [] {0,1,2,3,4}); 

            var largestValue = int.MinValue;

            foreach (var item in phaseSettingSequences)
            {
                var ampA = new IntCodeComputer();
                ampA.Run(programCopy, item[0], 0);
                var outputA = ampA.Output.Take();

                var ampB = new IntCodeComputer();
                ampB.Run(programCopy, item[1], outputA);
                var outputB = ampB.Output.Take();

                var ampC = new IntCodeComputer();
                ampC.Run(programCopy, item[2], outputB);
                var outputC = ampC.Output.Take();

                var ampD = new IntCodeComputer();
                ampD.Run(programCopy, item[3], outputC);
                var outputD = ampD.Output.Take();

                var ampE = new IntCodeComputer();
                ampE.Run(programCopy, item[4], outputD);
                var outputE = ampE.Output.Take();

                largestValue = Math.Max(largestValue, outputE);
            }
            sw.Stop();

            Console.WriteLine($"\t Answer: {largestValue}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        private static List<int[]> CreatePhaseSettingSequences(int[] phaseSettings)
        {            
            var combinations = Combine(phaseSettings);
            return combinations;
        }

        private static List<int[]> Combine(IEnumerable<int> phaseSettings, int[] sequence = null, List<int[]> results = null)
        {   
            if (phaseSettings.Count() == 5)
            {
                sequence = new int[5];
                results = new List<int[]>(120);
            }

            for (int i = 0; i < phaseSettings.Count(); i++)
            {   
                sequence[5 - phaseSettings.Count()] = phaseSettings.ElementAt(i);

                if (phaseSettings.Count() == 1)
                {
                    int[] result = new int[5];
                    sequence.CopyTo(result, 0);
                    results.Add(result);
                }
                Combine(phaseSettings.Except(new [] {phaseSettings.ElementAt(i)}), sequence, results);    
            }

            return results;
        }

        public static void Part2(int[] program)
        {
            Console.WriteLine("PART 2");
            var sw = new Stopwatch();
            sw.Start();

            var programCopy = new int[program.Length];
            Array.Copy(program, programCopy, program.Length);

            var  phaseSettingSequences = CreatePhaseSettingSequences(new [] {5,6,7,8,9}); 

            var largestValue = int.MinValue;

            foreach (var phases in phaseSettingSequences)
            {
                var inputA = new BlockingCollection<int>();
                var outputA = new BlockingCollection<int>();
                var ampA = Task.Run(() =>
                    {
                        inputA.Add(0);
                        var computerA = new IntCodeComputer(inputA, outputA);
                        computerA.Run(programCopy, phases[0]);
                    }
                );

                var outputB = new BlockingCollection<int>();
                var ampB = Task.Run(() =>
                    {                    
                        var computerB = new IntCodeComputer(outputA, outputB);
                        computerB.Run(programCopy, phases[1]);
                    }
                );

                var outputC = new BlockingCollection<int>();
                var ampC = Task.Run(() =>
                    {
                        var computerC = new IntCodeComputer(outputB, outputC);
                        computerC.Run(programCopy, phases[2]);
                    }
                );

                var outputD = new BlockingCollection<int>();
                var ampD = Task.Run(() =>
                    {  
                        var computerD = new IntCodeComputer(outputC, outputD);
                        computerD.Run(programCopy, phases[3]);
                    }   
                );

                var ampE = Task.Run(() =>
                    {
                        var computerE = new IntCodeComputer(outputD, inputA);
                        computerE.Run(programCopy, phases[4]);
                    }
                );

                Task.WaitAll(ampA, ampB, ampC, ampD, ampE);

                largestValue = Math.Max(largestValue, inputA.Take());
            }

            sw.Stop();
            Console.WriteLine($"\t Answer: {largestValue}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }
    }
}