<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Loggy.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    https://stackoverflow.com/questions/2161413/implementing-a-custom-error-page-on-an-asp-net-website
        https://msdn.microsoft.com/en-us/library/h0hfz6fc(v=vs.71).aspx

        https://stackoverflow.com/questions/18671975/how-to-redirect-all-httperrors-to-custom-url

        https://www.iis.net/configreference/system.webserver/httperrors
        https://msdn.microsoft.com/en-us/library/h0hfz6fc(v=vs.71).aspx


        Beaware that <httpErros> configures IIS, 
        while <customErrors> configures ASP.NET and some older versions of IIS (<=6?).
             
      <httpErrors errorMode="DetailedLocalOnly" existingResponse="Replace" defaultResponseMode="File">
        <clear />
        <error statusCode="400" prefixLanguageFilePath="" path="/WAS/BlackErrorTemplate_DE.htm?status=400" responseMode="Redirect" />
        <error statusCode="401" prefixLanguageFilePath="" path="/WAS/BlackErrorTemplate_DE.htm?status=401" responseMode="Redirect" />
        <error statusCode="402" prefixLanguageFilePath="" path="/WAS/BlackErrorTemplate_DE.htm?status=402" responseMode="Redirect" />
        <error statusCode="403" prefixLanguageFilePath="" path="/WAS/BlackErrorTemplate_DE.htm?status=403" responseMode="Redirect" />
        <error statusCode="404" prefixLanguageFilePath="" path="/WAS/BlackErrorTemplate_DE.htm?status=404" responseMode="Redirect" />
        <error statusCode="500" prefixLanguageFilePath="" path="/WAS/BlackErrorTemplate_DE.htm?status=500" responseMode="Redirect" />
      </httpErrors>
      


    </div>
    </form>
</body>
</html>
