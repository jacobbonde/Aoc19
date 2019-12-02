using System;

namespace AoC19
{
    public class Day2
    {
        public static void Part1(int[] intCodes)
        {
            bool halt = false;
            for (int i = 0; i < intCodes.Length; i = i + 4)
            {
                if (halt) {
                    break;
                }

                int opCode = intCodes[i];

                switch (opCode)
                {
                    case  1:
                        intCodes[i+3] = intCodes[i+1] + intCodes[i+2];
                        break;
                    case 2:
                        intCodes[i+3] = intCodes[i+1] + intCodes[i+2];
                        break;
                    case 99:
                        halt = true;
                        break;
                    default:
                    throw new InvalidOperationException()
                };
            }

            Console.WriteLine($"QUESTION 1: Fuel need: {result}");
        }
        public static void Part2(int[] masses)
        {
            int result = 0;

            foreach (var mass in masses)
            {   
                result += GetFuelRecursively(mass);
            }

            Console.WriteLine($"QUESTION 2: Fuel need: {result}");
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