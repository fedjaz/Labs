package com.fedjaz.calculator

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.view.View.GONE
import android.view.View.VISIBLE
import android.widget.Button
import android.widget.TableRow

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

    private var isScientific : Boolean = false

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        setListeners()
        showLess()
    }

    private fun setListeners(){
        val lessButton = findViewById<Button>(R.id.moreLessButton)
        lessButton.setOnClickListener {
            isScientific = if(isScientific){
                showLess()
                false
            } else{
                showMore()
                true
            }
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