int Solution(long n)
    => Factor(n).Max();

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

Console.WriteLine(Solution(13195)); // 29
Console.WriteLine(Solution(600851475143));