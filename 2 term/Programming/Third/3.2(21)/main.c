#include <stdio.h>
#include <stdlib.h>
#include <math.h>

void addStrs(float ** matrix, int size, int s1, int s2, float modifier){
    int i;
    for(i = 0; i < size; i++){
        matrix[s1][i] += matrix[s2][i] * modifier;
    }
}

incStr(float ** matrix, int size, int s, float modifier){
    int i;
    for(i = 0; i < size; i++){
        matrix[s][i] *= modifier;
    }
}

void printMatrix(float ** matrix, int size){
    int i, j;
    for(i = 0; i < size; i++){
        for(j = 0; j < size; j++){
            printf("%f ", matrix[i][j]);
        }
        printf("\n");
    }
}

void checkMatrix(float ** matrix, int size){
    int i;
    for(i = 0; i < size; i++){
        if(fabs(matrix[i][i]) < 0.0001){
            printf("%s", "Определитель матрицы равен 0. Обратной матрицы не существует");
            exit(0);
        }
    }
}
int main(){
    printf("%s", "Введите N: ");
    int n, i, j;
    scanf("%d", &n);
    printf("%s\n", "Введите матрицу NxN:");
    float ** arr = calloc(n, sizeof(float *));
    float ** identity = calloc(n, sizeof(float *));
    for(i = 0; i < n; i++){
        arr[i] = calloc(n, sizeof(float));
        identity[i] = calloc(n, sizeof(float));
        for(j = 0; j < n; j++){
            scanf("%f", &arr[i][j]);
            if(i == j){
            identity[i][j] = 1;
            }
        }     
    }
    checkMatrix(arr, n);
    for(i = 0; i < n; i++){
        for(j = i + 1; j < n; j++){
            float modifier = arr[j][i] / arr[i][i] * -1;
            addStrs(arr, n, j, i, modifier);
            addStrs(identity, n, j, i, modifier);
        }
    }
    checkMatrix(arr, n);
    for(i = 0; i < n; i++){
        float modifier = 1 / arr[i][i];
        incStr(arr, n, i, modifier);
        incStr(identity, n, i, modifier);
    }
    for(i = n - 1; i > 0; i--){
        for(j = i - 1; j >= 0; j--){
            float modifier =  arr[j][i] / arr[i][i]  * -1;
            addStrs(arr, n, j, i, modifier);
            addStrs(identity, n, j, i, modifier);
        }   
    }
    printMatrix(identity, n);
    return 0;
}