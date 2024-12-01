using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day01
{
    internal class Program
    {
        static void Part2(List<int> l1, List<int> l2)
        {
            int total = 0;

            foreach (int num in l1)
            {
                total += num * l2.Count(i => i == num);
            }

            Console.WriteLine(total);
        }

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            List<int> l1 = new List<int>();
            List<int> l2 = new List<int>();

            foreach (var line in input)
            {
                l1.Add(int.Parse(line.Split(new string[] {"   "}, StringSplitOptions.None)[0]));
                l2.Add(int.Parse(line.Split(new string[] {"   "}, StringSplitOptions.None)[1]));
            }

            l1.Sort();
            l2.Sort();

            int total = 0;
            for(int i = 0; i < l1.Count; i++)
            {
                total += Math.Abs(l2[i] - l1[i]);
            }

            Console.WriteLine(total);

            Part2(l1, l2);

            Console.ReadKey();
        }
    }
}
