﻿
namespace Loggy
{


    /// <summary>
    /// Zusammenfassungsbeschreibung für table
    /// </summary>
    public class table : System.Web.IHttpHandler
    {

        private static System.Random seed = new System.Random();


        public static int rand(int min, int max)
        {
            return seed.Next(min, max + 1);
        } // End Function rand 


        public void ProcessRequest(System.Web.HttpContext context)
        {
            string html = @"<!DOCTYPE html>
<html xmlns=""http://www.w3.org/1999/xhtml"" lang=""en"">
<head>
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"" />

    <meta http-equiv=""cache-control"" content=""max-age=0"" />
    <meta http-equiv=""cache-control"" content=""no-cache"" />
    <meta http-equiv=""expires"" content=""0"" />
    <meta http-equiv=""expires"" content=""Tue, 01 Jan 1980 1:00:00 GMT"" />
    <meta http-equiv=""pragma"" content=""no-cache"" />

    <meta charset=""utf-8"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />

    <meta http-equiv=""Content-Language"" content=""en"" />
    <meta name=""viewport"" content=""width=device-width,initial-scale=1"" />


    <!--
    <meta name=""author"" content=""name"" />
    <meta name=""description"" content=""description here"" />
    <meta name=""keywords"" content=""keywords,here"" />

    <link rel=""shortcut icon"" href=""favicon.ico"" type=""image/vnd.microsoft.icon"" />
    <link rel=""stylesheet"" href=""stylesheet.css"" type=""text/css"" />
    -->

    <title>Title</title>
    <style type=""text/css"" media=""all"">
        body
        {
            background-color: #0c70b4;
            color: #546775;
            font: normal 400 18px ""PT Sans"", sans-serif;
            -webkit-font-smoothing: antialiased;
        }

        .Table 
        {
            display: -webkit-box;
            display: -moz-box;
            display: box;
            display: -webkit-flex;
            display: -moz-flex;
            display: -ms-flexbox;
            display: flex;
            -webkit-flex-flow: column nowrap;
            -moz-flex-flow: column nowrap;
            flex-flow: column nowrap;
            -webkit-box-pack: justify;
            -moz-box-pack: justify;
            box-pack: justify;
            -webkit-justify-content: space-between;
            -moz-justify-content: space-between;
            -ms-justify-content: space-between;
            -o-justify-content: space-between;
            justify-content: space-between;
            -ms-flex-pack: justify;
            border: 1px solid #f2f2f2;
            font-size: 1rem;
            margin: 0.5rem;
            line-height: 1.5;
        }

        .Table-header 
        {
            display: none;
        }


        @media (min-width: 500px) 
        {

            .Table-header 
            {
            font-weight: 700;
            background-color: #f2f2f2;
            }

        }


        .Table-row 
        {
            width: 100%;
        }

        .Table-row:nth-of-type(even) 
        {
            background-color: #f2f2f2;
        }

        .Table-row:nth-of-type(odd) 
        {
            background-color: #ffffff;
        }


        @media (min-width: 500px) 
        {

            .Table-row 
            {
                display: -webkit-box;
                display: -moz-box;
                display: box;
                display: -webkit-flex;
                display: -moz-flex;
                display: -ms-flexbox;
                display: flex;
                -webkit-flex-flow: row nowrap;
                -moz-flex-flow: row nowrap;
                flex-flow: row nowrap;
            }

            .Table-row:nth-of-type(even) 
            {
                background-color: #ffffff;
            }

            .Table-row:nth-of-type(odd) 
            {
                background-color: #f2f2f2;
            }
        }


        .Table-row-item 
        {
            display: -webkit-box;
            display: -moz-box;
            display: box;
            display: -webkit-flex;
            display: -moz-flex;
            display: -ms-flexbox;
            display: flex;
            -webkit-flex-flow: row nowrap;
            -moz-flex-flow: row nowrap;
            flex-flow: row nowrap;
            -webkit-flex-grow: 1;
            -moz-flex-grow: 1;
            flex-grow: 1;
            -ms-flex-positive: 1;
            -webkit-flex-basis: 0;
            -moz-flex-basis: 0;
            flex-basis: 0;
            -ms-flex-preferred-size: 0;
            word-wrap: break-word;
            overflow-wrap: break-word;
            word-break: break-all;
            padding: 0.5em;
            word-break: break-word;
        }

        .Table-row-item:before 
        {
            content: attr(data-header);
            width: 30%;
            font-weight: 700;
        }


        @media (min-width: 500px) 
        {

            .Table-row-item 
            {
            border: 1px solid #ffffff;
            padding: 0.5em;
            }

            .Table-row-item:before 
            {
            content: none;
            }

        }

    </style>
</head>
<body>
    {@HTML}
</body>
</html>
";

            System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>> data =
                generateTableData()
            ;

            string table = responsiveTable("foo", data);
            html = html.Replace("{@HTML}", table);


            context.Response.ContentType = "text/html";
            context.Response.Write(html);
        } // End Sub ProcessRequest 


