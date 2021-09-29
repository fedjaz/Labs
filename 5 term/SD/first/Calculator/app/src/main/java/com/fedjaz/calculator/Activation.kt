package com.fedjaz.calculator

import android.annotation.SuppressLint
import android.content.Context
import android.provider.Settings
import android.util.Base64
import java.lang.Exception
import java.security.KeyFactory
import java.security.Signature
import java.security.spec.PKCS8EncodedKeySpec
import java.security.spec.X509EncodedKeySpec

class Activation(var context: Context) {
    fun checkActivation() : Boolean{
        val keyPreference = context.getSharedPreferences("CalculatorActivation", Context.MODE_PRIVATE) ?: return false
        val key = keyPreference.getString("key", null) ?: return false

        return checkKey(key)
    }

    @SuppressLint("HardwareIds")
    fun getDeviceID() : String{
        return Settings.Secure.getString(
            context.contentResolver,
            Settings.Secure.ANDROID_ID
        )
    }

    fun checkKey(key: String) : Boolean{
        val keyBytes = android.util.Base64.decode(
            key,
            Base64.NO_WRAP
        )

        val publicBytes = android.util.Base64.decode(
            context.resources.getString(R.string.publicKey),
            Base64.NO_WRAP
        )

        val keyFactory = KeyFactory.getInstance("DSA")
        val publicKey = keyFactory.generatePublic(X509EncodedKeySpec(publicBytes))

        val signature = Signature.getInstance("SHA256withDSA")
        signature.initVerify(publicKey)
        signature.update(getDeviceID().toByteArray())
        val res: Boolean
        try {
            res = signature.verify(keyBytes)
        }
        catch (e: Exception){
            return false
        }
        return if(res){
            saveKey(key)
            true
        }
        else{
            false
        }
    }

    private fun saveKey(key: String){
        val keyPreference = context.getSharedPreferences("CalculatorActivation", Context.MODE_PRIVATE)
        val editor = keyPreference.edit()
        editor.putString("key", key)
        editor.apply()
    }

    fun isDemo() : Boolean{
        val pInfo = context.packageManager.getPackageInfo(context.packageName, 0)
        val version = pInfo.versionName
        return "demo" in version
    }
}