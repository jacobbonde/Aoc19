using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC19
{
    internal class Day10
    {
        public static void Part1(string[] input)
        {
            Console.WriteLine("PART 1");
            var sw = new Stopwatch();
            sw.Start();

            var map = new List<(int,int)>();
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '#')
                    {
                        map.Add((j,i));
                    }
                }
            }

            int maxVisible = int.MinValue;
            foreach (var asteroid in map)
            {
                // var reldir = map.Where(a => a != asteroid).Select(a => new Direction(asteroid, a)).ToArray();
                // var visible = reldir.Distinct().ToArray();
                // maxVisible = Math.Max(maxVisible, visible.Length);
            }
                

            sw.Stop();

            Console.WriteLine($"\t Answer: {maxVisible}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2(long[] program)
        {
            Console.WriteLine("PART 2");
            var sw = new Stopwatch();
            sw.Start();

            

            sw.Stop();
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        class Direction
        {
            private Cardinal cardinal;
            private int horizontalSteps;
            private int verticalSteps;
            private int quotient;
            private int remainder;

            private double angle;

            public Direction((int, int) from, (int, int) to)
            {
                horizontalSteps = to.Item1 - from.Item1;
                verticalSteps = to.Item2 - from.Item2;

                angle = Math.Atan2(verticalSteps, horizontalSteps);

                if (horizontalSteps == 0)
                {
                    quotient = 0;
                    remainder = 0;

                    if (verticalSteps > 0)
                    {
                        cardinal = Cardinal.S;
                    }
                    else if (verticalSteps < 0)
                    {
                        cardinal = Cardinal.N;
                    }
                }
                else if (verticalSteps == 0)
                {
                    quotient = 0;
                    remainder = 0;

                    if (horizontalSteps > 0)
                    {
                        cardinal = Cardinal.E;
                    }
                    else if (horizontalSteps < 0)
                    {
                        cardinal = Cardinal.W;
                    }
                }
                else if (horizontalSteps > 0)
                {
                    quotient = horizontalSteps / verticalSteps;
                    remainder = horizontalSteps % verticalSteps;

                    if (verticalSteps > 0)
                    {
                        cardinal = Cardinal.SE;
                    }
                    else if (verticalSteps < 0)
                    {
                        cardinal = Cardinal.NE;
                    }
                }
                else if (horizontalSteps < 0)
                {
                    quotient = horizontalSteps / verticalSteps;
                    remainder = horizontalSteps % verticalSteps;

                    if (verticalSteps > 0)
                    {
                        cardinal = Cardinal.SW;
                    }
                    else if (verticalSteps < 0)
                    {
                        cardinal = Cardinal.NW;
                    }
                }
            }

            public override bool Equals(object obj)
            {
                return obj is Direction direction &&
                    // (                    
                    //     cardinal == direction.cardinal &&
                    //         (cardinal == Cardinal.N ||
                    //         cardinal == Cardinal.S ||
                    //         cardinal == Cardinal.W ||
                    //         cardinal == Cardinal.E) ||
                    //     quotient == direction.quotient &&
                    //     remainder == direction.remainder);
                    angle == direction.angle;
            }

            public override int GetHashCode()
            {
                return angle.GetHashCode();

                // if (cardinal == Cardinal.N ||
                //             cardinal == Cardinal.S ||
                //             cardinal == Cardinal.W ||
                //             cardinal == Cardinal.E)
                // {
                //     return cardinal.GetHashCode();   
                // }

                // return HashCode.Combine(cardinal, quotient, remainder);
            }

            enum Cardinal
            {
                None,
                N,
                NE,
                E,
                SE,
                S,
                SW,
                W,
                NW
            }
        }
    }
}