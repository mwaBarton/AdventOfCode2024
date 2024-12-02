using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02
{
    internal class Program
    {
        static bool CheckReportSafe(List<int> report)
        {
            // Check if increasing or decreasing
            bool increasing = report[1] > report[0];
            bool safe = true;

            for (int i = 0; i < report.Count - 1; i++)
            {
                if (increasing)
                {
                    if (report[i] >= report[i + 1])
                    {
                        safe = false;
                    }
                }
                else
                {
                    if (report[i] <= report[i + 1])
                    {
                        safe = false;
                    }
                }

                if (Math.Abs(report[i + 1] - report[i]) > 3)
                {
                    safe = false;
                }
            }

            return safe;
        }

        static void Part1(List<List<int>> reports)
        {
            int numSafe = 0;

            foreach (var report in reports)
            {
                if (CheckReportSafe(report)) numSafe++;
            }

            Console.WriteLine(numSafe);
        }

        static void Part2(List<List<int>> reports)
        {
            int numSafe = 0;

            foreach (var report in reports)
            {
                int optionsSafe = 0;
                // remove each level and check

                for (int i = 0; i < report.Count; i++)
                {
                    var temp = report.ToList();
                    temp.RemoveAt(i);

                    if (CheckReportSafe(temp)) optionsSafe++;
                }

                if (optionsSafe > 0) numSafe++;
            }

            Console.WriteLine(numSafe);
        }

        static void Main(string[] args)
        {
            List<List<int>> input = new List<List<int>>();
            string[] lines = File.ReadAllLines("input.txt");
            foreach (string line in lines)
            {
                input.Add(line.Split(' ').ToList().Select(x => int.Parse(x)).ToList());
            }

            Part2(input);

            Console.ReadKey();
        }
    }
}
