﻿int Solution(int n)
{
    var factorial = Factorial().Take(n + 1).Last();
    return factorial
        .Select(x => x.ToString())
        .Select(int.Parse)
        .Sum();
}

IEnumerable<string> Factorial()
{
    var factorial = "1";
    yield return factorial;
    var i = 1;
    while (true)
    {
        factorial = Multiply(factorial, i);
        yield return factorial;
        i++;
    }
}

string Multiply(string a, int b)
{
    var sums = new List<string>();
    var initial = string.Empty;
    while (b != 0)
    {
        var unit = b % 10;
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
        b = b / 10;
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

Console.WriteLine(Solution(10)); // 27
Console.WriteLine(Solution(100));