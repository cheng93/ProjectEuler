int Solution()
{
    var seen = new HashSet<(int Numerator, int Denominator)>();
    var curious = new List<(int Numerator, int Denominator)>();
    for (var i = 1; i < 100; i++)
    {
        for (var j = i + 1; j < 100; j++)
        {
            var simplified = Simplify((i, j));

            if (seen.Contains(simplified))
            {
                if (i >= 10)
                {
                    var newNumerator = i / 10;
                    var newDenominator = j % 10;

                    if (simplified == Simplify((newNumerator, newDenominator))
                        && i % 10 == j / 10)
                    {
                        curious.Add(simplified);
                    }
                }
            }

            seen.Add(simplified);
        }
    }

    return Simplify(curious.Aggregate((agg, cur) => (agg.Numerator * cur.Numerator, agg.Denominator * cur.Denominator))).Denominator;
}

(int Numerator, int Denominator) Simplify((int Numerator, int Denominator) fraction)
{
    var (numerator, denominator) = fraction;
    var gcd = Gcd(numerator, denominator);
    return (numerator / gcd, denominator / gcd);
}

int Gcd(int a, int b)
{
    while (b != 0)
    {
        var t = b;
        b = a % b;
        a = t;
    }
    return a;
}

Console.WriteLine(Solution());