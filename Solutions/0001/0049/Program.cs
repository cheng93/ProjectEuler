long Solution()
{
    var primes = Sieve(10000)
        .Where(x => ((int)Math.Log10(x)) == 3)
        .ToList();
    var set = new HashSet<int>(primes);

    foreach (var prime in primes)
    {
        if (prime == 1487)
        {
            continue;
        }
        var permutations = Permutations(prime)
            .Where(x => x > prime)
            .Where(set.Contains)
            .ToList();
        var pSet = new HashSet<int>(permutations);
        foreach (var permutation in permutations)
        {
            var diff = permutation - prime;
            var add = permutation + diff;
            if (pSet.Contains(add))
            {
                var sum = (long)prime;
                sum *= 10000;
                sum += permutation;
                sum *= 10000;
                sum += add;
                return sum;
            }
        }
    }

    throw new Exception();
}

IEnumerable<int> Permutations(int n)
{
    var digits = Enumerable.Range(1, 4)
        .Select(x => (n / (int)Math.Pow(10, x - 1)) % 10)
        .ToArray();

    return Dfs(0, new HashSet<int>());

    IEnumerable<int> Dfs(int current, HashSet<int> seen)
    {
        if (seen.Count == 4)
        {
            yield return current;
            yield break;
        }

        for (var i = 0; i < 4; i++)
        {
            if (seen.Contains(i))
            {
                continue;
            }

            var newSeen = new HashSet<int>(seen);
            newSeen.Add(i);
            foreach (var next in Dfs(current * 10 + digits[i], newSeen))
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
