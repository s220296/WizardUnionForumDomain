namespace WizardUnion.Unions;

public class UnionMembership
{
    public Union Union { get; protected set; }
    // Wizard's role within this Union

    public UnionMembership(Union _union)
    {
        (Union) = (_union);
    }
}
