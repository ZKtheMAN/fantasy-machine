// See https://aka.ms/new-console-template for more information
using fantasy_machine;

ProcessorState p = new ProcessorState();
uint[] program = new uint[256];
var index = 0;
using (var reader = new BinaryReader(File.Open(args[0], FileMode.Open)))
{
    while (reader.PeekChar() != -1)
    {
        program[index] = reader.ReadUInt32();
        index++;
    }
}
p.LoadProgram(program);
p.Run();