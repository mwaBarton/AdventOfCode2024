using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day03
{
    internal class Program
    {
        static void Part1(string input)
        {
            int total = 0;
            var matches = Regex.Matches(input, @"mul\(\d\d?\d?,\d\d?\d?\)");
            foreach (var match in matches)
            {
                Console.WriteLine(match);
                string paramString = match.ToString().Substring(4, match.ToString().Length - 5);
                List<int> parts = paramString.Split(',').ToList().Select(int.Parse).ToList();

                total += parts[0] * parts[1];
            }

            Console.WriteLine(total);
        }

        static void Part2(string input)
        {
            bool enabled = true;
            int total = 0;
            var matches = Regex.Matches(input, @"(mul\(\d\d?\d?,\d\d?\d?\))|(do\(\))|(don't\(\))");
            foreach (var match in matches)
            {
                
                if (match.ToString() == "do()")
                {
                    enabled = true;
                    Console.WriteLine(match);
                }
                else if (match.ToString() == "don't()")
                {
                    enabled = false;
                    Console.WriteLine(match);
                }
                else if (enabled)
                {
                    string paramString = match.ToString().Substring(4, match.ToString().Length - 5);
                    List<int> parts = paramString.Split(',').ToList().Select(int.Parse).ToList();

                    total += parts[0] * parts[1];
                    Console.WriteLine(match);
                }

            }
            
            Console.WriteLine(total);
        }

        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");
            Part2(input);

            Console.ReadKey();
        }
    }
}
