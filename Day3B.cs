using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AoC19
{
    internal class Day3B
    {
        public void Run()
        {
            var input = File.ReadAllLines("day3-input.txt");

             var sw = new Stopwatch();
            sw.Start();
            var wireA = ParseWire(input[0]);
            var wireB = ParseWire(input[1]);

            var intersections = wireA.Keys.Intersect(wireB.Keys);
            Console.WriteLine(intersections.Min(x => Manhattan(x.Item1, x.Item2)));
            sw.Stop();
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");

            // Part Two. Had to add 2 since I'm not counting 0, 0 on either wire
            Console.WriteLine(intersections.Min(x => wireA[x] + wireB[x]) + 2); 
        }

        private int Manhattan(int x, int y)
        {
            return Math.Abs(x) + Math.Abs(y);
        }

        private Dictionary<(int, int), int> ParseWire(string input)
        {
            var r = new Dictionary<(int, int), int>();
            int x = 0, y = 0, c = 0;

            foreach (var i in input.Split(','))
            {
                switch (i[0])
                {
                    case 'U':
                        for (int s = 0; s < int.Parse(i.Substring(1)); s++)
                        {
                            r.TryAdd((x, ++y), c++);
                        }
                        break;

                    case 'D':
                        for (int s = 0; s < int.Parse(i.Substring(1)); s++)
                        {
                            r.TryAdd((x, --y), c++);
                        }
                        break;

                    case 'R':
                        for (int s = 0; s < int.Parse(i.Substring(1)); s++)
                        {
                            r.TryAdd((++x, y), c++);
                        }
                        break;

                    case 'L':
                        for (int s = 0; s < int.Parse(i.Substring(1)); s++)
                        {
                            r.TryAdd((--x, y), c++);
                        }
                        break;

                }
            }

            return r;
        }
    }
}