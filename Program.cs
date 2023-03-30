using System.Text;

namespace Streebog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool incorrectInput;
            byte[] inputBytes;
            Streebog.TypeInput typeInput;
            Streebog.OutSize outSize;
            do
            {
                incorrectInput = false;
                Console.WriteLine("Input: ");
                
                inputBytes = Encoding.Default.GetBytes(Console.ReadLine());
                do
                {
                    Console.Write("\n0 - Text\n1 - HexCodes\nType input: ");
                    typeInput = (Streebog.TypeInput)Console.ReadKey().KeyChar - '0';
                } while ((int)typeInput > 1 || (int)typeInput < 0);
                if ((typeInput == Streebog.TypeInput.HexCodes) && 
                    ((inputBytes.Length % 2 == 1) || !inputBytes.All(x => char.IsAsciiHexDigit((char)x))))
                {
                    Console.WriteLine("\nhex code is incorrect!\n");
                    incorrectInput = true;
                }
            } while (incorrectInput);
            if (typeInput == Streebog.TypeInput.HexCodes)
            {
                Console.WriteLine($"\nInput length = {(inputBytes.Length + 1) / 2}");
            } 
            else 
            {
                Console.WriteLine($"\nInput length = {inputBytes.Length}");
            }
            do
            {
                Console.Write("\n0 - 256 bits\n1 - 512 bits\nOut size: ");
                outSize = (Streebog.OutSize)Console.ReadKey().KeyChar - '0';
            } while ((int)outSize > 1 || (int)outSize < 0);
            Console.WriteLine("\n");
            byte[] output = Streebog.HashFunc(inputBytes, outSize, typeInput);
            string hexCodes = BitConverter.ToString(output);
            Console.WriteLine($"HexCodes:\n{hexCodes}\nHash:\n{hexCodes.Replace("-", "")}\nHash.Length = {hexCodes.Replace("-", "").Length}");
        }
    }
}