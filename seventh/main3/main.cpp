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

const int N=(int)8;

int a[N], b[N], ans[N];
int mone[] = {0, 1, 1, 1, 1, 1, 1, 1};
void rev(int *n){
    for(int i = 0; i < N; i++)
        n[i] = abs(-1 + n[i]);
}

void add1(int *n){
    n[N - 1] += 1;
    for(int i = N - 1; i > 0; i--){
        if(n[i] > 1){
            n[i - 1] += 1;
            n[i] %= 2;
        }
    }
}

int main()
{
    //14
    int i;
    string s1, s2;
    cin>>s1>>s2;
    for(i = 0; i < N; i++){
        a[i] = s1[i] - '0';
        b[i] = s2[i] - '0';
    }
    if(a[0] == 1){
        a[0] = 0;
        rev(a);
        add1(a);
    }
    if(b[0] == 1){
        b[0] = 0;
        rev(b);
        add1(b);
    }
    for(i = 7; i >= 0; i--){
        ans[i] += a[i] + b[i];
        if(ans[i] > 1)
            ans[i - 1]++;
        ans[i] %= 2;
    }
    if(ans[0] == 1){
        for(i = 7; i >= 0; i--){
            ans[i] = ans[i] + mone[i];
            if(ans[i] > 1)
                ans[i - 1]++;
            ans[i] %= 2;
        }
        rev(ans);
    }
    for(int i = 0; i < N; i++){
        cout<<ans[i];
    }
    return 0;
}
