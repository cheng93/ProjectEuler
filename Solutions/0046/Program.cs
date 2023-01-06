int Solution()
{
    var primes = Sieve(1000000).ToList();
    var set = new HashSet<int>(primes);
    var squares = new HashSet<int>();
    var highest = 0;

    var i = 9;
    while (true)
    {
        if (!set.Contains(i))
        {
            var found = false;
            foreach (var prime in primes)
            {
                if (prime > i)
                {
                    break;
                }

                var n = (i - prime) / 2;
                if (IsSquare(n))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                return i;
            }
        }

        i += 2;
    }

    throw new Exception();

    bool IsSquare(int n)
    {
        while (n > highest * highest)
        {
            highest++;
            squares.Add(highest * highest);
        }

        return squares.Contains(n);
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
