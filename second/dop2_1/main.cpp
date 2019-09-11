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
#define sqr(x) (x)*(x)
#define cbr(x) sqr(x) * (x)
#define toRad(x) (x) * M_PI / 180

const int N=(int)1e6;

int main()
{
    //вариант 1
    setlocale(LC_ALL, "Russian");
    int type;
    cout<<"Выберите тип координат(1 - декартовы, 2 - полярные)"<<endl;
    cin>>type;
    if(type == 1){
        double x1, y1, x2, y2;
        cout<<"x1, y1, x2, y2=";
        cin>>x1>>y1>>x2>>y2;
        cout<<"Расстояние:"<<(sqrt(sqr(x1 - x2) + sqr(y1 - y2)));
    }
    else{
        double r1, a1, r2, a2, x1, y1, x2, y2;
        cout<<"r1, a1, r2, a2=";
        cin>>r1>>a1>>r2>>a2;
        a1 = toRad(a1);
        a2 = toRad(a2);
        x1 = cos(a1) * r1;
        y1 = sin(a1) * r1;
        x2 = cos(a2) * r2;
        y2 = sin(a2) * r2;
        cout<<"Расстояние:"<<(sqrt(sqr(x1 - x2) + sqr(y1 - y2)));
    }
    return 0;
}
