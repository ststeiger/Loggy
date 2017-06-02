HtmlTools.DomReady(function () {
    // var greeter = new MessageBox("Bemerkung Quartalsreport", "Do you really want to do this ? ");
    var greeter = new MessageBox("Bemerkung Quartalsreport", "<script>alert(\"hello\");</script> \nDo you really want to do <this> ? \n\u0412\u044B \u0434\u0435\u0439\u0441\u0442\u0432\u0438\u0442\u0435\u043B\u044C\u043D\u043E \u0445\u043E\u0442\u0438\u0442\u0435 <\u044D\u0442\u043E> \u0441\u0434\u0435\u043B\u0430\u0442\u044C ? \n\u4F60\u771F\u7684\u60F3<\u9019\u6A23>\u505A\u55CE \uFF1F ");
    // greeter.buttons.add(new MessageBoxButton("1", "OK"));
    // greeter.buttons.add(new MessageBoxButton("2", "Cancel"));
    for (var i = 0; i < 10; ++i) {
        greeter.buttons.add(new MessageBoxButton(i.toString(), "Button" + (i)));
    }
    greeter.show();
}); // End DOMReady
//# sourceMappingURL=messageBoxTest.js.map