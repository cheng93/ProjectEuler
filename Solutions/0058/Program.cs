int Solution() => Spiral()
    .Select((x, i) => new
    {
        Ratio = x,
        Length = i * 2 + 3
    })
    .First(x => x.Ratio < 10)
    .Length;


IEnumerable<int> Spiral()
{
    var diagonal = 1;
    var diagonalPrimes = 0;

    var i = 0;
    while (true)
    {
        var square = i * 2 + 1;
        var step = i * 2 + 2;
        var start = square * square;
        for (var j = 1; j <= 4; j++)
        {
            var n = start + j * step;
            diagonal++;
            if (IsPrime(n))
            {
                diagonalPrimes++;
            }
        }
        yield return 100 * diagonalPrimes / diagonal;
        i++;
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

Console.WriteLine(Solution());
