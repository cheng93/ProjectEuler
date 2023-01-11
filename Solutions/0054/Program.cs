using System.Reflection;

int Solution() => PokerHands().Count(x => x == 1);

IEnumerable<int> PokerHands()
{
    var path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, "poker.txt");
    var str = File.ReadAllText(path);
    var lines = str.Split(Environment.NewLine).ToArray();

    foreach (var line in lines)
    {
        var splits = line.Split(" ");
        yield return Hand.Create(splits[0..5]).CompareTo(Hand.Create(splits[5..]));
    }

}

Console.WriteLine(Solution());

record CardValue(string Value) : IComparable<CardValue>
{
    private int Rank
    {
        get => Value switch
        {
            "A" => 14,
            "K" => 13,
            "Q" => 12,
            "J" => 11,
            "T" => 10,
            _ => int.Parse(Value)
        };
    }

    public int CompareTo(CardValue? other)
    {
        if (other is null)
        {
            return 1;
        }

        return Rank.CompareTo(other.Rank);
    }

    public static implicit operator CardValue(string s) => new(s);

    public static int operator -(CardValue x, CardValue y) => x.Rank - y.Rank;
}

record Card(CardValue Value, string Suit) : IComparable<Card>
{
    public int CompareTo(Card? other)
    {
        if (other is null)
        {
            return 1;
        }

        return Value.CompareTo(other.Value);
    }

    public static explicit operator Card(string s) => new(s[..^1], s[^1].ToString());
}

abstract class Hand : IComparable<Hand>
{
    public Hand(Card[] kickers)
    {
        Kickers = kickers;
    }

    public Card[] Kickers { get; }

    protected abstract int Rank { get; }

    public virtual int CompareTo(Hand? other)
    {
        if (other is null)
        {
            return 1;
        }

        var rank = Rank.CompareTo(other.Rank);
        if (rank != 0)
        {
            return rank;
        }

        return Enumerable
            .Range(0, Kickers.Length)
            .Select(x => Kickers[x].CompareTo(other.Kickers[x]))
            .FirstOrDefault(x => x != 0);
    }

    public static Hand Create(string[] hand)
    {
        var cards = hand
            .Select(x => (Card)x)
            .OrderByDescending(x => x.Value)
            .ToArray();
        var highest = cards[0].Value;
        var hasFlush = cards.DistinctBy(x => x.Suit).Count() == 1;
        var hasStraight = Enumerable.Range(0, hand.Length - 1)
            .Select(x => new { Current = cards[x], Next = cards[x + 1] })
            .All(x => x.Current.Value - x.Next.Value == 1);
        var grouped = cards
            .GroupBy(x => x.Value)
            .OrderByDescending(x => x.Count())
            .ThenByDescending(x => x.Key)
            .ToArray();

        return (highest.Value, hasStraight, hasFlush, grouped[0].Count(), grouped[1].Count()) switch
        {
            ("A", true, true, _, _) => new RoyalFlush(),
            (_, true, true, _, _) => new StraightFlush(highest),
            (_, _, _, 4, _) => new FourOfAKind(grouped[0].Key, grouped[1].ToArray()),
            (_, _, _, 3, 2) => new FullHouse(grouped[0].Key, grouped[1].Key),
            (_, _, true, _, _) => new Flush(cards),
            (_, true, _, _, _) => new Straight(highest),
            (_, _, _, 3, _) => new ThreeOfAKind(grouped[0].Key, grouped[1].Concat(grouped[2]).ToArray()),
            (_, _, _, 2, 2) => new TwoPair(grouped[0].Key, grouped[1].Key, grouped[2].ToArray()),
            (_, _, _, 2, _) => new OnePair(grouped[0].Key, grouped[1].Concat(grouped[2]).Concat(grouped[3]).ToArray()),
            _ => new HighCard(cards)
        };
    }
}

class HighCard : Hand
{
    public HighCard(Card[] cards) : base(cards)
    {
    }

