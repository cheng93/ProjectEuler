int Solution() => Enumerable.Range(1, 10000).Where(IsLychrel).Count();

bool IsLychrel(int n)
{
    var s = n.ToString();
    for (var i = 0; i < 50; i++)
    {
        s = Sum(s, string.Join(string.Empty, s.Reverse()));
        if (IsPalindrome(s))
        {
            return false;
        }
    }
    return true;
}

bool IsPalindrome(string str)
{
    var half = str.Length / 2;
    return str[..half] == string.Join(string.Empty, str[(^half)..].Reverse());
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
