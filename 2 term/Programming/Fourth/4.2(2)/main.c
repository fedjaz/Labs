#include <stdio.h>
#include <stdlib.h>
#include <math.h>


int main(){
    FILE * file = fopen("input.txt", "r");
    int i = 0;
    char * s = calloc((int)1e6, sizeof(char));
    fscanf(file, "%s", s);
    int depth = 0;
    while(s[i] != 0){
        if(s[i] == '(')
            depth++;
        else
            depth--;

        if(depth < 0){
            exit(0);
        }
        i++;
    }
    if(depth != 0){
        exit(0);
    }
    i = depth = 0;
    while(s[i] != 0){
        if(s[i] == '(')
            depth++;
        else
            depth--;

        if(depth == 2 && s[i] == '(')
            s[i] = '[';
        else if(depth >= 3 && s[i] == '(')
            s[i] = '{';
        else if(depth >= 2 && s[i] == ')')
            s[i] = '}';
        else if(depth == 1 && s[i] == ')')
            s[i] = ']'; 
        i++;
    }
    FILE * output = fopen("output.txt", "w");
    fprintf(output, "%s", s);
    return 0;
}
