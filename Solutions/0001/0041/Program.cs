int Solution()
{
    var primes = new HashSet<int>(Sieve((int)Math.Sqrt(999999999)));
    return Enumerable.Range(0, 7)
        .SelectMany(x => Pandigital(7 - x))
        .First(x => primes.All(p => x % p != 0));
}

IEnumerable<int> Pandigital(int n)
{
    var digits = Enumerable.Range(0, 10).ToArray();

    return Dfs(0, new HashSet<int>());

    IEnumerable<int> Dfs(int x, HashSet<int> seen)
    {
        if (seen.Count == n)
        {
            yield return x;
            yield break;
        }

        for (var i = n; i >= 1; i--)
        {
            if (seen.Contains(i))
            {
                continue;
            }

            var y = x * 10 + i;

            var newSeen = new HashSet<int>(seen);
            newSeen.Add(i);
            foreach (var next in Dfs(y, newSeen))
            {
                yield return next;
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

Console.WriteLine(Solution());