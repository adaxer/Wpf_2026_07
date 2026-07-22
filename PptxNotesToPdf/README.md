# PptxNotesToPdf

Durchsucht ein Verzeichnis rekursiv nach `*.pptx` und exportiert jede Präsentation als gleichnamige PDF mit **allen Notizenseiten**.

## Voraussetzungen

- Windows 11
- Microsoft PowerPoint Desktop installiert
- .NET 8 SDK zum Erstellen

## Erstellen

```powershell
dotnet restore
dotnet publish -c Release -r win-x64 --self-contained false
```

Die EXE liegt danach unter:

```text
bin\Release\net8.0-windows\win-x64\publish\PptxNotesToPdf.exe
```

## Verwendung

```powershell
PptxNotesToPdf.exe "D:\Training\Wpf"
```

Vorhandene PDFs überschreiben:

```powershell
PptxNotesToPdf.exe "D:\Training\Wpf" --overwrite
```

Nur gefundene PowerPoint-Dateien anzeigen:

```powershell
PptxNotesToPdf.exe "D:\Training\Wpf" --list-only
```

## Hinweis

Das Programm verwendet PowerPoints eingebauten PDF-Export mit `ppPrintOutputNotesPages`. Dadurch entstehen Notizenseiten-PDFs ohne die problematische Automatisierung des Speichern-Dialogs von „Microsoft Print to PDF“.
