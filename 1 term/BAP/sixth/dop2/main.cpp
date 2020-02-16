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

ll bpow(ll a, ll n, ll mod){
    if(n == 0)
        return 1;
    if(n % 2 == 1)
        return a * bpow(a, n-1, mod) % mod;
    ll b = bpow(a, n/2, mod) % mod;
    return b * b % mod;
}
ll solve(int n, int m, string s1, string s2){
    if(s1.length() + s2.length() <= n){
        return bpow(26, n - (s1.length() + s2.length()), m);
    }
    else{
        int ptr = n - s2.length();
        for(int i = 0; ptr < s1.length(); i++){
            if(s1[ptr++] != s2[i])
                return 0;
        }
        return 1;
    }
}

int main()
{
    fin;
    int t, n, m;
    string s1, s2;
    cin>>t;
    while(t--){
        cin>>n>>m>>s1>>s2;
        cout<<solve(n, m, s1, s2) + solve(n, m, s2, s1)<<endl;
    }
    return 0;
}
