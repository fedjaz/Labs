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


void printBigInt(List * number);
void pushBack(List * list, int number);
void popBack(List * list);
void pushFront(List * list, int number);
void popFront(List * list);
void trim(List * list);
void dispose(List * list);
int compareInt(int a, int b); 
int compare(List * a, List * b);
List * newBigIntN(int number);
List * newBigIntC(char * number, int length);
List * copy(List * list, int startIndex, int length);
List * mulByInt(List * list, int n);
List * substract(List * a, List * b);
List * mod(List * a, List * b);



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
    node->next = NULL;
    node->prev = NULL;
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
    node->next = NULL;
    node->prev = NULL;
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

void dispose(List * list){
    if(list == NULL){
        return;
    }
    while(list->length > 0){
        popBack(list);
    }
    free(list);
}

List * newBigIntN(int number){
    List * output = malloc(sizeof(List));  
    output->begin = NULL;
    output->end = NULL;
    output->length = 0;
    if(number == 0){
        pushBack(output, 0);
        return output;
    }
    while(number != 0){
        pushFront(output, number % 10);
        number /= 10;
    }
    return output;
}

List * newBigIntC(char * number, int length){
    List * output = malloc(sizeof(List));  
    output->begin = NULL;
    output->end = NULL;
    output->length = 0;
    int i;
    for(i = 0; i < length; i++){
        pushBack(output, number[i] - '0');
    }
    return output;
}

List * copy(List * list, int startIndex, int length){
    List * output = newBigIntC("", 0);
    Node * cur = list->begin;
    int i;
    for(i = 0; i < list->length; i++, cur = cur->next){
        if(i >= startIndex && i - startIndex < length){
            pushBack(output, cur->value);
        }
    }
    return output;
}

List * mulByInt(List * list, int n){
    List * output = copy(list, 0, list->length);
    Node * cur = output->begin;
    int i;
    for(i = 0; i < output->length; i++){
        cur->value *= n;
        cur = cur->next;
    }
    cur = output->end;
    for(i = 0; i < output->length - 1; i++){
        cur->prev->value += cur->value / 10;
        cur->value %= 10;
        cur = cur->prev;
    }
    if(output->begin->value > 9){
        int val = output->begin->value / 10;
        output->begin->value %= 10;
        pushFront(output, val);
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
    int i;
    for(i = 0; i < b->length; i++, curA = curA->prev, curB = curB->prev){
        curA->value -= curB->value;
        if(curA->value < 0){
            curA->value += 10;
            curA->prev->value--;
        }
    }
    trim(output);
    return output;
}

List * mod(List * a, List * b){
    List * rest = newBigIntN(a->begin->value);
    List * try;
    Node * cur = a->begin;
    do{
        while(compare(rest, b) == -1 && cur != a->end){
            cur = cur->next;
            pushBack(rest, cur->value);
        }
        int l = 1, r = 10;
        while(l < r){
            int m = (l + r) / 2;
            dispose(try);
            try = mulByInt(b, m);
            if(compare(try, rest) != 1){
                l = m + 1;
            }
            else{
                r = m;
            }
        }
        dispose(try);
        try = mulByInt(b, l - 1);
        List * newRest = substract(rest, try);
        dispose(rest);
        rest = newRest;
    }while(cur != a->end);
    return rest;
}

void printBigInt(List * number){
    Node * cur = number->begin;
    int i;
    for(i = 0; i < number->length; i++, cur = cur->next){
        printf("%d", cur->value);
    }
}

int main(){
    List * a = newBigIntN(1488);
    List * b = newBigIntN(228);
    printBigInt(a);
    printf("\n");
    printBigInt(b);
    printf("\n");
    printBigInt(mod(a, b));
    return 0;
}