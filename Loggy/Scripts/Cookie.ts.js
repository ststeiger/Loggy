
namespace COR.Http.Cookie
{
	
	export function erase(name:string, path:string)
	{
	    create(name, "", path, -1);
	}
	
	
	export function read(name:string) 
	{
	    var nameEQ:string = name + "=";
	    var ca:string[] = document.cookie.split(';');

	    for(var i=0;i < ca.length;i++) 
	    {
	        var c:string = ca[i];
	        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
	        if (c.indexOf(nameEQ) == 0) 
	            return c.substring(nameEQ.length, c.length);
	    }

	    return null;
	}
	
	
	// http://stackoverflow.com/questions/14573223/set-cookie-and-get-cookie-with-javascript
	// http://blog.codinghorror.com/protecting-your-cookies-httponly/
	export function create(name:string, value:string, path:string, days:number) 
	{
	    var expires:string = "";
		
	    if(!path)
	        path = "/";
			
	    if (days) 
	    {
	        var date:Date = new Date();
	        date.setTime(date.getTime()+(days*24*60*60*1000));
	        expires = "expires=" + date.toUTCString() + "; ";
	    }
		
	    var allValues = name + "=" + encodeURIComponent(value) + "; " + expires + "path=" + path + ";";
	    if(window.location.protocol === 'https:')
	        allValues += " secure;"
			
	    document.cookie = allValues;
	}
	
	
	export function getDirName(path:string)
	{
	    if(path === null) return '/';
		
	    if(path.indexOf("/") !== -1)
	    {
	        var subs:string[] = path.split('/'); //break the string into an array
	        subs.pop(); //remove its last element
	        path = subs.join('/');  //join the array back into a string
	        if(path === '')
	            return '/';

	        return path;
	    }
		
	    return "/";
	}
	
	
}

// var value:string = "200CEB26807D6BF99FD6F4F0D1CA54D4¦12435¦Administrator DE";
// value = "4CF721379FA1319C0174E3FB5F0F2F9C¦12544¦Hafner Hanspeter";
// COR.Http.Cookie.create("proc", value, "/COR-Basic", 2);
// COR.Http.Cookie.create("proc", value, "/fm_cor_demo", 2);
// /fm_cor_demo/w8

// var test:string = '/this/is/a/path/to/a/file.html'
// test = 'file.html'
// test = '/file.html'
// COR.Http.Cookie.getDirName(test)
