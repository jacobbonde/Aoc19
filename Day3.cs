using System;
using System.Collections.Generic;

namespace AoC19
{
    internal class Day3
    {
        public static void Part1(string[] input1, string[] input2)
        {
            var layout1 = GetPoints(input1);
            var layout2 = GetPoints(input2);

            Point intersection = new Point{X =1000, Y=1000};

            foreach (var item1 in layout1)
            {
                foreach (var item2 in layout2)
                {
                    if (IsSame(item1,item2))
                    {
                        intersection = intersection.Distance() <= item1.Distance() ? intersection : item1;
                    }
                } 
            }

            Console.WriteLine("PART 1");
            Console.WriteLine($"\t Answer: {intersection.Distance()}");
        }

        private static bool IsSame(Point item1, Point item2)
        {
            return item1.X == item2.X && item1.Y == item2.Y;
        }

        private static Point[] GetPoints(string[] input1)
        {
            var result = new List<Point>(input1.Length);

            int x = 1;
            int y = 1;
            foreach (var item in input1)
            {
                char direction = item[0];
                int lenght = int.Parse(item.Substring(1));
                if (direction == 'U')
                {  
                    y += lenght;
                }
                if (direction == 'D')
                {
                    y -= lenght;
                }
                if (direction == 'L') {
                    x -= lenght;
                }
                if (direction == 'R') {
                    x += lenght;
                }
                result.Add(new Point {  X = x, Y = y});
            }

            return result.ToArray();
        }

        public struct Point
        {
            public int X {get; set;}
            public int Y { get; set;}

            public int Distance()
            {
                return X-1+Y-1;
            }
        }
        public static void Part2(int[] input)
        {
            Console.WriteLine("PART 2");
            Console.WriteLine($"\t Answer:");
        }
    }
}