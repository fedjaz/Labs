function getKey(){
    var deviceID = document.getElementsByClassName("input")[0].value;
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState === 4) {
                res = xmlHttp.responseText;
                var content = document.getElementById("cont");
                var element = document.createElement("span");
                var attr1 = document.createAttribute("class");
                attr1.value = "description";
                element.style.fontSize = "0.6em"
                element.style.width = "fit-content"
                element.setAttributeNode(attr1);
                element.appendChild(document.createTextNode("Your code: " + res));
                content.append(element);
            }};
    xmlHttp.open("POST", "/getKey");
    xmlHttp.send(deviceID);
}