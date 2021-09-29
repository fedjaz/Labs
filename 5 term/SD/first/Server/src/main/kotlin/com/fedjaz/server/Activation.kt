package com.fedjaz.server

import java.security.KeyFactory
import java.security.Signature
import java.security.spec.PKCS8EncodedKeySpec
import java.util.*

class Activation {
    private val privateKey = "MIICZQIBADCCAjkGByqGSM44BAEwggIsAoIBAQDR+2AaIELyMqxcHK1xqprr2rG7kvaD8BzUdqWEWLnkcuAiAy1Res5hPWNHb4w75Zh9B/9aQYhXzaQQkh3MUQRv7xpfo4vyoFIxM0pxnD6gzu24vTAU7PbVz2DzwpfGtz7/+L+rwNgqJq22MQKK1E3a6R8WzGWqOSSthYp7aIIm/g8eSwt89zhxVa8IvtGRUzHKdG2WRWtbyH36IG/dcNvh6pWyP84A10y2w/hKN3wNgWnNvL3+1E3QGRUaYUHUxd1t3/Ovj/HQwSxiGEFTwzodFXXM+q1jqSlmsqYVFwZfKyntzOYIZ4WNJexeWy/mm6TfsIffz8PiUsuZYfwdRzs9AiEAoVOiTMEt6cfBT5y5cAhuEDaX0I14GOTW8O7iL4Y1ufMCggEAeh/qT+NwLjKL75n36NW9afo4sTEqybJ5sY28XR8fZwKkaDebM/tad+wgD/ueaTiehLjWIgyoL5jDTgtzu5xTbiDSOa6KcrbF8ExhrVZjTY7//W0UdHcuJXoMklMSfP+7P8Wpdvw+p8NP5ovKEnm6LC7GysKiyHZkBjOvjkL3Kvm1p1HJNup4tTt3X8b5YZ9Oebd1ExoWSzDCvvtH11TqSuaKTf7MAi0ootGx1TMM8O2rSgTPkQOmos+4Qrp0rHZZ6SWmqhloWbHtitwvegT2J/GYdBzriJQqKoFjwUcmYXGZnl/xT1b43Ajh5Je/IeQwFMlMOVl+QNo9z7b3xEQJ7QQjAiEAjlQn+DHhRSYUOglnrfcJptIW3S8DAneCfMZ+W1Zjq/M="

    fun createKey(deviceID: String) : String{
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