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

double f(double x){

    //1_1 0.8 2.4
    return sqrt(1.5 * x * x + 2.3) / (3 + sqrt(0.3 * x + 1));

    //1_2 1.4 2.2
    //return 1 / (sqrt(3 * sqr(x) + 1));

    //2_1 0.3 1.1
    //return cos(0.3 * x + 0.5) / (1.8 + sin(sqr(x) + 0.8));

    //2_2 1.4 2.2
    //return log10(sqr(x) + 2) / (x + 1);
}

double leftRectangles(double a, double b, int n){
    double step = (b - a) / n, ans = 0;
    for(int i = 0; i < n; i++){
        ans += f(a + step * i) * step;
    }
    return ans;
}

double rightRectangles(double a, double b, int n){
    double step = (b - a) / n, ans = 0;
    for(int i = 0; i < n; i++){
        ans += f(a + step * (i+1)) * step;
    }
    return ans;
}

double centralRectangles(double a, double b, int n){
    double step = (b - a) / n, ans = 0;
    for(int i = 0; i < n; i++){
        ans += f(a + step * (i + 0.5)) * step;
    }
    return ans;
}

double trapezoid(double a, double b, int n){
    double step = (b - a) / n, ans = 0;
    for(int i = 0; i < n; i++){
        double f1 = f(a + step * i), f2 = f(a + step * (i+1));

        ans += step * f1 + (step * (f2 - f1)) / 2;
    }
    return ans;
}

int main()
{
    //14
    double a, b;
    int n;
    cin>>a>>b>>n;
    cout<<trapezoid(a, b, n);
    return 0;
}
