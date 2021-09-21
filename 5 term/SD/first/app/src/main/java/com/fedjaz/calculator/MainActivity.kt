package com.fedjaz.calculator

import android.content.Context
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Vibrator
import android.view.View
import android.view.View.GONE
import android.view.View.VISIBLE
import android.widget.Button
import android.widget.TextView
import com.fedjaz.calculator.calculations.Calculator
import com.fedjaz.calculator.calculations.Operations
import kotlin.math.sin

class MainActivity : AppCompatActivity() {
    private var scientificButtons: List<Int> =
            listOf(R.id.row1,
                    R.id.row2,
                    R.id.sqrtButton,
                    R.id.factorialButton,
                    R.id.reverseButton,
                    R.id.piButton,
                    R.id.eButton,
    )

    private lateinit var vibrator : Vibrator
    private var isScientific : Boolean = false
    private var calculator: Calculator = Calculator()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        vibrator = getSystemService(Context.VIBRATOR_SERVICE) as Vibrator
        val inputTextView = findViewById<TextView>(R.id.inputView)
        val resultTextView = findViewById<TextView>(R.id.resultView)
        calculator.setListeners(inputTextView, resultTextView)
        setListeners()
        showLess()
    }

    private fun setListeners() {
        val lessButton = findViewById<Button>(R.id.moreLessButton)
        lessButton.setOnClickListener {
            isScientific = if (isScientific) {
                showLess()
                false
            } else {
                showMore()
                true
            }
            vibrator.vibrate(25)
        }
        val buttons = listOf<Int>(
            R.id.n0Button,
            R.id.n1Button,
            R.id.n2Button,
            R.id.n3Button,
            R.id.n4Button,
            R.id.n5Button,
            R.id.n6Button,
            R.id.n7Button,
            R.id.n8Button,
            R.id.n9Button
        )

        for (id in buttons) {
            val button = findViewById<Button>(id)
            button.setOnClickListener {
                calculator.appendNumber('0' + buttons.indexOf(id))
                vibrator.vibrate(25)
            }
        }

        val addButton = findViewById<Button>(R.id.addButton)
        addButton.setOnClickListener {
            calculator.appendOperation(Operations.ADD)
            vibrator.vibrate(25)
        }

        val subtractButton = findViewById<Button>(R.id.subtractButton)
        subtractButton.setOnClickListener {
            calculator.appendOperation(Operations.SUBTRACT)
            vibrator.vibrate(25)
        }

        val multiplyButton = findViewById<Button>(R.id.multiplyButton)
        multiplyButton.setOnClickListener {
            calculator.appendOperation(Operations.MULTIPLY)
            vibrator.vibrate(25)
        }

        val divideButton = findViewById<Button>(R.id.divideButton)
        divideButton.setOnClickListener {
            calculator.appendOperation(Operations.DIVIDE)
            vibrator.vibrate(25)
        }

        val deleteButton = findViewById<Button>(R.id.deleteButton)
        deleteButton.setOnClickListener {
            calculator.delete()
            vibrator.vibrate(25)
        }

        val clearButton = findViewById<Button>(R.id.clearButton)
        clearButton.setOnClickListener {
            calculator.clear()
            vibrator.vibrate(25)
        }

        val sinButton = findViewById<Button>(R.id.sinButton)
        sinButton.setOnClickListener {
            calculator.appendFunction("sin(") { n1: Double -> sin(n1) }
            vibrator.vibrate(25)
        }

        val openBracketButton = findViewById<Button>(R.id.openBracketButton)
        openBracketButton.setOnClickListener {
            calculator.appendFunction("(") { n1: Double -> n1 }
            vibrator.vibrate(25)
        }

        val closeBracketButton = findViewById<Button>(R.id.closeBracketButton)
        closeBracketButton.setOnClickListener {
            calculator.complete()
            vibrator.vibrate(25)
        }

        val floatButton = findViewById<Button>(R.id.floatButton)
        floatButton.setOnClickListener {
            calculator.setFloat()
            vibrator.vibrate(25)
        }
    }

    private fun showMore(){
        for(id in scientificButtons){
            val view = findViewById<View>(id)
            view.visibility = VISIBLE;
        }
    }

    private fun showLess(){
        for(id in scientificButtons){
            val view = findViewById<View>(id)
            view.visibility = GONE;
        }
    }
}