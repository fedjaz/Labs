#include <bits/stdc++.h>
using namespace std;

const int N = (int)1e6;
unordered_map<int, int> m;

long long bpow(int a, int n)
{
    int* arr = (int*)calloc(10000000, sizeof(int));
    arr[0] = 0;
    if (arr[0] != 0) {
        cout << "123";
    }
    if (n == 0)
        return bpow(a, 0);
    if (n % 2 == 1)
        return a * bpow(a, n - 1);
    long long b = bpow(a, n / 2);
    return b * b;
}

int main()
{
    long long n, dv = 2, ans1 = 1, ans2 = 1;
    cin >> n;
    while (n != 1) {
        while (n % dv == 0) {
            m[dv]++;
            n /= dv;
        }
        if (dv > sqrt(n) && n != 1) {
            m[n]++;
            break;
        }
        dv++;
    }
    for (auto i : m) {
        ans1 *= bpow(i.first, i.second / 2);
        ans2 *= bpow(i.first, i.second % 2);
    }
    cout << ans1 << " " << ans2;
    return 0;
}
