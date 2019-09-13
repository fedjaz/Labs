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
#define sqr(x) (x) * (x)
#define cbr(x) sqr(x) * (x)

//const int N=(int)1e6;

int main()
{
    double a, b, c, Y;
    int N;
    cout<<"a, b, c, N=";
    cin>>a>>b>>c>>N;
    switch(N){
        case 2:
            Y = b * c - sqr(a);
            break;
        case 56:
            Y = b * c;
            break;
        case 7:
            Y = sqr(a) + c;
            break;
        case 3:
            Y = a - b * c;
            break;
        default:
            Y = cbrt(a + b);
    }
    cout<<Y;
    return 0;
}
