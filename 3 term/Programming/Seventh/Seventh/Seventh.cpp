// Seventh.cpp : Defines the entry point for the application.
//

#include "framework.h"
#include "Seventh.h"

#define MAX_LOADSTRING 100

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
void FillRectangle(int x1, int y1, int x2, int y2, int width, COLORREF color);
void DrawEllipse(int x1, int y1, int x2, int y2, int width, COLORREF color);
void FillEllipse(int x1, int y1, int x2, int y2, int width, COLORREF color);
void Fill(int x, int y, COLORREF color);

int lastX, lastY;

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

Modes mode = Modes::Fill;
int width = 5;
COLORREF color = RGB(255, 0, 0);
bool isDown = false;
HWND mainWindow;

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // TODO: Place code here.

    // Initialize global strings
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_SEVENTH, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Perform application initialization:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

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
            // TODO: Add any drawing code that uses hdc here...
            EndPaint(hWnd, &ps);
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
    isDown = true;
    lastX = x;
    lastY = y;
    if(mode == Modes::Pen)
    {
        DrawLine(x, y, x, y, width, color);
    }
    if (mode == Modes::Fill)
    {
        Fill(x, y, color);
    }
}

void OnMouseUp(int x, int y)
{
    if (isDown) 
    {
        if (mode == Modes::Rectangle)
        {
            DrawRectangle(lastX, lastY, x, y, width, color);
        }
        else if (mode == Modes::FillRectangle) 
        {
            FillRectangle(lastX, lastY, x, y, width, color);
        }
        else if (mode == Modes::Ellipse) 
        {
            DrawEllipse(lastX, lastY, x, y, width, color);
        }
        else if (mode == Modes::FillEllipse)
        {
            FillEllipse(lastX, lastY, x, y, width, color);
        }
        else if (mode == Modes::Line)
        {
            DrawLine(lastX, lastY, x, y, width, color);
        }
    }
    
    isDown = false;
}

void OnMouseMove(int x, int y) 
{
    if(mode == Modes::Pen)
    {
        if(isDown)
        {
            DrawLine(lastX, lastY, x, y, width, color);
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

void FillRectangle(int x1, int y1, int x2, int y2, int width, COLORREF color)
{
    HDC hdc = GetDC(mainWindow);
    HPEN pen = CreatePen(PS_SOLID, width, color);
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
    FloodFill(hdc, x, y, color);

    DeleteObject(brush);
    ReleaseDC(mainWindow, hdc);
}