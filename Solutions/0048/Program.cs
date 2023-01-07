string Solution(int n)
{
    var sum = 0L;
    foreach (var power in SelfPowers().Take(n))
    {
        Console.WriteLine(power);
        sum += power;
        sum %= 10000000000;
    }
    return sum.ToString().PadLeft(10, '0');
}

IEnumerable<long> SelfPowers()
{
    var i = 1;
    while (true)
    {
        yield return Power(i);
        i++;
    }
}

long Power(int n)
{
    var product = 1L;
    var exponent = (int)Math.Log10(n);
    for (var i = 1; i <= n; i++)
    {
        if (product == 0)
        {
            break;
        }
        product *= n;
        product %= (long)Math.Pow(10, 11 - exponent);
    }

    return product;
}

Console.WriteLine(Solution(10)); // 0405071317
Console.WriteLine(Solution(1000));
