.model small
.stack 100h
.data
	a dw 0
	b dw 2
	c dw 7
	d dw 9
.code
main:
	mov ax, @data
	mov ds, ax

	mov ax, a
	xor ax, b
	xor ax, c
	xor ax, d
	
	mov bx, a
	or bx, b
	or bx, c
	or bx, d
	
	cmp ax, bx
	jz ifbody1
	
		mov ax, b
		or ax, c
		add ax, a
		
		mov bx, b
		or bx, c
		or bx, d
		
		cmp ax, bx
		jz ifbody2
			mov ax, a
			xor ax, b
			mov bx, c
			and bx, d
			add ax, bx
			jmp continue2
		ifbody2:
			mov ax, a
			xor ax, b
			mov bx, c
			and bx, d
			xor ax, bx
		continue2:
	jmp continue1
	ifbody1:
		mov ax, a
		add ax, b
		add ax, c
		add ax, d
	
	continue1:

	mov ax, 4c00h
	int 21h
end main