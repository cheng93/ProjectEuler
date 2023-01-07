long Solution(int limit) => Sieve(limit).Sum();

IEnumerable<long> Sieve(int n)
{
    var nonPrimes = new HashSet<long>();
    for (var i = 2L; i <= n; i++)
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

Console.WriteLine(Solution(10)); // 17
Console.WriteLine(Solution(2000000));