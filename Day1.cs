using System;

namespace AoC19
{
    public class Day1
    {
        public static void Part1(int[] masses)
        {
            int result = 0;

            foreach (var mass in masses)
            {   
                result += GetFuel(mass);
            }

            Console.WriteLine($"PART 1");
            Console.WriteLine($"\t Fuel need: {result}");
        }
        public static void Part2(int[] masses)
        {
            int result = 0;

            foreach (var mass in masses)
            {   
                result += GetFuelRecursively(mass);
            }

            Console.WriteLine($"PART 2");
            Console.WriteLine($"\t Fuel need: {result}");
        }

        private static int GetFuel(int mass)
        {
            return mass/3-2;
        }

        private static int GetFuelRecursively(int mass)
        {
            int fuel = mass/3-2;

            if(fuel > 0) {
                return fuel + GetFuel(fuel);
            }
            else
            {
                return 0;
            }
        }
    }
}