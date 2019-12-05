using System;

namespace AoC19.Day5
{
    public class IntCodeComputerDay5
    {
        private static int Input;
        private static int Output;

        public static (int, int[]) Run(int[] program, int input)
        {
            Input = input;

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

           // Console.WriteLine($"Output: {Output}");
           return (memory[0], memory);
        }

        private static Instruction GetInstruction(int[] memory, int instructionPointer)
        {
            int opCode = memory[instructionPointer];

            var intructionCode = opCode % 100;

            switch (intructionCode)
                {
                    case  1:
                        return new Add(memory, instructionPointer, opCode);
                    case 2:
                        return new Multiply(memory, instructionPointer, opCode);
                    case 3:
                        return new Store(memory, instructionPointer, opCode);
                    case 4:
                        return new Retrieve(memory, instructionPointer, opCode);
                    case 99:
                        return new Halt();
                    default:
                    throw new InvalidOperationException();
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
        private readonly int paramMode1;
        private readonly int paramMode2;

            public Add(int[] memory, int instructionPointer, int opCode)
        {
            this.memory = memory;
            this.instructionPointer = instructionPointer;
            
            this.paramMode1 = (opCode / 100) % 10;
            this.paramMode2 = (opCode / 1000) % 10;
        }

        public void Execute() 
        {            
            int param1 = memory[instructionPointer+1];
            int param2 = memory[instructionPointer+2];
            int param3 = memory[instructionPointer+3];

            int addend1 = paramMode1 == 0 ? memory[param1] : param1;
            int addend2 = paramMode2 == 0 ? memory[param2] : param2;

            memory[param3] = addend1 + addend2;
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
        private readonly int paramMode1;
        private readonly int paramMode2;

        public Multiply(int[] memory, int instructionPointer, int opCode)
        {
            this.memory = memory;
            this.instructionPointer = instructionPointer;
            
            this.paramMode1 = (opCode / 100) % 10;
            this.paramMode2 = (opCode / 1000) % 10;
        }

        public void Execute() 
        {
            int param1 = memory[instructionPointer+1];
            int param2 = memory[instructionPointer+2];
            int param3 = memory[instructionPointer+3];

            int factor1 = paramMode1 == 0 ? memory[param1] : param1;
            int factor2 = paramMode2 == 0 ? memory[param2] : param2;

            memory[param3] = factor1 * factor2;
        }

        public int IncreasePointer() 
        {
            return instructionPointer + 4;
        }
    }

    public class Store : Instruction 
    {
        private int[] memory;
        private readonly int instructionPointer;
        private readonly int paramMode1;

        public Store(int[] memory, int instructionPointer, int opCode)
        {
            this.memory = memory;
            this.instructionPointer = instructionPointer;

            this.paramMode1 = (opCode / 100) % 10;
        }

        public void Execute() 
        {
            int param1 = memory[instructionPointer+1];

            int storeAt = paramMode1 == 0 ? memory[param1] : param1;

            memory[storeAt] = Input;
        }

        public int IncreasePointer() 
        {
            return instructionPointer + 2;
        }
    }
    public class Retrieve : Instruction 
    {
        private int[] memory;
        private readonly int instructionPointer;
        private readonly int paramMode1;

        public Retrieve(int[] memory, int instructionPointer, int opCode)
        {
            this.memory = memory;
            this.instructionPointer = instructionPointer;
            this.paramMode1 = (opCode / 100) % 10;        
        }

        public void Execute() 
        {
            int param1 = memory[instructionPointer+1];

            int retrieveFrom = paramMode1 == 0 ? memory[param1] : param1;
            Output = memory[retrieveFrom];
            Console.WriteLine($"Output: {Output}");
        }

        public int IncreasePointer() 
        {
            return instructionPointer + 2;
        }
    }

    public class Halt : Instruction 
    {
        public void Execute() {}

        public int IncreasePointer()
        {
            return int.MaxValue;
        }
    }}
}