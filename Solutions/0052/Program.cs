int Solution() => PermutedMultiples().First();

IEnumerable<int> PermutedMultiples()
{
    var i = 100000;
    while (true)
    {
        var exponent = (int)Math.Log10(i);
        if (Enumerable.Range(1, 6).Select(x => x * i).All(IsPermutation))
        {
            yield return i;
        }
        i++;

        bool IsPermutation(int n)
        {
            if (n == i)
            {
                return true;
            }

            if (exponent != (int)Math.Log10(n))
            {
                return false;
            }

            var iDigits = GetDigits(i).ToList();
            var lastSeen = new int[10];
            var nDigits = GetDigits(n);

            foreach (var nDigit in nDigits)
            {
                if (iDigits.IndexOf(nDigit, lastSeen[nDigit]) == -1)
                {
                    return false;
                }

                lastSeen[nDigit]++;
            }

            return true;
        }
    }
}

IEnumerable<int> GetDigits(int n)
{
    var exponent = (int)Math.Log10(n);
    return Enumerable.Range(0, exponent + 1)
                .Reverse()
                .Select(x => (n % (int)Math.Pow(10, x + 1)) / (int)Math.Pow(10, x));
}

Console.WriteLine(Solution());
