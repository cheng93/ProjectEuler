int Solution()
{
    var digits = Enumerable.Range(1, 9).ToArray();
    var lengths = new (int A, int B)[]
    {
        (1, 4),
        (2, 3)
    };

    var products = new HashSet<int>();

    foreach (var length in lengths)
    {
        foreach (var a in GetDigits(length.A, Array.Empty<int>()))
        {
            var multiplicand = ToNumber(a);
            foreach (var b in GetDigits(length.B, a))
            {
                var multiplier = ToNumber(b);
                var product = multiplicand * multiplier;

                var remaining = new HashSet<int>(digits.Except(a).Except(b));
                var productSet = new HashSet<int>(product.ToString().Select(x => x.ToString()).Select(int.Parse));

                if (remaining.SetEquals(productSet) && product < Math.Pow(10, remaining.Count))
                {
                    products.Add(product);
                }
            }
        }
    }

    return products.Sum();

    IEnumerable<ICollection<int>> GetDigits(int length, ICollection<int> taken)
    {
        if (length == 0)
        {
            yield return Array.Empty<int>();
            yield break;
        }
        foreach (var digit in digits)
        {
            if (taken.Contains(digit))
            {
                continue;
            }

            var array = new[] { digit };
            foreach (var next in GetDigits(length - 1, taken.Concat(array).ToArray()))
            {
                yield return array.Concat(next).ToArray();
            }
        }
    }

    int ToNumber(ICollection<int> ints)
        => ints.Aggregate(0, (agg, cur) => agg * 10 + cur);
}

Console.WriteLine(Solution());