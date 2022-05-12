/*uint[] hello_world = new uint[256];

string msg = "Hello, world!";
for (int i = 0; i < msg.Length; i++)
    hello_world[i] = (uint)msg[i];
hello_world[64] = 0b10101_011_000000000000000000000000;
hello_world[65] = 0b10101_000_000000000000000000000000;

using (BinaryWriter file = new BinaryWriter(File.Open("./program", FileMode.Create)))
{
    foreach (uint val in hello_world)
        file.Write(val);
}*/

uint[] double_or_nothing = new uint[256];
string msg1 = "Enter a number and I'll double it: ";
for (int i = 0; i < msg1.Length; i++)
    double_or_nothing[i] = (uint)msg1[i];
string msg2 = "Doubled, it's: ";
for (int i = 0; i < msg2.Length; i++)
    double_or_nothing[i + 37] = (uint)msg2[i];
// li 7 0
double_or_nothing[64] = 0b01010_111_000000000000000000000000;
// syscall 3
double_or_nothing[65] = 0b10101_011_000000000000000000000000;
// syscall 4
double_or_nothing[66] = 0b10101_100_000000000000000000000000;
// li 1 2
double_or_nothing[67] = 0b01010_001_000000000000000000000010;
// mul 7 1
double_or_nothing[68] = 0b00110_111_001_000000000000000000000;
// li 7 37
double_or_nothing[69] = 0b01010_111_000000000000000000100101;
// syscall 3
double_or_nothing[70] = 0b10101_011_000000000000000000000000;
// mflo 7
double_or_nothing[71] = 0b01001_111_000000000000000000000000;
// syscall 1
double_or_nothing[72] = 0b10101_001_000000000000000000000000;
// syscall 0
double_or_nothing[73] = 0b10101_000_000000000000000000000000;

using (BinaryWriter file = new BinaryWriter(File.Open("./program", FileMode.Create)))
{
    foreach (uint val in double_or_nothing)
        file.Write(val);
}