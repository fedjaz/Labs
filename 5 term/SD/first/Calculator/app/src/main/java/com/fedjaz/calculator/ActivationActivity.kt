package com.fedjaz.calculator

import android.content.ClipData
import android.content.ClipboardManager
import android.os.Bundle
import android.provider.Settings
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.core.content.ContextCompat
import com.google.android.material.textfield.TextInputEditText
import java.security.*
import java.security.spec.PKCS8EncodedKeySpec
import java.security.spec.X509EncodedKeySpec
import java.util.*

class ActivationActivity : AppCompatActivity() {
    private lateinit var activation : Activation
    private lateinit var deviceID: String
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_activation)

        activation = Activation(applicationContext)
        deviceID = activation.getDeviceID()

        val deviceIDView = findViewById<TextView>(R.id.deviceID)
        deviceIDView.text = deviceID
        setListeners()
    }

    private fun setListeners(){
        val copyButton = findViewById<Button>(R.id.copyButton)
        copyButton.setOnClickListener {
            val clipboard = ContextCompat.getSystemService(
                applicationContext,
                ClipboardManager::class.java
            )
            val clip = ClipData.newPlainText("code", deviceID)
            clipboard?.setPrimaryClip(clip)
        }

        val activateButton = findViewById<Button>(R.id.activateButton)
        activateButton.setOnClickListener {
            val codeInput = findViewById<TextInputEditText>(R.id.codeInput)
            checkActivation(codeInput.text.toString())
        }
    }

    private fun checkActivation(key: String){
        if(activation.checkKey(key)){
            Toast.makeText(applicationContext, "Application successfully activated", Toast.LENGTH_SHORT).show()
            finish()
        }
        else{
            Toast.makeText(applicationContext, "Activation key is incorrect", Toast.LENGTH_SHORT).show()
        }
    }

    override fun onBackPressed() {
        finishAffinity()
    }
}