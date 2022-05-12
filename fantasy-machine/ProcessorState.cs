namespace fantasy_machine
{
    internal class ProcessorState
    {
        public uint[] registers = new uint[8];
        public uint hi = 0;
        public uint lo = 0;
        public uint pc = 256 / 4;
        public uint[] memory = new uint[256];

        public void LoadProgram(uint[] program)
        {
            for (int i = 0; i < program.Length; i++)
                memory[i] = program[i];
        }

        public void Run()
        {
            while (true)
            {
                // If the program counter has run out of memory to execute
                if (pc >= memory.Length)
                    return;

                // If the current instruction is "syscall 0" (halt machine)
                if (memory[pc] == 0b10101000000000000000000000000000)
                    return;

                var instruction = memory[pc];
                var instructionKey = (instruction >> 32 - 5) & 0b11111;
                InstructionSet.Instructions[instructionKey](this, instruction);

                pc++;
            }
        }
    }
}
