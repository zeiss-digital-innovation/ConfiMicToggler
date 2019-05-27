# ConfiMicToggler

The ConfiMicToggler aka the Conference Microfon Toggler, is a tool for lazy people which uses Skype for Business or Microsoft Teams for video conferencing in a distributed team.
It works like a remote control for the microfon of SfB (Skype for Business) or Teams (Microsoft Teams).
It is a website which toggles the microfon of the conference tool when the index page of the website gets called.
For this to work, the ConfiMicToggler has to be installed, on the system where the SkypeForBusiness or Micorosoft Teams conference is hosted.

Installation:
The output of the builded project has to be put to the system where the SkypeForBusiness or Micorosoft Teams conference is hosted.
Then the appsettings.json has to be modified:

```xml
{
    "ConfiMicToggler": {
        "Host": "#HOST#",
        "Port": "#PORT#",
        "TargetConferenceTool": #CONFERENCETOOL#
    },
    "Logging": {
        "IncludeScopes": false,
        "LogLevel": {
                        "Default": "Warning"
         }
    }
}
```
Value #HOST# has to be the DNS name of the system where the ConfiMicToggler is installed.
The value #PORT# is a port number under which ConfiMicToggler gets started.
The value #CONFERENCETOOL# can be "Teams" or "Skype" depending on which conference tool is used.

To reserve the URI you have to register it once on the system. For that open a command prompt as administrator and execute the following command:
netsh http add urlacl url=http://##HOST##:##PORT##/ user=##DOMAIN##\##USER##

The value #DOMAIN# and #USER# must be the user, under whom the ConfiMicToggler gets started.
