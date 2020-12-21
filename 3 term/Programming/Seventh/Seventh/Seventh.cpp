// Seventh.cpp : Defines the entry point for the application.
//

using namespace std;
#include "framework.h"
#include "Seventh.h"
#include <algorithm>
#include <vector>
#include <queue>


#define MAX_LOADSTRING 100
#define ComboBox1 10001
#define ComboBox2 10002
#define ComboBox3 10003

bool used[3000][3000];
bool isAdded[3000][3000];

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
void OnMouseDown(int x, int y);
void OnMouseUp(int x, int y);
void OnMouseMove(int x, int y);
void DrawLine(int x1, int y1, int x2, int y2, int width, COLORREF color);
void DrawRectangle(int x1, int y1, int x2, int y2, int width, COLORREF color);
void FillRectangle(int x1, int y1, int x2, int y2, COLORREF color);
void DrawEllipse(int x1, int y1, int x2, int y2, int width, COLORREF color);
void FillEllipse(int x1, int y1, int x2, int y2, int width, COLORREF color);
void Fill(int x, int y, COLORREF color);
void Gradient(int x, int y);
int GetWindowSizeX(HWND window);
int GetWindowSizeY(HWND window);
void RegisterModeBox();
void RegisterWidthBox();
void RegisterColorBox();
void Repaint();

int lastX, lastY;

HWND hWndComboBox1;
HWND hWndComboBox2;
HWND hWndComboBox3;
bool movedObject = false;

enum class Modes
{
    Pen,
    Line,
    Ellipse,
    Rectangle,
    FillEllipse,
    FillRectangle,
    Fill,
    Eraser,
    Gradient
};

Modes mode = Modes::Pen;
int width = 1;
COLORREF curColor = RGB(0, 0, 0);
bool isDown = false;
HWND mainWindow;

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);


    // Initialize global strings
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_SEVENTH, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Perform application initialization:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    RegisterModeBox();
    RegisterWidthBox();
    RegisterColorBox();

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_SEVENTH));

    MSG msg;


    // Main message loop:
    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }

    return (int) msg.wParam;
}



//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_SEVENTH));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_SEVENTH);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // Store instance handle in our global variable

   HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);
   mainWindow = hWnd;

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE: Processes messages for the main window.
//
//  WM_COMMAND  - process the application menu
//  WM_PAINT    - Paint the main window
//  WM_DESTROY  - post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
    case WM_COMMAND:
        {
            if (HIWORD(wParam) == CBN_SELCHANGE)
            {
                int elem = LOWORD(wParam);
                int id;
                switch (elem)
                {
                case ComboBox1:
                    id = SendMessage(hWndComboBox1, (UINT)CB_GETCURSEL, (WPARAM)0, (LPARAM)0);
                    switch (id)
                    {
                    case 0:
                        mode = Modes::Pen;
                        break;
                    case 1:
                        mode = Modes::Line;
                        break;
                    case 2:
                        mode = Modes::Rectangle;
                        break;
                    case 3:
                        mode = Modes::FillRectangle;
                        break;
                    case 4:
                        mode = Modes::Ellipse;
                        break;
                    case 5:
                        mode = Modes::FillEllipse;
                        break;
                    case 6:
                        mode = Modes::Fill;
                        break;
                    case 7:
                        mode = Modes::Eraser;
                        break;
                    case 8:
                        mode = Modes::Gradient;
                        break;
                    }
                case ComboBox2:
                    id = SendMessage(hWndComboBox2, (UINT)CB_GETCURSEL, (WPARAM)0, (LPARAM)0);
                    switch (id)
                    {
                    case 0:
                        width = 1;
                        break;
                    case 1:
                        width = 2;
                        break;
                    case 2:
                        width = 3;
                        break;
                    case 3:
                        width = 5;
                        break;
                    case 4:
                        width = 10;
                        break;
                    case 5:
                        width = 25;
                        break;
                    case 6:
                        width = 50;
                        break;
                    }
                case ComboBox3:
                    id = SendMessage(hWndComboBox3, (UINT)CB_GETCURSEL, (WPARAM)0, (LPARAM)0);
                    switch(id)
                    {
                    case 0:
                        curColor = RGB(0, 0, 0);
                        break;
                    case 1:
                        curColor = RGB(255, 0, 0);
                        break;
                    case 2:
                        curColor = RGB(0, 255, 0);
                        break;
                    case 3:
                        curColor = RGB(0, 0, 255);
                        break;
                    case 4:
                        curColor = RGB(255, 255, 0);
                        break;
                    case 5:
                        curColor = RGB(127, 127, 127);
                        break;
                    case 6:
                        curColor = RGB(255, 127, 0);
                        break;
                    case 7:
                        curColor = RGB(255, 0, 255);
                        break;
                    case 8:
                        curColor = RGB(127, 0, 255);
                        break;
                    }
                }
            }
            int wmId = LOWORD(wParam);
            // Parse the menu selections:
            switch (wmId)
            {
            case IDM_ABOUT:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                break;
            case IDM_EXIT:
                DestroyWindow(hWnd);
                break;
            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;
    case WM_PAINT:
        {
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hWnd, &ps);
            
            EndPaint(hWnd, &ps);
        }
        break;
    case WM_SIZE:
        {
            Repaint();
        }
        break;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    case WM_LBUTTONDOWN:
        OnMouseDown(LOWORD(lParam), HIWORD(lParam));
        break;
    case WM_LBUTTONUP:
        OnMouseUp(LOWORD(lParam), HIWORD(lParam));
        break;
    case WM_MOUSEMOVE:
        OnMouseMove(LOWORD(lParam), HIWORD(lParam));
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

// Message handler for about box.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    UNREFERENCED_PARAMETER(lParam);
    switch (message)
    {
    case WM_INITDIALOG:
        return (INT_PTR)TRUE;

    case WM_COMMAND:
        if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
        {
            EndDialog(hDlg, LOWORD(wParam));
            return (INT_PTR)TRUE;
        }
        break;
    }
    return (INT_PTR)FALSE;
}

