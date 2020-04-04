// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <algorithm>

using namespace std;

void dfs(int** v, bool** used, int y, int x, int sizeY, int sizeX)
{
    used[y][x] = true;
    int directions[4] = { 1, 2, 3, 4 };
    for (int i = 0; i < 24; i++) {
        int a = rand() % 4, b = rand() % 4;
        swap(directions[a], directions[b]);
    }
    for (int i = 0; i < 4; i++) {
        if (directions[i] == 1 && y > 0 && !used[y - 1][x])
        {
            v[y][x] += 1;
            v[y - 1][x] += 2;
            dfs(v, used, y - 1, x, sizeY, sizeX);
        }

        if (directions[i] == 2 && y < sizeY - 1 && !used[y + 1][x])
        {
            v[y][x] += 2;
            v[y + 1][x] += 1;
            dfs(v, used, y + 1, x, sizeY, sizeX);
        }

        if (directions[i] == 3 && x > 0 && !used[y][x - 1])
        {
            v[y][x] += 4;
            v[y][x - 1] += 8;
            dfs(v, used, y, x - 1, sizeY, sizeX);
        }

        if (directions[i] == 4 && x < sizeX - 1 && !used[y][x + 1])
        {
            v[y][x] += 8;
            v[y][x + 1] += 4;
            dfs(v, used, y, x + 1, sizeY, sizeX);
        }
    }
}

extern "C" __declspec(dllexport) int ** _cdecl generate(int sizeY, int sizeX, int seed)
{
    srand(seed);
    int** arr = new int* [sizeY];
    bool** used = new bool* [sizeY];
    for (int i = 0; i < sizeY; i++)
    {
        arr[i] = new int[sizeX];
        used[i] = new bool[sizeX];
        memset(arr[i], 0, sizeof(int) * sizeX);
        memset(used[i], 0, sizeof(bool) * sizeX);
    }
    dfs(arr, used, 0, 0, sizeY, sizeX);
    return arr;
}


BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

