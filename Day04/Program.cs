using System;
using System.Collections.Generic;
using System.IO;

namespace Day04
{
    internal class Program
    {
        static void SearchMAS(char[,] grid, bool[,] included, List<Tuple<int, int, string>> results, int x, int y, string dir, int mx, int my, int ax, int ay, int sx, int sy)
        {
            if (grid[mx, my] == 'M' && grid[ax, ay] == 'A' && grid[sx, sy] == 'S')
            {
                // match
                results.Add(new Tuple<int, int, string>(x, y, dir));
                included[mx, my] = true;
                included[ax, ay] = true;
                included[sx, sy] = true;
                included[x, y] = true;
            }
        }

        static void Part1(char[,] grid)
        {
            List<Tuple<int, int, string>> result = new List<Tuple<int, int, string>>();
            bool[,] included = new bool[grid.GetLength(0), grid.GetLength(1)];

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] == 'X')
                    {
                        // Could be the start of something
                        if (x < grid.GetLength(0) - 3)
                        {
                            // Search right
                            SearchMAS(grid, included, result, x, y, "right", x + 1, y, x + 2, y, x + 3, y);

                            if (y < grid.GetLength(1) - 3)
                            {
                                // Search down right
                                SearchMAS(grid, included, result, x, y, "down right", x + 1, y + 1, x + 2, y + 2, x + 3, y + 3);
                            }

                            if (y > 2)
                            {
                                // Search up right
                                SearchMAS(grid, included, result, x, y, "up right", x + 1, y - 1, x + 2, y - 2, x + 3, y - 3);
                            }
                        }

                        if (x > 2)
                        {
                            // Search left
                            SearchMAS(grid, included, result, x, y, "left", x - 1, y, x - 2, y, x - 3, y);

                            if (y < grid.GetLength(1) - 3)
                            {
                                // Search down left
                                SearchMAS(grid, included, result, x, y, "down left", x - 1, y + 1, x - 2, y + 2, x - 3, y + 3);
                            }

                            if (y > 2)
                            {
                                // Search up left
                                SearchMAS(grid, included, result, x, y, "up left", x - 1, y - 1, x - 2, y - 2, x - 3, y - 3);
                            }
                        }

                        if (y < grid.GetLength(1) - 3)
                        {
                            // Search down
                            SearchMAS(grid, included, result, x, y, "down", x, y + 1, x, y + 2, x, y + 3);
                        }

                        if (y > 2)
                        {
                            // Search up
                            SearchMAS(grid, included, result, x, y, "up", x, y - 1, x, y - 2, x, y - 3);
                        }
                    }
                }
            }


            foreach (var item in result) Console.WriteLine(item.ToString());

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (included[x, y]) Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(grid[x, y]);

                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine();
            }
            Console.WriteLine(result.Count);
        }


        static bool SearchX(char[,] grid, bool[,] included, List<Tuple<int, int>> result, int x, int y)
        {
            List<char[,]> patterns = new List<char[,]>()
            {
                new char[,] {
                    {'M','.','S'},
                    {'.','A','.'},
                    {'M','.','S'}
                },
                new char[,] {
                    {'M','.','M'},
                    {'.','A','.'},
                    {'S','.','S'}
                },
                new char[,] {
                    {'S','.','S'},
                    {'.','A','.'},
                    {'M','.','M'}
                },
                new char[,] {
                    {'S','.','M'},
                    {'.','A','.'},
                    {'S','.','M'}
                }
            };

            foreach (char[,] pattern in patterns)
            {
                bool match = true;
                for (int j = -1; j <= 1; j++)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        if (pattern[i + 1, j + 1] != '.' && grid[x + i, y + j] != pattern[i + 1, j + 1]) match = false;
                    }
                }

                if (match)
                {
                    result.Add(new Tuple<int, int>(x, y));
                    for (int j = -1; j <= 1; j++)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            if (pattern[i + 1, j + 1] != '.') included[x + i, y + j] = true;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        static void Part2(char[,] grid)
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            bool[,] included = new bool[grid.GetLength(0), grid.GetLength(1)];

            for (int y = 1; y < grid.GetLength(1) - 1; y++)
            {
                for (int x = 1; x < grid.GetLength(0) - 1; x++)
                {
                    SearchX(grid, included, result, x, y);
                }
            }

            foreach (var item in result) Console.WriteLine(item.ToString());

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (included[x, y])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(grid[x, y]);
                    }
                    else Console.Write('.');


                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine();
            }
            Console.WriteLine(result.Count);
        }

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            char[,] grid = new char[lines[0].Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    grid[x, y] = lines[y][x];
                    Console.Write(grid[x, y]);
                }
                Console.WriteLine();
            }

            Part2(grid);

            Console.ReadKey();
        }
    }
}
