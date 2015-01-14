using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LFSR
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine();
            //ulong enter = Convert.ToUInt64 (Console.ReadLine());
            ulong enter = 18446744073709551615;
            enter = enter & 0x000000FFFFFFFFFF;
            //BitArray bitArray = new BitArray(Encoding.Default.GetBytes(Console.ReadLine()));
            //Console.WriteLine(enter);
            ulong result=0;
            Console.WriteLine(Math.Pow(2, 40) - 1);
            for (ulong i = 0; i < Math.Pow(2, 40) - 1; i++)
            {
                result = (result << 1) | LFSR(ref enter);
                if (i%10000000 == 0) Console.WriteLine (i + " " + result);
            }

            Console.WriteLine(result & 0x000000FFFFFFFFFF);

#region матожидание
            double math = 0;
            BitArray bitArray = new BitArray(Encoding.Default.GetBytes(Convert.ToString(result & 0x000000FFFFFFFFFF)));
            for (int i = 0; i < bitArray.Length; i++) 
            {
                switch (bitArray.Get(i))
                {
                    case true: 
                        math = math + 1;
                        break;
                    case false:
                        break;
                }
            }
            math = math / 40;

            Console.WriteLine(math);
#endregion

#region дисперсия

            double disp = 0;
            for (int i = 0; i < bitArray.Length; i++)
            {
                switch (bitArray.Get(i))
                {
                    case true:
                        disp = disp + Math.Pow((1 - math), 2);
                        break;
                    case false:
                        disp = disp + Math.Pow(math, 2);
                        break;
                }
            }
            disp = disp / 40;

            Console.WriteLine(disp);
#endregion
            Console.ReadKey();
        }

        static ulong LFSR (ref ulong ShiftRegister)
        {
            ShiftRegister = ((((ShiftRegister >> 39) ^ (ShiftRegister >> 36) ^ (ShiftRegister >> 35) ^ (ShiftRegister >> 34))
                << 39) & 0x000000FFFFFFFFFF) | (ShiftRegister >> 1);

            return (ulong)(ShiftRegister & 0x0000000000000001);
        }


    }
}
