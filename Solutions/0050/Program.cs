int Solution(int limit)
{
    var primes = Sieve(limit).ToList();
    var set = new HashSet<int>(primes);

    var highest = 1;
    var prime = primes.Last();

    Dfs(0).ToList();

    return prime;

    IEnumerable<(int Sum, int Terms)> Dfs(int i)
    {
        var current = primes[i];
        yield return (current, 1);
        if (i == primes.Count - 1)
        {
            yield break;
        }

        foreach (var (sum, terms) in Dfs(i + 1))
        {
            var newSum = current + sum;
            if (newSum > limit)
            {
                yield break;
            }

            var newTerms = terms + 1;
            if (set.Contains(newSum) && newTerms > highest)
            {
                highest = newTerms;
                prime = newSum;
            }

            yield return (newSum, newTerms);
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

Console.WriteLine(Solution(100)); // 41
Console.WriteLine(Solution(1000)); // 953
Console.WriteLine(Solution(1000000));
