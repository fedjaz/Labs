#pragma hdrstop
#pragma argsused
#include <bits/stdc++.h>

using namespace std;

int main()
{
    int a, b;
    cout<<"a, b=";
    cin>>a>>b;
    cout<<"0:a="<<a<<"; b="<<b<<endl;
    a = a + b;
    cout<<"1:a="<<a<<"; b="<<b<<endl;
    b = a - b;
    cout<<"2:a="<<a<<"; b="<<b<<endl;
    a = a - b;
    cout<<"3:a="<<a<<"; b="<<b<<endl;
    //alternative------------------------------------------------
    swap(a, b);
    return 0;
}
