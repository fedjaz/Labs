package com.fedjaz.server

import org.springframework.core.io.InputStreamResource
import org.springframework.core.io.Resource
import org.springframework.http.HttpHeaders
import org.springframework.http.MediaType
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.*
import java.io.File
import java.io.FileInputStream

@RestController
class ActivationController {
    @PostMapping("/getKey")
    fun getKey(@RequestBody deviceID: String): String{
        val activation = Activation()
        return activation.createKey(deviceID)
    }

    @GetMapping("/download/full")
    fun downloadFull(): ResponseEntity<Resource>{
        val file = File("/Downloads/calculator-full.apk")
        val inputStreamResource = InputStreamResource(FileInputStream(file))
        val headers = HttpHeaders()
        headers.add(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=calculator-full.apk")
        return ResponseEntity.ok()
            .headers(headers)
            .contentLength(file.length())
            .contentType(MediaType.APPLICATION_OCTET_STREAM)
            .body(inputStreamResource)
    }

    @GetMapping("/download/demo")
    fun downloadDemo(): ResponseEntity<Resource>{
        val file = File("/Downloads/calculator-demo.apk")
        val inputStreamResource = InputStreamResource(FileInputStream(file))
        val headers = HttpHeaders()
        headers.add(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=calculator-demo.apk")
        return ResponseEntity.ok()
            .headers(headers)
            .contentLength(file.length())
            .contentType(MediaType.APPLICATION_OCTET_STREAM)
            .body(inputStreamResource)
    }
}
