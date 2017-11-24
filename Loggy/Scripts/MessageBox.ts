
class MessageBoxButton
{
    private m_id: string;
    private m_text: string;
    private m_class: string;

    private m_onclick: string;
    private m_callback: (this: HTMLElement, ev: MouseEvent) => void;



    constructor(id: string, text: string, callback?: any)
    {
        this.m_id = id;
        this.m_text = text;
        this.m_class = "button";

        this.m_callback = callback;
    }


    create(): HTMLAnchorElement
    {
        var aa: HTMLAnchorElement = document.createElement("a");
        aa.text = this.m_text;
        aa.href = "#";
        aa.className = this.m_class;
        aa.target = "_blank";
        aa.onclick = this.m_callback;

        if (this.m_onclick != null)
            aa.setAttribute("onclick", this.m_onclick);

        return aa;
    }



    get class(): string
    {
        return this.m_class;
    }

    set class(value: string)
    {
        this.m_class = value;
    }


    get id(): string
    {
        return this.m_id;
    }

    set id(value: string)
    {
        this.m_id = value;
    }

    get text(): string
    {
        return this.m_text;
    }

    set text(value: string)
    {
        this.m_text = value;
    }

    get callback(): (this: HTMLElement, ev: MouseEvent) => void
    {
        return this.m_callback;
    }

    set callback(value: (this: HTMLElement, ev: MouseEvent) => void)
    {
        this.m_callback = value;
    }

    get onclick(): string
    {
        return this.m_onclick;
    }

    set onclick(value: string)
    {
        this.m_onclick = value;
    }



}



class List<T>
{
    private m_items: Array<T>;

    constructor()
    {
        this.m_items = [];
    }

    get items(): Array<T>
    {
        return this.m_items;
    }

    set items(value: Array<T>)
    {
        this.m_items = value;
    }

    get length(): number
    {
        return this.m_items.length;
    }

    add(value: T): void
    {
        this.m_items.push(value);
    }

    get(index: number): T
    {
        return this.m_items[index];
    }
}



class MessageBox
{
    private m_buttonCollection: List<MessageBoxButton>;

    private m_title: string;
    private m_message: string;
    private m_html: string;
    

    constructor(title: string, message: string)
    {
        this.m_buttonCollection = new List<MessageBoxButton>();

        this.m_title = title;
        this.m_message = message;
    }

    get buttons(): List<MessageBoxButton>
    {
        return this.m_buttonCollection;
    }

    set buttons(value: List<MessageBoxButton>)
    {
        this.m_buttonCollection = value;
    }


    get title(): string
    {
        return this.m_title;
    }

    set title(value: string)
    {
        this.m_title = value;
    }


    get message(): string
    {
        return this.m_message;
    }

    set message(value: string)
    {
        this.m_message = value;
    }

    get htmlMessage(): string
    {
        return this.m_html;
    }

    set htmlMessage(value: string)
    {
        this.m_html = value;
    }

    

    foo(this: HTMLElement, ev: MouseEvent): void
    {

    }


