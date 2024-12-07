using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
    internal class Program
    {
        static bool CheckUpdate(List<int> update, Dictionary<int, List<int>> rules)
        {
            for (int i = 0; i < update.Count; i++)
            {
                int num = update[i];

                if (rules.ContainsKey(num))
                {
                    // This num must appear before some other nums. Check if any of those appear before this one
                    List<int> before = update.Take(i).ToList();

                    foreach (int notAllowed in rules[num])
                    {
                        if (before.Contains(notAllowed))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        static List<int> FixUpdate(List<int> update, Dictionary<int, List<int>> rules)
        {
            List<int> result = new List<int> { update[0] };

            for (int i = 1; i < update.Count; i++)
            {
                int toInsert = update[i];
                // Currently have i many items in the result

                // Insert update[i] into the correct position in the array

                // Need to check rules. If update[i] should appear before any of the items, move it before the earliest

                if (rules.ContainsKey(toInsert))
                {
                    int insertIndex = -1;
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (rules[toInsert].Contains(result[j]))
                        {
                            // result[j] should appear after toInsert, so move along
                            insertIndex = j;
                        }
                    }

                    if (insertIndex >= 0) result.Insert(insertIndex, toInsert);
                    else result.Add(toInsert);
                }
                else
                {
                    // Just put it at the end
                    result.Add(toInsert);
                }
            }

            return result.ToList();
        }

        static void Part1(Dictionary<int, List<int>> rules, List<List<int>> updates)
        {
            int total = 0;
            foreach (List<int> update in updates)
            {
                // Check if the update is correct
                if (CheckUpdate(update, rules))
                {
                    string updateString = update.Select(x => x.ToString()).Aggregate((a, b) => a + ',' + b);

                    // Get the middle
                    int middle = update[update.Count / 2];
                    total += middle;

                    Console.WriteLine($"{middle}: {updateString}");
                }
            }
            Console.WriteLine(total);
        }

        static void Part2(Dictionary<int, List<int>> rules, List<List<int>> updates)
        {
            int total = 0;
            foreach (List<int> update in updates)
            {
                if (!CheckUpdate(update, rules))
                {
                    string updateString = update.Select(x => x.ToString()).Aggregate((a, b) => a + ',' + b);

                    List<int> result = FixUpdate(update, rules);

                    string newUpdateString = result.Select(x => x.ToString()).Aggregate((a, b) => a + ',' + b);

                    int middle = result[result.Count / 2];
                    total += middle;

                    Console.WriteLine($"{updateString} becomes {middle}: {newUpdateString}");
                }
            }
            Console.WriteLine(total);
        }

        static void Main(string[] args)
        {
            Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();
            List<List<int>> updates = new List<List<int>>();

            string[] lines = File.ReadAllLines("input.txt");

            bool updatesNow = false;
            foreach (string line in lines)
            {
                if (line == "")
                {
                    updatesNow = true;
                }
                else if (updatesNow)
                {
                    updates.Add(line.Split(',').Select(s => int.Parse(s)).ToList());
                }
                else
                {
                    string[] parts = line.Split('|');
                    int left = int.Parse(parts[0]);
                    int right = int.Parse(parts[1]);

                    if (rules.ContainsKey(left))
                    {
                        rules[left].Add(right);
                    }
                    else
                    {
                        rules[left] = new List<int>() { right };
                    }
                }
            }

            Part2(rules, updates);

            Console.ReadKey();
        }
    }
}
