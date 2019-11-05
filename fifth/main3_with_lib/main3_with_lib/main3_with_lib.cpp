#include <iostream>
#include "lib.h"
using namespace std;


int main()
{
	double a, b;
	int n, func, type;
	cout << "Function:" << endl << "1 - 1_1" << endl << "2 - 1_2" << endl << "3 - 2_1" << endl << "4 - 2_2" << endl;
	cin >> func;
	cout << "Type:" << endl << "1 - Right Rectangles" << endl << "2 - Left Rectagles" << endl << "3 - Central Rectangles" << endl << "4 - Trapezoid" << endl;
	cin >> type;
	cout << "a, b, n=";
	cin >> a >> b >> n;
	cout << solve(type, func, a, b, n);
	return 0;
}
