using SlotMachineJukebox.Util;

namespace SlotMachineJukebox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Testing \/\/\/ 

            WaveHeader header = new WaveHeader(44100, 16, 2, 476);
            header.SetDataSize();
            header.SetFileSize();

            byte[] bytes = header.ToBytes();

            foreach (byte b in bytes)
            {
                Console.WriteLine(b);
            }

            //File.Create("myFile.txt");

            FileHandling.WriteBytes("myFile.txt", bytes);
        }
    }
}