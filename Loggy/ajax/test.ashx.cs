using System;
using System.Collections.Generic;
using System.Web;

namespace Loggy.ajax
{
    /// <summary>
    /// Summary description for test
    /// </summary>
    public class test : IHttpHandler
    {

        static string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            foreach (char c in normalizedString)
            {
                System.Globalization.UnicodeCategory unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }


        public class MyCulture
        {

            public string TwoLetterISOLanguageName;
            public string ThreeLetterISOLanguageName;
            public string IetfLanguageTag;
            public string DisplayName;
            public string EnglishName;
            public string NativeName;

            public MyCulture(System.Globalization.CultureInfo cu)
            {
                this.TwoLetterISOLanguageName = cu.TwoLetterISOLanguageName;
                this.ThreeLetterISOLanguageName = cu.ThreeLetterISOLanguageName;
                this.IetfLanguageTag = cu.IetfLanguageTag;
                this.DisplayName = cu.DisplayName;
                this.EnglishName = cu.EnglishName;
                this.NativeName = cu.NativeName;
            }

        }



        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            // İstanbul / i - Recep İvedik
            bool ord = System.StringComparer.OrdinalIgnoreCase.Equals("İstanbul", "Istanbul");
            bool invariant = System.StringComparer.InvariantCultureIgnoreCase.Equals("İstanbul", "Istanbul");
            bool cur = System.StringComparer.CurrentCultureIgnoreCase.Equals("İstanbul", "istanbul");


            string foo = RemoveDiacritics("İstanbul");
            // foo = RemoveDiacritics("Les Mise\u0301rables");


            var cults = System.Globalization.CultureInfo
                .GetCultures(System.Globalization.CultureTypes.SpecificCultures);
            System.Collections.Generic.List<MyCulture> ls = new List<MyCulture>();


            foreach (var cu in cults)
            {
                ls.Add(new MyCulture(cu));
            }


            string json = Newtonsoft.Json.JsonConvert.SerializeObject(ls, Newtonsoft.Json.Formatting.Indented);
            System.Console.WriteLine(json);



            ord = System.StringComparer.OrdinalIgnoreCase.Equals(foo, "istanbul");
            System.Console.WriteLine(ord);


            // bool ord = System.StringComparer.OrdinalIgnoreCase.Equals("istanbul", "istanbul");
            // bool invariant = System.StringComparer.InvariantCultureIgnoreCase.Equals("istanbul", "istanbul");
            // bool cur =System.StringComparer.CurrentCultureIgnoreCase.Equals("istanbul", "istanbul");


            System.Console.WriteLine(ord);
            System.Console.WriteLine(invariant);
            System.Console.WriteLine(cur);


            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}