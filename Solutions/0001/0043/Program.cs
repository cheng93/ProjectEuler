long Solution() => PandigitalSubStringDivisbility().Sum();

IEnumerable<long> PandigitalSubStringDivisbility()
{
    var digits = Enumerable.Range(0, 10).ToArray();
    var primes = new[] { 2, 3, 5, 7, 11, 13, 17 };

    return Dfs(0, new HashSet<long>());

    IEnumerable<long> Dfs(long x, HashSet<long> seen)
    {
        if (seen.Count >= 4)
        {
            var prime = primes[seen.Count - 4];
            var remainder = x % 1000;
            if (remainder % prime != 0)
            {
                yield break;
            }
        }
        if (seen.Count == 10)
        {
            yield return x;
            yield break;
        }

        for (var i = 9; i >= 0; i--)
        {
            if (seen.Contains(i))
            {
                continue;
            }

            if (i == 0 && x == 0)
            {
                continue;
            }

            var y = x * 10 + i;

            var newSeen = new HashSet<long>(seen);
            newSeen.Add(i);
            foreach (var next in Dfs(y, newSeen))
            {
                yield return next;
            }
        }
    }
}

Console.WriteLine(Solution());