using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace xcore.Area.Test.Service
{
    /// <summary>
    /// 启动
    /// </summary>
   public delegate void HookStart();

    public delegate void HookStop();
    public class DllService
    {
        [DllImport("dllx.dll")]
        extern static public void HookStart();
        [DllImport("dllx.dll")]
        extern static public void HookStop();
        [DllImport("kernel32")]
         extern static public IntPtr LoadLibraryA(string dllName);
        [DllImport("kernel32")]
        extern static public IntPtr GetProcAddress(IntPtr dll,string funcName);
        [DllImport("kernel32")]
        extern static public IntPtr FreeLibrary(IntPtr intPtr);

        public static HookStart getHookStart(IntPtr intPtr)
        {
           return (HookStart)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(HookStart));
        }

        public static HookStop getHookStop(IntPtr intPtr)
        {
            return (HookStop)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(HookStop));
        }

        public void injectDll()
        {
            var d = DllService.LoadLibraryA("dllx.dll");
            var hookStartIntptr = DllService.GetProcAddress(d, "HookStart");
            HookStart hookStart = DllService.getHookStart(hookStartIntptr);
            var hookStopIntptr = DllService.GetProcAddress(d, "HookStart");
            HookStop hookStop = (HookStop)DllService.getHookStop(hookStopIntptr);

            hookStart();

            while (
                Console.ReadKey().KeyChar == 'q')
            {

            }
            hookStop();
            DllService.FreeLibrary(d);
            Console.WriteLine("卸载");
        }
    }
}
