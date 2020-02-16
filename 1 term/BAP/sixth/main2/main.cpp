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

const int N=(int)1e6;

char c[N];
unordered_map<ll, bool> m[N];
int main()
{
    int i, j, cur_pred = 0, len = 0;
    ll hs = 0;
    fgets(c, N, stdin);
    for(i = 0; c[i] != '\n'; i++){
        if(c[i] == ' ' || c[i] == '.'){
            if(hs == 0)
                continue;
            m[cur_pred][hs] = true;
            hs = 0;
            len = 0;
        }
        else{
            hs += bpowMod(31, len) * tolower(c[i]);
            hs %= mod;
            len++;
        }
        if(c[i] == '.'){
            cur_pred++;
        }
    }
    int maxval = 0;
    for(i = 0; i < cur_pred; i++){
        for(auto hs : m[i]){
            int tmp = 1;
            for(j = i + 1; j < cur_pred; j++){
                if(m[j][hs.fi]){
                    tmp++;
                }
            }
            if(tmp > maxval){
                maxval = tmp;
            }
        }
    }
    cout<<maxval;
    return 0;
}
