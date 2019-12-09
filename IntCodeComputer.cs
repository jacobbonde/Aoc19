using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AoC19
{
    public class IntCodeComputer
    {
        private Queue<int> InitialInput;
        private BlockingCollection<int> Input;
        public BlockingCollection<int> Output;
        private int[] memory;
        private int instructionPointer;

        public IntCodeComputer(BlockingCollection<int> input = null, BlockingCollection<int> output = null)
        {
            Input = input ?? new BlockingCollection<int>();
            Output = output ?? new BlockingCollection<int>();
            
        }

        public (int, int[]) Run(int[] program, params int[] input)
        {
            InitialInput = new Queue<int>(input);

            int programLength = program.Length;
            memory = new int[programLength];
            Array.Copy(program, memory, program.Length);

            while(instructionPointer < programLength)
            {
                var instruction = GetInstruction(memory, instructionPointer);
                instruction.Execute();
                instructionPointer = instruction.IncreasePointer();
            }

           return (memory[0], memory);
        }

        private Instruction GetInstruction(int[] memory, int instructionPointer)
        {
            int opCode = memory[instructionPointer];

            var intructionCode = opCode % 100;

            switch (intructionCode)
                {
                    case  1:
                        return new Add(this, opCode);
                    case 2:
                        return new Multiply(this, opCode);
                    case 3:
                        return new Store(this, opCode);
                    case 4:
                        return new Retrieve(this, opCode);
                    case 5:
                        return new JumpIfTrue(this, opCode);
                    case 6:
                        return new JumpIfFalse(this, opCode);
                    case 7:
                        return new LessThan(this, opCode);
                    case 8:
                        return new EqualTo(this, opCode);
                    case 99:
                        return new Halt(this);
                    default:
                    throw new InvalidOperationException($"Got instruction code {intructionCode} from {opCode} at intruction pointer {instructionPointer}");
                }
        }
    

        abstract class Instruction 
        {
            protected IntCodeComputer computer;
            protected int[] memory;
            protected int instructionPointer;   
            protected int[] parameters;
            protected int[] parameterModes;
            public Instruction(IntCodeComputer computer, int opCode, int numberOfParameters)
            {
                this.computer = computer;
                this.memory = computer.memory;
                this.instructionPointer = computer.instructionPointer;

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

        class Add : Instruction 
        {
            private const int NumberOfParameters = 3;

            public Add(IntCodeComputer computer, int opCode)
                : base(computer, opCode, NumberOfParameters)
            {}

            public override void Execute() 
            {            
                int addend1 = ValueFromParameter(1);
                int addend2 = ValueFromParameter(2);

                memory[RawParameter(3)] = addend1 + addend2;
            }
        }

        class Multiply : Instruction 
        {
            private const int NumberOfParameters = 3;

            public Multiply(IntCodeComputer computer, int opCode)
                : base(computer, opCode, NumberOfParameters)
            {}

            public override void Execute() 
            {
                int factor1 = ValueFromParameter(1);
                int factor2 = ValueFromParameter(2);

                memory[RawParameter(3)] = factor1 * factor2;
            }
        }

        class Store : Instruction 
        {
            private const int NumberOfParameters = 1;

            public Store(IntCodeComputer computer, int opCode)
                : base(computer, opCode, NumberOfParameters)
            {}

            public override void Execute() 
            {
                Console.WriteLine($"{Task.CurrentId} waiting for input");
                int address = RawParameter(1);
                int inputValue;
                if(!computer.InitialInput.TryDequeue(out inputValue))
                {
                    inputValue = computer.Input.Take();
                }
                memory[address] = inputValue;
            }
        }
        class Retrieve : Instruction 
        {
            private const int NumberOfParameters = 1;

            public Retrieve(IntCodeComputer computer, int opCode)
                : base(computer, opCode, NumberOfParameters)
            {}

            public override void Execute() 
            {
                var output = memory[RawParameter(1)];
                computer.Output.Add(output);
                //Console.WriteLine($"Output: {output}");
            }
        }

        class JumpIfTrue : Instruction 
        {
            private const int NumberOfParameters = 2;

            public JumpIfTrue(IntCodeComputer computer, int opCode)
            : base(computer, opCode, NumberOfParameters)
            {}

            public override void Execute() 
            {
                int param1 = ValueFromParameter(1);
                instructionPointer = param1 != 0 ? ValueFromParameter(2) : base.IncreasePointer();
            }

            public override int IncreasePointer() 
            {
                return instructionPointer;
            }
        }

        class JumpIfFalse : Instruction 
        {
            public JumpIfFalse(IntCodeComputer computer, int opCode)
            : base(computer, opCode, 2)
            {}

            public override void Execute() 
            {
                int param1 = ValueFromParameter(1);
                instructionPointer = param1 == 0 ? ValueFromParameter(2) : base.IncreasePointer();
            }

            public override int IncreasePointer() 
            {
                return instructionPointer;
            }
        }

        class LessThan : Instruction 
        {
            public LessThan(IntCodeComputer computer, int opCode)
                : base(computer, opCode, 3)
            {}

            public override void Execute() 
            {            
                int value1 = ValueFromParameter(1);
                int value2 = ValueFromParameter(2);

                memory[RawParameter(3)] = value1 < value2 ? 1 : 0;
            }
        }

        class EqualTo : Instruction 
        {
            public EqualTo(IntCodeComputer computer, int opCode)
                : base(computer, opCode, 3)
            {}

            public override void Execute() 
            {            
                int value1 = ValueFromParameter(1);
                int value2 = ValueFromParameter(2);

                memory[RawParameter(3)] = value1 == value2 ? 1 : 0;
            }
        }

        class Halt : Instruction 
        {
            public Halt(IntCodeComputer computer) : base(computer, 0, 0)
            {}

            public override void Execute() {}

            public override int IncreasePointer()
            {
                return int.MaxValue;
            }
        }
    }
}