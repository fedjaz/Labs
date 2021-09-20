package com.fedjaz.calculator.calculations

class CalculationBlock(private var resultFunction: (Double) -> Double,
                       private var stringPattern: String,
                       var parentBlock: CalculationBlock?) {

    var operation: Operations = Operations.NONE
    var number: String = ""
    var blocks: MutableList<CalculationBlock> = mutableListOf<CalculationBlock>()
    var isCompleted: Boolean = false
    var isNegative: Boolean = false
    var isPrimitive: Boolean = true

    constructor(resultFunction: (Double) -> Double,
                stringPattern: String,
                parentBlock: CalculationBlock,
                isPrimitive: Boolean) : this(resultFunction, stringPattern, parentBlock){
        this.isPrimitive = isPrimitive
    }

    fun appendNumber(char: Char){
        if(blocks.count() == 0){
            number += char
        }
    }

    fun setFloat(){
        if(number != "" && !number.contains(',')){
            number += ','
        }
    }

    fun delete() : Boolean{
        if(operation != Operations.NONE){
            operation = Operations.NONE
            return true
        }
        if(number != ""){
            number.dropLast(1)
            return true
        }
        if(blocks.count() > 0){
            if(blocks.last().delete()){
                blocks.dropLast(1)
            }
            return true
        }
        return false
    }

    override fun toString(): String {
        var output = ""
        if(isPrimitive){
            output += if (isNegative) "-" else "" + number
            return output + operation.symbol
        }
        for(block in blocks){
            output += block.toString()
        }
        output = stringPattern + output + if (stringPattern != "" && isCompleted) ")" else ""
        output = if(isNegative) "-$output" else output
        output += operation.symbol
        return output
    }
}