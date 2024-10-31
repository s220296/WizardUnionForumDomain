namespace WizardUnion.MagicAndSpells;

public struct SpellMastery : IEquatable<SpellMastery>, IComparable<SpellMastery>, IEquatable<string>, IComparable<string>
{
    public string Name { get; private set; }

    public SpellMastery()
    {
        Name = "None";
    }

    public SpellMastery(string _name)
    {
        if (string.IsNullOrWhiteSpace(_name)) _name = "None";

        Name = _name;
    }

    public int CompareTo(SpellMastery other)
    {
        return Name.CompareTo(other.Name);
    }

    public bool Equals(SpellMastery other)
    {
        return Name.Equals(other.Name);
    }

    public bool Equals(string? other)
    {
        return Name.Equals(other);
    }

    public int CompareTo(string? other)
    {
        return Name.CompareTo(other);
    }

    public static bool operator ==(SpellMastery a, SpellMastery b) { return a.Equals(b); }
    public static bool operator !=(SpellMastery a, SpellMastery b) { return !a.Equals(b); }

    public override bool Equals(object? obj)
    {
        return obj is SpellMastery && Equals((SpellMastery)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }
}
