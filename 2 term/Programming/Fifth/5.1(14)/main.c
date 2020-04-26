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

int compareInt(int a, int b){
    if(a > b){
        return 1;
    }
    else if(a < b){
        return -1;
    }
    return 0;
}

int compare(List * a, List * b){
    if(a->length != b->length){
        return compareInt(a->length, b->length);
    }
    else{
        Node * curA = a->begin, * curB = b->begin;
        int result = compareInt(curA->value, curB->value);
        if(result != 0){
            return result;
        }
        while(curA != a->end){
            curA = curA->next;
            curB = curB->next;
            int result = compareInt(curA->value, curB->value);
            if(result != 0){
                return result;
            }
        }
    }
    return 0;
}

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

void popBack(List * list){
    if(list->length == 1){
        free(list->end);
        list->begin = NULL;
        list->end = NULL;
    }
    else{
        list->end = list->end->prev;
        free(list->end->next);
        list->end->next = NULL;
    }
    list->length--;
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

void popFront(List * list){
    if(list->length == 1){
        free(list->end);
        list->begin = NULL;
        list->end = NULL;
    }
    else{
        list->begin = list->begin->next;
        free(list->begin->prev);
        list->begin->prev = NULL;
    }
    list->length--;
}

void trim(List * list){
    while(list->begin->value == 0 && list->length > 1){
        popFront(list);
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

List * copy(List * list, int startIndex, int length){
    List * output = malloc(sizeof(List));
    Node * cur = list->begin;
    pushBack(output, cur->value);
    int i;
    for(i = 0; cur != list->end; i++){
        cur = cur->next;
        if(i >= startIndex && i - startIndex < length){
            pushBack(output, cur->value);
        }
    }
    return output;
}

List * mulByInt(List * list, int n){
    List * output = copy(list, 0, output->length);
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

List * substract(List * a, List * b){
    if(compare(a, b) == -1){
        return NULL;
    }
    List * output = copy(a, 0, a->length);
    Node * curA = output->end;
    Node * curB = b->end;
    curA->value -= curB->value;
    if(curA->value < 0){
            curA->value += 10;
            curA->prev->value -= 1;
    }
    while(curB != b->begin){
        curA = curA->prev;
        curB = curB->prev;
        curA->value -= curB->value;
        if(curA->value < 0){
            curA->value += 10;
            curA->prev->value -= 1;
        }
    }
    trim(output);
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
    List * a = newBigInt("999", 3);
    List * b = newBigInt("999", 3);
    printBigInt(a);
    printf("\n");
    printBigInt(b);
    printf("\n");
    printBigInt(substract(a, b));
    return 0;
}