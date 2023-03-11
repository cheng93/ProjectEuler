int Solution(int n)
{
    var maxX = "0";
    var max = 0;
    for (var i = 1; i <= n; i++)
    {
        var continuousFraction = SquareRootContinuousFraction(i).ToList();
        if (!continuousFraction.Any())
        {
            continue;
        }

        var first = continuousFraction[0];
        var period = continuousFraction.Skip(1).ToList();
        var sequence = new List<int> { first };

        var j = 0;
        while (true)
        {
            sequence.Add(period[j % period.Count]);
            var (x, y) = ToFraction(sequence);
            if (IsDiophantine(x, y, i))
            {
                if (GreaterThan(x, maxX))
                {
                    maxX = x;
                    max = i;
                }
                break;
            }
            j++;
        }
    }

    return max;
}

IEnumerable<int> SquareRootContinuousFraction(int n)
{
    var quotient = Math.Sqrt(n);
    if (quotient % 1 == 0)
    {
        yield break;
    }

    var seen = new HashSet<(int Numerator, int Remainder)>();
    var numerator = 1;
    var remainder = (int)quotient;

    yield return remainder;

    do
    {
        seen.Add((numerator, remainder));
        var denominator = n - (remainder * remainder);
        (numerator, denominator) = Simplify((numerator, denominator));
        var j = 0;
        while (quotient + remainder > denominator)
        {
            remainder -= denominator;
            j++;
        }

        yield return j;

        numerator = denominator;
        remainder = Math.Abs(remainder);
    } while (!seen.Contains((numerator, remainder)));
}

(string Numerator, string Denominator) ToFraction(IEnumerable<int> values)
{
    values = values.Reverse();
    var fraction = (Numerator: "1", Denominator: "0");
    foreach (var value in values)
    {
        var (numerator, denominator) = fraction;
        fraction = (Sum(denominator, Multiply(numerator, value.ToString())), numerator);
    }
    return fraction;
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

bool IsDiophantine(string x, string y, int n)
{
    // x^2 - ny^2 = 1
    // x^2
    var a = Multiply(x, x);
    // 1 + ny^2
    var b = Sum("1", Multiply(n.ToString(), Multiply(y, y)));

    return a == b;
}

string Multiply(string a, string b)
{
    var sums = new List<string>();
    var initial = string.Empty;
    while (b != string.Empty)
    {
        var unit = int.Parse(b[^1].ToString());
        var product = initial;
        var carry = 0;
        for (var i = 1; i <= a.Length; i++)
        {
            var p = int.Parse(a[^i].ToString()) * unit + carry;
            product = p % 10 + product;
            carry = p / 10;
        }

        if (carry > 0)
        {
            product = carry + product;
        }

        sums.Add(product);
        b = b[..^1];
        initial += "0";
    }

    return sums.Aggregate("0", (agg, cur) => Sum(agg, cur));
}

string Sum(string a, string b)
{
    if (a == "0") return b;
    if (b == "0") return a;

    var head = a.Length >= b.Length
        ? a[..(a.Length - b.Length)]
        : b[..(b.Length - a.Length)];

    var sum = string.Empty;
    var carry = 0;
    for (var i = 1; i <= Math.Min(a.Length, b.Length); i++)
    {
        var added = int.Parse(a[^i].ToString()) + int.Parse(b[^i].ToString()) + carry;
        sum = added % 10 + sum;
        carry = added / 10;
    }

    if (carry > 0)
    {
        head = Sum(head, carry.ToString());
    }

    return head + sum;
}

bool GreaterThan(string a, string b)
{
    if (a.Length > b.Length) return true;
    if (a.Length < b.Length) return false;

    for (var i = 0; i < a.Length; i++)
    {
        var x = int.Parse(a[i].ToString());
        var y = int.Parse(b[i].ToString());

        if (x > y) return true;
        if (x < y) return false;
    }

    return false;
}

Console.WriteLine(Solution(7)); // 5
Console.WriteLine(Solution(1000));
