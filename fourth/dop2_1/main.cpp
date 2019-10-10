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

const int N=(int)1e2;

int a[N][N], sizes[N];
int main()
{
    int n, m, i, j;
    cin>>n>>m;
    int **arr = new int*[n];
    for(i = 0; i < n; i++){
        for(j = 0; j < m; j++){
            cin>>a[i][j];
        }
        sort(a[i], a[i] + m);
        int s = 0, it = 0;
        if(a[i][0] == a[i][1])
            s++;
        for(j = 1; j < m - 1; j++){
            if(a[i][j] == a[i][j-1] || a[i][j] == a[i][j+1])
                s++;
        }
        if(a[i][m - 1] == a[i][m - 2])
            s++;
        sizes[i] = s;
        arr[i] = new int[s];
        if(a[i][0] == a[i][1])
            arr[i][it++] = a[i][0];
        for(j = 1; j < m - 1; j++){
            if(a[i][j] == a[i][j-1] || a[i][j] == a[i][j+1])
                arr[i][it++] = a[i][j];
        }
        if(a[i][m - 1] == a[i][m - 2])
            arr[i][it++] = a[i][m - 1];
    }
    for(i = 0; i < n; i++){
        for(j = 0; j < sizes[i]; j++){
            cout<<a[i][j]<<" ";
        }
        cout<<endl;
    }
    return 0;
}