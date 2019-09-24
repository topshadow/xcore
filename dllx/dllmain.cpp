// dllmain.cpp : 定义 DLL 应用程序的入口点。
#include "pch.h"
#include "windows.h"
#include "stdio.h"
#define PROCESS_NAME "YoudaoDict.exe"
HINSTANCE g_hInstance=NULL;
HHOOK g_hHook = NULL;
HWND g_hWnd = NULL;



BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
		g_hInstance =hModule;
		break;
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

LRESULT CALLBACK KeyboardProc(int nCode,WPARAM wParam,LPARAM lParam) {
	char szPath[MAX_PATH] = { 0, };
	char* p = NULL;
	if (nCode == 0) {
		if (!(lParam & 0x80000000)) {
			GetModuleFileNameA(NULL, szPath, MAX_PATH);
			p = strrchr(szPath, '\\');
			if (!_stricmp(p + 1, PROCESS_NAME) ) {
				return 1;
			}
		}
	}
	return CallNextHookEx(g_hHook, nCode, wParam, lParam);
}
#ifdef __cplusplus
extern "C"{
#endif
 _declspec(dllexport)  void HookStart() {
	 g_hHook=SetWindowsHookEx(WH_KEYBOARD, KeyboardProc, g_hInstance, 0);
}


 _declspec(dllexport)  void HookStop() {

		 UnhookWindowsHookEx(g_hHook);
		 g_hHook = NULL;

 }
 



#ifdef  __cplusplus
}
#endif


