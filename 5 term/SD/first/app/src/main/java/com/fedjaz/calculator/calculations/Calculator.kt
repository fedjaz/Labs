package com.fedjaz.calculator.calculations

import android.media.VolumeShaper

class Calculator {
    private val defaultFunction: (Double) -> (Double) = {res:Double -> res}
    var mainBlock: CalculationBlock = CalculationBlock(defaultFunction, "", null)
    var currentBlock: CalculationBlock = mainBlock

    fun appendNumber(char: Char){
        if(currentBlock.isCompleted){
            currentBlock = currentBlock.parentBlock!!
        }
        if(!currentBlock.isPrimitive){
            currentBlock = CalculationBlock(defaultFunction, "", currentBlock, true)
            currentBlock.parentBlock?.blocks?.add(currentBlock)
            currentBlock.appendNumber(char)
        }
        else{
            currentBlock.appendNumber(char)
        }
    }

    fun appendOperation(operation: String){
        currentBlock.operation = Operations.valueOf(operation)
    }


}