#include <stdio.h>
#include <math.h>

int getAns(long long n, int k){
    while(k--){
        n /= 10;
    }
    return n % 10;
}
int main(){
    long long a = 1, b = 1, c, i;
    int len = 2, k;
    scanf("%d", &k);
    if(k <= 2){
        printf("%d", 1);
        return 0;
    }
    for(i = 0; i < 30; i++){
        c = a + b;
        a = b;
        b = c;
        len += floor(log10(c)) + 1;
        if(k <= len){
            printf("%d\n", getAns(c, len - k));
            return 0;
        }
    }
    printf("%s", "K is too big!");
    return 0;
}