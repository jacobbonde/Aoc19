using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace AoC19
{
    internal class Day3C
    {
        public static void Part1(string[] inputWire1, string[] inputWire2)
        {
            var sw = new Stopwatch();
            sw.Start();
            var wire1 = GetWireSegments(inputWire1);
            var wire2 = GetWireSegments(inputWire2);
            
            int shortestDistance = int.MaxValue;
            int fewestSteps = int.MaxValue;

            foreach (var segment1 in wire1)
            {
                foreach (var segment2 in wire2)
                {
                    var (intersects, intersection) = segment1.IntersectsWith(segment2);                    
                    if (intersects) 
                    {   
                        // Part 1
                        var manhattan = Math.Abs(intersection.Point.X)+Math.Abs(intersection.Point.Y);
                        shortestDistance = Math.Min(shortestDistance, manhattan);

                        // Part 2
                        fewestSteps = Math.Min(fewestSteps, intersection.CombinedSteps());
                    }
                }
            }

            sw.Stop();

            Console.WriteLine("PART 1");
            Console.WriteLine($"\t Shortest distance: {shortestDistance}");
            Console.WriteLine($"\t Fewest steps: {fewestSteps}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2(string[] inputWire1, string[] inputWire2)
        {
            var sw = new Stopwatch();
            sw.Start();

           var wire1 = GetWireSegments(inputWire1);
           var wire2 = GetWireSegments(inputWire2);
            
            int fewestSteps = int.MaxValue;

            foreach (var segment1 in wire1)
            {
                foreach (var segment2 in wire2)
                {
                    var (intersects, intersection) = segment1.IntersectsWith(segment2);
                    if (intersects) 
                    {
                        fewestSteps = Math.Min(fewestSteps, intersection.CombinedSteps());
                    }
                }
            }

            sw.Stop();

            Console.WriteLine("PART 2");
            Console.WriteLine($"\t Answer: {fewestSteps}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        private static WireSegment[] GetWireSegments(string[] input)
        {
            var result = new List<WireSegment>();

            int x = 0;
            int y = 0;

            int lengthOfPreviousSegments = 0;

            foreach (var item in input)
            {
                var origin = new Point(x, y);

                char direction = item[0];
                int length = int.Parse(item.Substring(1));
                
                if (direction == 'U')
                {  
                    y += length;
                }
                else if (direction == 'D')
                {
                    y -= length;
                }
                else if (direction == 'R') 
                {
                    x += length;
                }
                else if (direction == 'L') 
                {
                    x -= length;
                }

                result.Add(new WireSegment(origin, new Point(x, y), lengthOfPreviousSegments));
                lengthOfPreviousSegments += length;
            }

            return result.ToArray();
        }

        public struct WireSegment
        {
            private readonly int lengthOfPreviousSegments;

            public WireSegment(Point start, Point end, int lengthOfPreviousSegments)
            {
                Start = start;
                End = end;
                this.lengthOfPreviousSegments = lengthOfPreviousSegments;
            }

            public Point Start { get; }
            public Point End { get; }

            public Orientation Orientation 
            {
                get
                {
                    return Start.X == End.X ? Orientation.Vertical : Orientation.Horizontal;
                }
            }

            public int TotalLength => lengthOfPreviousSegments;

            public (bool, Intersection) IntersectsWith(WireSegment other) {
                
                if(this.Orientation == other.Orientation) 
                {
                    return (false, default(Intersection));
                }

                Point port = new Point(0, 0);
                if (this.Start == port && other.Start == port) 
                {
                    return (false, default(Intersection));
                }

                var horizontal = this.Orientation == Orientation.Horizontal ? this : other;
                var vertical = this.Orientation == Orientation.Vertical ? this : other;
                
                return Intersects(horizontal, vertical);
            }

            private (bool, Intersection) Intersects(WireSegment horizontal, WireSegment vertical)
            {
                var smallHorizontalX = Math.Min(horizontal.Start.X, horizontal.End.X);
                var largeHorizontalX = Math.Max(horizontal.Start.X, horizontal.End.X);

                var verticalX = vertical.Start.X;

                if (verticalX >= smallHorizontalX && verticalX <= largeHorizontalX)
                {
                    var largeVerticalY = Math.Max(vertical.Start.Y, vertical.End.Y);
                    var smallVerticalY = Math.Min(vertical.Start.Y, vertical.End.Y);

                    var horizontalY = horizontal.Start.Y;

                    if (horizontalY >= smallVerticalY && horizontalY <= largeVerticalY)
                    {
                        return (true,new Intersection(horizontal, vertical,  new Point(verticalX, horizontalY)));
                    }
                }

                return (false, default(Intersection));
            }
        }
        public enum Orientation
        {
            Horizontal,
            Vertical
        }

        public struct Intersection {
            private readonly WireSegment horizontal;
            private readonly WireSegment vertical;

            public Intersection(WireSegment horizontal, WireSegment vertical, Point point)
            {
                this.horizontal = horizontal;
                this.vertical = vertical;
                Point = point;
            }

            public Point Point {get; set;}

            public int CombinedSteps() 
            {
                return horizontal.TotalLength + Math.Abs(horizontal.Start.X - Point.X) + vertical.TotalLength + Math.Abs(vertical.Start.Y - Point.Y);
            }
        }
    }
}