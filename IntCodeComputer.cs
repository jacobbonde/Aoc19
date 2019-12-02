using System;

namespace AoC19
{
    public class IntCodeComputer
    {
        public static (int, int[]) Run(int[] programInput)
        {
            var program = new int[programInput.Length];
            Array.Copy(programInput, program, programInput.Length);

            int instructionPointer = 0;
            while(instructionPointer < program.Length)
            {
                var instruction = GetInstruction(program, instructionPointer);
                instruction.Execute();
                instructionPointer = instruction.IncreasePointer();
            }
           return (program[0], program);
        }

        private static Instruction GetInstruction(int[] program, int instructionPointer)
        {
            int opCode = program[instructionPointer];
            switch (opCode)
                {
                    case  1:
                        return new Add(program, instructionPointer);
                    case 2:
                        return new Multiply(program, instructionPointer);
                    case 99:
                        return new Halt(program);
                    default:
                    throw new InvalidOperationException();
                }
        }
    }

    public interface Instruction {
        void Execute();
        int IncreasePointer();
    }

    public class Add : Instruction 
    {
        private int[] program;
        private readonly int instructionPointer;

        public Add(int[] program, int instructionPointer)
        {
            this.program = program;
            this.instructionPointer = instructionPointer;
        }

        public void Execute() 
        {
            int pos1 = program[instructionPointer+1];
            int pos2 = program[instructionPointer+2];
            int pos3 = program[instructionPointer+3];
            program[pos3] = program[pos1] + program[pos2];
        }

        public int IncreasePointer() 
        {
            return instructionPointer + 4;
        }
    }

    public class Multiply : Instruction 
    {
        private int[] program;
        private readonly int instructionPointer;

        public Multiply(int[] program, int instructionPointer)
        {
            this.program = program;
            this.instructionPointer = instructionPointer;
        }

        public void Execute() 
        {
            int pos1 = program[instructionPointer+1];
            int pos2 = program[instructionPointer+2];
            int pos3 = program[instructionPointer+3];
            program[pos3] = program[pos1] * program[pos2];
        }

        public int IncreasePointer() 
        {
            return instructionPointer + 4;
        }
    }

    public class Halt : Instruction 
    {
        private int[] program;

        public Halt(int[] program)
        {
            this.program = program;
        }

        public void Execute() {}

        public int IncreasePointer()
        {
            return this.program.Length;
        }
    }
}