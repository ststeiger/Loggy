
function autoStartTest(error) {
    b = a + 1;
}


if (document.addEventListener) document.addEventListener("DOMContentLoaded", autoStartTest, false);
else if (document.attachEvent) document.attachEvent("onreadystatechange", autoStartTest);
else window.onload = autoStartTest;
