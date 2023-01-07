using System.Reflection;

int Solution() => NameScores().Sum();

IEnumerable<int> NameScores()
{
    var path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, "names.txt");
    var str = File.ReadAllText(path);
    var names = str.Split(",")
        .Select(x => x[1..(^1)])
        .OrderBy(x => x)
        .ToArray();

    for (var i = 0; i < names.Length; i++)
    {
        yield return (i + 1) * AlphabeticalValue(names[i]);
    }
}

int AlphabeticalValue(string name)
{
    var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    return name.Select(x => alphabet.IndexOf(x) + 1).Sum();
}

Console.WriteLine(Solution());