void OnMouseDown(int x, int y) 
{
    if (x >= GetWindowSizeX(mainWindow) - 200) 
    {
        return;
    }
    isDown = true;
    lastX = x;
    lastY = y;
    if(mode == Modes::Pen)
    {
        DrawLine(x, y, x, y, width, curColor);
    }
    else if (mode == Modes::Eraser) 
    {
        DrawLine(x, y, x, y, width, RGB(255, 255, 255));
    }
    else if (mode == Modes::Fill)
    {
        Fill(x, y, curColor);
    }
    else if (mode == Modes::Gradient) {
        Gradient(x, y);
    }
    Repaint();
}

void OnMouseUp(int x, int y)
{
    if (x >= GetWindowSizeX(mainWindow) - 200)
    {
        isDown = false;
        return;
    }
    if (isDown) 
    {
        if (mode == Modes::Rectangle)
        {
            DrawRectangle(lastX, lastY, x, y, width, curColor);
        }
        else if (mode == Modes::FillRectangle) 
        {
            FillRectangle(lastX, lastY, x, y, curColor);
        }
        else if (mode == Modes::Ellipse) 
        {
            DrawEllipse(lastX, lastY, x, y, width, curColor);
        }
        else if (mode == Modes::FillEllipse)
        {
            FillEllipse(lastX, lastY, x, y, width, curColor);
        }
        else if (mode == Modes::Line)
        {
            DrawLine(lastX, lastY, x, y, width, curColor);
        }
    }
    isDown = false;
    Repaint();
}

void OnMouseMove(int x, int y) 
{
    if (x >= GetWindowSizeX(mainWindow) - 200)
    {
        return;
    }
    if(mode == Modes::Pen)
    {
        if(isDown)
        {
            DrawLine(lastX, lastY, x, y, width, curColor);
            lastX = x;
            lastY = y;
        }
    }
    else if (mode == Modes::Eraser) 
    {
        if (isDown)
        {
            DrawLine(lastX, lastY, x, y, width, RGB(255, 255, 255));
            lastX = x;
            lastY = y;
        }
    }
}

void DrawLine(int x1, int y1, int x2, int y2, int width, COLORREF color)
{
    HDC hdc = GetDC(mainWindow);
    HPEN pen = CreatePen(PS_SOLID, width, color);
    SelectObject(hdc, pen);
    MoveToEx(hdc, x1, y1, nullptr);
    LineTo(hdc, x2, y2);
    DeleteObject(pen);
    ReleaseDC(mainWindow, hdc);
}

void DrawRectangle(int x1, int y1, int x2, int y2, int width, COLORREF color)
{
    HDC hdc = GetDC(mainWindow);
    HPEN pen = CreatePen(PS_SOLID, width, color);

    SelectObject(hdc, pen);
    SelectObject(hdc, GetStockObject(HOLLOW_BRUSH));
    Rectangle(hdc, x1, y1, x2, y2);

    DeleteObject(pen);
    ReleaseDC(mainWindow, hdc);
}

void FillRectangle(int x1, int y1, int x2, int y2, COLORREF color)
{
    HDC hdc = GetDC(mainWindow);
    HPEN pen = CreatePen(PS_SOLID, 1, color);
    HBRUSH brush = CreateSolidBrush(color);

    SelectObject(hdc, pen);
    SelectObject(hdc, brush);
    Rectangle(hdc, x1, y1, x2, y2);

    DeleteObject(pen);
    DeleteObject(brush);
    ReleaseDC(mainWindow, hdc);
}

