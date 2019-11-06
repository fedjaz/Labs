// StaticLib1.cpp : Defines the functions for the static library.
//

#include "pch.h"
#include "framework.h"
#include <math.h>
#define sqr(x) ((x) * (x))

double (*f)(double);
double f1(double x) {

	//1_1 0.8 2.4
	return sqrt(1.5 * sqr(x) + 2.3) / (3 + sqrt(0.3 * x + 1));
}

double f2(double x) {
	//1_2 1.4 2.2
	return 1 / (sqrt(3 * sqr(x) + 1));
}

double f3(double x) {
	//2_1 0.3 1.1
	return cos(0.3 * x + 0.5) / (1.8 + sin(sqr(x) + 0.8));
}

double f4(double x) {
	//2_2 1.4 2.2
	return log10(sqr(x) + 2) / (x + 1);
}
double leftRectangles(double a, double b, int n) {
	double step = (b - a) / n, ans = 0;
	for (int i = 0; i < n; i++) {
		ans += (*f)(a + step * i) * step;
	}
	return ans;
}

double rightRectangles(double a, double b, int n) {
	double step = (b - a) / n, ans = 0;
	for (int i = 0; i < n; i++) {
		ans += (*f)(a + step * (i + 1)) * step;
	}
	return ans;
}

double centralRectangles(double a, double b, int n) {
	double step = (b - a) / n, ans = 0;
	for (int i = 0; i < n; i++) {
		ans += (*f)(a + step * (i + 0.5)) * step;
	}
	return ans;
}

double trapezoid(double a, double b, int n) {
	double step = (b - a) / n, ans = 0;
	for (int i = 0; i < n; i++) {
		double fx1 = (*f)(a + step * i), fx2 = (*f)(a + step * (i + 1));

		ans += step * fx1 + (step * (fx2 - fx1)) / 2;
	}
	return ans;
}

double solve(int type, int func, double a, double b, int n)
{
	switch (func)
	{
	case 1:
		f = f1;
		break;
	case 2:
		f = f2;
		break;
	case 3:
		f = f3;
		break;
	case 4:
		f = f4;
		break;
	default:
		return 0;
	}
	switch (type)
	{
	case 1:
		return leftRectangles(a, b, n);
		break;
	case 2:
		return rightRectangles(a, b, n);
		break;
	case 3:
		return centralRectangles(a, b, n);
		break;
	case 4:
		return trapezoid(a, b, n);
		break;
	default:
		return 0;
	}
}
