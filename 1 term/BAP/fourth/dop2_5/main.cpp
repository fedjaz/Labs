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

const int N=(int)1e3;

int a[N][N];
int main()
{
    int n, m, i, j;
    cin>>n>>m;
    for(i = 0; i < n; i++){
        for(j = 0; j < m; j++){
            cin>>a[i][j];
        }
    }
    for(i = 1; i < m; i++){
        a[0][i] = max(a[0][i], a[0][i-1]);
    }
    for(i = 1; i < n; i++){
        a[i][0] = max(a[i][0], a[i-1][0]);
    }
    for(i = 1; i < n; i++){
        for(j = 1; j < m; j++){
            a[i][j] = max(a[i][j], max(a[i-1][j], a[i][j-1]));
        }
    }
    for(i = 0; i < n; i++){
        for(j = 0; j < m; j++){
            cout<<a[i][j]<<" ";
        }
        cout<<endl;
    }
    return 0;
}
