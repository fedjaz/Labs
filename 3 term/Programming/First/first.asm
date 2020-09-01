.model small
.stack 100h
.data
	a dw 10
	b dw 9
	c dw 5
	d dw 4
.code
main:
	mov ax, @data
	mov ds, ax

	mov ax, a
	xor ax, b
	mov bx, c
	add bx, d
	
	cmp ax, bx
	jz ifbody1
	
		mov ax, a
		and ax, b
		cmp ax, bx
		mov ax, a
		jz ifbody2
			or ax, b
			or ax, c
			or ax, d
			jmp continue2
		ifbody2:
			xor ax, b
			xor ax, c
			xor ax, d
		continue2:
	jmp continue1
	ifbody1:
		mov ax, c
		and ax, a
		mov bx, b
		or bx, d
		add ax, bx
	
	continue1:

	mov ax, 4c00h
	int 21h
end main