        // Generate a random word using a Markov chain.
        // The transition matrix was calculated with Oscar Wilde's The Picture of Dorian Gray.
        // Markov chains produce quite easy to type words.
        public static string generateCaptchaTextMarkov(int length)
        {
            var transitionMatrix = new double[]{
0.0001, 0.0218, 0.0528, 0.1184, 0.1189, 0.1277, 0.1450, 0.1458, 0.1914, 0.1915, 0.2028, 0.2792, 0.3131, 0.5293, 0.5304, 0.5448, 0.5448, 0.6397, 0.7581, 0.9047, 0.9185, 0.9502, 0.9600, 0.9601, 0.9982, 1.0000, 
0.0893, 0.0950, 0.0950, 0.0950, 0.4471, 0.4471, 0.4471, 0.4471, 0.4784, 0.4821, 0.4821, 0.6075, 0.6078, 0.6078, 0.7300, 0.7300, 0.7300, 0.7979, 0.8220, 0.8296, 0.9342, 0.9348, 0.9351, 0.9351, 1.0000, 1.0000, 
0.1313, 0.1317, 0.1433, 0.1433, 0.3264, 0.3264, 0.3264, 0.4887, 0.5454, 0.5454, 0.5946, 0.6255, 0.6255, 0.6255, 0.8022, 0.8022, 0.8035, 0.8720, 0.8753, 0.9545, 0.9928, 0.9928, 0.9928, 0.9928, 1.0000, 1.0000, 
0.0542, 0.0587, 0.0590, 0.0840, 0.3725, 0.3837, 0.3879, 0.3887, 0.5203, 0.5208, 0.5211, 0.5390, 0.5435, 0.5550, 0.8183, 0.8191, 0.8191, 0.8759, 0.9376, 0.9400, 0.9629, 0.9648, 0.9664, 0.9664, 1.0000, 1.0000, 
0.0860, 0.0877, 0.1111, 0.2533, 0.3017, 0.3125, 0.3183, 0.3211, 0.3350, 0.3355, 0.3378, 0.4042, 0.4381, 0.5655, 0.5727, 0.5842, 0.5852, 0.7817, 0.8718, 0.9191, 0.9201, 0.9530, 0.9652, 0.9792, 0.9998, 1.0000, 
0.1033, 0.1037, 0.1050, 0.1057, 0.2916, 0.3321, 0.3324, 0.3324, 0.4337, 0.4337, 0.4337, 0.4912, 0.4912, 0.4912, 0.7237, 0.7274, 0.7274, 0.8545, 0.8569, 0.9150, 0.9986, 0.9986, 0.9990, 0.9990, 1.0000, 1.0000, 
0.1014, 0.1017, 0.1024, 0.1028, 0.2725, 0.2729, 0.2855, 0.4981, 0.5770, 0.5770, 0.5770, 0.6184, 0.6191, 0.6384, 0.7783, 0.7797, 0.7797, 0.9249, 0.9663, 0.9688, 0.9923, 0.9923, 0.9937, 0.9937, 1.0000, 1.0000, 
0.2577, 0.2579, 0.2580, 0.2581, 0.6967, 0.6970, 0.6970, 0.6970, 0.8648, 0.8648, 0.8650, 0.8661, 0.8667, 0.8670, 0.9397, 0.9397, 0.9397, 0.9509, 0.9533, 0.9855, 0.9926, 0.9926, 0.9929, 0.9929, 1.0000, 1.0000, 
0.0324, 0.0478, 0.0870, 0.1267, 0.1585, 0.1908, 0.2182, 0.2183, 0.2193, 0.2193, 0.2309, 0.2859, 0.3426, 0.6110, 0.6501, 0.6579, 0.6583, 0.6923, 0.8211, 0.9764, 0.9781, 0.9948, 0.9949, 0.9965, 0.9965, 1.0000, 
0.1276, 0.1276, 0.1276, 0.1276, 0.4286, 0.4286, 0.4286, 0.4286, 0.4337, 0.4337, 0.4337, 0.4337, 0.4337, 0.4337, 0.6684, 0.6684, 0.6684, 0.6684, 0.6684, 0.6684, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 
0.0033, 0.0059, 0.0100, 0.0109, 0.5401, 0.5443, 0.5477, 0.5485, 0.7149, 0.7149, 0.7149, 0.7316, 0.7333, 0.9247, 0.9264, 0.9273, 0.9273, 0.9289, 0.9791, 0.9816, 0.9824, 0.9824, 0.9833, 0.9833, 1.0000, 1.0000, 
0.0850, 0.0865, 0.0874, 0.1753, 0.3439, 0.3725, 0.3744, 0.3746, 0.5083, 0.5083, 0.5192, 0.6784, 0.6840, 0.6848, 0.8088, 0.8128, 0.8128, 0.8147, 0.8326, 0.8511, 0.8743, 0.8817, 0.9054, 0.9054, 1.0000, 1.0000, 
0.1562, 0.1760, 0.1774, 0.1776, 0.5513, 0.5517, 0.5517, 0.5520, 0.6352, 0.6352, 0.6352, 0.6369, 0.6486, 0.6499, 0.7717, 0.8230, 0.8230, 0.8337, 0.8697, 0.8703, 0.9376, 0.9376, 0.9378, 0.9378, 1.0000, 1.0000, 
0.0255, 0.0265, 0.0682, 0.2986, 0.4139, 0.4204, 0.6002, 0.6009, 0.6351, 0.6360, 0.6507, 0.6672, 0.6679, 0.6786, 0.7718, 0.7723, 0.7732, 0.7873, 0.8364, 0.9715, 0.9753, 0.9797, 0.9803, 0.9804, 0.9997, 1.0000, 
0.0050, 0.0089, 0.0183, 0.0379, 0.0410, 0.1451, 0.1494, 0.1514, 0.1654, 0.1656, 0.1866, 0.2171, 0.2821, 0.4272, 0.4761, 0.4926, 0.4927, 0.6434, 0.6722, 0.7195, 0.9126, 0.9332, 0.9913, 0.9925, 0.9999, 1.0000, 
0.1596, 0.1688, 0.1688, 0.1688, 0.3799, 0.3799, 0.3799, 0.4011, 0.4827, 0.4827, 0.4833, 0.6081, 0.6087, 0.6090, 0.7353, 0.7953, 0.7953, 0.8804, 0.9181, 0.9584, 0.9952, 0.9952, 0.9952, 0.9952, 1.0000, 1.0000, 
0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 
0.0902, 0.0938, 0.1003, 0.1555, 0.4505, 0.4606, 0.4705, 0.4740, 0.5928, 0.5928, 0.6018, 0.6201, 0.6402, 0.6605, 0.7619, 0.7666, 0.7671, 0.8125, 0.8645, 0.9029, 0.9226, 0.9298, 0.9319, 0.9319, 0.9996, 1.0000, 
0.0584, 0.0598, 0.0903, 0.0912, 0.2850, 0.2870, 0.2883, 0.3902, 0.5057, 0.5058, 0.5165, 0.5271, 0.5400, 0.5447, 0.6525, 0.6762, 0.6792, 0.6792, 0.7512, 0.9370, 0.9843, 0.9851, 0.9953, 0.9953, 0.9999, 1.0000, 
0.0416, 0.0419, 0.0466, 0.0467, 0.1673, 0.1696, 0.1697, 0.6314, 0.7003, 0.7003, 0.7003, 0.7142, 0.7150, 0.7160, 0.8626, 0.8626, 0.8627, 0.9023, 0.9255, 0.9498, 0.9746, 0.9746, 0.9812, 0.9812, 0.9998, 1.0000, 
0.0141, 0.0308, 0.0668, 0.0877, 0.1241, 0.1282, 0.1874, 0.1874, 0.2191, 0.2192, 0.2210, 0.3626, 0.3794, 0.4618, 0.4632, 0.5097, 0.5097, 0.6957, 0.8373, 0.9949, 0.9949, 0.9961, 0.9963, 0.9982, 0.9984, 1.0000, 
0.0740, 0.0740, 0.0740, 0.0740, 0.8423, 0.8423, 0.8423, 0.8423, 0.9486, 0.9486, 0.9486, 0.9486, 0.9486, 0.9491, 0.9836, 0.9836, 0.9836, 0.9849, 0.9849, 0.9849, 0.9907, 0.9907, 0.9907, 0.9907, 1.0000, 1.0000, 
0.2785, 0.2789, 0.2795, 0.2823, 0.4088, 0.4118, 0.4118, 0.6070, 0.7774, 0.7774, 0.7782, 0.7840, 0.7840, 0.8334, 0.9704, 0.9704, 0.9704, 0.9861, 0.9996, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 
0.0741, 0.0741, 0.1963, 0.1963, 0.2519, 0.2741, 0.2741, 0.3333, 0.4000, 0.4000, 0.4000, 0.4000, 0.4000, 0.4000, 0.4037, 0.6741, 0.7667, 0.7667, 0.7667, 0.9667, 0.9963, 0.9963, 0.9963, 0.9963, 1.0000, 1.0000, 
0.0082, 0.0130, 0.0208, 0.0225, 0.1587, 0.1608, 0.1613, 0.1686, 0.2028, 0.2028, 0.2032, 0.2322, 0.2391, 0.2417, 0.8232, 0.8314, 0.8314, 0.8409, 0.9529, 0.9965, 0.9965, 0.9965, 0.9991, 0.9996, 1.0000, 1.0000, 
0.0678, 0.0678, 0.0763, 0.0763, 0.7373, 0.7373, 0.7373, 0.7458, 0.8729, 0.8729, 0.8729, 0.8814, 0.8814, 0.8814, 0.9237, 0.9237, 0.9237, 0.9237, 0.9237, 0.9407, 0.9492, 0.9492, 0.9492, 0.9492, 0.9492, 1.0000
	        };

            string chars = "abcdefghijklmnopqrstuvwxyz";
            // // string chars = "абцдефгхийклмнопюрстувжызчшщъыьэяжё";
            // string chars = "абцдефгхийклмножюрстувжязчшщъыьэяёып";

            string captchaText = "";
            int randomValue = rand(0, 25);

            for (int i = 0; i < length; i++)
            {
                // captchaText += (char)(@char + 65 + 32);
                // captchaText += (char)(@char + 'A' + 32);
                captchaText += chars[randomValue];

                // Look up next char in transition matrix
                double next = rand(0, 10000) / 10000.0;
                for (int j = 0; j < 26; j++)
                {

                    if (next < transitionMatrix[randomValue * 26 + j])
                    {
                        randomValue = j;
                        break;
                    } // End if (next < transitionMatrix[randomValue * 26 + j]) 

                } // Next j 

            } // Next i 

            return captchaText;
        } // End Function generateCaptchaTextMarkov 