    show()
    {
        const overlayStyle: string = `
position:fixed; 
z-index: 9990;
left: 0px; right: 0px; top: 0px; bottom: 0px; 
background-color: rgba(11,11,11,0.6);
background: -moz-linear-gradient(top, rgba(11,11,11,0.1) 0%, rgba(11,11,11,0.6) 100%);
background: -webkit-gradient(left top, left bottom, color-stop(0%, rgba(11,11,11,0.1)), color-stop(100%, rgba(11,11,11,0.6)));
background: -webkit-linear-gradient(top, rgba(11,11,11,0.1) 0%, rgba(11,11,11,0.6) 100%);
background: -o-linear-gradient(top, rgba(11,11,11,0.1) 0%, rgba(11,11,11,0.6) 100%);
background: -ms-linear-gradient(top, rgba(11,11,11,0.1) 0%, rgba(11,11,11,0.6) 100%);
background: linear-gradient(to bottom, rgba(11,11,11,0.1) 0%, rgba(11,11,11,0.6) 100%);
`;



        const msgboxStyle: string = `
display: block; width: 500px; height: 500px; 
position:absolute;
left: 50%;
top: 50%;

-webkit-transform: translate(-50%,-50%);
-ms-transform: translate(-50%,-50%);
transform: translate(-50%,-50%);

margin: auto;
background-color: #888;
border: 1px solid black;
box-shadow: 4px 4px 5px 0px #000;
-moz-box-shadow: 4px 4px 5px 0px #000;
-webkit
`;

        const titleStyle: string = `
font: 0.65cm/1 'Cuprum','Lucida Sans Unicode', 'Lucida Grande', sans-serif;
background: url(../../images/Confirm/header_bg.jpg) repeat-x left bottom #f5f5f5;

background: rgba(255,255,255,1);
background: -moz-linear-gradient(top, rgba(255,255,255,1) 0%, rgba(246,246,246,1) 37%, rgba(237,237,237,1) 100%);
background: -webkit-gradient(left top, left bottom, color-stop(0%, rgba(255,255,255,1)), color-stop(37%, rgba(246,246,246,1)), color-stop(100%, rgba(237,237,237,1)));
background: -webkit-linear-gradient(top, rgba(255,255,255,1) 0%, rgba(246,246,246,1) 37%, rgba(237,237,237,1) 100%);
background: -o-linear-gradient(top, rgba(255,255,255,1) 0%, rgba(246,246,246,1) 37%, rgba(237,237,237,1) 100%);
background: -ms-linear-gradient(top, rgba(255,255,255,1) 0%, rgba(246,246,246,1) 37%, rgba(237,237,237,1) 100%);
background: linear-gradient(to bottom, rgba(255,255,255,1) 0%, rgba(246,246,246,1) 37%, rgba(237,237,237,1) 100%);
filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#ffffff', endColorstr='#ededed', GradientType=0 );


padding: 10px 25px;
text-shadow: 0.5mm 0.5mm 0 rgba(255, 255, 255, 0.6);
color: #666;

letter-spacing: 0.3px;
white-space: nowrap;
overflow: hidden;
color: #888;
padding: 14px 25px;
margin-top: 0px;
`;

        // https://css-tricks.com/almanac/properties/j/justify-content/
        const footerStyle: string = `
padding: 3mm;
display: flex;
flex-direction: row;
flex-wrap: wrap;
align-items: center;
#justify-content: space-between;
#justify-content: space-around;
justify-content: center;
`;


        var overlay: HTMLDivElement = document.createElement("div");
        var msgbox: HTMLDivElement = document.createElement("div");
        var title: HTMLDivElement = document.createElement("div");
        var titleAfter: HTMLDivElement = document.createElement("div");
        var content: HTMLDivElement = document.createElement("div");
        var contentAfter: HTMLDivElement = document.createElement("div");
        var footer: HTMLDivElement = document.createElement("div");
        var footerAfter: HTMLDivElement = document.createElement("div");

        overlay.id = "uuid_" + this.guid("");
        overlay.setAttribute("style", overlayStyle);

        msgbox.setAttribute("style", msgboxStyle);

        title.setAttribute("style", titleStyle);
        titleAfter.setAttribute("style", "clear: both;");

        content.setAttribute("style", "background-color: red; color: white; font-weight: bold; padding: 5mm;");
        contentAfter.setAttribute("style", "clear: both;");
        
        footer.setAttribute("style", footerStyle);
        footerAfter.setAttribute("style", "clear: both;");


        

        var cb = `
confirmBox h1 
{
   letter-spacing: 0.3px;
   white-space: nowrap;
   overflow: hidden;
   color: #888;
   padding: 14px 25px;
   margin-top: 0px;
}

#confirmBox h1, #confirmBox p 
{
   font: 19px/1 'Cuprum','Lucida Sans Unicode', 'Lucida Grande', sans-serif;
   background: url(../../images/Confirm/header_bg.jpg) repeat-x left bottom #f5f5f5;
   padding: 10px 25px;
   text-shadow: 1px 1px 0 rgba(255, 255, 255, 0.6);
   color: #666;
}
`;


        var tn = document.createTextNode(this.title);
        title.appendChild(tn);

        if (this.m_html == null)
        {
            var lines: string[] = this.message.split("\n");

            for (var l = 0; l < lines.length; ++l)
            {
                var tnContent = document.createTextNode(lines[l]);
                content.appendChild(tnContent);
                if (l != lines.length - 1)
                {
                    content.appendChild(document.createElement("br"));
                }

            } // Next l
        }
        else
        { 
            // https://developer.mozilla.org/en-US/docs/Web/API/Element/insertAdjacentHTML
            content.insertAdjacentHTML('beforeend', this.m_html);
        }

        msgbox.appendChild(title);
        msgbox.appendChild(titleAfter);
        msgbox.appendChild(content);
        msgbox.appendChild(contentAfter);

        
        for (var k: number = 0; k < this.m_buttonCollection.length; ++k)
        {
            var btn: HTMLAnchorElement = this.m_buttonCollection.items[k].create();

            footer.appendChild(btn);
        }

        msgbox.appendChild(footer);
        msgbox.appendChild(footerAfter);
        overlay.appendChild(msgbox);

        document.body.appendChild(overlay);
    }


    // ID and NAME tokens must begin with a letter ([A-Za-z]) 
    // and may be followed by any number of 
    // letters, digits([0 - 9]), hyphens("-"), underscores("_"), colons(":"), and periods (".").
    // You can technically use colons and periods in id/name attributes, 
    // but I would strongly suggest avoiding both.
    // In CSS both the period and the colon have special meaning 
    // Periods are class selectors 
    // and colons are pseudo-selectors(eg., ":hover").
    private guid(s: string = "-")
    {
        function s4()
        {
            return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
        }

        return s4() + s4() + s + s4() + s + s4() + s + s4() + s + s4() + s4() + s4();
    }

    hide()
    {
        var buttons: string = `
<div id="` + "uuid_" + this.guid("") + `" class="overlay">
    <div id="" class="confirmBox">
        <h1>Title</h1>
        <div class="content"></div>
        <div class="buttons"></div>
    </div>
</div>
`;



    }

    toggle()
    { }
}


// let greeter = new MessageBox("Bemerkung Quartalsreport", "world");
// greeter.buttons.add(new MessageBoxButton("1", "OK"));
// greeter.show();

//greeter.buttons.add(new MessageBoxButton("foo", "bar"));



//var map: { [email: string]: string; } = {};
//map["foo"] = "bar";
//console.log(map["foo"]);
