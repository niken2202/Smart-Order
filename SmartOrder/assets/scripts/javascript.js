function w3_open() {
    document.getElementById("main").style.marginLeft = "20%";
    document.getElementById("mySidebar").style.width = "20%";
    document.getElementById("mySidebar").style.display = "block";
    document.getElementById("mySidebar").style.paddingTop = "1%";
    document.getElementById("mySidebar").style.paddingLeft = "2%";
    document.getElementById("mySidebar").style.paddingRight = "2%";
    document.getElementById("openNav").style.display = 'none';
    document.getElementById("mySidebar").style.backgroundColor = "white";
}
function w3_close() {
    document.getElementById("main").style.marginLeft = "0";
    document.getElementById("mySidebar").style.display = "none";
    document.getElementById("openNav").style.display = "inline-block";
}