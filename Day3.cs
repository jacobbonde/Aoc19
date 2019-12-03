using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace AoC19
{
    internal class Day3
    {
        public static void Part1(string[] inputWire1, string[] inputWire2)
        {
            var sw = new Stopwatch();
            sw.Start();
            var grid = PlotOnPrunedGrid(inputWire1, inputWire2);

            int shortestDistance = int.MaxValue;
            foreach (var point in grid)
            {
                var wireFragments = point.Value;
                if (Intersects(wireFragments))
                {
                    var distance = wireFragments.Where(p => p.WireNumber == 1).First().Distance();
                    shortestDistance = Math.Min(shortestDistance, distance);
                }
            }

            sw.Stop();

            Console.WriteLine("PART 1");
            Console.WriteLine($"\t Answer: {shortestDistance}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2(string[] inputWire1, string[] inputWire2)
        {
            var sw = new Stopwatch();
            sw.Start();

            var grid = PlotOnPrunedGrid(inputWire1, inputWire2);

            int fewestSteps = int.MaxValue;
            foreach (var point in grid)
            {
                var wireFragments = point.Value;
                if(Intersects(wireFragments)) 
                {
                    var wire1StepNumber = wireFragments.Where(p => p.WireNumber == 1).Min(p => p.StepNumber);
                    var wire2StepNumber = wireFragments.Where(p => p.WireNumber == 2).Min(p => p.StepNumber);

                    fewestSteps = Math.Min(fewestSteps, wire1StepNumber+wire2StepNumber);
                }
            }

            sw.Stop();

            Console.WriteLine("PART 2");
            Console.WriteLine($"\t Answer: {fewestSteps}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        private static bool Intersects(List<WireFragment> wireFragments)
        {
            return wireFragments.Count() > 1 && (wireFragments.Any(p => p.WireNumber == 1) && wireFragments.Any(p => p.WireNumber == 2));
        }

        private static Dictionary<Point, List<WireFragment>> PlotOnPrunedGrid(string[] input1, string[] input2)
        {
            var grid = new Dictionary<Point, List<WireFragment>>();
            var allWireFragments = GetWireFragments(input1, 1).Concat(GetWireFragments(input2, 2));

            foreach (var item in allWireFragments)
            {
                if (!grid.ContainsKey(item.Point))
                {
                    grid[item.Point] = new List<WireFragment>();
                }
                grid[item.Point].Add(item);
            }

            return grid;
        }

        private static WireFragment[] GetWireFragments(string[] input, int wireNumber)
        {
            var result = new List<WireFragment>();

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

                    result.Add(new WireFragment(new Point(x, y), wireNumber, ++step));
                }
            }

            return result.ToArray();
        }

        public struct WireFragment
        {
            public WireFragment(Point point, int wireNumber, int stepNumber)
            {
                Point = point;
                WireNumber = wireNumber;
                StepNumber = stepNumber;
            }

            public Point Point { get; }
            public int WireNumber { get; set; }
            public int StepNumber { get; set; }

            public int Distance()
            {
                return Math.Abs(Point.X)+Math.Abs(Point.Y);
            }

            public override bool Equals(object obj)
            {
                return obj is WireFragment wireFragment &&
                       Point == wireFragment.Point;
            }

            public override int GetHashCode()
            {
                return Point.GetHashCode();
            }
        }
    }
}