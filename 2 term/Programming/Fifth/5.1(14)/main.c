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

void pushBack(List * list, int number){
    Node * node = malloc(sizeof(Node));
    node->value = number;
    list->length++;
    if(list->begin == NULL){
        list->begin = node;
        list->end = node;
    }
    else{
        list->end->next = node;
        node->prev = list->end;
        list->end = node;
    }
}

void pushFront(List * list, int number){
    Node * node = malloc(sizeof(Node));
    node->value = number;
    list->length++;
    if(list->begin == NULL){
        list->begin = node;
        list->end = node;
    }
    else{
        list->begin->prev = node;
        node->next = list->begin;
        list->begin = node;
    }
}

List * newBigInt(char * number, int length){
    List * output = malloc(sizeof(List));   
    int i;
    for(i = 0; i < length; i++){
        pushBack(output, number[i] - '0');
    }
    return output;
}

List * copy(List * list){
    List * output = malloc(sizeof(List));
    Node * cur = list->begin;
    pushBack(output, cur->value);
    while(cur != list->end){
        cur = cur->next;
        pushBack(output, cur->value);
    }
    return output;
}

List * mulByInt(List * list, int n){
    List * output = copy(list);
    Node * cur = output->begin;
    cur->value *= n;
    while(cur != output->end){
        cur = cur->next;
        cur->value *= n;
    }
    while(cur != output->begin){
        cur->prev->value += cur->value / 10;
        cur->value %= 10;
        cur = cur->prev;
    }
    if(cur->value > 9){
        pushFront(output, cur->value / 10);
        cur->value %= 10;
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
    List * number = newBigInt("228", 3);
    printBigInt(mulByInt(number, 9));
    return 0;
}
