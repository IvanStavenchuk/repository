using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ConsoleApp1
{
    public class Program
    {
        //private static List<byte[]> _ipsBytes = new List<byte[]>();
        private static short ipLength = 4;
 
        static void Main(string[] args)
        {
            Console.WriteLine(CalculateMinSubnetIp(args));
            Console.ReadKey();
        }

        public static string CalculateMinSubnetIp(string[] args)
        {
            byte[] minIp = new byte[] { 255, 255, 255, 255 };
            byte[] maxIp = new byte[] { 0, 0, 0, 0 };
            //IPAddress minIp = new IPAddress(new byte[] { 255, 255, 255, 255 });
            //IPAddress maxIp = new IPAddress(new byte[] { 0, 0, 0, 0 });
            foreach (var arg in args)
            {
                IPAddress ip;
                if (IPAddress.TryParse(arg, out ip)) // валидация адресов
                {
                    byte[] ipByte = ip.GetAddressBytes();
                    Console.WriteLine(ip.ToString());
                    // поиск минимального и максимального адреса
                    for (int i = 0; i < ipLength; i++)
                    {
                        if (minIp[i] > ipByte[i])
                            minIp[i] = ipByte[i];
                        if (maxIp[i] < ipByte[i])
                            maxIp[i] = ipByte[i];
                    }
                }
            }
            // вычисление минимального адреса подсети
            byte[] subnetIp = new byte[ipLength];
            byte count = 0;
            int lastOctet = ipLength;
            for (int i = 0; i < ipLength; i++)
            {
                lastOctet--;
                if (maxIp[i] == minIp[i])
                {
                    subnetIp[i] = minIp[i];
                }
                else
                {
                    count = 1;
                    while (!((maxIp[i] - minIp[i]) < Math.Pow(2, count)
                        && minIp[i] > Math.Pow(2, count) * (Math.Truncate(minIp[i] / Math.Pow(2, count)))
                        && maxIp[i] < Math.Pow(2, count) * (Math.Truncate(minIp[i] / Math.Pow(2, count))+1)
                        ))
                    {
                        count++;
                    }
                    subnetIp[i] = (byte)(Math.Pow(2, count) * (Math.Truncate(minIp[i] / Math.Pow(2, count))));
                    break;
                }
            }
            return $"{subnetIp[0]}.{subnetIp[1]}.{subnetIp[2]}.{subnetIp[3]}/{ 32 - (count + lastOctet * 8)}";
        }
    }
}
