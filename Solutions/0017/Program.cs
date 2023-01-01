int Solution(int limit)
    => Enumerable.Range(1, limit)
        .Select(Words)
        .Select(x => x.Replace(" ", string.Empty).Length)
        .Sum();

string Words(int number)
{
    var words = new List<string>();
    var unit = new[]
    {
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine",
        "ten",
        "eleven",
        "twelve",
        "thirteen",
        "fourteen",
        "fifteen",
        "sixteen",
        "seventeen",
        "eighteen",
        "nineteen",
    };

    var tens = new[]
    {
        "twenty",
        "thirty",
        "forty",
        "fifty",
        "sixty",
        "seventy",
        "eighty",
        "ninety",
    };

    var thousands = number / 1000;
    number = number % 1000;
    if (thousands > 0)
    {
        words.Add(unit[thousands - 1]);
        words.Add("thousand");
    }
    var hundreds = number / 100;
    number = number % 100;

    if (hundreds > 0)
    {
        words.Add(unit[hundreds - 1]);
        words.Add("hundred");
    }

    if (number > 0)
    {
        if (words.Count > 0)
        {
            words.Add("and");
        }

        if (number <= 19)
        {
            words.Add(unit[number - 1]);
        }
        else
        {
            var quotient = number / 10;
            var remainder = number % 10;
            words.Add(tens[quotient - 2]);

            if (remainder > 0)
            {
                words.Add(unit[remainder - 1]);
            }
        }
    }

    return string.Join(" ", words);
}

Console.WriteLine(Solution(5)); // 19
Console.WriteLine(Solution(1000));