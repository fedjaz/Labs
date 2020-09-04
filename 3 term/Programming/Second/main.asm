.model small
.stack 100h
.data
	msg1_1 db "Type first decimal number:$"
	msg1_2 db "Type second decimal number:$"
.code
deleteSymbol proc
	push ax
	push dx
	mov ah, 2
	mov dl, 8
	int 21h
	mov dl, 32
	int 21h
	mov dl, 8
	int 21h
	mov ax, bx
	xor dx, dx
	mov bx, 10
	div bx
	mov bx, ax
	pop dx
	pop ax
deleteSymbol endp

checkSymbol proc
	push ax	
	sub al, '0'
	jc fal
	cmp al, 10
	jnc fal
	mov ah, 01000000b
	sahf
	pop ax
	ret
	fal:
		mov ah, 0
		sahf
		pop ax
	ret
checkSymbol endp

getNumber proc
	push bx
	push cx
	push dx
	
	xor bx, bx
	xor cx, cx
	begin1:
		mov ah, 8
		int 21h
		
		xor ah, ah
		call checkSymbol
		jnz er

		sub al, '0'
		mov cl, al
		xor dx, dx
		mov ax, 10
		mul bx
		xor ch, ch
		add ax, cx

		jc begin1
		cmp dx, 0
		jnz begin1

		mov bx, ax
		add cl, '0'
		mov dl, cl
		mov ah, 2
		int 21h

		jmp begin1

		er:
			cmp al, 8
			jz back
			cmp al, 27
			jz esck
			cmp al, 0Dh
			jz save
			jmp begin1
			back:
				cmp bx, 0
				jz begin1
					
					call deleteSymbol
					jmp begin1
			esck:
				begin4:
					cmp bx, 0
					jz begin1
					call deleteSymbol
					jmp begin4
					
		jmp begin1
		
	save:
		mov ax, bx
		pop dx
		pop cx
		pop bx
	
	ret
getNumber endp

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

main:
	mov ax, @data
	mov ds, ax
	
	lea dx, msg1_1
	mov ah, 09h
	int 21h 
	
	call getNumber
	mov cx, ax
	
	mov dl, 10
	mov ah, 2
	int 21h
	
	lea dx, msg1_2
	mov ah, 09h
	int 21h
	
	call getNumber
	mov bx, ax
	mov ax, cx
	xor dx, dx
	div bx
	mov bx, ax
	
	mov dl, 10
	mov ah, 2
	int 21h

	mov ax, bx
	call printNumber
	
	mov ah, 1
	int 21h
	mov ax, 4c00h
	int 21h
end main


