int Solution()
{
    var i = 1;
    var n = 1;
    var product = 1;

    foreach (var digit in ChampernowneDigits())
    {
        if (n == i)
        {
            product *= digit;
            n *= 10;
            if (i == 1000000)
            {
                break;
            }
        }
        i++;
    }

    return product;
}

IEnumerable<int> ChampernowneDigits()
{
    var n = 1;
    while (true)
    {
        var exponent = (int)Math.Log10(n);
        for (var i = exponent; i >= 0; i--)
        {
            var pow = (int)Math.Pow(10, i);
            var digit = (n % (pow * 10)) / pow;
            yield return digit;
        }
        n++;
    }
}

Console.WriteLine(Solution());
