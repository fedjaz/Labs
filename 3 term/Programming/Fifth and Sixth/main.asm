.model small
.stack 100h
.data
	graph db 10000 dup(0)
    used db 10000 dup(0)
    sizeX dw 40
    sizeY dw 40
    cellSize db 5
    lineWidth db 1
.code
.386


fillRectangle proc
    jmp fillRectangleVariables
        X1 dw 0
        X2 dw 0
        Y1 dw 0
        Y2 dw 0
        color db 0
    fillRectangleVariables:

    push ax
    push bx
    push cx
    push dx

    mov cx, X1
    mov dx, Y1
    mov ah, 0ch
    mov al, color
    mov bx, 0

    loop1_1:
        cmp dx, Y2
        jg loop1_1_end

        mov cx, x1
        loop2_1:
            cmp cx, X2
            jg loop2_1end

            int 10h

            inc cx
            jmp loop2_1

        loop2_1end:
        inc dx
        jmp loop1_1
    loop1_1_end:

    pop dx
    pop cx
    pop bx
    pop ax
    ret
fillRectangle endp


drawCell proc
    push ax
    push bx
    push cx
    push dx

    jmp drawCellVariables
        cornerY dw 0
        cornerX dw 0
    drawCellVariables:

    ;calculating coords
    mul cellSize
    push ax
    mov ax, bx
    mul cellSize
    mov bx, ax
    pop ax
    mov cornerY, ax
    mov cornerX, bx

    ;down border
    shr cx, 1
    rcr cx, 1
    jc rightborder

    ;Y1
    mov ax, cornerY
    xor bx, bx
    mov bl, cellSize
    add ax, bx
    mov bl, lineWidth
    sub ax, bx
    mov Y1, ax

    ;X1
    mov ax, cornerX
    mov X1, ax

    ;Y2
    mov ax, cornerY
    mov bl, cellSize
    add ax, bx
    mov Y2, ax 

    ;X2
    mov ax, cornerX
    mov bl, cellSize 
    add ax, bx
    mov X2, ax

    mov color, 0
    call fillRectangle


    rightborder:
    shr cx, 1
    rcr cx, 1
    jc endDrawCell

    ;Y1
    mov ax, cornerY
    mov Y1, ax

    ;X1
    mov ax, cornerX
    xor bx, bx
    mov bl, cellSize
    add ax, bx
    mov bl, lineWidth
    sub ax, bx
    mov X1, ax

    ;Y2
    mov ax, cornerY
    mov bl, cellSize
    add ax, bx
    mov Y2, ax

    ;X2
    mov ax, cornerX
    mov bl, cellSize
    add ax, bx
    mov X2, ax

    mov color, 0
    call fillRectangle

    endDrawCell:

    ;draw square
    mov ax, cornerY 
    mov bl, cellSize
    add ax, bx
    mov bl, lineWidth
    sub ax, bx
    mov Y1, ax
    mov Y2, ax
    inc Y2

    mov ax, cornerX
    mov bl, cellSize
    add ax, bx
    mov bl, lineWidth
    sub ax, bx
    mov X1, ax
    mov X2, ax
    inc X2

    call fillRectangle

    pop dx
    pop cx
    pop bx
    pop ax
    ret

drawCell endp


drawMaze proc
    push ax
    push bx

    ;drawing game background

    mov X1, 0
    mov Y1, 0
    mov X2, 320
    mov Y2, 200
    mov color, 6
    call fillRectangle
    ;drawing maze background

    mov X1, 0
    mov Y1, 0
    mov X2, 200
    mov Y2, 200
    mov color, 15
    call fillRectangle

    ;drawing borders
    mov color, 0

    mov X1, 0
    mov Y1, 0

    mov X2, 200
    xor ax, ax
    mov al, lineWidth
    mov Y2, ax
    call fillRectangle

    mov X2, ax
    mov Y2, 200
    call fillRectangle

    lea si, graph
    mov ax, 0
    drawMazeloop1:
        cmp ax, sizeY
        jge drawMazeloop1end

        mov bx, 0
        drawMazeloop2:
        cmp bx, sizeX
        jge drawMazeloop2end
        call getElem

        call drawCell

        inc bx
        jmp drawMazeloop2
        drawMazeloop2end:
        inc ax
        jmp drawMazeloop1
    drawMazeloop1end:
    pop bx
    pop ax
    ret
drawMaze endp

