.model small
.stack 100h
.data
	a dw 3
	b dw 6
	c dw 0
	d dw 7
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
		jnz else1
		mov ax, a
		add ax, b
		add ax, c
		add ax, d
		jmp endif1
		else1:
		mov ax, b
		and ax, c
		add ax, a
		jc else2
		
		mov bx, b
		and bx, c
		and bx, d
		cmp ax, bx
			jnz else2
			mov ax, a
			xor ax, b
			mov bx, c
			and bx, d
			xor ax, bx
			jmp endif2
			else2:
			mov ax, a
			xor ax, b
			mov bx, c
			and bx, d
			add ax, bx
			endif2:
		endif1:

	mov ax, 4c00h
	int 21h
end main