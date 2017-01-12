/*
var msg = `<div id='Login'>
<span>Name:</span>
<input id='inName' name='inName' type='text' /><br />
<span>Passwort:</span><input id='inPassword' name='inPassword' type='password' /><br />
<br /><br /><br />
<image id="imgCaptcha" width="256px" height="144px" src="{@CaptchaLink}" alt="Captcha" /><br />
<span>Captcha:</span>
<input id='inCaptcha' name='inCaptcha' type='text' /><br />
</div>`;
*/

// var abc = `<div onclick="window.location.href = window.location.href;">Reload</div>`;



var msg = `<div id='Login'>
<span>Name:</span>
<input id='inName' name='inName' type='text' /><br />
<span>Passwort:</span><input id='inPassword' name='inPassword' type='password' />`;

if (true)
    msg = msg + `<br /><br /><br />
<div style="position: relative;">
<image id="imgCaptcha" style="padding-left: 100px" width="200px" height="112px" src="{@CaptchaLink}" alt="Captcha" /><br />
<img class="captchaRefresh" title="Reload / Refresh" style="border-radius: 16px; position: absolute; right: -16px; 
top: 50%; margin-top: -16px; width: 32px; height: 32px;" src="{@src}" onclick="document.getElementById('imgCaptcha').src = {@CaptchaLink};" />
</div>
<span>&nbsp;</span>

<br />
<span>Captcha:</span>
<input id='inCaptcha' placeholder="Bitte Captcha-Text hier eingeben" name='inCaptcha' style="box-shadow: inset 0 0 3px #777;margin-bottom: 5px;width: 200px;" type='text' />
`;

msg = msg + "</div>";
