int Solution() => TruncatablePrimes().Take(11).Sum();

IEnumerable<int> TruncatablePrimes()
{
    var primes = new HashSet<int>();
    var truncatable = new HashSet<int>();
    foreach (var prime in Sieve(1000000))
    {
        primes.Add(prime);
        if (prime > 10)
        {
            if (TruncateLeft(prime) && TruncateRight(prime))
            {
                truncatable.Add(prime);
                yield return prime;
            }
        }
    }

    bool TruncateLeft(int n)
    {
        do
        {
            var exponent = (int)Math.Log10(n);
            n %= (int)Math.Pow(10, exponent);
            if (!primes.Contains(n))
            {
                return false;
            }
            if (truncatable.Contains(n))
            {
                return true;
            }
        }
        while (n / 10 > 0);

        return true;
    }

    bool TruncateRight(int n)
    {
        while (n > 0)
        {
            if (!primes.Contains(n))
            {
                return false;
            }
            if (truncatable.Contains(n))
            {
                return true;
            }
            n /= 10;
        }

        return true;
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
