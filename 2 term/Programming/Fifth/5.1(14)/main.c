#include <stdio.h>
#include <stdlib.h>
#include <math.h>

typedef struct Node{
    struct Node * next;
    struct Node * prev;
    int value;
} Node;

typedef struct List{
    Node * begin;
    Node * end;
    int length;
} List;

List * newBigInt(char * number, int length){
    List * output = malloc(sizeof(List));
    output->length = length;
    int i;
    for(i = 0; i < length; i++){
        Node * node = malloc(sizeof(Node));
        if(i == 0){
            output->begin = node;
            output->end = node;
        }
        node->value = number[i] - '0';
        output->end->next = node;
        node->prev = output->end;
        output->end = node;
    }
    return output;
}

void printBigInt(List * number){
    Node * cur = number->begin;
    printf("%d", cur->value);
    while(cur != number->end){
        cur = cur->next;
        printf("%d", cur->value);
    }
}


int main(){
    List * number = newBigInt("258", 3);
    printBigInt(number);
    return 0;
}
