using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Generator
{
    class Drawer
    {
        public static Bitmap Draw(Maze maze, float maxResolution)
        {
            float cellSize = maxResolution / Math.Max(maze.SizeX, maze.SizeY);
            Bitmap bitmap = new Bitmap((int)(maze.SizeX * cellSize), 
                                       (int)(maze.SizeY * cellSize));
            using(Graphics g = Graphics.FromImage(bitmap))
            {
                float wallWidth = cellSize * 0.15f;
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, 
                                bitmap.Size.Width, 
                                bitmap.Size.Height);
                g.FillRectangle(new SolidBrush(Color.Green), 0, 0,
                                cellSize - wallWidth / 2, 
                                cellSize - wallWidth / 2);
                g.FillRectangle(new SolidBrush(Color.Red),
                                cellSize * (maze.SizeX - 1) + wallWidth / 2, 
                                cellSize * (maze.SizeY - 1) + wallWidth / 2, 
                                cellSize,
                                cellSize);
                Pen p = new Pen(Color.Black, wallWidth);
                p.Width *= 2;
                g.DrawRectangle(p, 0, 0, bitmap.Size.Width, bitmap.Size.Height);
                p.Width /= 2;

                for(int i = 0; i < maze.SizeY; i++)
                {
                    for(int j = 0; j < maze.SizeX; j++)
                    {
                        g.FillRectangle(new SolidBrush(Color.Black),
                                        j * cellSize - wallWidth / 2,
                                        i * cellSize - wallWidth / 2,
                                        wallWidth,
                                        wallWidth);

                        if(!maze[i, j].Down)
                        {
                            g.DrawLine(p, 
                                       new PointF(j * cellSize, (i + 1) * cellSize),
                                       new PointF((j + 1) * cellSize, (i + 1) * cellSize));
                        }
                        if(!maze[i, j].Right)
                        {
                            g.DrawLine(p, 
                                       new PointF((j + 1) * cellSize, i * cellSize),
                                       new PointF((j + 1) * cellSize, (i + 1) * cellSize));
                        }
                    }
                }
            }
            return bitmap;
        }

        public static Bitmap DrawSolution(Maze maze, Bitmap bitmap)
        {
            bitmap = (Bitmap)bitmap.Clone();
            float cellSize = bitmap.Width / maze.SizeX;
            List<Maze.Directions> directions = maze.Solve();
            List<PointF> points = new List<PointF>();
            points.Add(new PointF(cellSize / 2, cellSize / 2));
            foreach(Maze.Directions dir in directions)
            {
                if(dir == Maze.Directions.Up)
                {
                    points.Add(new PointF(points.Last().X, 
                                          points.Last().Y - cellSize));
                }
                if(dir == Maze.Directions.Right)
                {
                    points.Add(new PointF(points.Last().X + cellSize, 
                                          points.Last().Y));
                }
                if(dir == Maze.Directions.Down)
                {
                    points.Add(new PointF(points.Last().X, 
                                          points.Last().Y + cellSize));
                }
                if(dir == Maze.Directions.Left)
                {
                    points.Add(new PointF(points.Last().X - cellSize, 
                                          points.Last().Y));
                }
            }
            using(Graphics g = Graphics.FromImage(bitmap))
            {
                float wallWidth = cellSize * 0.15f;
                Pen p = new Pen(Color.Red, wallWidth);
                g.DrawLines(p, points.ToArray());
            }
            return bitmap;
        }
    }
}
