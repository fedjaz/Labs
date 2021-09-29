package com.fedjaz.server

import java.security.KeyFactory
import java.security.KeyPairGenerator
import java.security.Signature
import java.security.spec.PKCS8EncodedKeySpec
import java.util.*

class Activation {
    private val privateKey = System.getenv("privateKey")

    fun createKey(deviceID: String) : String{
        gen()
        val privateKeyBytes = Base64.getDecoder().decode(
            privateKey
        )

        val keyFactory = KeyFactory.getInstance("DSA")
        val privateKey = keyFactory.generatePrivate(PKCS8EncodedKeySpec(privateKeyBytes))

        val signature = Signature.getInstance("SHA256withDSA")
        signature.initSign(privateKey)
        signature.update(deviceID.toByteArray())
        val keyBytes = signature.sign()
        return Base64.getEncoder().encodeToString(keyBytes)
    }
}
