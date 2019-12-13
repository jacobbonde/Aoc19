using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AoC19
{
    internal class Day11
    {
        public static void Part1(long[] program)
        {
            Console.WriteLine("PART 1");
            var sw = new Stopwatch();
            sw.Start();

            var programCopy = new long[program.Length];
            Array.Copy(program, programCopy, program.Length);

            var input = new BlockingCollection<long>();
            var output = new BlockingCollection<long>();

            var computer = new IntCodeComputer(input, output);

            var task = Task.Run(() => computer.Run(programCopy,1));

            var painted = new Dictionary<(int,int), int>();
            var panel = (0,0);

            var facing = 0;

            while (true)
            {
                Console.WriteLine($"On panel {panel}");
                int panelColor = 0;
                if (painted.ContainsKey((panel)))
                {
                    panelColor = painted[panel];
                }
                input.Add(panelColor);
                long newColor;
                if(output.TryTake(out newColor, TimeSpan.FromMilliseconds(1000)))
                {
                    painted[panel] = (int)newColor;

                    long direction;
                    if(output.TryTake(out direction, TimeSpan.FromMilliseconds(1000)))
                    {
                        facing = (facing + (int)(direction == 1 ? 1 : 3)) % 4;
                        switch (facing)
                        {
                            case 0:
                                panel = (panel.Item1, panel.Item2+1);
                                break;
                            case 1:
                                panel = (panel.Item1+1, panel.Item2);
                                break;
                            case 2:
                                panel = (panel.Item1, panel.Item2-1);
                                break;
                            case 3:
                                panel = (panel.Item1-1, panel.Item2);
                                break;
                            default:
                                throw new Exception("Not a valid direction");
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            var minX = painted.Keys.Min(k => k.Item1);
            var maxX = painted.Keys.Max(k => k.Item1);
            var shiftX = 0 + minX;
            var minY = painted.Keys.Min(k => k.Item2);
            var maxY = painted.Keys.Max(k => k.Item2);
            var shiftY = 0 + minY;

            var width = maxX - minX + 1;
            var height = maxY - minY + 1;
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    (int x, int y) key = (x+shiftX, y+shiftY);
                    if (painted.ContainsKey(key))
                    {
                        Console.Write(painted[key] == 0 ? ' ' : 'X');
                    }
                    else
                    {
                        Console.Write(' ');
                    }

                    if(x % width == 0)
                    {
                        Console.WriteLine();
                    }
                }
            }



            task.Wait();

            sw.Stop();

            Console.WriteLine($"\t Answer: {painted.Count()}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2(long[] program)
        {
            Console.WriteLine("PART 2");
            var sw = new Stopwatch();
            sw.Start();

            var programCopy = new long[program.Length];
            Array.Copy(program, programCopy, program.Length);

            var computer = new IntCodeComputer();
            computer.Run(programCopy, 2);

            sw.Stop();
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }
    }
}