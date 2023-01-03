int Solution(int power) => SumOfPowerDigits(power).Sum();

IEnumerable<int> SumOfPowerDigits(int power)
{
    var units = Enumerable
        .Range(0, 10)
        .Select(x => Enumerable
            .Range(x, power)
            .Aggregate((agg, _) => agg * x))
        .ToArray();

    var max = Max();

    for (var i = 10; i < max; i++)
    {
        var quotient = i;
        var sum = 0;
        while (quotient != 0)
        {
            var remainder = quotient % 10;
            sum += units[remainder];
            quotient /= 10;
        }

        if (sum == i)
        {
            yield return i;
        }
    }

    int Max()
    {
        var number = 9;
        var sum = units[9];

        while (sum > number)
        {
            sum += units[9];
            number *= 10;
            number += 9;
        }

        return number;
    }
}

Console.WriteLine(Solution(4)); // 19316
Console.WriteLine(Solution(5));