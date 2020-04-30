all: build run clean

build: main.c
	gcc main.c -o main -lm -std=c89

run:
	./main

valgrind: build
	valgrind --leak-check=full -s ./main
	rm -rf ./main

clean:
	rm -rf ./main