void DrawEllipse(int x1, int y1, int x2, int y2, int width, COLORREF color) 
{
    HDC hdc = GetDC(mainWindow);
    HPEN pen = CreatePen(PS_SOLID, width, color);

    SelectObject(hdc, pen);
    SelectObject(hdc, GetStockObject(HOLLOW_BRUSH));
    Ellipse(hdc, x1, y1, x2, y2);

    DeleteObject(pen);
    ReleaseDC(mainWindow, hdc);
}

void FillEllipse(int x1, int y1, int x2, int y2, int width, COLORREF color)
{
    HDC hdc = GetDC(mainWindow);
    HPEN pen = CreatePen(PS_SOLID, width, color);
    HBRUSH brush = CreateSolidBrush(color);

    SelectObject(hdc, pen);
    SelectObject(hdc, brush);
    Ellipse(hdc, x1, y1, x2, y2);

    DeleteObject(pen);
    DeleteObject(brush);
    ReleaseDC(mainWindow, hdc);
}

void Fill(int x, int y, COLORREF color)
{
    HDC hdc = GetDC(mainWindow);
    HBRUSH brush = CreateSolidBrush(color);

    SelectObject(hdc, brush);
    ExtFloodFill(hdc, x, y, GetPixel(hdc, x, y), FLOODFILLSURFACE);

    DeleteObject(brush);
    ReleaseDC(mainWindow, hdc);
}

void Gradient(int x, int y) 
{
	memset(used, 0, sizeof(bool) * 3000 * 3000);
	memset(isAdded, 0, sizeof(bool) * 3000 * 3000);
	HDC hdc = GetDC(mainWindow);
	queue<pair<int, int> > q;
	vector<pair<int, int> > v;
	q.push(make_pair(x, y));
	while (!q.empty()) {
		v.push_back(q.front());
		x = q.front().first;
		y = q.front().second;
		used[x][y] = 1;
		COLORREF color = GetPixel(hdc, x, y);
		
		COLORREF negative = RGB(255 - GetRValue(color), 255 - GetGValue(color), 255 - GetBValue(color));
		if (x - 1 > 0 && !used[x-1][y] && !isAdded[x-1][y]) {
			COLORREF subcolor = GetPixel(hdc, x - 1, y);
			if (color == subcolor) {
				q.push(make_pair(x - 1, y));
				isAdded[x - 1][y] = true;
			}
		}
		if (x + 1 < GetWindowSizeX(mainWindow) && !used[x + 1][y] && !isAdded[x + 1][y]) {
			COLORREF subcolor = GetPixel(hdc, x + 1, y);
			if (color == subcolor) {
				q.push(make_pair(x + 1, y));
				isAdded[x + 1][y] = true;
			}
		}
		if (y - 1 > 0 && !used[x][y - 1] && !isAdded[x][y - 1]) {
			COLORREF subcolor = GetPixel(hdc, x, y - 1);
			if (color == subcolor) {
				q.push(make_pair(x, y - 1));
				isAdded[x][y - 1] = true;
			}
		}
		if (y + 1 < GetWindowSizeY(mainWindow) && !used[x][y + 1] && !isAdded[x][y + 1]) {
			COLORREF subcolor = GetPixel(hdc, x, y + 1);
			if (color == subcolor) {
				q.push(make_pair(x, y + 1));
				isAdded[x][y + 1] = true;
			}
		}
		SetPixel(hdc, x, y, negative);
		q.pop();
	}
	sort(v.begin(), v.end());
	int minX = v[0].first;
	for (int i = 0; i < v.size(); i++) {
		x = v[i].first;
		y = v[i].second;
		if ((x - minX) % 64 < 32) {
			int green = 8 * ((x - minX) % 64);
			int blue = 255 - green;
			SetPixel(hdc, x, y, RGB(0, green, blue));
		}
		else {
			int blue = 8 * (((x - minX) % 64) - 32);
			int green = 255 - blue;
			SetPixel(hdc, x, y, RGB(0, green, blue));
		}
	}
}

int GetWindowSizeX(HWND window)
{
    RECT* windowRect = new RECT;
    GetWindowRect(window, windowRect);
    return windowRect->right - windowRect->left;
}

int GetWindowSizeY(HWND window)
{
	RECT* windowRect = new RECT;
	GetWindowRect(window, windowRect);
	return windowRect->bottom - windowRect->top;
}

