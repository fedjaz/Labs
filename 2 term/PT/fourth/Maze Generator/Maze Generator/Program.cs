using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Maze_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            int sizeY, sizeX, maxResolution;
            Console.WriteLine("Enter y size of maze(max 5000):");
            sizeY = GetInt(0, 5000);
            Console.WriteLine("Enter x size of maze(max 5000):");
            sizeX = GetInt(0, 5000);
            Console.WriteLine("Enter maximum resulution of an output image(max 10000, 1000+ recommended):");
            maxResolution = GetInt(10, 10000);
            Random r = new Random();
            Maze maze = Generator.Generate(sizeY, sizeX, r.Next());
            Bitmap mazeBitmap = Drawer.Draw(maze, maxResolution);
            Bitmap solution = Drawer.DrawSolution(maze, mazeBitmap);
            Save(mazeBitmap, solution);
        }

        static int GetInt(int lower, int upper)
        {
            while(true)
            {
                int output;
                string s = Console.ReadLine().Trim().Split(' ')[0];
                if(int.TryParse(s, out output) && output >= lower && output <= upper)
                {
                    return output;
                }
                Console.WriteLine("Input is incorrect, try again");
            }
        }

        static void Save(Bitmap maze, Bitmap solution)
        {
            while(true)
            {
                Console.WriteLine("Enter path to save image of maze(leave empty to place in the same directory as .exe):");
                string savePath = Console.ReadLine();
                savePath = savePath.Trim(' ');
                savePath = savePath.Trim('\"');
                if(savePath != "" && savePath.Last() != '\\')
                {
                    savePath += "\\";
                }
                try
                {
                    maze.Save(savePath + "output.png");
                    solution.Save(savePath + "solution.png");
                }
                catch
                {
                    Console.WriteLine("Wrong path, try again");
                    continue;
                }
                break;
            }
        }
    }
}
