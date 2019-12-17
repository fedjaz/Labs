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

struct num{
    char zn;
    int number[N];
};

bool comp(num a, num b){
    if(a.zn == '+' && b.zn == '-')
        return true;

    if(a.zn == '-' && b.zn == '+')
        return false;

    else if(a.zn == '+' && b.zn == '+'){
        for(int i = 0; i < N; i++){
            if(a.number[i] == b.number[i])
                continue;
            return a.number[i] > b.number[i];
        }
        return false;
    }
    else if(a.zn == '-' && b.zn == '-'){
        for(int i = 0; i < N; i++){
            if(a.number[i] == b.number[i])
                continue;
            return a.number[i] < b.number[i];
        }
        return false;
    }
}

int* add(int* a, int* b){
    int* ans = new int[8];
    ans[7] = 0;
    for(int i = 7; i > 0; i--){
        ans[i - 1] = 0;
        ans[i] += a[i] + b[i];
        if(ans[i] > 14)
            ans[i - 1]++;
        ans[i] %= 15;
    }
    return ans;
}

int* substract(int* a, int* b){
    int* ans = new int[8];
    ans[7] = 0;
    for(int i = 7; i > 0; i--){
        ans[i - 1] = 0;
        if(a[i] < b[i]){
            a[i - 1]--;
            a[i] += 15;
        }
        ans[i] = a[i] - b[i];
    }
    return ans;
}

int main()
{
    //14
    char zn;
    num a, b;
    string s1, s2;
    cin>>a.zn>>s1>>zn>>b.zn>>s2;
    for(int i = 0; i < N; i++){
        if(s1[i] >= '0' && s1[i] <= '9'){
            a.number[i] = s1[i] - '0';
        }
        else{
            a.number[i] = 10 + s1[i] - 'A';
        }
    }
    for(int i = 0; i < N; i++){
        if(s2[i] >= '0' && s2[i] <= '9'){
            b.number[i] = s2[i] - '0';
        }
        else{
            b.number[i] = 10 + s2[i] - 'A';
        }
    }
    int* ans;

    if(a.zn == '+' && b.zn == '+' && zn == '+'){
        ans = add(a.number, b.number);
        zn = '+';
    }
    else if(a.zn == '+' && b.zn == '+' && zn == '-'){
        if(!comp(a, b)){
            swap(a, b);
            zn = '-';
        }
        else
            zn = '+';
        ans = substract(a.number, b.number);
    }
    else if(a.zn == '+' && b.zn == '-'&& zn == '+'){
        if(!comp(a, b)){
            swap(a, b);
            zn = '-';
        }
        else
            zn = '+';
        ans = substract(a.number, b.number);
    }
    else if(a.zn == '+' && b.zn == '-'&& zn == '-'){
        zn = '+';
        ans = add(a.number, b.number);
    }
    else if(a.zn == '-' && b.zn == '+' && zn == '+'){
        a.zn = '+';
        if(comp(a, b)){
            zn = '-';
            ans = substract(a.number, b.number);
        }
        else{
            ans = substract(b.number, a.number);
            zn = '+';
        }
    }
    else if(a.zn == '-' && b.zn == '+' && zn == '-'){
        zn = '-';
        ans = add(a.number, b.number);
    }
    else if(a.zn == '-' && b.zn == '-' && zn == '+'){
        zn = '-';
        ans = add(a.number, b.number);
    }
    else if(a.zn == '-' && b.zn == '-' && zn == '-'){
        if(!comp(a, b)){
            zn = '-';
            ans = substract(a.number, b.number);
        }
        else{
            zn = '+';
            ans = substract(b.number, a.number);
        }
    }
    cout<<zn;
    for(int i = 0; i < N; i++){
        if(ans[i] < 9){
            cout<<ans[i];
        }
        else{
            cout<<(char)('A' + ans[i] - 10);
        }
    }
    return 0;
}
