#include <stdio.h>
#include <math.h>
#include <stdlib.h>

typedef struct{
    int * num;
    int len;
} bigInt;

int max(int a, int b){
    if(a > b)
        return a;
    return b;

}
bigInt * newBigInt(int n, int size){
    bigInt * a = malloc(sizeof(bigInt));
    if(a == NULL){
        printf("%s", "Not enough memory!");
        exit(228);
    }
    a->num = calloc(sizeof(int), size);
    if(a->num == NULL){
        printf("%s", "Not enough memory!");
        exit(228);
    }
    a->len = 0;
    int i;
    for(i = 0; i < size; i++){
        a->num[i] = 0;
    }
    i = 0;
    while(n != 0){
        a->num[i] = n % 10;
        n /= 10;
        i++;
        a->len++;
    }
    return a;
}

bigInt * add(bigInt * a, bigInt * b){
    int i = 0;
    bigInt * c = newBigInt(0, max(a->len, a->len) + 1);

    for(i = 0; i < max(a->len, b->len); i++){
        int tmp = a->num[i] + b->num[i];
        c->num[i] += tmp;
        c->num[i + 1] += c->num[i] / 10;
        c->num[i] %= 10;
        c->len++;
    }
    if(c->num[c->len] != 0){
        c->len++;
    }
    return c;
}

void printBigInt(bigInt * a){
    int i;
    for(i = a->len - 1; i >= 0; i--){
        printf("%d", a->num[i]);
    }
}

int main(){
    bigInt * a = newBigInt(1, 1), * b = newBigInt(1, 1), * c;
    int len = 2, k, i;
    scanf("%d", &k);
    if(k <= 2){
        printf("%d", 1);
        return 0;
    }
    while(a->len < 1e5){
        c = add(a, b);
        free(a->num);
        free(a); 
        a = b;
        b = c;
        len += c->len;
        if(k <= len){
            printf("%d\n", c->num[len - k]);
            return 0;
        }
    }
    return 0;
}