using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC19
{
    internal class Day3
    {
        public static void Part1(string[] input1, string[] input2)
        {
            var layout1 = GetPoints(input1);
            int maxX1 = layout1.Max(p => p.X);
            int maxY1 = layout1.Max(p => p.Y);
            int minX1 = layout1.Min(p => p.X);
            int minY1 = layout1.Min(p => p.Y);
            
            Console.WriteLine($"Points 1 {layout1.Length}");
            var layout2 = GetPoints(input2);
            int maxX2 = layout2.Max(p => p.X);
            int maxY2 = layout2.Max(p => p.Y);
            int minX2 = layout2.Min(p => p.X);
            int minY2 = layout2.Min(p => p.Y);


            Console.WriteLine($"Points 2 {layout2.Length}");

            Point intersection = new Point{X =int.MaxValue, Y=int.MaxValue};

            int dist = 0;
            for (int i = 0; i < int.MaxValue; i++)
            {
                var from1 = layout1.Where(p => p.Distance() == i);
                var from2 = layout2.Where(p => p.Distance() == i);
                if (from1.Any(p1 => from2.Any(p2 => IsSame(p1, p2))))
                {
                    dist = i;
                    break;
                }
            }
            

            // foreach (var item1 in layout1)
            // {
            //     foreach (var item2 in layout2)
            //     {
            //         if (IsSame(item1,item2))
            //         {
            //             intersection = intersection.Distance() <= item1.Distance() ? intersection : item1;
            //         }
            //     } 
            // }

            Console.WriteLine("PART 1");
            Console.WriteLine($"\t Answer: {dist}");
        }

        private static bool IsSame(Point item1, Point item2)
        {
            return item1.X == item2.X && item1.Y == item2.Y;
        }

        private static Point[] GetPoints(string[] input1)
        {
            var result = new List<Point>();

            int x = 0;
            int y = 0;

            foreach (var item in input1)
            {
                char direction = item[0];
                int length = int.Parse(item.Substring(1));

                for (int i = 0; i < length; i++)
                {
                    if (direction == 'U')
                    {  
                        y++;
                    }
                    if (direction == 'D')
                    {
                        y--;
                    }
                    if (direction == 'R') {
                        x++;
                    }
                    if (direction == 'L') {
                        x--;
                    }

                    result.Add(new Point {  X = x, Y = y});

                }
                
            }

            return result.ToArray();
        }

        public struct Point
        {
            public int X {get; set;}
            public int Y { get; set;}

            public int Distance()
            {
                return Math.Abs(X)+Math.Abs(Y);
            }
        }
        public static void Part2(int[] input)
        {
            Console.WriteLine("PART 2");
            Console.WriteLine($"\t Answer:");
        }
    }
}