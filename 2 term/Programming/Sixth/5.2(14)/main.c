#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <string.h>

typedef struct Node {
    struct Node* right;
    struct Node* left;
    char* value;
    int length;
} Node;

typedef struct Tree {
    Node* root;
} Tree;


void checkPtr(void* ptr);
void balance(Tree* tree);
void getSortedTree(Node* cur, Node** outArr, int* length);
void addSorted(Tree* tree, Node** arr, int l, int r);
void printTree(Tree* tree, Node* begin);
void pushNode(Tree* tree, char* c, int length);
void disposeRecursive(Node* cur);
void dispose(Tree* tree);
int getBranchLength(Node* begin);
Tree* newTree();


void checkPtr(void* ptr) {
    if (ptr == NULL) {
        printf("fatal error ocurred");
        exit(-1);
    }
}

int getBranchLength(Node* begin) {
    int length = 0;
    if (begin->left != NULL) {
        length += getBranchLength(begin->left);
    }
    if (begin->right != NULL) {
        length += getBranchLength(begin->right);
    }
    return length + 1;
}
void balance(Tree* tree)
{
    int length = 0;
    Node** sorted = (Node**)calloc(getBranchLength(tree->root), sizeof(Node));
    getSortedTree(tree->root, sorted, &length);
    tree->root = NULL;
    addSorted(tree, sorted, 0, length - 1);
}

void getSortedTree(Node* cur, Node** outArr, int* length) {
    if (cur->left != NULL) {
        getSortedTree(cur->left, outArr, length);
    }
    outArr[(*length)++] = cur;
    if (cur->right != NULL) {
        getSortedTree(cur->right, outArr, length);
    }
}

void addSorted(Tree* tree, Node** arr, int l, int r)
{
    if (l == r) {
        pushNode(tree, arr[l]->value, arr[l]->length);
    }
    else if (l + 1 == r) {
        pushNode(tree, arr[l]->value, arr[l]->length);
        pushNode(tree, arr[r]->value, arr[r]->length);
    }
    else {
        int m = (l + r) / 2;
        pushNode(tree, arr[m]->value, arr[m]->length);
        addSorted(tree, arr, l, m - 1);
        addSorted(tree, arr, m + 1, r);
    }
}

Tree* newTree() {
    Tree* output = (Tree*)malloc(sizeof(Tree));
    checkPtr(output);
    output->root = NULL;
    return output;
}

void pushNode(Tree* tree, char* c, int length) {
    Node* cur = tree->root;
    Node* prev = NULL;
    int compareResult;
    while (cur != NULL) {
        prev = cur;
        compareResult = strcmp(c, cur->value);
        if (compareResult > 0) {
            cur = cur->right;
        }
        else {
            cur = cur->left;
        }
    }
    cur = (Node*)malloc(sizeof(Node));
    checkPtr(cur);
    cur->left = NULL;
    cur->right = NULL;
    cur->value = c;
    cur->length = length;
    if (prev != NULL) {
        if (compareResult > 0) {
            prev->right = cur;
        }
        else {
            prev->left = cur;
        }
    }
    else {
        tree->root = cur;
    }
}

void printTree(Tree* tree, Node* begin) {
    if (tree->root == NULL) {
        return;
    }
    if (begin == NULL) {
        begin = tree->root;
    }
    if (begin->left != NULL) {
        printTree(tree, begin->left);
    }
    if (begin->value[0] == begin->value[begin->length - 1]) {
        printf("%s ", begin->value);
    }
    if (begin->right != NULL) {
        printTree(tree, begin->right);
    }
}

void disposeRecursive(Node* cur) {
    if (cur->left != NULL) {
        disposeRecursive(cur->left);
    }
    if (cur->right != NULL) {
        disposeRecursive(cur->right);
    }
    free(cur->value);
    free(cur);
}

void dispose(Tree* tree) {
    if (tree == NULL) {
        return;
    }
    if (tree->root != NULL) {
        disposeRecursive(tree->root);
    }
    free(tree);
}

char buffer[1000000];

int main() {
    FILE* file;
    file = fopen("input.txt", "r");
    Tree* tree = newTree();
    checkPtr(file);
    while (fscanf(file, "%s", buffer) > 0) {
        int length = strlen(buffer);
        char* c = (char*)calloc(length + 1, sizeof(char));
        checkPtr(c);
        strcpy(c, buffer);
        pushNode(tree, c, length);
    }
    balance(tree);
    printTree(tree, NULL);
    dispose(tree);
    fclose(file);
    return 0;
}