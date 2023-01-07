int Solution() => Sieve().Sum();

IEnumerable<int> Sieve()
{
    var abundants = new HashSet<int>();
    var invalid = new HashSet<int>();
    for (var i = 1; i <= 28123; i++)
    {
        if (abundants.Contains(i))
        {
            continue;
        }
        if (!invalid.Contains(i))
        {
            yield return i;
        }
        if (SumOfProperDivisors(i) > i)
        {
            foreach (var a in abundants.ToList())
            {
                for (var j = i; j <= 28123; j += i)
                {
                    invalid.Add(a + j);
                }
            }
            for (var j = i; j <= 28123; j += i)
            {
                abundants.Add(j);
                invalid.Add(j);
            }
        }
    }
}

int SumOfProperDivisors(int n)
{
    var properDivisors = Factors(n).Where(x => x != n);
    return properDivisors.Sum();
}

IEnumerable<int> Factors(int n)
{
    var sqrt = (int)Math.Sqrt(n);
    for (var i = 1; i <= sqrt; i++)
    {
        if (n % i == 0)
        {
            yield return i;
            if (i != n / i)
            {
                yield return n / i;
            }
        }
    }
}

Console.WriteLine(Solution());