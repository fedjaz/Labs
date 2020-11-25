.model small
.stack 100h
.data
	
.code
.386

printNumber proc
	push bx
	push cx
	push dx
	
	mov bx, 10
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
    mov cl, 10
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
    mov dl, 10
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
    db 0fh, 31h
    xor ah, ah
    mov bl, 4
    div bl
    mov al, ah
    xor ah, ah

    ret
rand endp 

dfs proc
    jmp variables
        posY dw 0
        posX dw 0
    variables:   
    pop [posy]
    pop [posx]
dfs endp

main:
    mov ax, @data
	mov ds, ax
	mov es, ax

	mov cx, 100
    
    loop1:
    call rand
    call printNumber
    loop loop1
    
    mov ah, 1
    int 21h

    mov ax, 4c00h
	int 21h
end main