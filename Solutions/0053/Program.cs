int Solution() => BinomialGreaterThan(1000000).Sum();

IEnumerable<int> BinomialGreaterThan(int n)
{
    for (var i = 1; i <= 100; i++)
    {
        var product = 1;
        for (var j = 1; j <= i / 2; j++)
        {
            product *= (i + 1 - j);
            product /= j;

            if (product > n)
            {
                var hi = i + 1 - j;
                yield return hi - j;
                break;
            }
        }
    }
}


Console.WriteLine(Solution());
