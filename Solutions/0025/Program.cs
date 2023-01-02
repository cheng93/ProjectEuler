int Solution(int length)
    => Fibonacci()
        .Select((x, i) => (N: x, Index: i + 1))
        .First(x => x.N.Length == length)
        .Index;

IEnumerable<string> Fibonacci()
{
    var a = "1";
    var b = "1";
    yield return a;
    yield return b;
    while (true)
    {
        var t = b;
        b = Sum(a, b);
        yield return b;
        a = t;
    }
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

Console.WriteLine(Solution(3)); // 12
Console.WriteLine(Solution(1000));