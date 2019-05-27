# ConfiMicToggler

The ConfiMicToggler aka the Conference Microfon Toggler, is a tool for lazy people which uses Skype for Business or Microsoft Teams for video conferencing in a distributed team.
It works like a remote control for the microfon of SfB (Skype for Business) or Teams (Microsoft Teams).
It is a website which toggles the microfon of the conference tool when the index page of the website gets called.
For this to work, the ConfiMicToggler has to be installed, on the system where the SkypeForBusiness or Micorosoft Teams conference is hosted.

Installation:
The output of the builded project has to be put to the system where the SkypeForBusiness or Micorosoft Teams conference is hosted.
Then the appsettings has to be modified.
Anschließend muss man in dem entpackten Ordner die Datei appsettings.json öffnen. Diese sieht wie folgt aus:


{
                "ConfiMicToggler": {
                               "Host": "#HOST#",
                               "Port": "#PORT#",
                               "TargetConferenceTool": "Teams"
                },
                "Logging": {
                               "IncludeScopes": false,
                               "LogLevel": {
                                               "Default": "Warning"
                               }
                }
}

Den Wert #HOST# muss man ersetzen mit dem jeweiligen Rechnernamen auf welchem der ConfiMicToggler laufen soll, zum Beispiel touch041.
Den Wert bei  #PORT# kann man frei wählen, zum Beispiel 1111.
Bei TargetConferenceTool gibt es nur die Werte „Teams“ oder „Skype“. Hier muss eingetragen werden, welche der beiden Tools verwendet wird.
Der Block Logging kann ignoriert werden.

Abschliessend muss man einmalig eine CMD als Administrator auf dem Rechner öffnen, auf welchem der ConfiMicToggler laufen soll und folgenden Befehl ausführen:
netsh http add urlacl url=http://##HOST##:##PORT##/ user=##DOMAIN##\##USER##

Dadurch wird die angegebene URL für einen Nutzer reserviert, welcher kein Administrator ist. Da die touchXX User dies meist nicht sind, ist es nötig diesen Befehl einmalig auszuführen.
Die Werte #HOST# und #PORT# müssen dieselben sein, wie die, welche in der appsettings.json verwendet wurden.
#DOMAIN# und #USER# müssen den User repräsentieren, unter welchem der SkypeMicToggler gestartet wird, also der eingeloggte Nutzer.
Beispielsweise könnte der Befehl wie folgt aussehen:
netsh http add urlacl url=http://touch041:1111/ user=SAXSYS\touch041

Damit der SkypeMicToggler nicht jedesmal manuell mitgestartet werden muss, bietet es sich an, eine Verknüpfung in den Autostart zu legen.

Applikationsstart:

Nun kann der SkypeMicToggler über die SkypeMicToggler.exe gestartet werden. Es sollte nun wie folgt aussehen, wobei sich die Angaben im Konsolenfenster auf die Settings beziehen, welche in der appsettings.json vorgenommen wurden:
