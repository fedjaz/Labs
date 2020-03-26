#include <stdio.h>
#include <stdlib.h>

int main(){
    printf("%s", "Введите n: ");
    int n, i, j;
    scanf("%d", &n);
    printf("%s\n", "Введите перестановку из n чисел:");
    int * arr = calloc(n, sizeof(int));
    for(i = 0; i < n; i++){
        scanf("%d", &arr[i]);
    }
    int counter = 0;
    for(i = 0; i < n; i++){
        for(j = 0; j < n - i; j++){
            if(arr[j] < arr[j - 1]){
                counter++;
                int temp = arr[j];
                arr[j] = arr[j - 1];
                arr[j - 1] = temp;
            }
        }
    }
    printf("%s %d\n", "Количество необходимых перестановок - ", counter);
    return 0;
}