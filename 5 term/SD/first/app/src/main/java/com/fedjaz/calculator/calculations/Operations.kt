package com.fedjaz.calculator.calculations

enum class Operations(val symbol: String) {
    ADD("+"),
    SUBTRACT("-"),
    MULTIPLY("*"),
    DIVIDE("/"),
    NONE("")
}

fun getOrder(operation: Operations) : Int{
    return if(operation == Operations.ADD || operation == Operations.SUBTRACT){
        1
    }
    else{
        2
    }
}

fun getOperation(operation: Operations) : (Double, Double) -> Double{
    return when (operation){
        Operations.ADD -> {n1 : Double, n2: Double -> n1 + n2 }
        Operations.SUBTRACT -> {n1 : Double, n2: Double -> n1 - n2 }
        Operations.MULTIPLY -> {n1 : Double, n2: Double -> n1 * n2 }
        Operations.DIVIDE -> {n1 : Double, n2: Double -> n1 / n2 }
        Operations.NONE -> { _: Double, _: Double -> 0.0 }
    }

}