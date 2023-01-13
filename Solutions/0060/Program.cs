int Solution(int n) => PrimePairSets(n).First().Sum();

IEnumerable<IEnumerable<int>> PrimePairSets(int n)
{
    var primes = Sieve(1000000).ToList();

    var primePairsDict = new Dictionary<int, int[]>();

    foreach (var prime in primes)
    {
        var primePairs = primePairsDict.Keys
            .Where(x => IsPrimePair(x, prime))
            .OrderByDescending(x => x)
            .ToArray();
        primePairsDict[prime] = primePairs;

        foreach (var k in Dfs(primePairs))
        {
            if (k.Length == n - 1)
            {
                yield return k.Concat(new[] { prime });
            }
        }

        IEnumerable<int[]> Dfs(IEnumerable<int> remaining)
        {
            foreach (var r in remaining)
            {
                var next = remaining.Intersect(primePairsDict[r]);
                foreach (var result in Dfs(next.OrderByDescending(x => x)))
                {
                    yield return result.Concat(new[] { r }).ToArray();
                }

                yield return new[] { r };
            }
        }
    }
}

bool IsPrimePair(int a, int b)
{
    var aExponent = (int)Math.Log10(a);
    var bExponent = (int)Math.Log10(b);

    var ab = a * (int)Math.Pow(10, bExponent + 1) + b;
    var ba = b * (int)Math.Pow(10, aExponent + 1) + a;

    return IsPrime(ab) && IsPrime(ba);
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

bool IsPrime(int n)
{
    if (n == 2 || n == 3)
    {
        return true;
    }

    if (n <= 1 || n % 2 == 0 || n % 3 == 0)
    {
        return false;
    }

    for (int i = 5; i * i <= n; i += 6)
    {
        if (n % i == 0 || n % (i + 2) == 0)
        {
            return false;
        }
    }

    return true;
}

// See https://aka.ms/new-console-template for more information
Console.WriteLine(Solution(4)); // 792
Console.WriteLine(Solution(5));
