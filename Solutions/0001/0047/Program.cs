int Solution(int n)
{
    var i = 1;
    while (true)
    {
        for (var j = 0; j < n; j++)
        {
            if (PrimeFactors(i + j).Count() >= n)
            {
                if (j == n - 1)
                {
                    return i;
                }
            }
            else
            {
                i += j + 1;
                break;
            }
        }
    }
}

IEnumerable<int> PrimeFactors(int n)
{
    var seen = new HashSet<int>();
    while (n % 2 == 0)
    {
        if (!seen.Contains(2))
        {
            yield return 2;
            seen.Add(2);
        }
        n /= 2;
    }


    for (var i = 3; i < Math.Sqrt(n); i += 2)
    {
        while (n % i == 0)
        {
            if (!seen.Contains(i))
            {
                yield return i;
                seen.Add(i);
            }
            n /= i;
        }
    }

    if (n > 2)
    {
        yield return n;
    }
}

Console.WriteLine(Solution(2)); // 14
Console.WriteLine(Solution(3)); // 644
Console.WriteLine(Solution(4));
