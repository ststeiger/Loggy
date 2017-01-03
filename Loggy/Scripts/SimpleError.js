
//function LogError(error) {
function autoStartTest(error) {

    var onFail = function (r) {
        // throw "is not a number";
        console.log("fail");
    }

    var onSuccess = function (r) {
        // r = JSON.parse(r);
        // COR.Basic.SIBE.Populate(r.data);

        // r.data[i].SIBE_UID
        // console.log(r.data);
        console.log(r);
    };

    var uid = "NEWID()";

    var postData = {
        SQL: "Test 123" 
        , "uid": uid
    };


    console.log(postData);

    new Http.Post("./ajax/jsErrorLog.ashx", postData)
        .success(onSuccess)
        // .failure(onFail)
        // .always(cbAlways)
        .send()
        ;
}


if (document.addEventListener) document.addEventListener("DOMContentLoaded", autoStartTest, false);
else if (document.attachEvent) document.attachEvent("onreadystatechange", autoStartTest);
else window.onload = autoStartTest;
