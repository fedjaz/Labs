#include <bits/stdc++.h>
using namespace std;
typedef long long ll;
typedef long double ld;
typedef unsigned long long ull;
typedef pair<int, int> pii;
typedef pair<ll, ll> pll;
typedef unordered_map<int, int> umii;
typedef unordered_map<int, bool> umib;
typedef priority_queue<int> pqi;
const int inf=INT_MAX;
const ll INF=LLONG_MAX;
const int mod=(int)1e9+7;
ll bpow(int a, int n){if(n==0) return 1; if(n%2==1) return a*bpow(a, n-1); ll b=bpow(a, n/2); return b*b;}
ll bpowMod(int a, int n){if(n==0) return 1; if(n%2==1) return a*bpowMod(a, n-1)%mod; ll b=bpowMod(a, n/2)%mod; return b*b%mod;}
#define fin freopen("input.txt", "r", stdin)
#define fout freopen("output.txt", "w", stdout)
#define fall fin; fout
#define fast_io ios_base::sync_with_stdio(0) , cin.tie(0) , cout.tie(0)
#define fi first
#define se second
#define mp make_pair
#define all(x) x.begin(), x.end()
#define pb push_back
#define endl '\n'
#define M_PI acos(-1)
#define M_E exp(1)
#define sqr(x) ((x)*(x))
#define cbr(x) (sqr(x) * (x))
#define toRad(x) (x) * M_PI / 180

const int N=(int)2e3;

int a[N][N];
int main()
{
    setlocale(LC_ALL, "Russian");
    int n, i, j, c = 1;
    cout<<"¬ведите нечетное n=";
    cin>>n;
    for(i = 0; i < n; i++){
        int y = n + i - 1, x = i;
        for(j = 0; j < n; j++){
            a[y--][x++] = c++;
        }
    }
    int s = n / 2;
    for(i = 0; i < s; i++){
        int y1 = n - 2, x = 1, y2 = n;
        if(i % 2 == 0){
            a[n-1][n + i] = a[n-1][i];
        }
        for(j = 0; j < i; j++){
            a[y1][n + x] = a[y1][x];
            a[y2][n + x] = a[y2][x];
            y1--;
            y2++;
            x++;
        }
    }
    for(i = 0; i < s; i++){
        int y = 1, x1 = n - 2, x2 = n;
        if(i % 2 == 0){
            a[n + i][n-1] = a[i][n-1];
        }
        for(j = 0; j < i; j++){
            a[n + y][x1] = a[y][x1];
            a[n + y][x2] = a[y][x2];
            y++;
            x1--;
            x2++;
        }
    }
    for(i = 0; i < s; i++){
        int y1 = n - 2, x = 2 * n - 3, y2 = n;
        if(i % 2 == 0){
            a[n-1][n - 2 - i] = a[n-1][2 * n - 2 - i];
        }
        for(j = 0; j < i; j++){
            a[y1][n - 3 - j] = a[y1][x];
            a[y2][n - 3 - j] = a[y2][x];
            x--;
            y1--;
            y2++;
        }
    }
    for(i = 0; i < s; i++){
        int y = 2 * n - 3, x1 = n - 2, x2 = n;
        if(i % 2 == 0){
            a[n - 2 - i][n-1] = a[2 * n - 2 - i][n-1];
        }
        for(j = 0; j < i; j++){
            a[n - 3 - j][x1] = a[y][x1];
            a[n - 3 - j][x2] = a[y][x2];
            y--;
            x1--;
            x2++;
        }
    }
    for(i = s; i < n + s; i++){
        for(j = s; j < n + s; j++){
            cout<<setw(2)<<(a[i][j] == 0 ? " " : to_string(a[i][j]))<<" ";
        }
        cout<<endl;
    }
    return 0;
}
