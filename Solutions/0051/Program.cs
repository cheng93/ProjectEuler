int Solution(int n)
{
    var primes = Sieve(1000000).ToList();
    var replacements = new Dictionary<string, int>();
    var initial = new Dictionary<string, int>();

    foreach (var prime in primes)
    {
        foreach (var replacement in Replacements(prime))
        {
            if (!replacements.ContainsKey(replacement))
            {
                replacements.TryAdd(replacement, 0);
                initial[replacement] = prime;
            }
            replacements[replacement]++;
            if (replacements[replacement] >= n)
            {
                return initial[replacement];
            }
        }
    }

    throw new Exception();
}

IEnumerable<string> Replacements(int n)
{
    var exponent = (int)Math.Log10(n);
    var digits = Enumerable.Range(0, exponent + 1)
        .Reverse()
        .Select(x => (n % (int)Math.Pow(10, x + 1)) / (int)Math.Pow(10, x))
        .ToArray();
    var seen = new HashSet<int>();

    for (var i = 0; i < digits.Length - 1; i++)
    {
        var digit = digits[i];
        if (seen.Contains(digit))
        {
            continue;
        }
        seen.Add(digit);

        foreach (var replacement in Dfs(0, digit))
        {
            if (!int.TryParse(replacement, out _))
            {
                yield return replacement;
            }
        }
    }

    IEnumerable<string> Dfs(int i, int find)
    {
        var digit = digits[i];
        if (i == digits.Length - 1)
        {
            yield return digit.ToString();
            yield break;
        }

        foreach (var replacement in Dfs(i + 1, find))
        {
            yield return $"{digit}{replacement}";
            if (find == digit)
            {
                yield return $"*{replacement}";
            }
        }
    }
}

IEnumerable<int> Sieve(int n)
{
    var nonPrimes = new HashSet<int>();
    for (var i = 2; i <= n; i++)
    {
        if (nonPrimes.Contains(i))
        {
            continue;
        }
        yield return i;
        for (var j = 1L * i * i; j <= n; j += i)
        {
            nonPrimes.Add((int)j);
        }
    }
}

Console.WriteLine(Solution(6)); // 13
Console.WriteLine(Solution(7)); // 56003
Console.WriteLine(Solution(8));