dfs proc
    jmp variables
        posY dw 0
        posX dw 0
    variables:   

    mov posY, ax
    mov posX, bx
    lea di, used
    mov cx, 1
    call setElem

    bigloop:
        mov ax, posY
        mov bx, posX

        call check
        jz endbigloop

        push ax
        call rand
        mov dx, ax
        pop ax

        upDir:
            ;checking is up
            cmp dx, 0
            jne downDir
            
            ;checking if can move up
            cmp ax, 0
            je bigloop

            ;checking if up direction is not used
            sub ax, 1
            lea si, used
            call getElem
            cmp cx, 1
            je bigloop

            ;adding info that can move up from this cell
            lea si, graph
            add ax, 1
            call getElem
            add cx, 0001b
            lea di, graph
            call setElem

            ;adding info that can move down from upper cell
            sub ax, 1
            lea di, graph
            mov cx, 0010b
            call setElem

            ;calling recursivly dfs into 
            call dfs
            add ax, 1
            mov posY, ax

            ;getting out of loop
            jmp bigloop

        downDir:
            ;checking is down
            cmp dx, 1
            jne leftDir

            ;checking if can move down
            add ax, 1
            cmp ax, sizeY
            je bigloop
            
            ;checking if down direction is not used
            lea si, used
            call getElem
            cmp cx, 1
            je bigloop

            ;adding info that can move down from this cell
            lea si, graph
            sub ax, 1
            call getElem
            add cx, 0010b
            lea di, graph
            call setElem

            ;adding info that can move up from down cell
            add ax, 1
            lea di, graph
            mov cx, 0001b
            call setElem

            ;calling recursivly dfs into 
            call dfs
            sub ax, 1
            mov posY, ax

            ;getting out of loop
            jmp bigloop


        leftDir:
            ;checking is left
            cmp dx, 2
            jne rightDir

            ;checking if can move left
            cmp bx, 0
            je bigloop

            ;checking if left direction is not used
            sub bx, 1
            lea si, used
            call getElem
            cmp cx, 1
            je bigloop

            ;adding info that can move left from this cell
            lea si, graph
            add bx, 1
            call getElem
            add cx, 0100b
            lea di, graph
            call setElem

            ;adding info that can move right from left cell
            sub bx, 1
            lea di, graph
            mov cx, 1000b
            call setElem

            ;calling recursivly dfs into 
            call dfs
            add bx, 1
            mov posX, bx

            ;getting out of loop
            jmp bigloop

        rightDir:
            ;checking if can move right
            add bx, 1
            cmp bx, sizeX
            je bigloop

            ;checking if right direction is not used
            lea si, used
            call getElem
            cmp cx, 1
            je bigloop

            ;adding info that can move right from this cell
            lea si, graph
            sub bx, 1
            call getElem
            add cx, 1000b
            lea di, graph
            call setElem

            ;adding info that can move left from right cell
            add bx, 1
            lea di, graph
            mov cx, 0100b
            call setElem

            ;calling recursivly dfs into 
            call dfs
            sub bx, 1
            mov posX, bx

            jmp bigloop

    endbigloop:
    ret
dfs endp

check proc
    push ax
    push bx
    push cx
    push dx
    push si

    jmp variables1
        y dw 0
        x dw 0
    variables1:

    mov y, ax
    mov x, bx
    mov dx, 0
    ;checking up
    up:
        mov ax, y
        mov bx, x

        cmp ax, 0
        je down

        sub ax, 1

        lea si, used
        call getElem
        cmp cx, 0
        
        jne down
        mov dx, 1

    ;checking down
    down:
        mov ax, y
        mov bx, x
        
        add ax, 1
        cmp ax, sizeY
        je left

        lea si, used
        call getElem
        cmp cx, 0
        jne left
        mov dx, 1

    ;checking left
    left:
        mov ax, y
        mov bx, x
        
        cmp bx, 0
        je right

        sub bx, 1

        lea si, used
        call getElem
        cmp cx, 0
        jne right
        mov dx, 1

    ;checking right
    right:
        mov ax, y
        mov bx, x
        
        add bx, 1
        cmp bx, sizeX
        je exit

        lea si, used
        call getElem
        cmp cx, 0
        jne exit
        mov dx, 1

    exit:
        cmp dx, 1
        jne cantMove

    canMove:
        mov ah, 0
        jmp endcheck
    cantMove:
        mov ah, 01000000b
    
    endCheck:
        sahf
        pop si
        pop dx
        pop cx
        pop bx
        pop ax

    ret
check endp

printNumber proc
	push bx
	push cx
	push dx
	
	mov bx, 2
	xor cx, cx
	xor dx, dx
	begin2:
		div bx
		push dx
		xor dx, dx
		inc cx
		cmp ax, 0
		jnz begin2
	
	begin3:
		pop ax
		mov dl, '0'
		add dl, al
		mov ah, 2
		int 21h
		loop begin3
		
	pop dx
	pop cx
	pop bx
	ret
printNumber endp

getElem proc
    push ax
    push si
    mov cx, sizeY
    mul cl
    add ax, bx
    add si, ax
    lodsb
    xor cx, cx
    mov cl, al
    pop si
    pop ax

    ret
getElem endp

setElem proc
    push ax
    push dx
    push di
    mov dx, sizeY
    mul dl
    add ax, bx
    add di, ax
    mov al, cl
    stosb
    pop di
    pop dx
    pop ax

    ret
setElem endp

rand proc
    push bx
    db 0fh, 31h
    xor ah, ah
    mov bl, 4
    div bl
    mov al, ah
    xor ah, ah
    pop bx

    ret
rand endp 

main:
    mov ax, @data
	mov ds, ax
	mov es, ax

    mov ah,0Fh
    int 10h
    mov ax, 0013h
    int 10h

    mov ax, 0
    mov bx, 0
    call dfs

    call drawMaze

    mov ah, 1
    int 21h

    mov ax, 4c00h
	int 21h
end main