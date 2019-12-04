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
            // int begin = 156218;
            // int end = 652527; 
            int pwCounter = CountPossiblePasswords(currentGroupCount => currentGroupCount >= 2);

            sw.Stop();

            Console.WriteLine("PART 1");
            Console.WriteLine($"\t Answer: {pwCounter}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2()
        {
            var sw = new Stopwatch();
            sw.Start();
            
            int pwCounter = CountPossiblePasswords(currentGroupCount => currentGroupCount == 2);

            sw.Stop();

            Console.WriteLine("PART 2");
            Console.WriteLine($"\t Answer: {pwCounter}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }


        public static int CountPossiblePasswords(Predicate<int> isValidPair)
        {
            var sw = new Stopwatch();
            sw.Start();
            int begin = 156218;
            int end = 652527; 
            int pwCounter = 0;
            for (int i = begin; i < end; i++)
            {
                string pw = i.ToString();
                char prev = '0';
                bool foundPair = false;
                bool isIncreasingOrEven = true;
                int currentGroupCount = 1;
                for (int j = 1; j < pw.Length; j++)
                {
                    prev = pw[j-1];
                    char current = pw[j];
                    
                    if (current < prev)
                    {
                        isIncreasingOrEven = false;
                        break;
                    }
                    else if (current == prev)
                    {
                        currentGroupCount++;

                        if (j == pw.Length - 1 && isValidPair(currentGroupCount))
                        {
                            foundPair = true;
                        }
                    }
                    else
                    {
                        if (isValidPair(currentGroupCount))
                        {
                            foundPair = true;
                        }
                        currentGroupCount = 1;
                    }
                }

                if (foundPair && isIncreasingOrEven)
                {
                    pwCounter++;
                }
            }
            return pwCounter;
        }
    }
}