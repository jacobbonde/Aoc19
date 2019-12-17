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

            var moons = new Moon[4];
            int index = 0;
            foreach (var line in input)
            {
                Match match = Regex.Match(line, @"-?\d+");

                int x = int.Parse(match.Value);
                match = match.NextMatch();
                int y = int.Parse(match.Value);
                match = match.NextMatch();
                int z = int.Parse(match.Value);
                var moon = new Moon(x, y, z);
                moons[index++] = moon;
            }

            for (int i = 0; i < 1000; i++)
            {                
                ApplyGravity(moons);
                ApplyVelocity(moons);
            }

            var totalEnergy = moons.Sum(m => m.Energy());
            sw.Stop();

            Console.WriteLine($"\t Total energy: {totalEnergy}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }

        public static void Part2(string[] input)
        {
            Console.WriteLine("PART 2");
            var sw = new Stopwatch();
            sw.Start();

            var moons = new Moon[4];
            Moon initialPostion = null;
            int index = 0;
            foreach (var line in input)
            {
                Match match = Regex.Match(line, @"-?\d+");

                int x = int.Parse(match.Value);
                match = match.NextMatch();
                int y = int.Parse(match.Value);
                match = match.NextMatch();
                int z = int.Parse(match.Value);
                initialPostion = new Moon(x, y, z);
                moons[index++] = initialPostion;
            }

            int counter = 0;
            while (true)
            {                
                ApplyBoth(moons);

                counter++;

                // if (counter % 1_000_000 == 0)
                // {
                //     Console.WriteLine($"Counter: {counter} in {sw.ElapsedMilliseconds}");
                // }

                if (BackToOriginalPosition(moons))
                {
                    break;   
                }
            }

            sw.Stop();
            Console.WriteLine($"\t Counter: {counter}");
            Console.WriteLine($"\t Time: {sw.ElapsedMilliseconds}");
        }
        private static void ApplyGravity(Moon[] moons)
        {
            foreach (var moon in moons)
            {
                moon.VelocityX = moon.VelocityX + moons.Count(m => moon.Position[0] < m.Position[0]) - moons.Count(m => moon.Position[0] > m.Position[0]);
                moon.VelocityY = moon.VelocityY + moons.Count(m => moon.Position[1] < m.Position[1]) - moons.Count(m => moon.Position[1] > m.Position[1]);
                moon.VelocityZ = moon.VelocityZ + moons.Count(m => moon.Position[2] < m.Position[2]) - moons.Count(m => moon.Position[2] > m.Position[2]);
            }
        }

        private static void ApplyVelocity(Moon[] moons)
        {
            foreach (var moon in moons)
            {
                int newX = moon.Position[0] + moon.VelocityX;
                int newY = moon.Position[1] + moon.VelocityY;
                int newZ = moon.Position[2] + moon.VelocityZ;

                moon.Position = new[] { newX, newY, newZ };
            }
        }

        private static void ApplyBoth(Moon[] moons)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = i+1; j < 4; j++)
                {
                    int moonIposX = moons[i].Position[0];
                    int moonJposX = moons[j].Position[0];
                    if (moonIposX > moonJposX)
                    { 
                        moons[i].Position[0]--;
                        moons[j].Position[0]++;
                    }
                    else if(moonIposX < moonJposX)
                    {
                        moons[i].Position[0]++;
                        moons[j].Position[0]--;
                    }

                    int moonIposY = moons[i].Position[1];
                    int moonJposY = moons[j].Position[1];
                    if (moonIposY > moonJposY)
                    { 
                        moons[i].Position[1]--;
                        moons[j].Position[1]++;
                    }
                    else if(moonIposY < moonJposY)
                    {
                        moons[i].Position[1]++;
                        moons[j].Position[1]--;
                    }


                    int moonIposZ = moons[i].Position[2];
                    int moonJposZ = moons[j].Position[2];
                    if (moonIposZ > moonJposZ)
                    { 
                        moons[i].Position[2]--;
                        moons[j].Position[2]++;
                    }
                    else if(moonIposZ < moonJposZ)
                    {
                        moons[i].Position[2]++;
                        moons[j].Position[2]--;
                    }
                }
            }
        }

        private static void ApplyBoth2(Moon[] moons)
        {
            for (int i = 0; i < 3; i++)
            {
                Moon[] ordered = moons.OrderBy(m => m.Position[i]).ToArray();

                for (int j = 0; j < 3; j++)
                {
                    if (ordered[j].Position[i] == ordered[j+1].Position[i])
                    {
                        continue;
                    }

                    ordered[j].Position[i] += 3-j;
                }
            }
        }

        private static bool BackToOriginalPosition(Moon[] moons)
        {
            foreach (var moon in moons)
            {                
                var initial = moon.InitialPosition;
                var current = moon.Position;
                if (initial[0] != current[0] ||
                    initial[1] != current[1] ||
                    initial[2] != current[2] ||
                    moon.VelocityX != 0 ||
                    moon.VelocityY != 0 ||
                    moon.VelocityZ != 0)
                {
                    return false;
                }
            }

            return true;
        }

        class Moon
        {
            public int VelocityX;
            public int VelocityY;
            public int VelocityZ;

            public int[] InitialPosition;
            public int[] Position;

            public Moon(int x, int y, int z)
            {
                InitialPosition = new[] {x, y, z};
                Position = new[] {x, y, z};
                VelocityX = 0;
                VelocityY = 0;
                VelocityZ = 0;
            }

            internal int Energy()
            {
                int potentialEnergy = Position.Sum(v => Math.Abs(v));
                int kineticEnergy = Math.Abs(VelocityX) +Math.Abs(VelocityY) +Math.Abs(VelocityZ);
                return potentialEnergy * kineticEnergy; 
            }
        }
    }
}