using System.Reflection;

int Solution() => TriangleWords().Count();

IEnumerable<string> TriangleWords()
{
    var path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, "words.txt");
    var str = File.ReadAllText(path);
    var words = str.Split(",")
        .Select(x => x[1..(^1)])
        .ToArray();
    var max = words.Select(x => x.Length).Max();
    var triangles = Enumerable.Range(1, 100)
        .Select(x => Triangle(x))
        .TakeWhile(x => x <= max * 26);
    var set = new HashSet<int>(triangles);

    foreach (var word in words)
    {
        if (set.Contains(AlphabeticalValue(word)))
        {
            yield return word;
        }
    }
}

int Triangle(int n) => (n * (n + 1)) / 2;

int AlphabeticalValue(string word)
{
    var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    return word.Select(x => alphabet.IndexOf(x) + 1).Sum();
}

Console.WriteLine(Solution());