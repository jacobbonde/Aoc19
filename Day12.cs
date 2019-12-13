using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC19
{
    internal class Day12
    {
        public static void Part1(string[] input)
        {
            Console.WriteLine("PART 1");
            var sw = new Stopwatch();
            sw.Start();

            var step0 = new List<Moon>();
            foreach (var line in input)
            {
                Match match = Regex.Match(line, @"\d+");

                int x = int.Parse(match.Value);
                match = match.NextMatch();
                int y = int.Parse(match.Value);
                match = match.NextMatch();
                int z = int.Parse(match.Value);
                var initialPosition = new Moon(x, y, z);
                step0.Add(initialPosition);
            }

            var steps = new List<List<Moon>>();
            var currentStep = step0;
            steps.Add(currentStep);

            for (int i = 0; i < 999; i++)
            {
                currentStep = UpdateVelocity(currentStep);
            }


            sw.Stop();

            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        private static List<Moon> UpdateVelocity(List<Moon> currentStep)
        {
            var nextStep = new List<Moon>();

            foreach (var moon in currentStep)
            {
                int velX = currentStep.Count(m => moon.Velocity[0] > m.Velocity[0]) - currentStep.Count(m => moon.Velocity[0] > m.Velocity[0]);
                int velY = currentStep.Count(m => moon.Velocity[1] > m.Velocity[1]) - currentStep.Count(m => moon.Velocity[1] > m.Velocity[1]);
                int velZ = currentStep.Count(m => moon.Velocity[2] > m.Velocity[2]) - currentStep.Count(m => moon.Velocity[2] > m.Velocity[2]);

                moon.Velocity = new[] { velX, velY, velZ };
                nextStep.Add()
            }
        }

        public static void Part2(long[] program)
        {
            Console.WriteLine("PART 2");
            var sw = new Stopwatch();
            sw.Start();

            

            sw.Stop();
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        class Moon
        {
            public int[] Position;
            public int[] Velocity;

            public Moon(int x, int y, int z)
            {
                Position = new[] {x, y, z};
                Velocity = new[] {0, 0, 0};
            }
        }
    }
}