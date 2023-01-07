int Solution(int power)
{
    var product = "1";
    for (var i = 0; i < power; i++)
    {
        product = Square(product);
    }


    return product
        .Select(x => x.ToString())
        .Select(int.Parse)
        .Sum();
}

string Square(string n)
{
    var carry = 0;
    var product = string.Empty;

    for (var i = 1; i <= n.Length; i++)
    {
        var unit = int.Parse(n[^i].ToString());
        var multiplied = unit * 2;
        multiplied += carry;
        carry = multiplied / 10;
        product = multiplied % 10 + product;
    }

    if (carry > 0)
    {
        product = carry + product;
    }

    return product;
}

Console.WriteLine(Solution(15)); // 26
Console.WriteLine(Solution(1000));