
class color
{
    public rgb_R: number;
    public rgb_G: number;
    public rgb_B: number;

    public hsv_H: number;
    public hsv_S: number;
    public hsv_V: number;

    public hsl_H: number;
    public hsl_S: number;
    public hsl_L: number;

    public cmyk_C: number;
    public cmyk_M: number;
    public cmyk_Y: number;
    public cmyk_K: number;

}



function parseColor(input: string) :number[]
{
    var m:string, mm: RegExpMatchArray; 
    
    // Obviously, the numeric values will be easier to parse than names.So we do those first.
    mm = input.match(/^#?([0-9a-f]{3})$/i);
    
    if (mm)
    {
        m = mm[1];

        // in three-character format, each value is multiplied by 0x11 to give an
        // even scale from 0x00 to 0xff
        return [
            parseInt(m.charAt(0), 16) * 0x11,
            parseInt(m.charAt(1), 16) * 0x11,
            parseInt(m.charAt(2), 16) * 0x11
        ];
    }
    // That's one. Now for the full six-digit format: 
    mm = input.match(/^#?([0-9a-f]{6})$/i);
    if (mm)
    {
        m = mm[1];

        return [
            parseInt(m.substr(0, 2), 16),
            parseInt(m.substr(2, 2), 16),
            parseInt(m.substr(4, 2), 16)
        ];
    }
    // And now for rgb() format:
    var mm = input.match(/^rgb\s*\(\s*(\d+)\s*,\s*(\d+)\s*,\s*(\d+)\s*\)$/i);
    if (mm)
    {
        return [parseFloat(mm[1]), parseFloat(mm[2]), parseFloat(mm[3])];
    }

    // https://www.w3schools.com/colors/colors_names.asp
    // https://en.wikipedia.org/wiki/Web_colors
    // http://www.colors.commutercreative.com/grid/
    var webColors = {
        "AliceBlue": "#F0F8FF"
        , "AntiqueWhite": "#FAEBD7"
        , "Aqua": "#00FFFF"
        , "Aquamarine": "#7FFFD4"
        , "Azure": "#F0FFFF"
        , "Beige": "#F5F5DC"
        , "Bisque": "#FFE4C4"
        , "Black": "#000000"
        , "BlanchedAlmond": "#FFEBCD"
        , "Blue": "#0000FF"
        , "BlueViolet": "#8A2BE2"
        , "Brown": "#A52A2A"
        , "BurlyWood": "#DEB887"
        , "CadetBlue": "#5F9EA0"
        , "Chartreuse": "#7FFF00"
        , "Chocolate": "#D2691E"
        , "Coral": "#FF7F50"
        , "CornflowerBlue": "#6495ED"
        , "Cornsilk": "#FFF8DC"
        , "Crimson": "#DC143C"
        , "Cyan": "#00FFFF"
        , "DarkBlue": "#00008B"
        , "DarkCyan": "#008B8B"
        , "DarkGoldenRod": "#B8860B"
        , "DarkGray": "#A9A9A9"
        , "DarkGrey": "#A9A9A9"
        , "DarkGreen": "#006400"
        , "DarkKhaki": "#BDB76B"
        , "DarkMagenta": "#8B008B"
        , "DarkOliveGreen": "#556B2F"
        , "DarkOrange": "#FF8C00"
        , "DarkOrchid": "#9932CC"
        , "DarkRed": "#8B0000"
        , "DarkSalmon": "#E9967A"
        , "DarkSeaGreen": "#8FBC8F"
        , "DarkSlateBlue": "#483D8B"
        , "DarkSlateGray": "#2F4F4F"
        , "DarkSlateGrey": "#2F4F4F"
        , "DarkTurquoise": "#00CED1"
        , "DarkViolet": "#9400D3"
        , "DeepPink": "#FF1493"
        , "DeepSkyBlue": "#00BFFF"
        , "DimGray": "#696969"
        , "DimGrey": "#696969"
        , "DodgerBlue": "#1E90FF"
        , "FireBrick": "#B22222"
        , "FloralWhite": "#FFFAF0"
        , "ForestGreen": "#228B22"
        , "Fuchsia": "#FF00FF"
        , "Gainsboro": "#DCDCDC"
        , "GhostWhite": "#F8F8FF"
        , "Gold": "#FFD700"
        , "GoldenRod": "#DAA520"
        , "Gray": "#808080"
        , "Grey": "#808080"
        , "Green": "#008000"
        , "GreenYellow": "#ADFF2F"
        , "HoneyDew": "#F0FFF0"
        , "HotPink": "#FF69B4"
        , "IndianRed ": "#CD5C5C"
        , "Indigo ": "#4B0082"
        , "Ivory": "#FFFFF0"
        , "Khaki": "#F0E68C"
        , "Lavender": "#E6E6FA"
        , "LavenderBlush": "#FFF0F5"
        , "LawnGreen": "#7CFC00"
        , "LemonChiffon": "#FFFACD"
        , "LightBlue": "#ADD8E6"
        , "LightCoral": "#F08080"
        , "LightCyan": "#E0FFFF"
        , "LightGoldenRodYellow": "#FAFAD2"
        , "LightGray": "#D3D3D3"
        , "LightGrey": "#D3D3D3"
        , "LightGreen": "#90EE90"
        , "LightPink": "#FFB6C1"
        , "LightSalmon": "#FFA07A"
        , "LightSeaGreen": "#20B2AA"
        , "LightSkyBlue": "#87CEFA"
        , "LightSlateGray": "#778899"
        , "LightSlateGrey": "#778899"
        , "LightSteelBlue": "#B0C4DE"
        , "LightYellow": "#FFFFE0"
        , "Lime": "#00FF00"
        , "LimeGreen": "#32CD32"
        , "Linen": "#FAF0E6"
        , "Magenta": "#FF00FF"
        , "Maroon": "#800000"
        , "MediumAquaMarine": "#66CDAA"
        , "MediumBlue": "#0000CD"
        , "MediumOrchid": "#BA55D3"
        , "MediumPurple": "#9370DB"
        , "MediumSeaGreen": "#3CB371"
        , "MediumSlateBlue": "#7B68EE"
        , "MediumSpringGreen": "#00FA9A"
        , "MediumTurquoise": "#48D1CC"
        , "MediumVioletRed": "#C71585"
        , "MidnightBlue": "#191970"
        , "MintCream": "#F5FFFA"
        , "MistyRose": "#FFE4E1"
        , "Moccasin": "#FFE4B5"
        , "NavajoWhite": "#FFDEAD"
        , "Navy": "#000080"
        , "OldLace": "#FDF5E6"
        , "Olive": "#808000"
        , "OliveDrab": "#6B8E23"
        , "Orange": "#FFA500"
        , "OrangeRed": "#FF4500"
        , "Orchid": "#DA70D6"
        , "PaleGoldenRod": "#EEE8AA"
        , "PaleGreen": "#98FB98"
        , "PaleTurquoise": "#AFEEEE"
        , "PaleVioletRed": "#DB7093"
        , "PapayaWhip": "#FFEFD5"
        , "PeachPuff": "#FFDAB9"
        , "Peru": "#CD853F"
        , "Pink": "#FFC0CB"
        , "Plum": "#DDA0DD"
        , "PowderBlue": "#B0E0E6"
        , "Purple": "#800080"
        , "RebeccaPurple": "#663399"
        , "Red": "#FF0000"
        , "RosyBrown": "#BC8F8F"
        , "RoyalBlue": "#4169E1"
        , "SaddleBrown": "#8B4513"
        , "Salmon": "#FA8072"
        , "SandyBrown": "#F4A460"
        , "SeaGreen": "#2E8B57"
        , "SeaShell": "#FFF5EE"
        , "Sienna": "#A0522D"
        , "Silver": "#C0C0C0"
        , "SkyBlue": "#87CEEB"
        , "SlateBlue": "#6A5ACD"
        , "SlateGray": "#708090"
        , "SlateGrey": "#708090"
        , "Snow": "#FFFAFA"
        , "SpringGreen": "#00FF7F"
        , "SteelBlue": "#4682B4"
        , "Tan": "#D2B48C"
        , "Teal": "#008080"
        , "Thistle": "#D8BFD8"
        , "Tomato": "#FF6347"
        , "Turquoise": "#40E0D0"
        , "Violet": "#EE82EE"
        , "Wheat": "#F5DEB3"
        , "White": "#FFFFFF"
        , "WhiteSmoke": "#F5F5F5"
        , "Yellow": "#FFFF00"
        , "YellowGreen": "#9ACD32"
    };

    for (var p in webColors)
    {
        webColors[p.toLowerCase()] = webColors[p];
    }


    var wc = webColors[input];
    if (wc != null)
        return parseColor(wc);

    console.log(input);
    throw Error("'" + input + "' is not a valid color...");
}


// parseColor("steelblue");
// parseColor("SteelBlue");


// JSON.stringify(webColors, null, 2);
// JSON.stringify(lcWebColors, null, 2);




// https://gist.github.com/mjackson/5311256 
function rgbToHsl(r:number, g:number, b:number)
{
    r /= 255, g /= 255, b /= 255;

    var max:number = Math.max(r, g, b), min:number = Math.min(r, g, b);
    var h:number, s:number, l:number = (max + min) / 2;

    if (max == min)
    {
        h = s = 0; // achromatic
    }
    else
    {
        var d:number = max - min;
        s = l > 0.5 ? d / (2 - max - min) : d / (max + min);

        switch (max)
        {
            case r: h = (g - b) / d + (g < b ? 6 : 0); break;
            case g: h = (b - r) / d + 2; break;
            case b: h = (r - g) / d + 4; break;
        }

        h /= 6;
    }

    return [h, s, l];
}


// https://gist.github.com/mjackson/5311256 
function hslToRgb(h:number, s:number, l:number) : number[]
{
    var r:number, g:number, b:number;

    if (s == 0)
    {
        r = g = b = l; // achromatic
    }
    else
    {
        function hue2rgb(p, q, t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (t < 1 / 6) return p + (q - p) * 6 * t;
            if (t < 1 / 2) return q;
            if (t < 2 / 3) return p + (q - p) * (2 / 3 - t) * 6;
            return p;
        }

        var q:number = l < 0.5 ? l * (1 + s) : l + s - l * s;
        var p:number = 2 * l - q;

        r = hue2rgb(p, q, h + 1 / 3);
        g = hue2rgb(p, q, h);
        b = hue2rgb(p, q, h - 1 / 3);
    }

    return [
         Math.max(0, Math.min(Math.round(r * 255), 255)) 
        ,Math.max(0, Math.min(Math.round(g * 255), 255)) 
        ,Math.max(0, Math.min(Math.round(b * 255), 255)) 
    ];
}




function rgbToHex(col: number[]) : string
{
    function componentToHex(c : number) : string
    {
        var hex = c.toString(16);
        return hex.length == 1 ? "0" + hex : hex;
    }

    return "#" + componentToHex(col[0]) + componentToHex(col[1]) + componentToHex(col[2]);
}


function lightness(color:string) : number
{
    var col :number[] = parseColor(color);
    var colHSL : number[] = rgbToHsl(col[0], col[1], col[2]);
    // return colHSL[2];

    return parseFloat((colHSL[2] * 100).toFixed(5));
    // return Math.max(0, Math.min(Math.round(colHSL[2] * 100), 100)) 
}

function lighten(color:string, percent:number) :number[]
{
    var col:number[] = parseColor(color);
    var colHSL: number[] = rgbToHsl(col[0], col[1], col[2]);
    colHSL[2] = colHSL[2] + percent; // percent: 20% = 0.2

    if (colHSL[2] < 0)
        colHSL[2] = 0.0;

    if (colHSL[2] > 1.0)
        colHSL[2] = 1.0;

    return hslToRgb(colHSL[0], colHSL[1], colHSL[2]);
}


function darken(color:string, percent:number) :number[]
{
    return lighten(color, percent * -1.0);
}


var col:number[] = darken("Fuchsia", 0.2);
console.log(rgbToHex(col));
