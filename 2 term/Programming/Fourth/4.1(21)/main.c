#include <stdio.h>
#include <stdlib.h>
#include <math.h>


void ItoB(int n, char * s, int b){
    int i = 0;
    while(n != 0){
        char c;
        int mod = n % b;
        if(mod > 9){
            c = 'A' + mod - 10;
        }
        else{
            c = '0' + mod;
        }
        s[i] = c;
        i++;
        n /= b;
    }
    int len = i;
    for(i = 0; i < len / 2; i++){
        char c = s[len - i - 1];
        s[len - i - 1] = s[i];
        s[i] = c;
    }
}

int main(){
    printf("%s", "Введите число n и основание системы счисления b:");
    int n, b;
    scanf("%d %d", &n, &b);
    if(b > 37){
        printf("%s", "Основание не должно быть больше 37\n");
        exit(0);
    }
    char * s = calloc(10, sizeof(char));
    ItoB(n, s, b);
    printf("%s\n", s);
    return 0;
}
