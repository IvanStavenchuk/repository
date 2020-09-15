using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ConsoleApp1
{
    public class Program
    {
        private static readonly short _ipLength = 4;
 
        static void Main(string[] args)
        {
            Console.WriteLine(CalculateMinSubnetIp(args));
            Console.ReadKey();
        }

        public static string CalculateMinSubnetIp(string[] args)
        {
            byte[] minIp = new byte[] { 255, 255, 255, 255 };
            byte[] maxIp = new byte[] { 0, 0, 0, 0 };
            foreach (var arg in args)
            {
                IPAddress ip;
                if (IPAddress.TryParse(arg, out ip)) // валидация адресов
                {
                    byte[] ipByte = ip.GetAddressBytes();
                    Console.WriteLine(ip.ToString());
                    // поиск минимального и максимального адреса
                    for (int i = 0; i < _ipLength; i++)
                    {
                        if (minIp[i] > ipByte[i])
                            minIp[i] = ipByte[i];
                        if (maxIp[i] < ipByte[i])
                            maxIp[i] = ipByte[i];
                    }
                }
            }
            // вычисление минимального адреса подсети
            byte[] subnetIp = new byte[_ipLength];
            byte pow = 0;
            int lastOctet = _ipLength;
            for (int i = 0; i < _ipLength; i++)
            {
                lastOctet--;
                if (maxIp[i] == minIp[i])
                {
                    subnetIp[i] = minIp[i];
                }
                else
                {
                    int twoPowCount = 1;
                    while (!((maxIp[i] - minIp[i]) < twoPowCount
                        && minIp[i] > twoPowCount * (minIp[i] / twoPowCount)
                        && maxIp[i] < twoPowCount * ((minIp[i] / twoPowCount) +1)
                        ))
                    {
                        pow++;
                        twoPowCount <<= 1;
                    }
                    subnetIp[i] = (byte)(twoPowCount * (minIp[i] / twoPowCount));
                    break;
                }
            }
            return $"{subnetIp[0]}.{subnetIp[1]}.{subnetIp[2]}.{subnetIp[3]}/{ 32 - (pow + lastOctet * 8)}";
        }
    }
}
