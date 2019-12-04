using System;
using System.Diagnostics;

namespace AoC19
{
    internal class Day4
    {
        public static void Part1()
        {
            var sw = new Stopwatch();
            sw.Start();
            int begin = 156218;
            // int end = begin + 1000;
            int end = 652527; 
            int pwCounter = 0;
            for (int i = begin; i < end; i++)
            {
                 string pw = i.ToString();
                // string pw = "123788";
                char prev = '0';
                bool isPair = false;
                bool isIncreasingOrEven = true;
                for (int j = 1; j < pw.Length; j++)
                {
                    prev = pw[j-1];
                    char current = pw[j];
                    
                    if (!isPair)
                    {
                        isPair = prev == current;    
                    }
                    if (isIncreasingOrEven)
                    {
                        isIncreasingOrEven = prev <= current;    
                    }                    
                }

                if (isPair && isIncreasingOrEven)
                {
                    pwCounter++;
                    Console.WriteLine(pw);
                }
            }

            sw.Stop();

            Console.WriteLine("PART 1");
            Console.WriteLine($"\t Answer: {pwCounter}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2(string[] input)
        {
            var sw = new Stopwatch();
            sw.Start();


            sw.Stop();

            Console.WriteLine("PART 2");
            Console.WriteLine($"\t Answer:");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }
    }
}