void RegisterModeBox() 
{
    int sizeX = GetWindowSizeX(mainWindow);
    hWndComboBox1 = CreateWindow(L"COMBOBOX", NULL, WS_VISIBLE | WS_CHILD | CBS_DROPDOWN, sizeX - 200 + 5, 20, 180, 500, mainWindow, (HMENU)ComboBox1,
        (HINSTANCE)GetWindowLong(mainWindow, NULL), NULL);

    SendMessage(hWndComboBox1, CB_ADDSTRING, 0, (LPARAM)L"Pen");
    SendMessage(hWndComboBox1, CB_ADDSTRING, 0, (LPARAM)L"Line");
    SendMessage(hWndComboBox1, CB_ADDSTRING, 0, (LPARAM)L"Rectangle");
    SendMessage(hWndComboBox1, CB_ADDSTRING, 0, (LPARAM)L"Fill rectangle");
    SendMessage(hWndComboBox1, CB_ADDSTRING, 0, (LPARAM)L"Ellipse");
    SendMessage(hWndComboBox1, CB_ADDSTRING, 0, (LPARAM)L"Fill ellipse");
    SendMessage(hWndComboBox1, CB_ADDSTRING, 0, (LPARAM)L"Fill");
    SendMessage(hWndComboBox1, CB_ADDSTRING, 0, (LPARAM)L"Eraser"); 
    SendMessage(hWndComboBox1, CB_ADDSTRING, 0, (LPARAM)L"Gradient");

    SendMessage(hWndComboBox1, CB_SETCURSEL, (WPARAM)0, (LPARAM)0);
}

void RegisterWidthBox()
{
    int sizeX = GetWindowSizeX(mainWindow);
    hWndComboBox2 = CreateWindow(L"COMBOBOX", NULL, WS_VISIBLE | WS_CHILD | CBS_DROPDOWN, sizeX - 200 + 5, 60, 180, 500, mainWindow, (HMENU)ComboBox1,
        (HINSTANCE)GetWindowLong(mainWindow, NULL), NULL);

    SendMessage(hWndComboBox2, CB_ADDSTRING, 0, (LPARAM)L"1");
    SendMessage(hWndComboBox2, CB_ADDSTRING, 0, (LPARAM)L"2");
    SendMessage(hWndComboBox2, CB_ADDSTRING, 0, (LPARAM)L"3");
    SendMessage(hWndComboBox2, CB_ADDSTRING, 0, (LPARAM)L"5");
    SendMessage(hWndComboBox2, CB_ADDSTRING, 0, (LPARAM)L"10");
    SendMessage(hWndComboBox2, CB_ADDSTRING, 0, (LPARAM)L"25");
    SendMessage(hWndComboBox2, CB_ADDSTRING, 0, (LPARAM)L"50");

    SendMessage(hWndComboBox2, CB_SETCURSEL, (WPARAM)0, (LPARAM)0);
}

void RegisterColorBox()
{
    int sizeX = GetWindowSizeX(mainWindow);
    hWndComboBox3 = CreateWindow(L"COMBOBOX", NULL, WS_VISIBLE | WS_CHILD | CBS_DROPDOWN, sizeX - 200 + 5, 100, 180, 500, mainWindow, (HMENU)ComboBox1,
        (HINSTANCE)GetWindowLong(mainWindow, NULL), NULL);

    SendMessage(hWndComboBox3, CB_ADDSTRING, 0, (LPARAM)L"Black");
    SendMessage(hWndComboBox3, CB_ADDSTRING, 0, (LPARAM)L"Red");
    SendMessage(hWndComboBox3, CB_ADDSTRING, 0, (LPARAM)L"Green");
    SendMessage(hWndComboBox3, CB_ADDSTRING, 0, (LPARAM)L"Blue");
    SendMessage(hWndComboBox3, CB_ADDSTRING, 0, (LPARAM)L"Yellow");
    SendMessage(hWndComboBox3, CB_ADDSTRING, 0, (LPARAM)L"Gray");
    SendMessage(hWndComboBox3, CB_ADDSTRING, 0, (LPARAM)L"Orange");
    SendMessage(hWndComboBox3, CB_ADDSTRING, 0, (LPARAM)L"Pink");
    SendMessage(hWndComboBox3, CB_ADDSTRING, 0, (LPARAM)L"Violet");

    SendMessage(hWndComboBox3, CB_SETCURSEL, (WPARAM)0, (LPARAM)0);
}

void Repaint() 
{
    RECT* window = new RECT;
    GetWindowRect(mainWindow, window);
    window->bottom -= window->top;
    window->right -= window->left;
    window->top = 0;
    window->left = 0;
    int x1 = window->right - 200;
    /*int y1 = 0;
    int x2 = window->right;
    int y2 = window->bottom;
    FillRectangle(x1, y1, x2, y2, RGB(255, 221, 105));*/

    MoveWindow(hWndComboBox1, x1 + 5, 20, 180, 300, false);
    MoveWindow(hWndComboBox2, x1 + 5, 60, 180, 300, false);
    MoveWindow(hWndComboBox3, x1 + 5, 100, 180, 300, false);
}