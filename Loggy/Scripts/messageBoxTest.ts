
HtmlTools.DomReady(function ()
{
    // var greeter = new MessageBox("Bemerkung Quartalsreport", "Do you really want to do this ? ");
    
    var greeter = new MessageBox("Bemerkung Quartalsreport", `<script>alert("hello");</script> 
Do you really want to do <this> ? 
Вы действительно хотите <это> сделать ? 
你真的想<這樣>做嗎 ？ `);
    
    
    // greeter.buttons.add(new MessageBoxButton("1", "OK"));
    // greeter.buttons.add(new MessageBoxButton("2", "Cancel"));

    for (var i = 0; i < 10; ++i)
    {
        greeter.buttons.add(new MessageBoxButton(i.toString(), "Button" + (i)));
    }


    greeter.show();
}); // End DOMReady
