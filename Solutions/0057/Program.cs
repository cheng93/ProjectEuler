int Solution() => Expansions()
    .Take(1000)
    .Count(x => x.Numerator.Length > x.Denominator.Length);

IEnumerable<(string Numerator, string Denominator)> Expansions()
{
    var fraction = (Numerator: "3", Denominator: "2");
    do
    {
        yield return fraction;
        var denominator = Sum(fraction.Numerator, fraction.Denominator);
        var numerator = Sum(denominator, fraction.Denominator);
        fraction = (numerator, denominator);
    }
    while (true);
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

Console.WriteLine(Solution());
