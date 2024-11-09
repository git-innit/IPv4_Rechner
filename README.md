# IPv4_Rechner (aka. ipcalc)

Berechnet verschiedene Werte aus einer IP-Adresse 

## Installation

Die aktuellste so wie ältere Versionen der Anwendung sind im [Releases](https://github.com/git-innit/IPv4_Rechner/releases) Tab zu finden.

IPv4_Rechner benötigt keine Installation, jedoch empfiehlt es sich, die Anwendung in einem Unterordner zu hinterlegen und diesen zum Pfad (PATH) hinzuzufügen.

**Windows:**

```
setx path "%path%;C:\dein\pfad\hier\"
```
**Hinweis Linux:**

Je nach Konfiguration muss der Anwendung gegebenenfalls die "execute" Berechtigung erteilt werden.
```
chmod +x /dein/pfad/hier/ipcalc
```


## Nutzung

IPv4_Rechner kann je nach Bedarf auf verschiedene Wege ausgeführt werden.

**1.** Die Anwendung kann wie eine herkömmliche Anwendung durch einen doppelklick ausgeführt werden.

**2.** Die Anwendung kann durch Aufrufen in der Eingabeaufforderung (CMD) ausgeführt werden.
<details>
<summary>Windows</summary>

**Wenn im Pfad hinterlegt:**
```
ipcalc
```
**Wenn nicht im Pfad hinterlegt:**
```
cd C:\dein\pfad\hier\
ipcalc.exe
```
</details>

<details>
<summary>Linux</summary>

```
cd /dein/pfad/hier/
./ipcalc
```
</details>

**3.** Die Anwendung kann durch Aufrufen im CMD und Mitgebens der Eingabe als Argument ausgeführt werden.
<details>
<summary>Windows</summary>

**Wenn im Pfad hinterlegt:**
```
ipcalc eingabe
```
**Wenn nicht im Pfad hinterlegt:**
```
cd C:\dein\pfad\hier\
ipcalc.exe eingabe
```
**Beispiel**
```
ipcalc 192.168.1.1/24
```
</details>

<details>
<summary>Linux</summary>

```
cd /dein/pfad/hier/
./ipcalc eingabe
```
**Beispiel**
```
./ipcalc 192.168.1.1/24
```
</details>


## Probleme

Bei Problemen mit der Anwendung kann gegebenenfalls ein [Issue](https://github.com/git-innit/IPv4_Rechner/issues/new) geöffnet werden.