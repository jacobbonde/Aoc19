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
            (int, int) bestAsteroid = (-1,-1);
            RelativePosition[] mapRelativeToBestAsteroid = null;
            RelativePosition[] mapVisible = null;
            foreach (var asteroid in map)
            {
                var mapRelativeToAsteroid = map.Where(a => a != asteroid).Select(a => new RelativePosition(asteroid, a)).ToArray();
                var visible = mapRelativeToAsteroid.Distinct(new RelativePosition.LineOfSightAngleComparer()).ToArray();
                if (visible.Length > maxVisible)
                {
                    maxVisible = visible.Length;
                    bestAsteroid = asteroid;
                    mapRelativeToBestAsteroid = mapRelativeToAsteroid;
                    mapVisible = visible;
                }
            }

            var asteroidsByAngle = mapRelativeToBestAsteroid.OrderBy(r => r.angle).GroupBy(a => a.angle);

            var sortedByAngleAndDistance = asteroidsByAngle.ToDictionary(r => r.Key, r => r.OrderBy(a => a.manhattanDistance).ToList());

            int killCount = 0;
            RelativePosition kill200 = null;
            foreach (var angle in sortedByAngleAndDistance)
            {
                
                killCount++;
                if (killCount == 200)
                {
                    kill200 = angle.Value.ElementAt(0);
                    break;
                }
                angle.Value.RemoveAt(0);
            }

            sw.Stop();

            Console.WriteLine($"\t Answer: {maxVisible}");
            Console.WriteLine($"\t Asteroid: {bestAsteroid}");
            Console.WriteLine($"\t Kill 200: {kill200.to}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2(string[] input)
        {
            Console.WriteLine("PART 2");
            var sw = new Stopwatch();
            sw.Start();

            // var map = new List<(int,int)>();
            // for (int i = 0; i < input.Length; i++)
            // {
            //     for (int j = 0; j < input[i].Length; j++)
            //     {
            //         if (input[i][j] == '#')
            //         {
            //             map.Add((j,i));
            //         }
            //     }
            // }

            // foreach (var asteroid in map)
            // {
            //     var reldir = map.Where(a => a != asteroid).Select(a => new RelativePosition(asteroid, a)).ToArray();
            // }

            sw.Stop();
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        class RelativePosition
        {
            private Cardinal cardinal;
            private int horizontalSteps;
            private int verticalSteps;
            public int manhattanDistance;
            private int quotient;
            private int remainder;

            public double angle;
            public readonly (int, int) from;
            public readonly (int, int) to;

            public RelativePosition((int, int) from, (int, int) to)
            {
                this.to = to;
                this.from = from;

                horizontalSteps = to.Item1 - from.Item1;
                verticalSteps = to.Item2 - from.Item2;
                manhattanDistance = Math.Abs(horizontalSteps) + Math.Abs(verticalSteps);

                double rawAngleInverted = -Math.Atan2(verticalSteps, horizontalSteps);
                double absoluteAngle = rawAngleInverted >= 0 ? rawAngleInverted : Math.PI * 2 + rawAngleInverted;
                double clockwiseAbsoluteAngle = (Math.PI * 2 - absoluteAngle);
                double shiftedQuarterTurn = (clockwiseAbsoluteAngle + Math.PI / 2);
                double normalizedAngle = shiftedQuarterTurn % (Math.PI * 2);
                angle = normalizedAngle;
                //angle = Math.Atan2(verticalSteps, horizontalSteps);

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

            public class LineOfSightComparer : IEqualityComparer<RelativePosition>
            {
                public bool Equals(RelativePosition x, RelativePosition y)
                {
                    return 
                                        
                        x.cardinal == y.cardinal &&
                            (x.cardinal == Cardinal.N ||
                            x.cardinal == Cardinal.S ||
                            x.cardinal == Cardinal.W ||
                            x.cardinal == Cardinal.E) ||
                        (x.quotient == y.quotient &&
                        x.remainder == y.remainder);
                }

                public int GetHashCode(RelativePosition obj)
                {

                    if (obj.cardinal == Cardinal.N ||
                            obj.cardinal == Cardinal.S ||
                            obj.cardinal == Cardinal.W ||
                            obj.cardinal == Cardinal.E)
                            {
                    return obj.cardinal.GetHashCode();   }

                return HashCode.Combine(obj.cardinal, obj.quotient, obj.remainder);
                }
            }

            public class LineOfSightAngleComparer : IEqualityComparer<RelativePosition>
            {
                public bool Equals(RelativePosition x, RelativePosition y)
                {
                    return x.angle.Equals(y.angle);
                }

                public int GetHashCode(RelativePosition obj)
                {
                    return obj.angle.GetHashCode();
                }
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