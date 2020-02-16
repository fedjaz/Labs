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
double bpow(double a, int n){if(n==0) return 1; if(n%2==1) return a*bpow(a, n-1); double b=bpow(a, n/2); return b*b;}
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
#define sqr(x) (x)*(x)
#define cbr(x) sqr(x) * (x)
#define toRad(x) (x) * M_PI / 180

const int N=(int)1e6;

ll fact(int n){
    return n==1 ? 1 : n * fact(n-1);
}
int main()
{
    //14
    int n, k, i, j;
    cout<<"n, k=";
    cin>>n>>k;
    for(i = 0; i < k; i++){
        double x, s=0, y;
        cout<<"x=";
        cin>>x;
        y = 0.25 * ((x + 1) / sqrt(x) * sinh(sqrt(x)) - cosh(sqrt(x)));
        for(j = 1; j <= n; j++){
            s += sqr(j) * bpow(x, j) / fact(2 * j + 1);
        }
        cout<<"s("<<x<<")="<<s<<" y("<<x<<")= "<<y<<endl;
    }
    return 0;
}
