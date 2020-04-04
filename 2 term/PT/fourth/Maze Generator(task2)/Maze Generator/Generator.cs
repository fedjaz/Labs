using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Generator
{
    class Generator
    {
        [DllImport("Maze Generator DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr generate(int sizeY, int sizeX, int seed);
        public static Maze Generate(int sizeY, int sizeX, int seed)
        {
            IntPtr pointer = IntPtr.Zero;
            Thread dfsThread = new Thread(new ThreadStart(() => pointer = generate(sizeY,
                                                                                   sizeX,
                                                                                   seed)),
                                                         75 * sizeX * sizeY  + 1024 * 1024);
            dfsThread.Start();
            dfsThread.Join();
            IntPtr[] pointers = new IntPtr[sizeY];
            Marshal.Copy(pointer, pointers, 0, sizeY);
            int[][] directions = new int[sizeY][];
            for(int i = 0; i < sizeY; i++)
            {
                int[] row = new int[sizeX];
                Marshal.Copy(pointers[i], row, 0, sizeX);
                directions[i] = row;
            }

            Maze maze = new Maze(sizeY, sizeX);
            for(int i = 0; i < sizeY; i++)
            {
                for(int j = 0; j < sizeX; j++)
                {
                    int direction = directions[i][j];
                    if(direction % 2 == 1)
                        maze[i, j].Up = true;
                    direction /= 2;
                    if(direction % 2 == 1)
                        maze[i, j].Down = true;
                    direction /= 2;
                    if(direction % 2 == 1)
                        maze[i, j].Left = true;
                    direction /= 2;
                    if(direction % 2 == 1)
                        maze[i, j].Right = true;
                }
            }
            return maze;
        }
    }
}
