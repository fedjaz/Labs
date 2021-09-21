package com.fedjaz.calculator.calculations

import android.media.VolumeShaper
import android.opengl.Visibility
import android.provider.Settings.Global.getString
import android.view.View.GONE
import android.view.View.VISIBLE
import android.widget.TextView
import com.fedjaz.calculator.R
import kotlin.math.round

class Calculator {
    private val defaultFunction: (Double) -> (Double) = {res:Double -> res}
    var mainBlock: CalculationBlock = CalculationBlock(defaultFunction, "", null, false)
    var currentBlock: CalculationBlock = mainBlock
    private var inputTextView: TextView? = null
    private var resultTextView: TextView? = null


    fun appendNumber(char: Char){
        if(currentBlock.requiresFilling){
            currentBlock.requiresFilling = false
            currentBlock.requiresBrackets = false
            currentBlock.isPrimitive = true
            currentBlock.stringPattern = ""
            currentBlock.resultFunction = {n: Double -> n}
        }
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
        if((!currentBlock.isPrimitive && !currentBlock.isCompleted) || currentBlock == mainBlock){
            if(operation == Operations.SUBTRACT){
                val newBlock = CalculationBlock(currentBlock, true)
                newBlock.isNegative = true
                currentBlock.blocks.add(newBlock)
                currentBlock = newBlock
            }
            update()
            return
        }
        currentBlock.operation = operation
        currentBlock.isCompleted = true

        update()
    }

    fun appendFunction(stringPattern: String, function: (n1: Double) -> Double){
        if(currentBlock.requiresFilling){
            currentBlock.stringPattern = stringPattern
            currentBlock.resultFunction = function
            currentBlock.isPrimitive = false
            currentBlock.requiresFilling = false
            currentBlock.requiresBrackets = true
        }
        else if(!currentBlock.isPrimitive && currentBlock.blocks.isEmpty()){
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

        val result = try{
            evaluate()
        }
        catch (e: Exception){
            resultTextView?.text = "Error"
            return
        }
        var resultString = if(result < 1e10 && result > -1e10){
            String.format("= %.5f", result).trimEnd('0')
        }
        else{
            String.format("= %.5e", result)
        }
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