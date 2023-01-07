int Solution(int limit) => CircularPrimes(limit).Count();

IEnumerable<int> CircularPrimes(int n)
{
    var primes = new HashSet<int>(Sieve(n));
    var circular = new HashSet<int>();

    foreach (var prime in primes)
    {
        if (circular.Contains(prime))
        {
            yield return prime;
            continue;
        }

        var rotations = Rotate(prime).ToArray();
        if (rotations.All(primes.Contains))
        {
            yield return prime;
            foreach (var rotation in rotations)
            {
                circular.Add(rotation);
            }
        }
    }
}

IEnumerable<int> Rotate(int n)
{
    var rotated = n;
    var exponent = (int)Math.Log10(n);
    do
    {
        yield return rotated;
        var remainder = rotated % 10;
        rotated /= 10;
        rotated += remainder * (int)Math.Pow(10, exponent);

    }
    while (rotated != n);
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

Console.WriteLine(Solution(100)); // 13
Console.WriteLine(Solution(1000000));