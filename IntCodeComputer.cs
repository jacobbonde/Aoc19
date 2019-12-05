using System;

namespace AoC19
{
    public class IntCodeComputer
    {
        private static int Input;

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
                    case 5:
                        return new JumpIfTrue(memory, instructionPointer, opCode);
                    case 6:
                        return new JumpIfFalse(memory, instructionPointer, opCode);
                    case 7:
                        return new LessThan(memory, instructionPointer, opCode);
                    case 8:
                        return new EqualTo(memory, instructionPointer, opCode);
                    case 99:
                        return new Halt();
                    default:
                    throw new InvalidOperationException($"Got instruction code {intructionCode} from {opCode} at intruction pointer {instructionPointer}");
                }
        }
    

    public abstract class Instruction {
        protected int[] memory;
        protected int instructionPointer;

        protected int[] parameters;
        protected int[] parameterModes;
        public Instruction(int[] memory, int instructionPointer, int opCode, int numberOfParameters)
        {
            this.memory = memory;
            this.instructionPointer = instructionPointer;

            this.parameterModes = new[] 
            {
                (opCode / 100) % 10,
                (opCode / 1000) % 10,
                (opCode / 10000)
            };

            this.SetParameters(numberOfParameters);
        }
        public abstract void Execute();
        public virtual int IncreasePointer()
        {
            return instructionPointer + parameters.Length + 1;
        }

        protected int ValueFromParameter(int parameterNumber) 
        {
            int index = parameterNumber-1;
            int parameter = parameters[index];
            return parameterModes[index] == 0 ? memory[parameter] : parameter;
        }
        protected int RawParameter(int parameterNumber) 
        {
            return parameters[parameterNumber - 1];
        }
        private void SetParameters(int numberOfParameters)
        {
            this.parameters = new int[numberOfParameters];

            for (int i = 0; i < numberOfParameters; i++)
            {
                this.parameters[i] = memory[instructionPointer+i+1];
            }
        }
    }

    public class Add : Instruction 
    {
        public Add(int[] memory, int instructionPointer, int opCode)
            : base(memory, instructionPointer, opCode, 3)
        {}

        public override void Execute() 
        {            
            int addend1 = ValueFromParameter(1);
            int addend2 = ValueFromParameter(2);

            memory[RawParameter(3)] = addend1 + addend2;
        }
    }

    public class Multiply : Instruction 
    {
        public Multiply(int[] memory, int instructionPointer, int opCode)
            : base(memory, instructionPointer, opCode, 3)
        {}

        public override void Execute() 
        {
            int factor1 = ValueFromParameter(1);
            int factor2 = ValueFromParameter(2);

            memory[RawParameter(3)] = factor1 * factor2;
        }
    }

    public class Store : Instruction 
    {
            public Store(int[] memory, int instructionPointer, int opCode)
                : base(memory, instructionPointer, opCode, 1)
            {}

            public override void Execute() 
            {
                    int v = RawParameter(1);
                    memory[v] = Input;
            }
            }
            public class Retrieve : Instruction 
            {

            public Retrieve(int[] memory, int instructionPointer, int opCode)
                : base(memory, instructionPointer, opCode, 1)
            {}

            public override void Execute() 
            {
                var output = memory[RawParameter(1)];
                Console.WriteLine($"Output: {output}");
            }
        }

        public class JumpIfTrue : Instruction 
        {
            public JumpIfTrue(int[] memory, int instructionPointer, int opCode)
            : base(memory, instructionPointer, opCode, 2)
            {}

            public override void Execute() 
            {
                int param1 = ValueFromParameter(1);
                if(param1 != 0) 
                {
                    instructionPointer = ValueFromParameter(2);
                }
                else {
                    instructionPointer = base.IncreasePointer();
                }
            }

            public override int IncreasePointer() 
            {
                return instructionPointer;
            }
        }

        public class JumpIfFalse : Instruction 
        {
            public JumpIfFalse(int[] memory, int instructionPointer, int opCode)
            : base(memory, instructionPointer, opCode, 2)
            {}

            public override void Execute() 
            {
                int param1 = ValueFromParameter(1);
                if(param1 == 0) 
                {
                    instructionPointer = ValueFromParameter(2);
                }
                else {
                    instructionPointer = base.IncreasePointer();
                }
            }

            public override int IncreasePointer() 
            {
                return instructionPointer;
            }
        }

        public class LessThan : Instruction 
        {
            public LessThan(int[] memory, int instructionPointer, int opCode)
                : base(memory, instructionPointer, opCode, 3)
            {}

            public override void Execute() 
            {            
                int value1 = ValueFromParameter(1);
                int value2 = ValueFromParameter(2);

                memory[RawParameter(3)] = value1< value2 ? 1 : 0;
            }
        }

        public class EqualTo : Instruction 
        {
            public EqualTo(int[] memory, int instructionPointer, int opCode)
                : base(memory, instructionPointer, opCode, 3)
            {}

            public override void Execute() 
            {            
                int value1 = ValueFromParameter(1);
                int value2 = ValueFromParameter(2);

                memory[RawParameter(3)] = value1 == value2 ? 1 : 0;
            }
        }

        public class Halt : Instruction 
        {
            public Halt() : base(null, 0, 0, 0)
            {}

            public override void Execute() {}

            public override int IncreasePointer()
            {
                return int.MaxValue;
            }
        }
    }
}