using System;

namespace AoC19
{
    public class Day2
    {
        public static void Part1(int[] program)
        {
            var programCopy = new int[program.Length];
            Array.Copy(program, programCopy, program.Length);

            programCopy[1] = 12;
            programCopy[2] = 2;

            var cpu = new IntCodeComputer();
            var (output,_) = cpu.Run(programCopy,0);

            Console.WriteLine("PART 1");
            Console.WriteLine($"\t Output: {output}");
        }
        public static void Part2(int[] programInput)
        {
            Console.WriteLine("PART 2");

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    var program = new int[programInput.Length];
                    Array.Copy(programInput, program, programInput.Length);
                    
                    program[1] = i;
                    program[2] = j;
                    var computer = new IntCodeComputer();
                    var (output, completedProgram) = computer.Run(program, 0);

                    if (output == 19690720) {
                        Console.WriteLine($"\t Noun: {completedProgram[1]}");
                        Console.WriteLine($"\t Verb: {completedProgram[2]}");
                        Console.WriteLine($"\t Answer: {completedProgram[1]*100 + completedProgram[2]}");

                        // Break both loops
                        i = 101;
                        j = 101;
                    }
                }
            }
        }
    }
}