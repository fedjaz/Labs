.model small
.stack 100h
.data
	string db 100 dup('$')
	pi db 100 dup(0)
	len db 0
.code
.386

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

getString proc
	push ax
	push bx
	push cx
	lea di, string
	begin1:
		mov ah, 1
		int 21h
		cmp al, 10
		jz endf
		cmp al, 13
		jz endf

		cld 
		stosb
		
		inc len
		jmp begin1
	endf:
	pop cx
	pop bx
	pop ax
	ret
getstring endp

prefix proc
	push ax
	push bx
	push cx
	mov cx, 1
	xor ax, ax
	xor bx, bx
	begin4:
		mov bl, len
		cmp cx, bx
		jge end4
		lea si, pi
		add si, cx
		dec si
		lodsb

		begin5:
			cmp ax, 0
			jle end5
			lea si, string
			lea di, string
			add si, cx
			add di, ax
			cmpsb 
			je end5

			lea si, pi
			add si, ax
			dec si
			lodsb
		jmp begin5
		end5:
		lea si, string
		lea di, string
		add si, cx
		add di, ax
		cmpsb
		jne endif1
			inc ax
		endif1:
		lea di, pi
		add di, cx
		stosb
		inc cx
	jmp begin4

	end4:
	pop cx
	pop bx
	pop ax
	ret
prefix endp


main:
	mov ax, @data
	mov ds, ax
	mov es, ax
	
	call getstring
	
	call prefix

	lea si, pi
	xor ax, ax
	mov al, len
	dec ax
	add si, ax
	lodsb 

	xor bx, bx
	mov bl, len
	sub bx, ax
	push ax
	mov cx, 1
	lp1:
		cmp bl, len
		jge lp1end

		xor ax, ax
		lea si, pi
		add si, bx
		lodsb

		cmp ax, cx
		jne returnbad

		inc bx
		inc cx
		jmp lp1
	lp1end:
	pop ax
	xor bx, bx
	mov bl, len
	mov cx, bx
	sub bx, ax
	xor dx, dx

	div bx
	cmp dx, 0
	jne returnbad

	mov ax, 2
	xor dx, dx
	mul bx
	xor cx, cx
	mov cl, len
	cmp ax, cx
	jg returnbad
	mov ax, bx
	jmp returngood

	returnbad:
		xor ax, ax
		mov al, len
	returngood:
		call printNumber
	mov ah, 1
	int 21h
	mov ax, 4c00h
	int 21h
end main