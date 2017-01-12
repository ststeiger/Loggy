
Array.prototype.contains = function(obj) 
{
    var i = this.length;
    while (i--) 
    {
        if (this[i] === obj) 
        {
            return true;
        }
    }
    return false;
}



// Read a page's GET URL variables and return them as an associative array.
function getUrlVars()
{
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
 
    for(var i = 0; i < hashes.length; i++)
    {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
 
    return vars;
}


var dictParameters = getUrlVars();


if (dictParameters.contains("proc"))
{
    var strUserName = dictParameters["proc"].toLowerCase();
    // document.write("Benutzername: " + strUserName + "<br />");
    strUserName = GetMD5Hash(strUserName);
    // document.write("MD5: " + strUserName);
}
