package com.fedjaz.calculator

import android.content.Context
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.graphics.drawable.BitmapDrawable
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Vibrator
import android.view.View
import android.view.View.GONE
import android.view.View.VISIBLE
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import android.widget.Toast.LENGTH_SHORT
import androidx.appcompat.content.res.AppCompatResources
import com.fedjaz.calculator.calculations.Calculator
import com.fedjaz.calculator.calculations.Operations
import kotlin.math.*

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

    private fun setCompatibleImage(button: Button, id: Int, coeff: Double){

        val bitmap = BitmapFactory.decodeResource(applicationContext.resources, id)
        val widthCoeff = button.width.toDouble() / bitmap.width
        val width = (button.width * coeff).toInt()
        val height = (bitmap.height * widthCoeff * coeff).toInt()

        val newDrawable = BitmapDrawable(applicationContext.resources, Bitmap.createScaledBitmap(bitmap, width, height, true))

        button.setCompoundDrawablesWithIntrinsicBounds(newDrawable, null, null, null)
        button.setPadding((button.width - width) / 2, 0, 0, 0)
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
        lessButton.addOnLayoutChangeListener { v, left, top, right, bottom, oldLeft, oldTop, oldRight, oldBottom ->
            setCompatibleImage(lessButton, R.mipmap.moreless_foreground, 0.6)
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
                calculator.appendNumber(('0' + buttons.indexOf(id)).toString())
                vibrator.vibrate(25)
            }
        }

        val addButton = findViewById<Button>(R.id.addButton)
        addButton.setOnClickListener {
            calculator.appendOperation(Operations.ADD)
            vibrator.vibrate(25)
        }
        addButton.addOnLayoutChangeListener { _, _, _, _, _, _, _, _, _ ->
            setCompatibleImage(addButton, R.mipmap.add_foreground, 0.3)
        }

        val subtractButton = findViewById<Button>(R.id.subtractButton)
        subtractButton.setOnClickListener {
            calculator.appendOperation(Operations.SUBTRACT)
            vibrator.vibrate(25)
        }
        subtractButton.addOnLayoutChangeListener { _, _, _, _, _, _, _, _, _ ->
            setCompatibleImage(subtractButton, R.mipmap.subtract_foreground, 0.3)
        }

        val multiplyButton = findViewById<Button>(R.id.multiplyButton)
        multiplyButton.setOnClickListener {
            calculator.appendOperation(Operations.MULTIPLY)
            vibrator.vibrate(25)
        }
        multiplyButton.addOnLayoutChangeListener { _, _, _, _, _, _, _, _, _ ->
            setCompatibleImage(multiplyButton, R.mipmap.multiply_foreground, 0.3)
        }

        val divideButton = findViewById<Button>(R.id.divideButton)
        divideButton.setOnClickListener {
            calculator.appendOperation(Operations.DIVIDE)
            vibrator.vibrate(25)
        }
        divideButton.addOnLayoutChangeListener { _, _, _, _, _, _, _, _, _ ->
            setCompatibleImage(divideButton, R.mipmap.divide_foreground, 0.3)
        }

        val deleteButton = findViewById<Button>(R.id.deleteButton)
        deleteButton.setOnClickListener {
            calculator.delete()
            vibrator.vibrate(25)
        }
        deleteButton.addOnLayoutChangeListener { _, _, _, _, _, _, _, _, _ ->
            setCompatibleImage(deleteButton, R.mipmap.remove_foreground, 0.5)
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

        val cosButton = findViewById<Button>(R.id.cosButton)
        cosButton.setOnClickListener {
            calculator.appendFunction("cos(") { n1: Double -> cos(n1) }
            vibrator.vibrate(25)
        }

        val tanButton = findViewById<Button>(R.id.tanButton)
        tanButton.setOnClickListener {
            calculator.appendFunction("tan(") { n1: Double -> tan(n1) }
            vibrator.vibrate(25)
        }

        val powButton = findViewById<Button>(R.id.powButton)
        powButton.setOnClickListener {
            calculator.appendOperation(Operations.POWER)
            vibrator.vibrate(25)
        }
        powButton.addOnLayoutChangeListener { _, _, _, _, _, _, _, _, _ ->
            setCompatibleImage(powButton, R.mipmap.power_foreground, 0.4)
        }

        val degButton = findViewById<Button>(R.id.degButton)
        degButton.setOnClickListener {
            calculator.appendFunction("deg(") { n1: Double -> (Math.toDegrees(n1)) }
            vibrator.vibrate(25)
        }

        val radButton = findViewById<Button>(R.id.radButton)
        radButton.setOnClickListener {
            calculator.appendFunction("rad(") { n1: Double -> (Math.toRadians(n1)) }
            vibrator.vibrate(25)
        }

        val lgButton = findViewById<Button>(R.id.lgButton)
        lgButton.setOnClickListener {
            calculator.appendFunction("lg(") { n1: Double -> (log10(n1)) }
            vibrator.vibrate(25)
        }

        val lnButton = findViewById<Button>(R.id.lnButton)
        lnButton.setOnClickListener {
            calculator.appendFunction("ln(") { n1: Double -> (ln(n1)) }
            vibrator.vibrate(25)
        }

        val sqrtButton = findViewById<Button>(R.id.sqrtButton)
        sqrtButton.setOnClickListener {
            calculator.appendFunction("sqrt(") { n1: Double -> (sqrt(n1)) }
            vibrator.vibrate(25)
        }
        sqrtButton.addOnLayoutChangeListener { _, _, _, _, _, _, _, _, _ ->
            setCompatibleImage(sqrtButton, R.mipmap.sqrt_foreground, 0.4)
        }

        val reverseButton = findViewById<Button>(R.id.reverseButton)
        reverseButton.setOnClickListener {
            calculator.appendFunction("rev(") { n1: Double -> 1 / n1 }
            vibrator.vibrate(25)
        }
        reverseButton.addOnLayoutChangeListener { _, _, _, _, _, _, _, _, _ ->
            setCompatibleImage(reverseButton, R.mipmap.reverse_foreground, 0.4)
        }

        val factorialButton = findViewById<Button>(R.id.factorialButton)
        factorialButton.setOnClickListener {
            calculator.appendFunction("fact(") { n1: Double -> calculator.factorial(n1) }
            vibrator.vibrate(25)
        }
        factorialButton.addOnLayoutChangeListener { _, _, _, _, _, _, _, _, _ ->
            setCompatibleImage(factorialButton, R.mipmap.factorial_foreground, 0.4)
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

        val percentButton = findViewById<Button>(R.id.percentButton)
        percentButton.setOnClickListener {
            calculator.percent()
            vibrator.vibrate(25)
        }
        percentButton.addOnLayoutChangeListener { _, _, _, _, _, _, _, _, _ ->
            setCompatibleImage(percentButton, R.mipmap.percent_foreground, 0.4)
        }

        val eButton = findViewById<Button>(R.id.eButton)
        eButton.setOnClickListener {
            calculator.appendNumber("e")
            vibrator.vibrate(25)
        }

        val piButton = findViewById<Button>(R.id.piButton)
        piButton.setOnClickListener {
            calculator.appendNumber("π")
            vibrator.vibrate(25)
        }

        val equalsButton = findViewById<Button>(R.id.equalsButton)
        equalsButton.setOnClickListener {
            calculator.equals()
            vibrator.vibrate(25)
        }
        equalsButton.addOnLayoutChangeListener { _, _, _, _, _, _, _, _, _ ->
            setCompatibleImage(equalsButton, R.mipmap.equals_foreground, 0.3)
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