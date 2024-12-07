using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day06
{
    internal class Program
    {
        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        struct Point
        {
            public int x;
            public int y;
            public Direction dir;
        }

        static void PrintGrid(char[,] grid)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    Console.Write(grid[x, y]);
                }
                Console.WriteLine();
            }
        }

        static bool IsInfiniteLoop(Point guardPos, char[,] grid)
        {
            List<Point> turningPoints = new List<Point>();

            try
            {
                while (true)
                {
                    switch (guardPos.dir)
                    {
                        case Direction.Left:
                            if (grid[guardPos.x - 1, guardPos.y] == '#')
                            {
                                if (turningPoints.Contains(guardPos)) return true;
                                turningPoints.Add(guardPos);

                                guardPos.dir = Direction.Up;
                            }
                            else
                            {
                                guardPos.x--;
                            }
                            break;
                        case Direction.Right:
                            if (grid[guardPos.x + 1, guardPos.y] == '#')
                            {
                                if (turningPoints.Contains(guardPos)) return true;
                                turningPoints.Add(guardPos);

                                guardPos.dir = Direction.Down;
                            }
                            else
                            {
                                guardPos.x++;
                            }
                            break;
                        case Direction.Up:
                            if (grid[guardPos.x, guardPos.y - 1] == '#')
                            {
                                if (turningPoints.Contains(guardPos)) return true;
                                turningPoints.Add(guardPos);

                                guardPos.dir = Direction.Right;
                            }
                            else
                            {
                                guardPos.y--;
                            }
                            break;
                        case Direction.Down:
                            if (grid[guardPos.x, guardPos.y + 1] == '#')
                            {
                                if (turningPoints.Contains(guardPos)) return true;
                                turningPoints.Add(guardPos);

                                guardPos.dir = Direction.Left;
                            }
                            else
                            {
                                guardPos.y++;
                            }
                            break;
                    }
                }
            }
            catch
            {
                // Gone off the grid, so not an infinite loop
                return false;
            }
        }

        static void Part2(Point guardPos, char[,] grid)
        {
            int checkedNum = 0;
            int total = 0;

            // Try adding obstacles in each possible area
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    
                    if (!"^#".Contains(grid[x, y]))
                    {
                        // Make a copy of the grid and insert the obstacle
                        char[,] tempGrid = (char[,]) grid.Clone();
                        tempGrid[x, y] = '#';

                        if (IsInfiniteLoop(guardPos, tempGrid))
                        {
                            total++;
                        }
                    }

                    Console.Clear();
                    checkedNum++;
                    Console.WriteLine($"Checked {checkedNum} ({(double)checkedNum / (grid.GetLength(0) * grid.GetLength(1)) * 100}%). Total found so far: {total}");
                }
            }
            Console.WriteLine(total);
        }

        static void Part1(Point guardPos, char[,] grid)
        {
            int total = 0;

            try
            {
                while (true)
                {
                    switch (guardPos.dir)
                    {
                        case Direction.Left:
                            if (grid[guardPos.x - 1, guardPos.y] == '#')
                            {
                                guardPos.dir = Direction.Up;
                                grid[guardPos.x, guardPos.y] = '^';
                            } else
                            {
                                if (grid[guardPos.x - 1, guardPos.y] != 'X') total++;
                                grid[guardPos.x - 1, guardPos.y] = grid[guardPos.x, guardPos.y];
                                grid[guardPos.x, guardPos.y] = 'X';
                                guardPos.x--;
                            }
                            break;
                        case Direction.Right:
                            if (grid[guardPos.x + 1, guardPos.y] == '#')
                            {
                                guardPos.dir = Direction.Down;
                                grid[guardPos.x, guardPos.y] = 'v';
                            }
                            else
                            {
                                if (grid[guardPos.x + 1, guardPos.y] != 'X') total++;
                                grid[guardPos.x + 1, guardPos.y] = grid[guardPos.x, guardPos.y];
                                grid[guardPos.x, guardPos.y] = 'X';
                                guardPos.x++;
                            }
                            break;
                        case Direction.Up:
                            if (grid[guardPos.x, guardPos.y - 1] == '#')
                            {
                                guardPos.dir = Direction.Right;
                                grid[guardPos.x, guardPos.y] = '>';
                            }
                            else
                            {
                                if (grid[guardPos.x, guardPos.y - 1] != 'X') total++;
                                grid[guardPos.x, guardPos.y - 1] = grid[guardPos.x, guardPos.y];
                                grid[guardPos.x, guardPos.y] = 'X';
                                guardPos.y--;
                            }
                            break;
                        case Direction.Down:
                            if (grid[guardPos.x, guardPos.y + 1] == '#')
                            {
                                guardPos.dir = Direction.Left;
                                grid[guardPos.x, guardPos.y] = '<';
                            }
                            else
                            {
                                if (grid[guardPos.x, guardPos.y + 1] != 'X') total++;
                                grid[guardPos.x, guardPos.y + 1] = grid[guardPos.x, guardPos.y];
                                grid[guardPos.x, guardPos.y] = 'X';
                                guardPos.y++;
                            }
                            break;
                    }
                }
            } catch
            {
                // Gone off the grid
                grid[guardPos.x, guardPos.y] = 'X';
                total++;
            }

            PrintGrid(grid);
            Console.WriteLine(total);
        }

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");

            char[,] grid = new char[lines[0].Length, lines.Length];

            Point guardPos = new Point();
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    grid[x,y] = lines[y][x];

                    if (grid[x,y] == '^')
                    {
                        guardPos.x = x;
                        guardPos.y = y;
                        guardPos.dir = Direction.Up;
                    }
                }
            }
            Console.ReadKey();

            Part2(guardPos, grid);

            Console.ReadKey();
        }
    }
}
