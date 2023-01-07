int Solution(int limit)
{
    var primes = new HashSet<int>(Sieve(10000));
    var maxN = 0;
    var product = 0;

    for (var a = -limit + 1; a < limit; a++)
    {
        for (var b = -limit; b <= limit; b++)
        {
            var f = (int x) => x * x + a * x + b;
            var n = 0;
            while (true)
            {
                var y = f(n);
                if (!primes.Contains(y))
                {
                    if (n > maxN)
                    {
                        maxN = n;
                        product = a * b;
                    }
                    break;
                }
                n++;
            }
        }
    }

    return product;
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

Console.WriteLine(Solution(41)); // 41
Console.WriteLine(Solution(1000));