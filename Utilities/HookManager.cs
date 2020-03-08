using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace IUL.Utilities {
    public class HookManager {
        public delegate int HookProc(int nCode, int wParam, IntPtr lParam);
        public enum HookType {
            WH_CALLWNDPROC = 4,
            WH_CALLWNDPROCRET = 12,
            WH_CBT = 5,
            WH_DEBUG = 9,
            WH_FOREGROUNDIDLE = 11,
            WH_GETMESSAGE = 3,
            WH_JOURNALPLAYBACK = 1,
            WH_JOURNALRECORD = 0,
            WH_KEYBOARD = 2,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE = 7,
            WH_MOUSE_LL = 14,
            WH_MSGFILTER = -1,
            WH_SHELL = 10,
            WH_SYSMSGFILTER = 6,
        }
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(HookType hook, HookProc callback, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);

        private Dictionary<string, IntPtr> LoadedHookListener;

        public HookManager() {
            LoadedHookListener = new Dictionary<string, IntPtr>();
        }

        ~HookManager() {
            foreach (IntPtr hookId in this.LoadedHookListener.Values) {
                UnhookWindowsHookEx(hookId);
            }
        }
        public bool AddHookListener(string listenerName, HookType hookType, HookProc hookProc) {
            IntPtr hookId = SetWindowsHookEx(hookType, hookProc, new IntPtr(0), 0);
            if (hookId.ToInt32() != 0) {
                this.LoadedHookListener.Add(
                listenerName,
                hookId
                );
            }
            if (hookId.ToInt32() != 0) return true;
            else return false;
        }

        public bool UnloadHookListener(string listenerName) {
            if (LoadedHookListener.ContainsKey(listenerName)) {
                return UnhookWindowsHookEx(LoadedHookListener[listenerName]);
            } else return false;
        }
    }
}
