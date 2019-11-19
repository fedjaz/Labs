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

struct date{
    int day;
    int month;
    int year;
};

struct unit{
    string name;
    int cnt;
    float price;
    date receiptDate;
};

vector<unit> data;

bool comp(unit a, unit b){
    return a.name < b.name;
}

date init(){
    freopen("input.txt", "r", stdin);
    int n;
    date curDate;
    scanf("%d %d.%d.%d", &n, &curDate.day, &curDate.month, &curDate.year);
    for(int i = 0; i < n; i++){
        unit newUnit;
        char tmpName[100];
        scanf("%s %d %f %d.%d.%d", &tmpName, &newUnit.cnt, &newUnit.price,
                                   &newUnit.receiptDate.day, &newUnit.receiptDate.month, &newUnit.receiptDate.year);
        newUnit.name = tmpName;
        data.pb(newUnit);
    }
    freopen("CON", "r", stdin);
    return curDate;
}

void printUnit(unit u){
    cout<<u.name<<" "<<u.cnt<<" "<<u.price<<" ";
    cout<<u.receiptDate.day<<"."<<u.receiptDate.month<<"."<<u.receiptDate.year<<endl;
}

void printAll(){
    for(int i = 0; i < data.size(); i++){
        printUnit(data[i]);
    }
}

void addUnit(){
    unit newUnit;
    char tmpName[100];
    scanf("%s %d %f %d.%d.%d", &tmpName, &newUnit.cnt, &newUnit.price,
                               &newUnit.receiptDate.day, &newUnit.receiptDate.month, &newUnit.receiptDate.year);
    newUnit.name = tmpName;
    data.pb(newUnit);
}

void editByName(string name){
    for(int i = 0; i < data.size(); i++){
        if(data[i].name == name){
            char tmpName[100];
            scanf("%s %d %f %d.%d.%d", &tmpName, &data[i].cnt, &data[i].price,
                                       &data[i].receiptDate.day, &data[i].receiptDate.month, &data[i].receiptDate.year);
            data[i].name = tmpName;
            return;
        }
    }
    cout<<"Товар с таким названием не найден\n";
}

void deleteByName(string name){
    for(int i = 0; i < data.size(); i++){
        if(data[i].name == name){
            data.erase(data.begin() + i, data.begin() + i + 1);
            return;
        }
    }
    cout<<"Товар с таким названием не найден\n";
}

void printOld(date curDate){
    vector<unit> sortedData = data;
    sort(all(sortedData), comp);
    for(int i = 0; i < sortedData.size(); i++){
        date unitDate = sortedData[i].receiptDate;
        int age = (curDate.year * 365 + curDate.month * 30 + curDate.day) -
                  (unitDate.year * 365 + unitDate.month * 30 + unitDate.day);
        if(sortedData[i].price * sortedData[i].cnt >= (int)1e6 && age >= 30){
            printUnit(sortedData[i]);
        }
    }
}

void save(date curDate){
    freopen("output.txt", "w", stdout);
    cout<<data.size()<<" "<<curDate.day<<"."<<curDate.month<<"."<<curDate.year<<endl;
    for(int i = 0; i < data.size(); i++){
        printUnit(data[i]);
    }
}

int main()
{
    //14
    setlocale(LC_ALL, "Russian");
    date curDate = init();
    for(int i = 0; i < data.size(); i++){
        printUnit(data[i]);
    }

    while(1){
        int n;
        cout<<"1 - Вывести все товары\n";
        cout<<"2 - Добавить товар\n";
        cout<<"3 - Редактировать товар по названию\n";
        cout<<"4 - Удалить товар\n";
        cout<<"5 - Отсортировать товары в алфавитном порядке\n";
        cout<<"6 - Отобразить товары, стоимостью более 1.000.000 рублей, находящихся на складе более месяца\n";
        cout<<"0 - Сохранить и завершить работу\n";
        cin>>n;
        string name;
        switch(n){
            case 1:
                printAll();
                break;
            case 2:
                addUnit();
                break;
            case 3:
                cout<<"Введите название товара: ";
                cin>>name;
                editByName(name);
                break;
            case 4:
                cout<<"Введите название товара: ";
                cin>>name;
                deleteByName(name);
                break;
            case 5:
                sort(all(data), comp);
            case 6:
                printOld(curDate);
                break;
            case 0:
                save(curDate);
                return 0;
        }
    }
    return 0;
}
