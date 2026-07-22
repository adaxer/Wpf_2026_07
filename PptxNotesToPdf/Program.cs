using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

internal static class Program
{
    [STAThread]
    private static int Main(string[] args)
    {
        try
        {
            var options = Options.Parse(args.Count() > 0 ? args : new string[]{ "D:\\Training\\Wpf\\PPKURS-WPF" });
            return Run(options);
        }
        catch (ArgumentException ex)
        {
            Console.Error.WriteLine(ex.Message);
            PrintUsage();
            return 2;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
            return 1;
        }
    }

    private static int Run(Options options)
    {
        var root = Path.GetFullPath(options.RootDirectory);
        if (!Directory.Exists(root))
            throw new DirectoryNotFoundException($"Verzeichnis nicht gefunden: {root}");

        var files = Directory
            .EnumerateFiles(root, "*.pptx", SearchOption.AllDirectories)
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        Console.WriteLine($"{files.Length} Präsentation(en) gefunden.");

        if (options.ListOnly)
        {
            foreach (var file in files)
                Console.WriteLine(file);
            return 0;
        }

        PowerPoint.Application? app = null;
        var errors = 0;

        try
        {
            app = new PowerPoint.Application
            {
                Visible = Office.MsoTriState.msoTrue,
                WindowState = PowerPoint.PpWindowState.ppWindowMinimized
            };

            foreach (var pptxPath in files)
            {
                var pdfPath = Path.ChangeExtension(pptxPath, ".pdf");

                if (File.Exists(pdfPath) && !options.Overwrite)
                {
                    Console.WriteLine($"Übersprungen: {pdfPath}");
                    continue;
                }

                Console.WriteLine($"Konvertiere: {pptxPath}");

                PowerPoint.Presentation? presentation = null;
                try
                {
                    if (File.Exists(pdfPath))
                        File.Delete(pdfPath);

                    presentation = app.Presentations.Open(
                        FileName: pptxPath,
                        ReadOnly: Office.MsoTriState.msoTrue,
                        Untitled: Office.MsoTriState.msoFalse,
                        WithWindow: Office.MsoTriState.msoFalse);

                    presentation.ExportAsFixedFormat(
                        Path: pdfPath,
                        FixedFormatType: PowerPoint.PpFixedFormatType.ppFixedFormatTypePDF,
                        Intent: PowerPoint.PpFixedFormatIntent.ppFixedFormatIntentPrint,
                        FrameSlides: Office.MsoTriState.msoFalse,
                        HandoutOrder: PowerPoint.PpPrintHandoutOrder.ppPrintHandoutVerticalFirst,
                        OutputType: PowerPoint.PpPrintOutputType.ppPrintOutputNotesPages,
                        PrintHiddenSlides: Office.MsoTriState.msoTrue,
                        PrintRange: null,
                        RangeType: PowerPoint.PpPrintRangeType.ppPrintAll,
                        SlideShowName: string.Empty,
                        IncludeDocProperties: true,
                        KeepIRMSettings: true,
                        DocStructureTags: true,
                        BitmapMissingFonts: true,
                        UseISO19005_1: false);

                    if (!File.Exists(pdfPath))
                        throw new IOException("PowerPoint meldete keinen Fehler, aber die PDF wurde nicht erzeugt.");

                    Console.WriteLine($"Erstellt: {pdfPath}");
                }
                catch (Exception ex)
                {
                    errors++;
                    Console.Error.WriteLine($"FEHLER bei '{pptxPath}':\n{ex}");
                }
                finally
                {
                    if (presentation is not null)
                    {
                        try { presentation.Close(); } catch { }
                        FinalRelease(presentation);
                    }
                }
            }
        }
        finally
        {
            if (app is not null)
            {
                try { app.Quit(); } catch { }
                FinalRelease(app);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        Console.WriteLine(errors == 0 ? "Fertig." : $"Fertig mit {errors} Fehler(n).");
        return errors == 0 ? 0 : 1;
    }

    private static void FinalRelease(object comObject)
    {
        if (Marshal.IsComObject(comObject))
            Marshal.FinalReleaseComObject(comObject);
    }

    private static void PrintUsage()
    {
        Console.WriteLine("""
Verwendung:
  PptxNotesToPdf.exe <Verzeichnis> [--overwrite]
  PptxNotesToPdf.exe <Verzeichnis> --list-only

Beispiele:
  PptxNotesToPdf.exe "D:\Training\Wpf"
  PptxNotesToPdf.exe "D:\Training\Wpf" --overwrite
""");
    }

    private sealed record Options(string RootDirectory, bool Overwrite, bool ListOnly)
    {
        public static Options Parse(string[] args)
        {
            if (args.Length == 0 || args.Any(a => a is "-h" or "--help" or "/?"))
                throw new ArgumentException("Bitte ein Stammverzeichnis angeben.");

            var root = args.FirstOrDefault(a => !a.StartsWith('-'))
                       ?? throw new ArgumentException("Bitte ein Stammverzeichnis angeben.");

            var unknown = args
                .Where(a => a.StartsWith('-'))
                .Where(a => a is not "--overwrite" and not "--list-only")
                .ToArray();

            if (unknown.Length > 0)
                throw new ArgumentException($"Unbekannte Option: {string.Join(", ", unknown)}");

            return new Options(
                RootDirectory: root,
                Overwrite: args.Contains("--overwrite"),
                ListOnly: args.Contains("--list-only"));
        }
    }
}
