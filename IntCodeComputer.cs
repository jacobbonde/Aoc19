using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AoC19
{
    public class IntCodeComputer
    {
        private Queue<long> InitialInput;
        private BlockingCollection<long> Input;
        public BlockingCollection<long> Output;
        private long[] memory = new long[10000];
        private long instructionPointer;
        protected long relativeBase;

        public IntCodeComputer(BlockingCollection<long> input = null, BlockingCollection<long> output = null)
        {
            Input = input ?? new BlockingCollection<long>();
            Output = output ?? new BlockingCollection<long>();
            
        }

        public (long, long[]) Run(long[] program, params long[] input)
        {
            InitialInput = new Queue<long>(input);

            int programLength = program.Length;
            //memory = new int[programLength];
            Array.Copy(program, memory, program.Length);

            while(instructionPointer > -1)
            {
                var instruction = GetInstruction(memory, instructionPointer);
                instruction.Execute();
                instructionPointer = instruction.IncreasePointer();
            }

           return (memory[0], memory);
        }

        private Instruction GetInstruction(long[] memory, long instructionPointer)
        {
            int opCode = (int)memory[instructionPointer];

            var intructionCode = opCode % 100;

            switch (intructionCode)
                {
                    case  1:
                        return new Add(this, opCode);
                    case 2:
                        return new Multiply(this, opCode);
                    case 3:
                        return new ReadFromInput(this, opCode);
                    case 4:
                        return new WriteToOutput(this, opCode);
                    case 5:
                        return new JumpIfTrue(this, opCode);
                    case 6:
                        return new JumpIfFalse(this, opCode);
                    case 7:
                        return new LessThan(this, opCode);
                    case 8:
                        return new EqualTo(this, opCode);
                    case 9:
                        return new RelativeBaseOffset(this, opCode);
                    case 99:
                        return new Halt(this);
                    default:
                    throw new InvalidOperationException($"Got instruction code {intructionCode} from {opCode} at intruction pointer {instructionPointer}");
                }
        }
    

        abstract class Instruction 
        {
            protected IntCodeComputer computer;
            protected long[] memory;
            protected long instructionPointer;
            protected long[] parameters;
            private ParameterMode[] parameterModes;
            public Instruction(IntCodeComputer computer, int opCode, int numberOfParameters)
            {
                this.computer = computer;
                this.memory = computer.memory;
                this.instructionPointer = computer.instructionPointer;

                this.parameterModes = new[] 
                {
                    (ParameterMode)((opCode / 100) % 10),
                    (ParameterMode)((opCode / 1000) % 10),
                    (ParameterMode)((opCode / 10000))
                };

                this.SetParameters(numberOfParameters);
            }
            public abstract void Execute();
            public virtual long IncreasePointer()
            {
                return instructionPointer + parameters.Length + 1;
            }

            protected long ValueFromParameter(int parameterNumber) 
            {
                int index = parameterNumber-1;
                long parameter = parameters[index];
                
                switch (parameterModes[index])
                {
                    case ParameterMode.Position:
                        return memory[parameter];
                    case ParameterMode.Immediate:
                        return parameter;
                    case ParameterMode.Relative:
                        return memory[parameter + computer.relativeBase];
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            protected long AddressFromParameter(int parameterNumber) 
            {
                int index = parameterNumber-1;
                long parameter = parameters[index];
                
                switch (parameterModes[index])
                {
                    case ParameterMode.Position:
                        return parameter;
                    case ParameterMode.Relative:
                        return parameter + computer.relativeBase;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            private void SetParameters(int numberOfParameters)
            {
                this.parameters = new long[numberOfParameters];

                for (int i = 0; i < numberOfParameters; i++)
                {
                    this.parameters[i] = memory[instructionPointer+i+1];
                }
            }

            enum ParameterMode {
                Position = 0,
                Immediate = 1,
                Relative = 2
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
                long addend1 = ValueFromParameter(1);
                long addend2 = ValueFromParameter(2);

                memory[AddressFromParameter(3)] = addend1 + addend2;
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
                long factor1 = ValueFromParameter(1);
                long factor2 = ValueFromParameter(2);

                memory[AddressFromParameter(3)] = factor1 * factor2;
            }
        }

        class ReadFromInput : Instruction 
        {
            private const int NumberOfParameters = 1;

            public ReadFromInput(IntCodeComputer computer, int opCode)
                : base(computer, opCode, NumberOfParameters)
            {}

            public override void Execute() 
            {
                Console.WriteLine($"{Task.CurrentId} waiting for input");
                long address = AddressFromParameter(1);
                long inputValue;
                if(!computer.InitialInput.TryDequeue(out inputValue))
                {
                    inputValue = computer.Input.Take();
                }
                memory[address] = inputValue;
            }
        }
        class WriteToOutput : Instruction 
        {
            private const int NumberOfParameters = 1;

            public WriteToOutput(IntCodeComputer computer, int opCode)
                : base(computer, opCode, NumberOfParameters)
            {}

            public override void Execute() 
            {
                var output = ValueFromParameter(1);
                computer.Output.Add(output);
                Console.WriteLine($"Output: {output}");
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
                long param1 = ValueFromParameter(1);
                instructionPointer = param1 != 0 ? ValueFromParameter(2) : base.IncreasePointer();
            }

            public override long IncreasePointer() 
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
                long param1 = ValueFromParameter(1);
                instructionPointer = param1 == 0 ? ValueFromParameter(2) : base.IncreasePointer();
            }

            public override long IncreasePointer() 
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
                long value1 = ValueFromParameter(1);
                long value2 = ValueFromParameter(2);

                memory[AddressFromParameter(3)] = value1 < value2 ? 1 : 0;
            }
        }

        class EqualTo : Instruction 
        {
            public EqualTo(IntCodeComputer computer, int opCode)
                : base(computer, opCode, 3)
            {}

            public override void Execute() 
            {            
                long value1 = ValueFromParameter(1);
                long value2 = ValueFromParameter(2);

                memory[AddressFromParameter(3)] = value1 == value2 ? 1 : 0;
            }
        }

        class RelativeBaseOffset : Instruction 
        {
            public RelativeBaseOffset(IntCodeComputer computer, int opCode)
                : base(computer, opCode, 1)
            {}

            public override void Execute() 
            {            
                long value1 = ValueFromParameter(1);

                computer.relativeBase += value1;
            }
        }

        class Halt : Instruction 
        {
            public Halt(IntCodeComputer computer) : base(computer, 0, 0)
            {}

            public override void Execute() {}

            public override long IncreasePointer()
            {
                return -1;
            }
        }
    }
}
