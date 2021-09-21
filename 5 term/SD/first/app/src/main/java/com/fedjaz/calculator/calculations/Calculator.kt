package com.fedjaz.calculator.calculations

import android.media.VolumeShaper
import android.opengl.Visibility
import android.view.View.GONE
import android.view.View.VISIBLE
import android.widget.TextView
import kotlin.math.round

class Calculator {
    private val defaultFunction: (Double) -> (Double) = {res:Double -> res}
    var mainBlock: CalculationBlock = CalculationBlock(defaultFunction, "", null, false)
    var currentBlock: CalculationBlock = mainBlock
    private var inputTextView: TextView? = null
    private var resultTextView: TextView? = null


    fun appendNumber(char: Char){
        if(currentBlock.isCompleted){
            if(currentBlock.operation == Operations.NONE){
                currentBlock.operation = Operations.MULTIPLY
            }
            currentBlock = currentBlock.parentBlock!!
            val newBlock = CalculationBlock(defaultFunction, "", currentBlock, true)
            currentBlock.blocks.add(newBlock)
            currentBlock = newBlock
        }
        if(!currentBlock.isPrimitive){
            currentBlock = CalculationBlock(defaultFunction, "", currentBlock, true)
            currentBlock.parentBlock?.blocks?.add(currentBlock)
            currentBlock.appendNumber(char)
        }
        else{
            currentBlock.appendNumber(char)
        }

        update()
    }

    fun appendOperation(operation: Operations){
        currentBlock.operation = operation
        currentBlock.isCompleted = true

        update()
    }

    fun appendFunction(stringPattern: String, function: (n1: Double) -> Double){
        if(!currentBlock.isPrimitive && currentBlock.blocks.isEmpty()){
            val newBlock = CalculationBlock(function, stringPattern, currentBlock,
                isPrimitive = false,
                requiresBrackets = true
            )
            currentBlock.blocks.add(newBlock)
            currentBlock = newBlock
        }
        else {
            if(currentBlock.operation == Operations.NONE)
            {
                currentBlock.operation = Operations.MULTIPLY
            }
            val newBlock = CalculationBlock(function, stringPattern, currentBlock.parentBlock,
                isPrimitive = false,
                requiresBrackets = true
            )
            currentBlock.parentBlock?.blocks?.add(newBlock)
            currentBlock = newBlock
        }
        update()
    }

    fun complete(){
        if(currentBlock.requiresBrackets && currentBlock.blocks.isEmpty()){
            return
        }
        if(currentBlock.parentBlock != null && currentBlock.parentBlock!!.requiresBrackets){
            currentBlock.parentBlock!!.isCompleted = true
            currentBlock = currentBlock.parentBlock!!
        }
        update()
    }

    fun setFloat(){
        if(currentBlock.operation != Operations.NONE || currentBlock.isCompleted){
            return
        }
        currentBlock.setFloat()
        update()
    }

    fun delete(){
        if(!currentBlock.isPrimitive && currentBlock.isCompleted && currentBlock.operation == Operations.NONE){
            currentBlock.delete()
            currentBlock = currentBlock.blocks.last()
        }
        else if(!currentBlock.delete()){
            val parent = currentBlock.parentBlock ?: return
            parent.blocks.remove(currentBlock)

            currentBlock = if(parent.blocks.count() == 0){
                parent
            } else{
                parent.blocks.last()
            }
        }

        update()
    }

    fun clear(){
        mainBlock = CalculationBlock(defaultFunction, "", null, false)
        currentBlock = mainBlock

        update()
    }

    private fun update(){
        if(mainBlock.blocks.isEmpty()){
            resultTextView?.visibility = GONE;
            inputTextView?.text = "0"
            return
        }
        else{
            resultTextView?.visibility = VISIBLE;
        }
        inputTextView?.text = mainBlock.toString()

        val result = evaluate()

        var resultString = String.format("= %.5f", result)
        resultString = resultString.trimEnd('0')
        if(resultString.last() == '.'){
            resultString = resultString.dropLast(1)
        }
        resultTextView?.text = resultString

    }

    fun evaluate() : Double{
         return mainBlock.evaluate()
    }

    fun setListeners(inputTextView: TextView, resultTextView: TextView){
        this.inputTextView = inputTextView
        this.resultTextView = resultTextView
        update()
    }

    fun factorial(n: Double) : Double{
        if (n < 0){
            throw IllegalArgumentException()
        }

        val num = round(n)
        if(num == 0.0){
            return 1.0
        }
        return num * factorial(num - 1)
    }
}