int Solution(int limit)
    => Primes().Take(limit).Last();

IEnumerable<int> Primes()
{
    yield return 2;
    var i = 3;
    while (true)
    {
        if (!Factor(i).Any())
        {
            yield return i;
        }
        i++;
    }
}

IEnumerable<int> Factor(long n)
{
    var quotient = n;
    var sqrt = (int)Math.Ceiling(Math.Sqrt(n));
    foreach (var prime in Sieve(sqrt))
    {
        if (quotient % prime == 0)
        {
            yield return prime;
            while (quotient % prime == 0)
            {
                quotient /= prime;
            }
        }
        if (quotient == 1)
        {
            yield break;
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
        for (var j = i * i; j <= n; j += i)
        {
            nonPrimes.Add(j);
        }
    }
}


Console.WriteLine(Solution(6)); // 13
Console.WriteLine(Solution(10001));