using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC19
{
    internal class Day3
    {
        public static void Part1(string[] input1, string[] input2)
        {
            var sw = new Stopwatch();
            sw.Start();

            var grid = new Dictionary<Point, List<Point>>();
            var allPoints = GetPoints(input1, 1).Concat(GetPoints(input2, 2));

            foreach (var item in allPoints)
            {
                if (!grid.ContainsKey(item)) {
                    grid[item] = new List<Point>();
                }
                grid[item].Add(item);
            }

            int shortestDistance = int.MaxValue;
            foreach (var item in grid)
            {
                if(item.Value.Count() > 1) 
                {
                    if (item.Value.Any(p => p.WireNumber == 1) && item.Value.Any(p => p.WireNumber == 2))
                    {
                        var wire1 = item.Value.Where(p => p.WireNumber == 1).First().Distance();
                        shortestDistance = Math.Min(shortestDistance, wire1);
                    }
                }
            }

            sw.Stop();

            Console.WriteLine("PART 1");
            Console.WriteLine($"\t Answer: {shortestDistance}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2(string[] input1, string[] input2)
        {
            var sw = new Stopwatch();
            sw.Start();

            var grid = new Dictionary<Point, List<Point>>();
            var allPoints = GetPoints(input1, 1).Concat(GetPoints(input2, 2));

            foreach (var item in allPoints)
            {
                if (!grid.ContainsKey(item)) {
                    grid[item] = new List<Point>();
                }
                grid[item].Add(item);
            }
            
            int fewestSteps = int.MaxValue;

            foreach (var item in grid)
            {
                if(item.Value.Count() > 1) {
                    if (item.Value.Any(p => p.WireNumber == 1) && item.Value.Any(p => p.WireNumber == 2))
                    {
                        var wire1 = item.Value.Where(p => p.WireNumber == 1).Min(p => p.Step);
                        var wire2 = item.Value.Where(p => p.WireNumber == 2).Min(p => p.Step);

                        fewestSteps = Math.Min(fewestSteps, wire1+wire2);
                    }
                }
            }

            sw.Stop();

            Console.WriteLine("PART 2");
            Console.WriteLine($"\t Answer: {fewestSteps}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        private static Point[] GetPoints(string[] input, int wireNumber)
        {
            var result = new List<Point>();

            int x = 0;
            int y = 0;
            int step = 0;

            foreach (var item in input)
            {
                char direction = item[0];
                int length = int.Parse(item.Substring(1));

                for (int i = 0; i < length; i++)
                {
                    if (direction == 'U')
                    {  
                        y++;
                    }
                    else if (direction == 'D')
                    {
                        y--;
                    }
                    else if (direction == 'R') {
                        x++;
                    }
                    else if (direction == 'L') {
                        x--;
                    }

                    result.Add(new Point(x, y, wireNumber, ++step));
                }
            }

            return result.ToArray();
        }

        public struct Point
        {
            public Point(int x, int y, int wireNumber, int step)
            {
                X = x;
                Y = y;
                WireNumber = wireNumber;
                Step = step;
            }

            public int X {get; set;}
            public int Y { get; set;}
            public int WireNumber { get; set; }
            public int Step { get; set; }

            public int Distance()
            {
                return Math.Abs(X)+Math.Abs(Y);
            }

            public override bool Equals(object obj)
            {
                return obj is Point point &&
                       X == point.X &&
                       Y == point.Y;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(X, Y);
            }
        }
    }
}