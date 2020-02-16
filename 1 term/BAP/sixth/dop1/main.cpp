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

const int N=(int)105;

char c[N];
int main()
{
    fgets(c, N, stdin);
    string s = "";
    for(int i = 0; c[i] != '\n' && c[i] != 0; i++){
        bool isUp = c[i] == toupper(c[i]);
        c[i] = tolower(c[i]);
        if(c[i] == 'c'){
            if(c[i + 1] == 'e' || c[i + 1] == 'i' || c[i + 1] == 'y'){
                if(isUp)
                    s += 'S';
                else
                    s += 's';
            }
            else{
                if(isUp)
                    s += 'K';
                else
                    s += 'k';
            }
        }
        else if(c[i] == 'q'){
            if(isUp)
                s += 'K';
            else
                s += 'k';
            if(c[i + 1] == 'u'){
                s += 'v';
                i++;
            }
        }
        else if(c[i] == 'x'){
            if(isUp)
                s += "Ks";
            else
                s += "ks";
        }
        else if(c[i] == 'w'){
            if(isUp)
                s += 'V';
            else
                s += 'v';
        }
        else if(c[i] == 'p' && c[i + 1] == 'h'){
            if(isUp)
                s += 'F';
            else
                s += 'f';
            i++;
        }
        else if(c[i] == 'o' && c[i + 1] == 'o'){
            if(isUp)
                s += 'U';
            else
                s += 'u';
            i++;
        }
        else if(c[i] == 'y' && c[i + 1] == 'o' && c[i + 2] == 'u'){
            if(isUp)
                s += 'U';
            else
                s += 'u';
            i += 2;
        }
        else if(c[i] == 'e' && c[i + 1] == 'e'){
            if(isUp)
                s += 'I';
            else
                s += 'i';
            i++;
        }
        else if(c[i] == 't' && c[i + 1] == 'h'){
            if(isUp)
                s += 'Z';
            else
                s += 'z';
            i++;
        }
        else{
            if(isUp)
                s += toupper(c[i]);
            else
                s += c[i];
        }
    }

    string s2;
    s2 += s[0];
    for(int i = 1; i < s.length(); i++){
        if(tolower(s[i]) == tolower(s2[s2.length() - 1]))
            continue;

        s2 += s[i];
    }
    cout<<s2;
    return 0;
}
