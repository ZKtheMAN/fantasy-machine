namespace fantasy_machine
{
    internal static class InstructionSet
    {
        internal static readonly Dictionary<uint, Action<ProcessorState, uint>> Instructions =
            new Dictionary<uint, Action<ProcessorState, uint>>()
            {
                // mov source destination
                {0, (ProcessorState processor, uint instruction) => {
                    var source = (instruction >> 32 - 5 - 3) & 7;
                    var destination = (instruction >> 32 - 5 - 3 - 3) & 7;
                    processor.registers[destination] = processor.registers[source];
                } },

                // and left right destination
                {1, (ProcessorState processor, uint instruction) => {
                    var left = (instruction >> 32 - 5 - 3) & 7;
                    var right = (instruction >> 32 - 5 - 3 - 3) & 7;
                    var destination = (instruction >> (32 - 5 - 3 - 3 - 3)) & 7;
                    processor.registers[destination] =
                        processor.registers[left] & processor.registers[right];
                } },

                // or left right destination
                {2, (ProcessorState processor, uint instruction) => {
                    var left = (instruction >> 32 - 5 - 3) & 7;
                    var right = (instruction >> 32 - 5 - 3 - 3) & 7;
                    var destination = (instruction >> (32 - 5 - 3 - 3 - 3)) & 7;
                    processor.registers[destination] =
                        processor.registers[left] | processor.registers[right];
                } },

                // not source destination
                {3, (ProcessorState processor, uint instruction) => {
                    var source = (instruction >> 32 - 5 - 3) & 7;
                    var destination = (instruction >> 32 - 5 - 3 - 3) & 7;
                    processor.registers[destination] = ~processor.registers[source];
                } },

                // add left right destination
                {4, (ProcessorState processor, uint instruction) => {
                    var left = (instruction >> 32 - 5 - 3) & 7;
                    var right = (instruction >> 32 - 5 - 3 - 3) & 7;
                    var destination = (instruction >> (32 - 5 - 3 - 3 - 3)) & 7;
                    processor.registers[destination] =
                        processor.registers[left] + processor.registers[right];
                } },

                // sub left right destination
                {5, (ProcessorState processor, uint instruction) => {
                    var left = (instruction >> 32 - 5 - 3) & 7;
                    var right = (instruction >> 32 - 5 - 3 - 3) & 7;
                    var destination = (instruction >> (32 - 5 - 3 - 3 - 3)) & 7;
                    processor.registers[destination] =
                        processor.registers[left] - processor.registers[right];
                } },

                // mul left right
                {6, (ProcessorState processor, uint instruction) => {
                    var left = (instruction >> 32 - 5 - 3) & 7;
                    var right = (instruction >> 32 - 5 - 3 - 3) & 7;
                    var temp = (ulong)processor.registers[left] * (ulong)processor.registers[right];
                    processor.lo = (uint)(temp & uint.MaxValue);
                    processor.hi = (uint)(temp >> 32 & uint.MaxValue);
                } },

                // div left right
                {7, (ProcessorState processor, uint instruction) => {
                    var left = (instruction >> 32 - 5 - 3) & 7;
                    var right = (instruction >> 32 - 5 - 3 - 3) & 7;
                    processor.lo = processor.registers[left] / processor.registers[right];
                    processor.hi = processor.registers[left] % processor.registers[right];
                } },

                // mfhi destination
                {8, (ProcessorState processor, uint instruction) => {
                    var destination = (instruction >> 32 - 5 - 3) & 7;
                    processor.registers[destination] = processor.hi;
                } },

                // mflo destination
                {9, (ProcessorState processor, uint instruction) => {
                    var destination = (instruction >> 32 - 5 - 3) & 7;
                    processor.registers[destination] = processor.lo;
                } },

                // li destination immediate
                {10, (ProcessorState processor, uint instruction) => {
                    var destination = (instruction >> 32 - 5 - 3) & 7;
                    var immediate = (instruction) & 0b00000000111111111111111111111111;
                    processor.registers[destination] = immediate;
                } },

                // lw source destination
                {11, (ProcessorState processor, uint instruction) => {
                    var source = (instruction >> 32 - 5 - 8) & 255;
                    var destination = (instruction >> 32 - 5 - 8 - 3) & 7;
                    processor.registers[destination] = processor.memory[source];
                } },

                // si destination immediate
                {12, (ProcessorState processor, uint instruction) => {
                    var destination = (instruction >> 32 - 5 - 8) & 255;
                    var immediate = (instruction) & 0b00000000000001111111111111111111;
                    processor.memory[destination] = immediate;
                } },

                // sw source destination
                {13, (ProcessorState processor, uint instruction) => {
                    var source = (instruction >> 32 - 5 - 3) & 7;
                    var destination = (instruction >> 32 - 5 - 3 - 8) & 255;
                    processor.memory[destination] = processor.registers[source];
                } },

                // j address
                { 14, (ProcessorState processor, uint instruction) => {
                    var address = (instruction >> 32 - 5 - 8) & 255;
                    processor.pc = address;
                } },

                // je source target address
                { 15, (ProcessorState processor, uint instruction) => {
                    var source = (instruction >> 32 - 5 - 3) & 7;
                    var target = (instruction >> 32 - 5 - 3 - 3) & 7;
                    var address = (instruction >> 32 - 5 - 3 - 3 - 8) & 255;
                    if (processor.registers[source] == processor.registers[target]) {
                        processor.pc = address;
                    }
                } },

                // jne source target address
                { 16, (ProcessorState processor, uint instruction) => {
                    var source = (instruction >> 32 - 5 - 3) & 7;
                    var target = (instruction >> 32 - 5 - 3 - 3) & 7;
                    var address = (instruction >> 32 - 5 - 3 - 3 - 8) & 255;
                    if (processor.registers[source] != processor.registers[target]) {
                        processor.pc = address;
                    }
                } },

                // jgt source target address
                { 17, (ProcessorState processor, uint instruction) => {
                    var source = (instruction >> 32 - 5 - 3) & 7;
                    var target = (instruction >> 32 - 5 - 3 - 3) & 7;
                    var address = (instruction >> 32 - 5 - 3 - 3 - 8) & 255;
                    if (processor.registers[source] > processor.registers[target]) {
                        processor.pc = address;
                    }
                } },

                // jlt source target address
                { 18, (ProcessorState processor, uint instruction) => {
                    var source = (instruction >> 32 - 5 - 3) & 7;
                    var target = (instruction >> 32 - 5 - 3 - 3) & 7;
                    var address = (instruction >> 32 - 5 - 3 - 3 - 8) & 255;
                    if (processor.registers[source] < processor.registers[target]) {
                        processor.pc = address;
                    }
                } },

                // jge source target address
                { 19, (ProcessorState processor, uint instruction) => {
                    var source = (instruction >> 32 - 5 - 3) & 7;
                    var target = (instruction >> 32 - 5 - 3 - 3) & 7;
                    var address = (instruction >> 32 - 5 - 3 - 3 - 8) & 255;
                    if (processor.registers[source] >= processor.registers[target]) {
                        processor.pc = address;
                    }
                } },

                // jle source target address
                { 20, (ProcessorState processor, uint instruction) => {
                    var source = (instruction >> 32 - 5 - 3) & 7;
                    var target = (instruction >> 32 - 5 - 3 - 3) & 7;
                    var address = (instruction >> 32 - 5 - 3 - 3 - 8) & 255;
                    if (processor.registers[source] <= processor.registers[target]) {
                        processor.pc = address;
                    }
                } },

                // syscall func
                {21, (ProcessorState processor, uint instruction) => {
                    var func = (instruction >> 32 - 5 - 3) & 7;
                    syscalls[func](processor);
                } }
            };

        internal static readonly Dictionary<uint, Action<ProcessorState>> syscalls =
            new Dictionary<uint, Action<ProcessorState>>()
            {
                // exit
                {0, (ProcessorState processor) =>
                {
                    // Do nothing, run function in ProcessorState will handle halting execution.
                } 
                },

                // print uint in register 7
                {1, (ProcessorState processor) =>
                {
                    Console.Write(processor.registers[7]);
                } 
                },

                // print char in register 7
                {2, (ProcessorState processor) =>
                {
                    Console.Write((char) processor.registers[7]);
                } 
                },

                // print string whose address is in register 7
                {3, (ProcessorState processor) =>
                {
                    uint index = processor.registers[7];
                    while (true)
                    {
                        if (processor.memory[index] == 0)
                            break;

                        if (index >= 256)
                            break;

                        Console.Write((char)processor.memory[index]);
                        index++;
                    }
                }
                },

                // read uint and store in register 7
                {4, (ProcessorState processor) =>
                {
                    var input = Console.ReadLine();
                    if (input == null) return;
                    processor.registers[7] = uint.Parse(input);
                }
                },

                // read char and store in register 7
                {5, (ProcessorState processor) =>
                {
                    var input = Console.Read();
                    processor.registers[7] = (uint)input;
                }
                },

                // read string and store at address in register 7
                {6, (ProcessorState processor) =>
                {
                    var input = Console.ReadLine();
                    if (input == null) return;
                    var address = processor.registers[7];
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (address + i >= processor.memory.Length)
                            return;

                        processor.memory[address + i] = (uint)input[i];
                    }
                }
                }
            };
    }
}
