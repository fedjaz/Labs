using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Generator
{
    public class Maze
    {
        public enum Directions
        {
            Up,
            Down,
            Left,
            Right
        }
        public int SizeX { get => cells.GetLength(1); }
        public int SizeY { get => cells.GetLength(0); }
        Cell[,] cells;

        public Maze(int sizeY, int sizeX)
        {
            cells = new Cell[sizeY, sizeX];
            for(int i = 0; i < sizeY; i++)
            {
                for(int j = 0; j < sizeX; j++)
                {
                    cells[i, j] = new Cell();
                }
            }
        }

        public Cell this[int i, int j]
        {
            get => cells[i, j];
            set => cells[i, j] = value;
        }

        public List<Directions> Solve()
        {
            return Solve(this);
        }

        public static List<Directions> Solve(Maze maze)
        {
            List<Directions> ans = new List<Directions>();
            int[,] solution = Bfs(maze);
            int y = maze.SizeY - 1, x = maze.SizeX - 1;
            while(!(x == 0 && y == 0))
            {
                Directions direction = Directions.Up;
                if(maze[y, x].Up && solution[y - 1, x] < solution[y, x])
                {
                    direction = Directions.Down;
                    y--;
                }
                else if(maze[y, x].Right && solution[y, x + 1] < solution[y, x])
                {
                    direction = Directions.Left;
                    x++;
                }
                else if(maze[y, x].Down && solution[y + 1, x] < solution[y, x])
                {
                    direction = Directions.Up;
                    y++;
                }
                else if(maze[y, x].Left && solution[y, x - 1] < solution[y, x])
                {
                    direction = Directions.Right;
                    x--;
                }
                ans.Add(direction);
            }
            ans.Reverse();
            return ans;
        }

        static int[,] Bfs(Maze maze)
        {
            int[,] solution = new int[maze.SizeY, maze.SizeX];
            bool[,] used = new bool[maze.SizeY, maze.SizeX];
            Queue<Tuple<int, int>> cells = new Queue<Tuple<int, int>>();
            cells.Enqueue(new Tuple<int, int>(0, 0));
            while(cells.Count > 0)
            {
                Tuple<int, int> cell = cells.Dequeue();
                int y = cell.Item1, x = cell.Item2;
                used[y, x] = true;
                if(maze[y, x].Down && !used[y + 1, x])
                {
                    solution[y + 1, x] = solution[y, x] + 1;
                    cells.Enqueue(new Tuple<int, int>(y + 1, x));
                }
                if(maze[y, x].Up && !used[y - 1, x])
                {
                    solution[y - 1, x] = solution[y, x] + 1;
                    cells.Enqueue(new Tuple<int, int>(y - 1, x));
                }
                if(maze[y, x].Left && !used[y, x - 1])
                {
                    solution[y, x - 1] = solution[y, x] + 1;
                    cells.Enqueue(new Tuple<int, int>(y, x - 1));
                }
                if(maze[y, x].Right && !used[y, x + 1])
                {
                    solution[y, x + 1] = solution[y, x] + 1;
                    cells.Enqueue(new Tuple<int, int>(y, x + 1));
                }

            }
            return solution;
        }
    }
}
