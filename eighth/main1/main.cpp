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

struct abitur{
    string surname;
    double mean_score;
};

vector<abitur> data;

bool comp(abitur a, abitur b){
    return a.mean_score > b.mean_score;
}

void addNStructures(int n){
    for(int i = 0; i < n; i++){
        cout<<"���������� #"<<data.size() + 1<<":\n�������: ";
        abitur a;
        cin>>a.surname;
        cout<<"������� ����: ";
        cin>>a.mean_score;
        data.pb(a);
    }
}

void addUntilStop(){
    cout<<"��� ����, ����� ���������� ����, ������� -1"<<endl;
    for(int i = 0; ; i++){
        cout<<"���������� #"<<i + 1<<":\n�������: ";
        abitur a;
        cin>>a.surname;
        cout<<"������� ����: ";
        cin>>a.mean_score;
        if(a.surname == "-1" || a.mean_score == -1)
            return;
        data.pb(a);
    }
}

void printAll(){
    for(int i = 0; i < data.size(); i++){
        cout<<data[i].surname<<" "<<data[i].mean_score<<endl;
    }
}

void editBySurname(string surname){
    for(int i = 0; i < data.size(); i++){
        if(data[i].surname == surname){
            cout<<"�������: ";
            cin>>data[i].surname;
            cout<<"������� ����: ";
            cin>>data[i].mean_score;
            return;
        }
    }
    cout<<"���������� � ����� �������� �� ������\n";
}

void deleteBySurname(string surname){
    for(int i = 0; i < data.size(); i++){
        if(data[i].surname == surname){
            data.erase(data.begin() + i, data.begin() + i + 1);
            return;
        }
    }
    cout<<"���������� � ����� �������� �� ������\n";
}

void searchBySurname(string surname){
    for(int i = 0; i < data.size(); i++){
        if(data[i].surname == surname){
            cout<<data[i].surname<<" "<<data[i].mean_score<<endl;
            return;
        }
    }
    cout<<"���������� � ����� �������� �� ������\n";
}

double getMeanScore(){
    double mean = 0;
    for(int i = 0; i < data.size(); i++){
        mean += data[i].mean_score;
    }
    return mean / data.size();
}

void printAllAboveScore(double score){
    for(int i = 0; i < data.size(); i++){
        if(data[i].mean_score >= score){
            cout<<data[i].surname<<" "<<data[i].mean_score<<endl;
        }
    }
}

int main()
{
    //14
    setlocale(LC_ALL, "Russian");
    int n;
    cout<<"������� ��� �����:\n���� n �������� - 1\n���� �� ��������� �������� - 2\n";
    cin>>n;
    if(n == 1){
        cout<<"������� ���������� ����������, ������� ������ ��������: ";
        cin>>n;
        addNStructures(n);
    }
    else{
        addUntilStop();
    }
    while(1){
        cout<<"1 - ������� ���� ������������\n";
        cout<<"2 - �������� �����������\n";
        cout<<"3 - ������������� �����������\n";
        cout<<"4 - ������� �����������\n";
        cout<<"5 - ����� �����������\n";
        cout<<"6 - ���������� ������������ �� �������� �����\n";
        cout<<"7 - ����� ������� ���� �� ������������\n";
        cout<<"8 - ������� ������������, � ������� ������� ���� ���� ��������\n";
        cout<<"0 - ��������� ������\n";
        cin>>n;
        string surname;
        switch(n){
            case 1:
                printAll();
                break;
            case 2:
                addNStructures(1);
                break;
            case 3:
                cout<<"������� ������� ����������� ��� ��������������: ";
                cin>>surname;
                editBySurname(surname);
                break;
            case 4:
                cout<<"������� ������� ����������� ��� ��������: ";
                cin>>surname;
                deleteBySurname(surname);
                break;
            case 5:
                cout<<"������� ������� ����������� ��� �����: ";
                cin>>surname;
                searchBySurname(surname);
                break;
            case 6:
                sort(all(data), comp);
                break;
            case 7:
                cout<<"������� ���� �� ������������ - "<<getMeanScore()<<endl;
                break;
            case 8:
                printAllAboveScore(getMeanScore());
                break;
            case 0:
                return 0;
        }
    }
    return 0;
}
