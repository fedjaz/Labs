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
#define M_PI acos(-1)
#define fi first
#define se second
#define mp make_pair
#define all(x) x.begin(), x.end()
#define pb push_back
#define endl '\n'
#define sqr(x) (x) * (x)
#define cbr(x) sqr(x) * (x)

const int N=(int)1e6;

int main()
{
    double x1, x2, m, L;
    cin>>x1>>x2>>m;
    double k = cbr(cos(sqr(x1) * M_PI / 180)) + sqr(sin(cbr(x2) * M_PI / 180));
    if(k < 1){
        L = cbr(k) + pow(m, 0.2);
    }
    else{
        L = sqr(k) - exp(m);
    }
    cout<<L;
    return 0;
}
