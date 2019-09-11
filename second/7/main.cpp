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
    double a, b, c;
    cin>>a>>b>>c;
    if(a == 0 && b != 0){
        double q = -c / b;
        if(q < 0)
            cout<<"Уравнение не имеет корней";
        else if(q != 0)
            cout<<"x1="<<sqrt(q)<<", x2="<<-sqrt(q);
        else
            cout<<"x="<<0;
    }
    else if(b == 0 && a != 0){
        double q = -c / a;
        if(q < 0)
            cout<<"Уравнение не имеет корней";
        else if(q != 0)
            cout<<"x1="<<pow(q, 0.25)<<", x2="<<-pow(q, 0.25);
        else
            cout<<"x="<<0;
    }
    else if(a != 0 && b != 0 && c != 0){
        double D = sqr(b) - 4 * a * c;
        if(D < 0){
            cout<<"Уравнение не имеет корней";
            return 0;
        }
        double t1 = (-b + sqrt(D)) / (2 * a);
        double t2 = (-b - sqrt(D)) / (2 * a);
        if(t1 < t2)
            swap(t1, t2);
        if(t1 >= 0){
            cout<<"x1="<<sqrt(t1)<<", x2="<<-sqrt(t1);
        }
        if(t2 >= 0){
            cout<<", x3="<<sqrt(t2)<<", x4="<<-sqrt(t2);
        }
    }
    else if(a != 0 && b != 0 && c == 0){
        cout<<"x1=0";
        double q = -b / a;
        if(q >= 0)
            cout<<", x2="<<sqrt(q)<<", x3="<<-sqrt(q);
    }
    else if(c == 0)
        cout<<"x принадлежит R";
    else
        cout<<"Уравнение не имеет корней";
    return 0;
}
