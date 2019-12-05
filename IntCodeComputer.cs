using System;

namespace AoC19
{
    public class IntCodeComputer
    {
        public static (int, int[]) Run(int[] program)
        {
            int programLength = program.Length;
            var memory = new int[programLength];
            Array.Copy(program, memory, program.Length);

            int instructionPointer = 0;
            while(instructionPointer < programLength)
            {
                var instruction = GetInstruction(memory, instructionPointer);
                instruction.Execute();
                instructionPointer = instruction.IncreasePointer();
            }
           return (memory[0], memory);
        }

        private static Instruction GetInstruction(int[] memory, int instructionPointer)
        {
            int opCode = memory[instructionPointer];
            switch (opCode)
                {
                    case  1:
                        return new Add(memory, instructionPointer);
                    case 2:
                        return new Multiply(memory, instructionPointer);
                    case 99:
                        return new Halt();
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
        private int[] memory;
        private readonly int instructionPointer;

        public Add(int[] memory, int instructionPointer)
        {
            this.memory = memory;
            this.instructionPointer = instructionPointer;
        }

        public void Execute() 
        {
            int address1 = memory[instructionPointer+1];
            int address2 = memory[instructionPointer+2];
            int address3 = memory[instructionPointer+3];
            memory[address3] = memory[address1] + memory[address2];
        }

        public int IncreasePointer() 
        {
            return instructionPointer + 4;
        }
    }

    public class Multiply : Instruction 
    {
        private int[] memory;
        private readonly int instructionPointer;

        public Multiply(int[] memory, int instructionPointer)
        {
            this.memory = memory;
            this.instructionPointer = instructionPointer;
        }

        public void Execute() 
        {
            int address1 = memory[instructionPointer+1];
            int address2 = memory[instructionPointer+2];
            int address3 = memory[instructionPointer+3];
            memory[address3] = memory[address1] * memory[address2];
        }

        public int IncreasePointer() 
        {
            return instructionPointer + 4;
        }
    }


    public class Halt : Instruction 
    {
        public void Execute() {}

        public int IncreasePointer()
        {
            return int.MaxValue;
        }
    }
}