        public static string generateRandomWords()
        {
            var count = rand(1, 5);
            string[] text = new string[count];


            for (var i = 0; i < count; ++i)
            {
                text[i] = generateCaptchaTextMarkov(rand(10, 50));
            } // Next i 

            return string.Join(" ", text);
        } // End Function generateRandomWords 


        public static System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>> generateTableData()
        {
            System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>> table = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>();
            System.Collections.Generic.Dictionary<int, string> columnNames = new System.Collections.Generic.Dictionary<int, string>();

            System.Collections.Generic.Dictionary<string, string> columns = null;

            for (int j = 0; j < 9; ++j)
            {
                // columnNames[j] = "Column " + j.ToString();
                columnNames[j] = generateCaptchaTextMarkov(rand(3, 12));
            } // Next j 


            for (int i = 0; i < 11; ++i)
            {
                columns = new System.Collections.Generic.Dictionary<string, string>();

                for (int j = 0; j < columnNames.Count; ++j)
                {
                    string key = columnNames[j];
                    // string value = "row " + i + " col " + j;
                    string value = generateRandomWords();
                    columns[key] = value;
                } // Next j 

                table.Add(columns);
            } // Next i 

            return table;
        } // End Function generateTableData 


        public static string responsiveTable(string id, System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>> table)
        {
            string retVal = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            responsiveTable(id, table, sb);
            retVal = sb.ToString();
            sb.Length = 0;
            sb = null;
            return retVal;
        } // End Function responsiveTable 


