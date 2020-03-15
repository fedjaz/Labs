#include <stdio.h>
#include <math.h>

long double fact(int n){
    if(n > 20)
        return __LDBL_MAX__;
    if(n == 0)
        return 1;
    return n * fact(n - 1);
}

double rec(double x, double e, int * i, double sum){
    if(fabs(sum - sin(x)) < e)
        return sum;
    
    sum += pow(x, 2 * (*i) - 1) / fact(2 * (*i) - 1) * (((*i) - 1) % 2 == 0 ? 1 : -1); 
    (*i)++;
    return rec(x, e, i, sum);
}

int main(){
    double x, e;
    printf("%s\n", "Введите х и е:");
    scanf("%lf %lf", &x, &e);
    x = fmod(x, 2 * acos(-1));
    double sum = 0;
    int i;
    printf("%s%lf\n", "sin(x) = ", sin(x));
    for(i = 1; fabs(sum - sin(x)) > e; i++){
        sum += pow(x, 2 * i - 1) / fact(2 * i - 1) * ((i - 1) % 2 == 0 ? 1 : -1);
    }
    printf("%s\n%s%lf%s%d\n", "Результат работы цикла:", "sin(x) = ", sum, ", n = ", i);
    i = 1;
    sum = rec(x, e, &i, 0);
    printf("%s\n%s%lf%s%d\n", "Результат работы рекурсии:", "sin(x) = ", sum, ", n = ", i);
    return 0;
}