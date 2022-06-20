// See https://aka.ms/new-console-template for more information
using System.Text;

string currentPath = AppContext.BaseDirectory;
string outputPath = Directory.CreateDirectory("output").FullName;
foreach (var file in GetAllFiles(currentPath))
{
    Console.WriteLine($"Parsing: {Path.GetFileName(file)}");
    string fileName = Path.GetFileNameWithoutExtension(file);
    string[] lines = File.ReadAllLines(file).ToArray();
    StringBuilder sb = new();
    sb.AppendLine(@"""music""");
    sb.AppendLine("{");
    foreach (string line in lines)
    {
        if (string.IsNullOrWhiteSpace(line))
            continue;

        string[] split = line.Split("||");
        if (split.Length > 1)
        {
            string musicname = Path.GetFileName(split[0]);
            sb.AppendLine($"\t\"{musicname}\"\t\t\t\"{split[1]}\"");
        }
    }

    sb.AppendLine("}");

    File.WriteAllText(Path.Combine(outputPath, $"{fileName}.cfg"), sb.ToString());
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("The end. Press a key to exit...");
Console.ReadKey();

static string[] GetAllFiles(string currentPath) => Directory.GetFiles($@"{currentPath}", "*txt").ToArray();
