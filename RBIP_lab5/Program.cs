using Apache.Ignite.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.Ignite.Core.Compute;
using Apache.Ignite.Core.Binary;

namespace RBIP_lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 6;
            Random r = new Random();

            int[][] mas = new int[size][];

            for(int i = 0; i < size; i++)
            {
                mas[i] = new int[size];
                for(int j = 0;j < size; j++)
                {
                    mas[i][j] = r.Next(1, 10);
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(mas[i][j].ToString() + " ");
                }
                Console.WriteLine();
            }

            var cfg = new IgniteConfiguration { BinaryConfiguration = new BinaryConfiguration(typeof(CountFunc)), ClientMode = true };

            using (var igninte = Ignition.Start(cfg))
            {
                int Sum = 0;
                var res = igninte.GetCompute().Apply(new CountFunc(), mas);
                foreach(var c in res)
                {
                    Sum += c;
                }
                int b = 0;
                for (int i = 0; i < size - 1; i++)
                {
                    for (int k = 0; k < size - 1; k++)
                    {
                        if (mas[k][ 0] > mas[k + 1][ 0])
                        {
                            for (int j = 0; j < size; j++)
                            {
                                b = mas[k][j];
                                mas[k][ j] = mas[k + 1][j];
                                mas[k + 1][j] = b;
                            }
                        }
                    }
                }
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Console.Write(mas[i][j].ToString() + " ");
                    }
                    Console.WriteLine();
                }
                Console.Read();
            }
        }
    }

    internal class CountFunc : IComputeFunc<int[], int>
    {
        public int Invoke(int[] arg)
        {
            return 0;
        }
    }
}
