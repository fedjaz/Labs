.model small
.stack 100h
.data
	msg1 db "Type decimal number:$"
	msg2 db "This symbol is deprecated, try again$"
	msg3 db "This number is too large, try again$"
.code

getNumber proc
	push bx
	push cx
	push dx
	
	xor bx, bx
	xor cx, cx
	lea dx, msg1
	mov ah, 09h
	int 21h
	begin1:
		mov ah, 1
		int 21h
		xor ah, ah
		cmp al, 0Dh
		jz save
				
		lea dx, msg2
		sub al, '0'
		jc er
		cmp al, 10
		jnc er
		
		xor dx, dx
		mov cl, al
		mov ax, bx
		mov bx, 10
		mul bx
		cmp dx, 0
		lea dx, msg3
		jnz er
		
		add ax, cx
		jc er
		
		mov bx, ax
		jmp begin1
	
	er:
		mov bx, dx
		mov ah, 2
		mov dl, 0Ah
		int 21h
		mov dx, bx
		mov ah, 09h
		int 21h
		mov ah, 2
		mov dl, 0Ah
		int 21h
		
		xor bx, bx
		xor cx, cx
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
	
	call getNumber
	call printNumber
	mov ax, 4c00h
	int 21h
end main


