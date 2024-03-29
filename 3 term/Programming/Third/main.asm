.model small
.stack 100h
.data
	msg1_1 db "Type first decimal number: $"
	msg1_2 db "Type second decimal number: $"
	msg2 db "Result: $"
	msg3 db ", Mod: $"
	msg4 db 10, "You can't divide by zero$"
.code
.386
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
	
	jmp endinit
		isMinus db 0
		len db 0
	endinit:
	xor bx, bx
	xor cx, cx
	mov isMinus, 0
	mov len, 0
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
		mov dl, isMinus
		add dx, 7fffh
		cmp ax, dx
		jz checked
		jnc begin1

		cmp cl, 0
		jnz checked
		cmp len, 1
		jnz checked
		cmp bx, 0
		jnz checked
		jmp begin1

		checked:
		mov bx, ax

		mov al, cl
		xor ah, ah
		or ax, bx


		add cl, '0'
		mov dl, cl
		mov ah, 2
		int 21h
		inc len
		jmp begin1

		er:
			cmp al, 8
			jz back
			cmp al, 27
			jz esck
			cmp al, 0Dh
			jz save
			cmp al, '-'
			jz minus
			jmp begin1
			back:
				cmp len, 0
				jz checkMinus1
					call deleteSymbol
					dec len
					jmp begin1
				checkMinus1:
					cmp isMinus, 0
					jz begin1
					call deleteSymbol
					mov isMinus, 0
				jmp begin1
			esck:
				begin4:
					cmp len, 0
					jz checkMinus2
					call deleteSymbol
					dec len
					jmp begin4
				checkMinus2:
					cmp isMinus, 0
					jz begin1
					call deleteSymbol
					mov isMinus, 0
				jmp begin1
			minus:
				cmp len, 0
				jnz begin1
				cmp isMinus, 1
				jz begin1
				mov isMinus, 1
				mov dl, '-'
				mov ah, 2
				int 21h
					
		jmp begin1
		
	save:
		mov ax, bx
		cmp isMinus, 0
		jz notinverse
		neg ax
		notinverse:
		pop dx
		pop cx
		pop bx
	
	ret
getNumber endp

printNumber proc
	push ax
	push bx
	push cx
	push dx
	
	xor cx, cx
	xor dx, dx
	mov bx, 10	
	cmp ax, 7fffh
	jc begin2
	mov cx, ax
	mov dl, '-'
	mov ah, 2
	int 21h
	mov ax, cx
	neg ax
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
	pop ax
	ret
printNumber endp

main:
	mov ax, @data
	mov ds, ax
	
	lea dx, msg1_1
	mov ah, 9
	int 21h 
	
	call getNumber
	mov cx, ax
	
	mov dl, 10
	mov ah, 2
	int 21h
	
	lea dx, msg1_2
	mov ah, 9
	int 21h
	
	call getNumber
	mov bx, ax
	mov ax, cx
	xor dx, dx
	cmp bx, 0
	jnz notnull
		lea dx, msg4
		mov ah, 9
		int 21h
		jmp waitkey
	notnull:
	cwd
	idiv bx
	mov bx, ax
	mov cx, dx
	
	mov dl, 10
	mov ah, 2
	int 21h

	lea dx, msg2
	mov ah, 9
	int 21h
	
	mov ax, bx
	call printNumber
	
	lea dx, msg3
	mov ah, 9
	int 21h

	mov ax, cx
	call printNumber

	waitkey:
	mov ah, 1
	int 21h

	mov ax, 4c00h
	int 21h
end main