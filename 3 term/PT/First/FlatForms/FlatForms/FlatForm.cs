using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticAlgorithms
{
    public partial class FlatForm : Form
    {
        bool isDown;
        int lastPosY, lastPosX;
        public FlatForm()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SystemPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
        }

        private void SystemPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(!isDown)
            {
                return;
            }
            int deltaX = e.X - lastPosX;
            int deltaY = e.Y - lastPosY;
            Location = new Point(Location.X + deltaX, Location.Y + deltaY);
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void SystemPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            lastPosY = e.Y;
            lastPosX = e.X;
        }
    }
}
