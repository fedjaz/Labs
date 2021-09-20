package com.fedjaz.calculator.calculations

import android.media.VolumeShaper

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
                parentBlock: CalculationBlock?,
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
            number = number.dropLast(1)
            return number.isNotEmpty()
        }
        if(blocks.count() > 0){
            if(blocks.last().delete()){
                blocks.dropLast(1)
            }
            return true
        }
        return false
    }

    fun evaluate() : Double{
        if(isPrimitive){
            val num = number.toDoubleOrNull() ?: return 0.0
            return num * if(isNegative) -1 else 1
        }

        val evaluated = mutableListOf<Double>()
        val operations = mutableListOf<Operations>()
        if(blocks.isEmpty()){
            return 0.0
        }
        for(block in blocks){
            evaluated.add(block.evaluate())
            operations.add(block.operation)
        }

        var i = 0
        while(i < operations.count() - 1){
            val operation = operations[i]
            if(getOrder(operation) == 2){
                val n1 = evaluated[i]
                val n2 = evaluated[i + 1]
                val res = getOperation(operation)(n1, n2)
                evaluated[i] = res
                evaluated.removeAt(i + 1)
                operations.removeAt(i)
                i--
            }
            i++
        }

        var result = evaluated[0]
        i = 0
        while(i < operations.count() - 1){
            val operation = operations[i]
            result = getOperation(operation)(result, evaluated[i + 1])
            i++
        }
        return resultFunction(result) * if (isNegative) -1 else 1
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