        public static void responsiveTable(string id, System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>> table, System.Text.StringBuilder sb)
        {
            string[] columns = new string[table[0].Count];

            int k = 0;
            foreach (System.Collections.Generic.KeyValuePair<string, string> x in table[0])
            {
                columns[k] = x.Key;
                ++k;
            } // Next k 


            sb.AppendLine(@"<div id=""" + System.Web.HttpUtility.HtmlAttributeEncode(id) + @""" class=""table"">");
            sb.AppendLine(@"<div class=""Table-row Table-header"">");
            for (var i = 0; i < columns.Length; ++i)
            {
                sb.Append(@"<div class=""Table-row-item"">");
                sb.Append(System.Web.HttpUtility.HtmlEncode(columns[i]));
                sb.AppendLine("</div>"); // End Header-Cell
            } // Next i 
            sb.AppendLine("</div>"); // End Header-Row


            for (int i = 0; i < table.Count; ++i)
            {
                sb.AppendLine(@"<div class=""Table-row"">");

                for (int j = 0; j < columns.Length; ++j)
                {
                    sb.Append(@"<div class=""Table-row-item"" data-header=""" + System.Web.HttpUtility.HtmlAttributeEncode(columns[j]) + @""">");
                    sb.Append(System.Web.HttpUtility.HtmlEncode(table[i][columns[j]]));
                    sb.AppendLine("</div>"); // End Cell
                } // Next j 
                sb.AppendLine("</div>"); // End Row
            } // Next i 
            sb.AppendLine("</div>"); // End Table
        } // End Sub responsiveTable 


        public bool IsReusable
        {
            get
            {
                return false;
            }
        } // End Property IsReusable 


    } // End Class table : System.Web.IHttpHandler 


} // End Namespace Loggy 