    protected override int Rank => 0;
}

class OnePair : Hand
{
    public OnePair(CardValue pair, Card[] kickers) : base(kickers)
    {
        Pair = pair;
    }

    public CardValue Pair { get; }

    protected override int Rank => 1;

    public override int CompareTo(Hand? other)
    {
        if (other is not OnePair p)
        {
            return base.CompareTo(other);
        }

        var high = Pair.CompareTo(p.Pair);
        return high != 0 ? high : base.CompareTo(other);
    }
}

class TwoPair : Hand
{
    public TwoPair(CardValue highPair, CardValue lowPair, Card[] kickers) : base(kickers)
    {
        HighPair = highPair;
        LowPair = lowPair;
    }

    public CardValue HighPair { get; }

    public CardValue LowPair { get; }

    protected override int Rank => 2;

    public override int CompareTo(Hand? other)
    {
        if (other is not TwoPair p)
        {
            return base.CompareTo(other);
        }

        var high = HighPair.CompareTo(p.HighPair);
        var low = LowPair.CompareTo(p.LowPair);
        return high != 0
            ? high
            : low != 0
            ? low
            : base.CompareTo(other);
    }
}

class ThreeOfAKind : Hand
{
    public ThreeOfAKind(CardValue triplet, Card[] kickers) : base(kickers)
    {
        Triplet = triplet;
    }

    public CardValue Triplet { get; }

    protected override int Rank => 3;

    public override int CompareTo(Hand? other)
    {
        if (other is not ThreeOfAKind t)
        {
            return base.CompareTo(other);
        }

        var high = Triplet.CompareTo(t.Triplet);
        return high != 0 ? high : base.CompareTo(other);
    }
}

class Straight : Hand
{
    public Straight(CardValue highest) : base(Array.Empty<Card>())
    {
        Highest = highest;
    }

    public CardValue Highest { get; }

    protected override int Rank => 4;

    public override int CompareTo(Hand? other)
    {
        if (other is not Straight s)
        {
            return base.CompareTo(other);
        }

        return Highest.CompareTo(s.Highest);
    }
}

class Flush : Hand
{
    public Flush(Card[] cards) : base(cards)
    {
    }

    protected override int Rank => 5;
}

class FullHouse : Hand
{
    public FullHouse(CardValue triplet, CardValue pair) : base(Array.Empty<Card>())
    {
        Triplet = triplet;
        Pair = pair;
    }

    public CardValue Triplet { get; }
    public CardValue Pair { get; }

    protected override int Rank => 6;

    public override int CompareTo(Hand? other)
    {
        if (other is not FullHouse f)
        {
            return base.CompareTo(other);
        }

        var high = Triplet.CompareTo(f.Triplet);
        return high != 0 ? high : Pair.CompareTo(f.Pair);
    }
}

class FourOfAKind : Hand
{
    public FourOfAKind(CardValue quartet, Card[] kickers) : base(kickers)
    {
        Quartet = quartet;
    }

    public CardValue Quartet { get; }

    protected override int Rank => 7;

    public override int CompareTo(Hand? other)
    {
        if (other is not FourOfAKind f)
        {
            return base.CompareTo(other);
        }

        var high = Quartet.CompareTo(f.Quartet);
        return high != 0 ? high : base.CompareTo(other);
    }
}

class StraightFlush : Hand
{
    public StraightFlush(CardValue highest) : base(Array.Empty<Card>())
    {
        Highest = highest;
    }

    public CardValue Highest { get; }

    protected override int Rank => 8;

    public override int CompareTo(Hand? other)
    {
        if (other is not StraightFlush s)
        {
            return base.CompareTo(other);
        }

        return Highest.CompareTo(s.Highest);
    }
}

class RoyalFlush : Hand
{
    public RoyalFlush() : base(Array.Empty<Card>())
    {
    }

    protected override int Rank => 9;
}