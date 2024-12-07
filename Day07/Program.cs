using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07
{
    internal class Program
    {
        struct Equation
        {
            public ulong result;
            public List<ulong> operands;
        }

        static void generateOperators(List<List<char>> result, int lengthRequired, List<char> possibleOperators, List<char> currentSequence)
        {
            if (currentSequence.Count == lengthRequired)
            {
                result.Add(currentSequence);
                return;
            }

            foreach (char op in possibleOperators)
            {
                generateOperators(result, lengthRequired, possibleOperators, new List<char>(currentSequence) { op });
            }
        }

        static ulong EvaluateEquation(Equation e, List<char> operators)
        {
            ulong result = e.operands[0];

            for (int i = 0; i < operators.Count; i++)
            {
                if (operators[i] == '*')
                {
                    result *= e.operands[i+1];
                } else if (operators[i] == '+')
                {
                    result += e.operands[i + 1];
                }
            }

            return result;
        }

        static ulong EvaluateEquation2(Equation e, List<char> operators)
        {
            ulong result = e.operands[0];

            for (int i = 0; i < operators.Count; i++)
            {
                if (operators[i] == '*')
                {
                    result *= e.operands[i + 1];
                }
                else if (operators[i] == '+')
                {
                    result += e.operands[i + 1];
                }
                else if (operators[i] == '|')
                {
                    result = ulong.Parse(result.ToString() + e.operands[i + 1].ToString());
                }
            }

            return result;
        }

        static void Part1(List<Equation> equations)
        {
            ulong total = 0;

            foreach (Equation e in equations)
            {
                bool works = false;
                List<List<char>> possibleOperators = new List<List<char>>();
                generateOperators(possibleOperators, e.operands.Count - 1, new List<char> { '*', '+' }, new List<char>());
                
                foreach (List<char> operators in possibleOperators)
                {
                    // Does this equation work
                    if (EvaluateEquation(e, operators) == e.result)
                    {
                        works = true;
                    }
                }

                if (works) total += e.result;
            }

            Console.WriteLine(total);
        }

        static void Part2(List<Equation> equations)
        {
            ulong total = 0;

            int numDone = 0;

            foreach (Equation e in equations)
            {
                bool works = false;
                List<List<char>> possibleOperators = new List<List<char>>();
                generateOperators(possibleOperators, e.operands.Count - 1, new List<char> { '*', '+', '|' }, new List<char>());

                foreach (List<char> operators in possibleOperators)
                {
                    // Does this equation work
                    if (EvaluateEquation2(e, operators) == e.result)
                    {
                        works = true;
                    }
                }

                if (works) total += e.result;

                numDone++;
                Console.Clear();
                Console.WriteLine($"{(double)numDone / equations.Count * 100.0}%");
            }

            Console.WriteLine(total);
        }

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");

            List<Equation> equations = new List<Equation>();
            foreach (string line in lines)
            {
                Equation e;
                string[] parts = line.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                e.result = ulong.Parse(parts[0]);

                string[] nums = parts[1].Split(' ');

                e.operands = nums.Select(x => ulong.Parse(x)).ToList();

                equations.Add(e);
            }

            Part2(equations);

            Console.ReadKey();
        }
    }
}
