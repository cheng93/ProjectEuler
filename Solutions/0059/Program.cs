using System.Reflection;

int Solution() => Decrypt().Select(x => (int)x).Sum();

string Decrypt()
{
    var path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, "p059_cipher.txt");
    var str = File.ReadAllText(path);
    var values = str.Split(",")
        .Select(int.Parse)
        .ToArray();

    foreach (var key in GenerateKey())
    {
        var decryptedValues = Enumerable.Range(0, values.Length)
            .Select(x => values[x] ^ key[x % 3])
            .Select(x => (char)x)
            .ToArray();

        var decrypted = string.Join(string.Empty, decryptedValues);
        // var words = decrypted.Split(" ");
        // var commonWords = new[] { "the", "be", "and", "of", "a", "in", "to", "have", "too", "i" };
        // if (words.Any(x => commonWords.Any(c => string.Equals(x, c, StringComparison.OrdinalIgnoreCase))))
        // {
        //     Console.WriteLine((key, decrypted));
        //     Console.WriteLine();
        // }

        if (key == "exp")
        {
            return decrypted;
        }
    }

    throw new Exception();
}

IEnumerable<string> GenerateKey(string key = "")
{
    if (key.Length == 3)
    {
        yield return key;
        yield break;
    }

    for (var i = (int)'a'; i <= (int)'z'; i++)
    {
        foreach (var k in GenerateKey(key + ((char)i).ToString()))
        {
            yield return k;
        }
    }
}

// See https://aka.ms/new-console-template for more information
Console.WriteLine(Solution());
