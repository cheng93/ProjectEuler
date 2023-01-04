int Solution() => DigitFactorials().Sum();

IEnumerable<int> DigitFactorials()
{
    var factorials = Factorial().Take(10).ToArray();

    for (var i = 3; i <= 9999999; i++)
    {
        var remainder = i;
        var sum = 0;
        while (remainder != 0)
        {
            sum += factorials[remainder % 10];
            remainder /= 10;
        }

        if (sum == i)
        {
            yield return i;
        }
    }

}

IEnumerable<int> Factorial()
{
    var factorial = 1;
    yield return factorial;
    var i = 1;
    while (true)
    {
        factorial = factorial * i;
        yield return factorial;
        i++;
    }
}

Console.WriteLine(Solution());