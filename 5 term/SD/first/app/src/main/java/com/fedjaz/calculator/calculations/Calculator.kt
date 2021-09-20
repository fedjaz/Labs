package com.fedjaz.calculator.calculations

import android.media.VolumeShaper
import android.widget.TextView

class Calculator {
    private val defaultFunction: (Double) -> (Double) = {res:Double -> res}
    var mainBlock: CalculationBlock = CalculationBlock(defaultFunction, "", null, false)
    var currentBlock: CalculationBlock = mainBlock
    private var inputTextView: TextView? = null
    private var resultTextView: TextView? = null


    fun appendNumber(char: Char){
        if(currentBlock.isCompleted){
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

    fun delete(){
        if(!currentBlock.delete()){
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
        inputTextView?.text = mainBlock.toString()
        resultTextView?.text = evaluate().toString()
    }

    fun evaluate() : Double{
        return 0.0
    }

    fun setListeners(inputTextView: TextView, resultTextView: TextView){
        this.inputTextView = inputTextView
        this.resultTextView = resultTextView
    }


}