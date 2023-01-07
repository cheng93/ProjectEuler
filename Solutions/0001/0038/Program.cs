int Solution() => PandigitalMultiples().Max();

IEnumerable<int> PandigitalMultiples()
{
    var digits = Enumerable.Range(1, 9).ToArray();

    for (var i = 1; i < 10000; i++)
    {
        var pandigital = GetPandigitalMultiple(i);
        if (pandigital.HasValue)
        {
            yield return pandigital.Value;
        }
    }
}

int? GetPandigitalMultiple(int n)
{
    var pandigital = 0;
    var seen = new HashSet<int>();
    var i = 1;
    while (seen.Count < 9)
    {
        var p = n * i;
        var exponent = (int)Math.Log10(p) + 1;
        pandigital *= (int)Math.Pow(10, exponent);
        pandigital += p;
        while (p > 0)
        {
            var remainder = p % 10;
            if (seen.Contains(remainder) || remainder == 0)
            {
                return null;
            }

            seen.Add(remainder);
            p /= 10;
        }
        i++;
    }

    return pandigital;
}

Console.WriteLine(Solution());
