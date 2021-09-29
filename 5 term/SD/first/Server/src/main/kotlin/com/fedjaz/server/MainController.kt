package com.fedjaz.server

import org.springframework.stereotype.Controller
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RestController

@Controller
class MainController {
    @GetMapping("/")
    fun index(): String{
        return "index"
    }

    @GetMapping("/downloads")
    fun downloads(): String{
        return "downloads"
    }

    @GetMapping("/activation")
    fun activation(): String{
        return "activation"
    }
}