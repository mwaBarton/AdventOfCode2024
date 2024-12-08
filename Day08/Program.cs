using System;
using System.Collections.Generic;
using System.IO;

namespace Day08
{
    internal class Program
    {
        struct Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        static bool IsPointInGrid(Point p, char[,] grid)
        {
            return p.x >= 0 && p.x < grid.GetLength(0) && p.y >= 0 && p.y < grid.GetLength(1);
        }

        static void Part1(char[,] grid, bool part2 = false)
        {
            Dictionary<char, List<Point>> antennas = new Dictionary<char, List<Point>>();

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] != '.')
                    {
                        if (antennas.ContainsKey(grid[x, y]))
                        {
                            antennas[grid[x, y]].Add(new Point(x, y));
                        }
                        else
                        {
                            antennas.Add(grid[x, y], new List<Point> { new Point(x, y) });
                        }
                    }
                }
            }

            List<Point> antinodes = new List<Point>();
            int[,] antinodeMap = new int[grid.GetLength(0), grid.GetLength(1)];

            // For each type of antennas
            foreach (char type in antennas.Keys)
            {
                // For each distinct pair of antennas
                for (int i = 0; i < antennas[type].Count; i++)
                {
                    for (int j = i; j < antennas[type].Count; j++)
                    {
                        if (i != j)
                        {
                            // Calculate the x and y difference
                            int xDiff = antennas[type][j].x - antennas[type][i].x;
                            int yDiff = antennas[type][j].y - antennas[type][i].y;

                            if (!part2)
                            {
                                // Calculate the positions of the two antinodes
                                Point a1 = new Point(antennas[type][i].x - xDiff, antennas[type][i].y - yDiff);
                                Point a2 = new Point(antennas[type][j].x + xDiff, antennas[type][j].y + yDiff);

                                // Check they're in the grid. If so, add them
                                if (IsPointInGrid(a1, grid)) antinodes.Add(a1);
                                if (IsPointInGrid(a2, grid)) antinodes.Add(a2);
                            } else
                            {
                                // Use i as starting point and loop forwards and backwards until we're off the map
                                Point a = antennas[type][i];
                                while (IsPointInGrid(a, grid))
                                {
                                    antinodes.Add(a);
                                    a.x += xDiff;
                                    a.y += yDiff;
                                }

                                a = antennas[type][i];
                                a.x -= xDiff;
                                a.y -= yDiff;
                                while (IsPointInGrid(a, grid))
                                {
                                    antinodes.Add(a);
                                    a.x -= xDiff;
                                    a.y -= yDiff;
                                }
                            }

                        }
                    }
                }
            }

            // Put the antinodes in the grid
            int total = 0;
            foreach (Point antinode in antinodes)
            {
                if (antinodeMap[antinode.x, antinode.y] == 0) total++;
                antinodeMap[antinode.x, antinode.y]++;
            }

            // Print the antinode grid
            for (int y = 0; y < antinodeMap.GetLength(1); y++)
            {
                for (int x = 0; x < antinodeMap.GetLength(0); x++)
                {
                    if (antinodeMap[x, y] > 0) Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(antinodeMap[x, y]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine();
            }

            Console.WriteLine(total);
        }

        static void Main(string[] args)
        {
            char[,] grid;
            string[] lines = File.ReadAllLines("input.txt");
            grid = new char[lines[0].Length, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    grid[x, y] = lines[y][x];
                }
            }

            Part1(grid, true);

            Console.ReadKey();
        }
    }
}
