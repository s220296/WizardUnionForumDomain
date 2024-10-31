namespace WizardUnion.Unions;

public class UnionMembership
{
    public Union Union { get; protected set; }
    public IUnionRole Role { get; protected set; }

    public UnionMembership(Union _union, IUnionRole _role)
    {
        (Union, Role) = (_union, _role);
    }
}
